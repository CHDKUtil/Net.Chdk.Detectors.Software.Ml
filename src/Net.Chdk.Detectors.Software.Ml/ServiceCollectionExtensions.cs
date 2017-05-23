using Microsoft.Extensions.DependencyInjection;

namespace Net.Chdk.Detectors.Software.Ml
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMlSoftwareDetector(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddSingleton<IProductBinarySoftwareDetector, MlSoftwareDetector>();
        }
    }
}
