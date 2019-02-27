using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.IO.Compression;
using System.Reflection;
namespace Maptz.Testing
{

    /// <summary>
    /// A place to run test files. 
    /// </summary>
    public class DefaultWorkspace : ITestWorkspace
    {
        public DefaultWorkspace()
        {
        }


        public virtual void Dispose()
        {

        }
    }
}