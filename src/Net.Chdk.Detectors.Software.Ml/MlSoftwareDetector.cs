using Net.Chdk.Detectors.Software.Binary;
using Net.Chdk.Model.Software;
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
            var versionStr = TrimStart(strings[0], "Nightly.");
            if (versionStr == null)
                return null;
            var split = versionStr.Split('.');
            DateTime date;
            if (!DateTime.TryParse(split[0], out date))
                return null;
            return new Version(date.Year, date.Month, date.Day);
        }

        protected override SoftwareCameraInfo GetCamera(string[] strings)
        {
            var platform = GetPlatform(strings);
            var revision = GetRevision(strings);
            return GetCamera(platform, revision);
        }

        protected override DateTime? GetCreationDate(string[] strings)
        {
            var builtStr = TrimStart(strings[4], "Built on : ");
            if (builtStr == null)
                return null;
            var index = builtStr.IndexOf(" by ");
            if (index > 0)
                builtStr = builtStr.Substring(0, index);
            DateTime date;
            if (!DateTime.TryParse(builtStr, out date))
                return null;
            return date;
        }

        protected override CultureInfo GetLanguage(string[] strings)
        {
            return CultureInfo.GetCultureInfo("en");
        }

        private static string GetPlatform(string[] strings)
        {
            return TrimStart(strings[1], "Camera   : ");
        }

        private static string GetRevision(string[] strings)
        {
            return TrimStart(strings[2], "Firmware : ");
        }
    }
}
