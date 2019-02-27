using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.IO.Compression;
using System.Reflection;
namespace Maptz.Testing
{


    /// <summary>
    /// A test fixture with service injection provided. 
    /// </summary>
    public class InjectedServicesFixture : IInjectedServicesFixture
    {
        /* #region Interface: 'Maptz.Testing.IInjectedServicesFixture' Properties */
        IServiceProvider IInjectedServicesFixture.ServiceProvider => this.ServiceProvider;
        /* #endregion Interface: 'Maptz.Testing.IInjectedServicesFixture' Properties */
        /* #region Protected Methods */
        protected virtual void AddServices(ServiceCollection servicesCollection)
        {
            servicesCollection.AddTransient<ITemporaryFilesService, TemporaryFilesService>();
            servicesCollection.AddTransient<ITestWorkspace, DefaultWorkspace>();
        }
        /* #endregion Protected Methods */
        /* #region Public Properties */
        public ServiceProvider ServiceProvider { get; }
        /* #endregion Public Properties */
        /* #region Public Constructors */
        public InjectedServicesFixture(Action<IServiceCollection> addServices = null)
        {
            var servicesCollection = new ServiceCollection();
            this.AddServices(servicesCollection);
            if (addServices != null)
            {
                addServices(servicesCollection);
            }
            var serviceProvider = servicesCollection.BuildServiceProvider();
            this.ServiceProvider = serviceProvider;
        }
        /* #endregion Public Constructors */
        /* #region Interface: 'System.IDisposable' Methods */
        public void Dispose()
        {
            this.OnDisposing();
            this.ServiceProvider.Dispose();
        }

        protected virtual void OnDisposing()
        {
            
        }
        /* #endregion Interface: 'System.IDisposable' Methods */
    }
}