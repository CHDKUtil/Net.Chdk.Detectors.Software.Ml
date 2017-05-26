using Net.Chdk.Detectors.Software.Product;
using Net.Chdk.Providers.Boot;
using System;
using System.Globalization;

namespace Net.Chdk.Detectors.Software.Ml
{
    sealed class MlProductDetector : ProductDetector
    {
        public MlProductDetector(IBootProviderResolver bootProviderResolver)
            : base(bootProviderResolver)
        {
        }

        public override string CategoryName => "EOS";

        protected override string ProductName => "ML";

        protected override Version GetVersion(string rootPath)
        {
            return null;
        }

        protected override CultureInfo GetLanguage(string rootPath)
        {
            return CultureInfo.GetCultureInfo("en");
        }
    }
}
