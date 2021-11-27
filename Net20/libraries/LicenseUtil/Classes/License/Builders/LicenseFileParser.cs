using LicenseUtil.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using YamlDotNet.RepresentationModel;

namespace LicenseUtil.Classes.License.Builders
{
    public class LicenseFileParser
    {
        public static ValLicenseFile Build(string licenseFile)
        {
            ValLicenseFileBuilder lfileBuilder = new ValLicenseFileBuilder();

            var yaml = new YamlStream();
            yaml.Load(new StringReader(licenseFile));
            var mapping = (YamlMappingNode)yaml.Documents[0].RootNode;

            string fileId = ((YamlScalarNode)mapping[new YamlScalarNode(LicDict.FILE_ID)]).Value;
            DateTime issueTime = DateTime.Parse(((YamlScalarNode)mapping[new YamlScalarNode(LicDict.ISSUE_TIME)]).Value);

            lfileBuilder.SetId(fileId);
            lfileBuilder.SetIssueTime(issueTime);

            var licenseList = (YamlSequenceNode)mapping.Children[new YamlScalarNode(LicDict.LICENSES_BLOCK)];

            foreach (YamlMappingNode item in licenseList)
            {
                ValLicenseBuilder licBuilder = new ValLicenseBuilder();

                string guid = ((YamlScalarNode)item[new YamlScalarNode(LicDict.GUID)]).Value;

                licBuilder.SetGuid(guid);
                licBuilder.SetPeriod(ParsePeriod(item));

                #region Check Prerequisites
                ValPrerequisitesBuilder prereqBuilder = new ValPrerequisitesBuilder();

                var prerequisites = item.Children[new YamlScalarNode(LicDict.PREREQUISITES_BLOCK)];
                var fingerprints = (YamlSequenceNode)prerequisites[new YamlScalarNode(LicDict.HOST_INFO)]
                    [new YamlScalarNode(LicDict.FINGERPRINTS_BLOCK)];

                ValHost host = new ValHost();
                foreach (YamlMappingNode fingerprint in fingerprints)
                {
                    string hash = ((YamlScalarNode)fingerprint[new YamlScalarNode(LicDict.HASH_VALUE)]).Value;
                    string hashAlg = ((YamlScalarNode)fingerprint[new YamlScalarNode(LicDict.ALGORITHM)]).Value;

                    host.Add(new ValFingerprint() { Value = hash, Algorithm = hashAlg });
                }

                prereqBuilder.AddHost(host);
                licBuilder.SetPrerequisites(prereqBuilder.Build());
                #endregion

                #region Coverage
                ValCoverageBuilder covBuilder = new ValCoverageBuilder();
                var coverage = item.Children[new YamlScalarNode(LicDict.COVERAGE)];

                var products = (YamlSequenceNode)coverage[new YamlScalarNode(LicDict.PRODUCTS_BLOCK)];
                foreach (YamlMappingNode product in products)
                {
                    ValProductBuilder prodBuilder = new ValProductBuilder();
                    prodBuilder.SetGuid(((YamlScalarNode)product.Children["productGuid"]).Value);

                    var versions = (YamlSequenceNode)product[new YamlScalarNode(LicDict.VERSIONS_BLOCK)];
                    var editions = (YamlSequenceNode)product[new YamlScalarNode(LicDict.EDITIONS_BLOCK)];

                    foreach (YamlScalarNode version in versions)
                        prodBuilder.AddVersion(version.Value);

                    foreach (YamlScalarNode edition in editions)
                        prodBuilder.AddEdition(edition.Value);

                    #region Applications
                    if (product.Children.Keys.Contains(LicDict.APPLICATIONS_BLOCK) && product.Children[new YamlScalarNode(LicDict.APPLICATIONS_BLOCK)] is YamlMappingNode)
                    {
                        var applications = (YamlMappingNode)product.Children[new YamlScalarNode(LicDict.APPLICATIONS_BLOCK)];

                        foreach (YamlNode app in applications.Children.Keys)
                        {
                            ValApplicationBuilder appBuilder = new ValApplicationBuilder();

                            var appName = ((YamlScalarNode)app).Value;

                            string state =
                                ((YamlScalarNode)applications.Children[app][new YamlScalarNode(LicDict.LICENSE_STATE)]).Value;
                            bool enabled = StringHelper.Equals(state, LicDict.LICENSE_SUCCESS_STATE);

                            appBuilder.SetName(appName);
                            appBuilder.SetState(enabled);
                            prodBuilder.AddApplication(appBuilder.Build());
                        }
                    }
                    #endregion

                    #region Components
                    if (product.Children.Keys.Contains(LicDict.COMPONENTS_BLOCK) && product.Children[new YamlScalarNode(LicDict.COMPONENTS_BLOCK)] is YamlMappingNode)
                    {
                        var components = (YamlMappingNode)product.Children[new YamlScalarNode(LicDict.COMPONENTS_BLOCK)];

                        foreach (YamlNode cmp in components.Children.Keys)
                        {
                            ValComponentBuilder cmpBuilder = new ValComponentBuilder();

                            var componentName = ((YamlScalarNode)cmp).Value;

                            var restrictions = ((YamlMappingNode)components.Children[cmp][new YamlScalarNode(LicDict.RESTRICTIONS)]);

                            cmpBuilder.SetComponentName(componentName);

                            foreach (var restr in restrictions)
                                cmpBuilder.AddRestriction(((YamlScalarNode)restr.Key).Value, ((YamlScalarNode)restr.Value).Value);

                            prodBuilder.AddComponent(cmpBuilder.Build());
                        }
                    }
                    #endregion

                    covBuilder.AddProduct(prodBuilder.Build());
                }

                licBuilder.SetCoverage(covBuilder.Build());
                #endregion

                lfileBuilder.AddLicense(licBuilder.Build());
            }

            return lfileBuilder.Build();
        }

        private static DateTime[] ParsePeriod(YamlMappingNode licenseNode)
        {
            var issueTime = ((YamlScalarNode)licenseNode.Children[new YamlScalarNode(LicDict.ISSUE_TIME)]).Value;
            var effectiveTime = ((YamlScalarNode)licenseNode.Children[new YamlScalarNode(LicDict.EFFECTIVE_TIME)]).Value;
            DateTime from = DateTime.Parse(issueTime);
            DateTime to = DateTime.Parse(effectiveTime);

            return new DateTime[] { from, to };
        }
    }
}
