using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
namespace Maptz.Testing
{

   
    /// <summary>
    /// Extensions
    /// </summary>
    public static class TempDirectoryWorkspaceExtensions
    {
        /// <summary>
        /// Extracts a named resource into the test workspace
        /// </summary>
        /// <param name="containingAssembly"></param>
        /// <param name="resourceName"></param>
        public static void ExtractNamedResource(this ITempDirectoryWorkspace testWorkspace, Assembly containingAssembly, string resourceName, string outputName)
        {
            var fileInfo = new FileInfo(Path.Combine(testWorkspace.TempDirectoryPath, outputName));
            using (var fs = fileInfo.Create())
            {
                var assemblyName = containingAssembly.GetName().Name;
                var fullResourceName = $"{assemblyName}.{resourceName}";

                var nms = containingAssembly.GetManifestResourceNames();
                if (!nms.Any(p => p == fullResourceName))
                {
                    throw new Exception($"Cannot find resource named {fullResourceName} in assembly.");
                }

                using (var stream = containingAssembly.GetManifestResourceStream(fullResourceName))
                {
                    stream.CopyTo(fs);
                }
            }
        }

        /// <summary>
        /// Extracts a named resource into the test workspace
        /// </summary>
        /// <param name="containingAssembly"></param>
        /// <param name="resourceName"></param>
        public static  void ExtractNamedResourceZipFile(this ITempDirectoryWorkspace testWorkspace, Assembly containingAssembly, string resourceName, string outputName)
        {
            var fileInfo = new FileInfo(Path.Combine(testWorkspace.TempDirectoryPath, outputName));

            var td = new TemporaryFilesService().GetTemporaryDirectory();
            var tempZipFileInfo = new FileInfo(Path.Combine(td, outputName));
            using (var fs = tempZipFileInfo.Create())
            {
                var assemblyName = containingAssembly.GetName().Name;
                var fullResourceName = $"{assemblyName}.{resourceName}";

                var nms = containingAssembly.GetManifestResourceNames();
                if (!nms.Any(p => p == fullResourceName))
                {
                    throw new Exception($"Cannot find resource named {fullResourceName} in assembly.");
                }
                using (var stream = containingAssembly.GetManifestResourceStream(fullResourceName))
                {
                    stream.CopyTo(fs);
                }
            }
            ZipFile.ExtractToDirectory(tempZipFileInfo.FullName, testWorkspace.TempDirectoryPath);
            new DirectoryInfo(td).Delete(true);
        }
    }
}