using Net.Chdk.Detectors.Software.Binary;
using Net.Chdk.Model.Software;
using Net.Chdk.Providers.Software;
using System;
using System.Globalization;

namespace Net.Chdk.Detectors.Software.Ml
{
    abstract class MlSoftwareDetector : ProductBinarySoftwareDetector
    {
        protected MlSoftwareDetector(ISourceProvider sourceProvider)
            : base(sourceProvider)
        {
        }

        public sealed override string ProductName => "ML";

        protected sealed override Version GetProductVersion(string[] strings)
        {
            var split = GetVersionString(strings).Split('.');
            if (split.Length < 3)
                return null;
            var versionStr = split[split.Length - 2];
            DateTime date;
            if (!DateTime.TryParse(versionStr, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out date))
                return null;
            return new Version(date.Year, date.Month, date.Day);
        }

        protected sealed override DateTime? GetCreationDate(string[] strings)
        {
            var dateStr = GetCreationDateString(strings);
            DateTime date;
            if (!DateTime.TryParse(dateStr, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out date))
                return null;
            return date;
        }

        protected sealed override SoftwareBuildInfo GetBuild(string[] strings)
        {
            return new SoftwareBuildInfo
            {
                Name = string.Empty,
                Status = string.Empty,
                Changeset = GetChangeset(strings)
            };
        }

        protected sealed override CultureInfo GetLanguage(string[] strings)
        {
            return CultureInfo.GetCultureInfo("en");
        }

        protected abstract string GetVersionString(string[] strings);
        protected abstract string GetCreationDateString(string[] strings);
        protected abstract string GetChangeset(string[] strings);
    }
}
