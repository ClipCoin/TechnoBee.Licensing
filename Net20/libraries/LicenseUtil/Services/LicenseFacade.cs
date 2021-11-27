using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using YamlDotNet.RepresentationModel;
using LicenseUtil.Helpers;
using LicenseUtil.Classes;
using LicenseUtil.Classes.License.Builders;
using LicenseUtil.Classes.License;
using System.Text.RegularExpressions;

namespace LicenseUtil
{
    public class LicenseFacade : ILicenseFacade
    {
        public LicenseFacade()
        {
            _hostInformationDigestGenerator = new HostInformationDigestProvider();
        }

        public bool GenerateFingerprint(string filepath, bool overwrite)
        {
            IHostInformationDigest hostInformationDigest = _hostInformationDigestGenerator
                .GenerateHostInformationDigest();

            if (File.Exists(filepath) && !overwrite)
                throw new Exception($"'{filepath}' already exist!");

            String tempFileName = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

            if (!String.IsNullOrEmpty(filepath))
            {
                var directory = Path.GetDirectoryName(filepath);
                if (!String.IsNullOrEmpty(directory))
                    Directory.CreateDirectory(directory);
            }

            using (Stream stream = File.Open(tempFileName, FileMode.Create, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(stream))
                {
                    sw.WriteLine("version: " + hostInformationDigest.Version);
                    sw.WriteLine("timestamp: " + hostInformationDigest.Timestamp.ToString("yyyy-MM-ssThh:mm:ss.fffffff%K"));
                    sw.WriteLine("hostName: " + hostInformationDigest.HostName);
                    sw.WriteLine("diskDrives:");
                    foreach (var d in hostInformationDigest.DiskDrives)
                    {
                        sw.WriteLine("- deviceId: '" + d.DeviceId + "'");
                        sw.WriteLine("  serialNumber: '" + d.SerialNumber + "'");
                    }
                    sw.WriteLine("networkInterfaces:");
                    foreach (var ni in hostInformationDigest.NetworkInterfaces)
                        sw.WriteLine("- macAddress: " + ni.MacAddress);
                }
            }

            if (File.Exists(filepath))
                File.Replace(tempFileName, filepath, Path.Combine(Path.GetTempPath(), Path.GetRandomFileName()));
            else
                File.Copy(tempFileName, filepath);

            return true;
        }

        public ValLicenseFile License(string IssuerCertLocation, string LicenseFileName)
        {
            if (!File.Exists(IssuerCertLocation))
                throw new Exception(((int)LicenseErrorCodes.IssuerCertificateNotFound).ToString());// return new LicenseState(LicenseErrorCodes.IssuerCertificateNotFound);

            if (!File.Exists(LicenseFileName))
                throw new Exception(((int)LicenseErrorCodes.LicenseFileNotFound).ToString());//return new LicenseState(LicenseErrorCodes.LicenseFileNotFound);

            string licenseFile = "";
            Stream licenseMs = new MemoryStream();
            Stream signatureMs = new MemoryStream();

            #region Open zip
            ZipFile zip = ZipFile.Read(LicenseFileName);
            byte[] licenseFileData = null;

            // Firstly define license content
            foreach (ZipEntry e in zip)
            {
                if (StringHelper.Equals(e.FileName, "document"))
                {
                    e.Extract(licenseMs);
                    licenseMs.Position = 0;

                    licenseFileData = new byte[licenseMs.Length];
                    licenseMs.Read(licenseFileData, 0, (int)licenseMs.Length);
                    licenseFile = Encoding.Default.GetString(licenseFileData);
                    licenseMs.Dispose();
                    break;
                }
            }

            // Secondary define license signature
            foreach (ZipEntry e in zip)
            {
                if (StringHelper.Equals(e.FileName, "signature"))
                {
                    e.Extract(signatureMs);
                    signatureMs.Position = 0;

                    byte[] data = new byte[signatureMs.Length];
                    signatureMs.Read(data, 0, (int)signatureMs.Length);

                    if (!CheckSignature(new X509Certificate2(IssuerCertLocation), data, licenseFileData))
                        throw new Exception(((int)LicenseErrorCodes.SignatureOrCertificateCorrupted).ToString()); //return new LicenseState(LicenseErrorCodes.SignatureOrCertificateCorrupted);

                    break;
                }
            }
            #endregion

            return LicenseFileParser.Build(licenseFile);
        }


        public string GetLicenseExpirationDate(string IssuerCertLocation, string LicenseFileName, string ApplicationName, string ProductGuid, string ProductVersion, string ProductEdition)
        {
            ValLicenseFile licFile = License(IssuerCertLocation, LicenseFileName);
            List <LicenseErrorCodes> errors = new List<LicenseErrorCodes>();
            bool licenseFound = false; // Indicates when current license enabled
            bool productFound = false; // Indicates when product found
            bool currentExists = false;

            DateTime expired = DateTime.MinValue;

            foreach (var l in licFile)
            {
                if (!l.IsCurrent)
                    continue;

                currentExists = true;

                bool fingerprintFound = false;

                foreach (var host in l.Prerequisites.Hosts)
                {
                    foreach (var fingerprint in host)
                    {
                        if (fingerprint.Value == CalculateFingerprintHash("Ddsn1:Sha256"))
                        {
                            fingerprintFound = true;
                            break;
                        }
                    }
                }

                if (!fingerprintFound)
                    continue;

                foreach (var p in l.Coverage)
                {
                    if (p.Guid.ToLower() == ProductGuid.ToLower())
                    {
                        bool versionValid = p.Versions.Find(x => x == ProductVersion) == ProductVersion || p.Versions.Find(x => Regex.Match(ProductVersion, x).Success) != null;
                        bool editionValid = String.Equals(p.Editions.Find(x => String.Equals(x, ProductEdition, StringComparison.InvariantCultureIgnoreCase)), ProductEdition, StringComparison.InvariantCultureIgnoreCase);

                        foreach (var a in p.Applications)
                        {
                            if (a.Name.ToLower() == ApplicationName.ToLower())
                            {
                                expired = l.To;
                                productFound = true;
                                licenseFound = true;

                                if ( !a.Enabled)
                                    errors.Add(LicenseErrorCodes.ApplicationDisabled);

                                if (!versionValid)
                                    errors.Add(LicenseErrorCodes.InvalidVersion);

                                if (!editionValid)
                                    errors.Add(LicenseErrorCodes.InvalidEdition);
                            }
                        }
                    }
                }
            }

            if (!productFound)
                errors.Add(LicenseErrorCodes.ProductNotAllowed);
            if (!licenseFound)
                errors.Add(LicenseErrorCodes.DetectNoSuitableLicense);
            if (!currentExists)
                errors.Add(LicenseErrorCodes.Expired);

            if (errors.Count > 0)
            {
                String total = "";

                foreach (var er in errors)
                    total += ((int)er).ToString() + ";";

                if (total.EndsWith(";"))
                    total = total.Substring(0, total.Length - 1);

                throw new Exception(total);
            }

            if (expired == DateTime.MinValue)
                throw new Exception(LicenseErrorCodes.Unknown.ToString());

            return expired.ToString("yyyy-MM-dd");
        }

        #region Private
        private string CalculateFingerprintHash(string hashAlg)
        {
            switch (hashAlg)
            {
                case "Ddsn1:Sha256":
                    {
                        IHostInformationDigest hostInformationDigest =
                            _hostInformationDigestGenerator.GenerateHostInformationDigest();

                        return Convert.ToBase64String(new Ddsn1Sha256Fingerprint().GenerateFingerprint(hostInformationDigest));
                    }
                default:
                    throw new NotSupportedException(hashAlg);
            }
        }

        /// <summary>
        /// Validate period
        /// </summary>
        /// <param name="licenseNode"></param>
        /// <returns></returns>
        private DateTime[] Validity(YamlMappingNode licenseNode, ref bool isValid)
        {
            var issueTime = ((YamlScalarNode)licenseNode.Children[new YamlScalarNode(LicDict.ISSUE_TIME)]).Value;
            var effectiveTime = ((YamlScalarNode)licenseNode.Children[new YamlScalarNode(LicDict.EFFECTIVE_TIME)]).Value;
            DateTime from = DateTime.Parse(issueTime);
            DateTime to = DateTime.Parse(effectiveTime);

            isValid = from <= DateTime.Now && to >= DateTime.Now.Date;

            return new DateTime[] { from, to };
        }

        /// <summary>
        /// Check license signature matching
        /// </summary>
        /// <param name="cert">Issue certificate</param>
        /// <param name="signatureFile">Signature data</param>
        /// <param name="licenseFile">Document data</param>
        /// <returns></returns>
        private bool CheckSignature(X509Certificate2 cert, byte[] signatureFile, byte[] licenseFile)
        {
            if (cert == null || !String.Equals(cert.SerialNumber, LicDict.CERTIFICATE_SERIAL_NUMBER, StringComparison.InvariantCultureIgnoreCase))
                throw new ArgumentException(((int)LicenseErrorCodes.IssuerCertificateNotFound).ToString());

            if (signatureFile.Length == 0)
                throw new ArgumentException(((int)LicenseErrorCodes.SignatureOrCertificateCorrupted).ToString());

            if (licenseFile.Length == 0)
                throw new ArgumentException(((int)LicenseErrorCodes.LicenseFileCorrupted).ToString());

            RSACryptoServiceProvider rsa = (RSACryptoServiceProvider)cert.PublicKey.Key;

            using (SHA1 hashAlgorithm = SHA1.Create())
            {
                bool result = rsa.VerifyData(licenseFile, hashAlgorithm, signatureFile);

                return result;
            }
        }

        private bool CompareByteArrays(byte[] array1, byte[] array2)
        {
            if (array1.Length != array2.Length)
                return false;

            for (int i = 0; i < array1.Length; i++)
            {
                if (array1[i] != array2[i])
                    return false;
            }

            return true;
        }
        #endregion

        private readonly HostInformationDigestProvider _hostInformationDigestGenerator;
    }
}