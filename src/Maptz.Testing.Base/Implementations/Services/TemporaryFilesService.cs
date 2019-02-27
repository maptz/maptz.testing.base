using System.IO;
namespace Maptz.Testing
{

    public class TemporaryFilesService : ITemporaryFilesService
    {
        /// <summary>
        /// Gets a new temporary directory.
        /// </summary>
        /// <returns></returns>
        public string GetTemporaryDirectory()
        {
            string tempDirectory = System.IO.Path.Combine(System.IO.Path.GetTempPath(), System.IO.Path.GetRandomFileName());
            Directory.CreateDirectory(tempDirectory);
            return tempDirectory;
        }
    }
}