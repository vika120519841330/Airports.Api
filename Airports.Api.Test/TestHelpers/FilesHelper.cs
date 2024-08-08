using System.Reflection;

namespace Airports.Api.Test.TestHelpers;

public class FilesHelper
{
    private string AssemblyPath => Assembly.GetCallingAssembly().Location;

    protected string ContentPath => Path.Combine(AssemblyPath.Substring(0, AssemblyPath.IndexOf("bin") - 1), "");
}