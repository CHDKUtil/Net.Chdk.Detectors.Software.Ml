using Net.Chdk.Detectors.Software.Binary;
using Net.Chdk.Model.Software;
using Net.Chdk.Providers.Software;
using System;
using System.Globalization;
using System.Linq;

namespace Net.Chdk.Detectors.Software.Ml
{
    abstract class MlSoftwareDetector : ProductBinarySoftwareDetector
    {
        protected MlSoftwareDetector(ISourceProvider sourceProvider)
            : base(sourceProvider)
        {
        }

        public sealed override string ProductName => "ML";

        protected sealed override bool GetProductVersion(string[] strings, out Version version, out string versionPrefix)
        {
            version = null;
            versionPrefix = null;
            var split = GetVersionString(strings).Split('.');
            if (split.Length < 3)
                return false;
            var versionStr = split[split.Length - 2];
            DateTime date;
            if (!DateTime.TryParse(versionStr, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out date))
                return false;
            version = new Version(date.Year, date.Month, date.Day);
            versionPrefix = string.Join(".", split.Take(split.Length - 2));
            return true;
        }

        protected override Version GetProductVersion(string[] strings)
        {
            throw new NotImplementedException();
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
                Status = GetStatus(strings),
                Changeset = GetChangeset(strings)
            };
        }

        protected sealed override CultureInfo GetLanguage(string[] strings)
        {
            return CultureInfo.GetCultureInfo("en");
        }

        protected abstract string GetVersionString(string[] strings);
        protected abstract string GetCreationDateString(string[] strings);
        protected abstract string GetStatus(string[] strings);
        protected abstract string GetChangeset(string[] strings);
    }
}
