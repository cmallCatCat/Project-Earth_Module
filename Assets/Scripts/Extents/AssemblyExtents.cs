using System.IO;
using System.Linq;
using System.Reflection;

namespace Extents
{
    public static class AssemblyExtents
    {
        public static Assembly[] GetSolutionAssemblies(string path)
        {
            var assemblies = Directory.GetFiles(path, "*.dll")
                .Select(x => Assembly.Load(AssemblyName.GetAssemblyName(x)));
            return assemblies.ToArray();
        }
    }
}