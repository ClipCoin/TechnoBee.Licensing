namespace LicenseUtil.Classes.License
{
    public class ValFingerprint
    {
        public string Value { get; set; }
        public string Algorithm { get; set; } = "Ddsn1:Sha256";

        public override string ToString()
        {
            return Value + "/" + Algorithm;
        }
    }
}