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
    public class TempDirectoryWorkspace :DefaultWorkspace, ITempDirectoryWorkspace
    {
        public TempDirectoryWorkspace(ITemporaryFilesService temporaryFilesService)
        {
            this.TemporaryFilesService = temporaryFilesService;
            var tempDirectory = new TemporaryFilesService().GetTemporaryDirectory();
            this.TempDirectoryPath = tempDirectory;
        }

        public ITemporaryFilesService TemporaryFilesService { get; }
        public string TempDirectoryPath { get; }

        public override void Dispose()
        {
            var tpdi = new DirectoryInfo(this.TempDirectoryPath);
            tpdi.Delete(true);

            base.Dispose();
        }
    }
}