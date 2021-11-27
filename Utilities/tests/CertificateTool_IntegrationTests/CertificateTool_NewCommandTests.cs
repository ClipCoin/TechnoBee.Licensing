using System;
using System.Diagnostics;
using Xunit;

namespace CertificateTool_IntegrationTests
{
    public class CertificateTool_NewCommandTests
    {
        private const String EXECUTABLE_UNDER_TEST_NAME = "TechnoBee.Licensing.Utilities.CertificateTool.exe";

        [Fact]
        public void CertificateTool__New__Without_File_Path()
        {
            //  Prepare
            String arguments = "new";

            ProcessStartInfo processStartInfo = new ProcessStartInfo()
            {
                FileName = EXECUTABLE_UNDER_TEST_NAME,
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            //  Pre-validate

            //  Perform
            Process process = Process.Start(processStartInfo);

            //  Post-validate
            Assert.Equal(0, process.ExitCode);
        }
    }
}
