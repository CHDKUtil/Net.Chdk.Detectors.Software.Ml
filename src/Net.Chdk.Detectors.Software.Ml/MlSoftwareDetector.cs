using Net.Chdk.Detectors.Software.Binary;
using Net.Chdk.Providers.Software;
using System;
using System.Globalization;
using System.Linq;

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

        protected override int StringCount => 6;

        protected override char SeparatorChar => '\n';

        protected override Version GetProductVersion(string[] strings)
        {
            var split = strings[0].Split('.');
            if (split.Length < 3)
                return null;
            var versionStr = split[split.Length - 2];
            DateTime date;
            if (!DateTime.TryParse(versionStr, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out date))
                return null;
            return new Version(date.Year, date.Month, date.Day);
        }

        protected override DateTime? GetCreationDate(string[] strings)
        {
            var builtStr = GetValue(strings, 1, "Built on ");
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
            return GetValue(strings, 1, "Camera");
        }

        protected override string GetRevision(string[] strings)
        {
            return GetValue(strings, 2, "Firmware");
        }

        private static string GetValue(string[] strings, int skip, string prefix)
        {
            return strings
                .Skip(skip)
                .FirstOrDefault(s => s.StartsWith(prefix))
                ?.TrimStart(prefix)
                ?.TrimStart(':', ' ');
        }
    }
}
