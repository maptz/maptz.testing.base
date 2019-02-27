using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.IO.Compression;
using System.Reflection;
namespace Maptz.Testing
{
    public interface ITempDirectoryWorkspace : ITestWorkspace
    {
        string TempDirectoryPath { get; }
    }
}