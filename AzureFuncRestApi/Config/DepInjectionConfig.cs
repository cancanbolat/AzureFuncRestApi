using Autofac;
using AzureFuncRestApi.Interfaces;
using AzureFuncRestApi.Services;
using AzureFunctions.Autofac.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzureFuncRestApi.Config
{
    public class DepInjectionConfig
    {
        public DepInjectionConfig(string functionName)
        {
            DependencyInjection.Initialize(b => b.RegisterType<ProductService>().As<IProductService>(), functionName);
        }
    }
}
