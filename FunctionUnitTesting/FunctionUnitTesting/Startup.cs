using FunctionUnitTesting;
using FunctionUnitTesting.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]
namespace FunctionUnitTesting
{

    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            // Registering services
            builder
                .Services
                .AddSingleton<IContext, CacheContext>()
                .AddSingleton<ITopicService, TopicService>();
        }
    }
}
