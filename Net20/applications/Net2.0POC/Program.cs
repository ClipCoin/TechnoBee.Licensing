using LicenseUtil;
using LicenseUtil.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Net2_0POC
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                LicenseFacade facade = new LicenseFacade();
              //  facade.GenerateFingerprint(@"C:\temp\hostinfo_client.tbhid", true);
                string check = facade.GetLicenseExpirationDate(@"C:\temp\Elementstore.cer", @"C:\temp\sample01.tblcd", "ESService", "b0f41126-bd56-497f-bd22-ff7d57ebb375", "1.2.12.0", "service Full");

                var lic = facade.License(@"C:\temp\Elementstore.cer", @"C:\temp\sample01.tblcd");
                // Console.WriteLine(check.ToString());
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine((ex.InnerException != null ? ex.InnerException + Environment.NewLine + ex.Message : ex.Message) + Environment.NewLine + ex.StackTrace);
                Console.ReadKey();
            }
        }
    }
}
 