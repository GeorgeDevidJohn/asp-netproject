using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using AutoMapper;


namespace Library_management.Middleware
{
    public sealed class ConfigurationProvider
    {
        private static readonly object padlock = new object();
        private IConfigurationRoot Configuration = null;
        private ServiceProvider serviceProvider = null;

        public ConfigurationProvider()
        {

        }
        private IConfigurationRoot ConfigInstance
        {
            get
            {
                lock (padlock)
                {
                    if (Configuration == null)
                    {

                    }

                }
                return Configuration;
            }
        }

        public ServiceProvider ServiceCollectionInstance
        {
            get
            {
                lock (padlock)
                {
                    if (serviceProvider == null)
                    {

                        IServiceCollection services = new ServiceCollection();
                        var mappingConfig = new MapperConfiguration(mc =>
                        {
                            mc.AddProfile(new MapperConfig());

                        });
                        IMapper mapper = mappingConfig.CreateMapper();
                        services.AddSingleton(mapper);
                    }
                }
                return serviceProvider;
            }

        }


        public string GetConnectionString()
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["LibraryManagementConnectionString"].ConnectionString;
            return connectionString;
        }
    }
}
