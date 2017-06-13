using System;
using Cake.Core.Annotations;
using Cake.Core.Composition;
using Cake.Core.Packaging;
using Cake.APT.Module;

[assembly: CakeModule(typeof(APTModule))]

namespace Cake.APT.Module
{
    public class APTModule : ICakeModule
    {
        public void Register(ICakeContainerRegistrar registrar)
        {
            if (registrar == null) {
                throw new ArgumentNullException(nameof(registrar));
            }
            registrar.RegisterType<APTContentResolver>().As<IAPTContentResolver>();
            registrar.RegisterType<APTPackageInstaller>().As<IPackageInstaller>();
        }
    }
}
