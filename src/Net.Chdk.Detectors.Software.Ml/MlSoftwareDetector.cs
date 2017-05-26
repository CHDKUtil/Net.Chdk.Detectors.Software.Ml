using Net.Chdk.Detectors.Software.Binary;
using Net.Chdk.Providers.Software;
using System;
using System.Globalization;

namespace Net.Chdk.Detectors.Software.Ml
{
    sealed class MlSoftwareDetector : ProductBinarySoftwareDetector
    {
        public MlSoftwareDetector(ISourceProvider sourceProvider)
            : base(sourceProvider)
        {
        }

        public override string ProductName => "ML";

        protected override string[] Strings => new[]
        {
            "Magic Lantern "
        };

        protected override int StringCount => 5;

        protected override char SeparatorChar => '\n';

        protected override Version GetProductVersion(string[] strings)
        {
            var versionStr = strings[0].TrimStart("Nightly.");
            if (versionStr == null)
                return null;
            var split = versionStr.Split('.');
            DateTime date;
            if (!DateTime.TryParse(split[0], CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out date))
                return null;
            return new Version(date.Year, date.Month, date.Day);
        }

        protected override DateTime? GetCreationDate(string[] strings)
        {
            var builtStr = strings[4].TrimStart("Built on : ");
            if (builtStr == null)
                return null;
            var index = builtStr.IndexOf(" by ");
            if (index > 0)
                builtStr = builtStr.Substring(0, index);
            DateTime date;
            if (!DateTime.TryParse(builtStr, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out date))
                return null;
            return date;
        }

        protected override CultureInfo GetLanguage(string[] strings)
        {
            return CultureInfo.GetCultureInfo("en");
        }

        protected override string GetPlatform(string[] strings)
        {
            return strings[1].TrimStart("Camera   : ");
        }

        protected override string GetRevision(string[] strings)
        {
            return strings[2].TrimStart("Firmware : ");
        }
    }
}
