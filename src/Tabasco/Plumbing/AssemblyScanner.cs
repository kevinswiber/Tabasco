using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Tabasco.Plumbing
{
    public class AssemblyScanner
    {
        public static IEnumerable<Type> FindPublicTypesInBaseDirectory(Func<Type, bool> filter = null)
        {
            var assemblyFiles =
                new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory)
                    .GetFiles("*.dll", SearchOption.TopDirectoryOnly)
                    .Concat(new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory)
                               .GetFiles("*.exe", SearchOption.TopDirectoryOnly));

            if (Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin")))
            {
                assemblyFiles =
                    assemblyFiles.Concat(
                        new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin"))
                        .GetFiles("*.dll", SearchOption.TopDirectoryOnly));
            }

            IEnumerable<Type> typesCollected = Type.EmptyTypes;

            foreach (var assemblyFile in assemblyFiles)
            {
                Type[] typesInAsm;
                try
                {
                    var assembly = Assembly.LoadFrom(assemblyFile.FullName);
                    typesInAsm = assembly.GetTypes();
                }
                catch (Exception)
                {
                    continue;
                }

                typesCollected = typesCollected.Concat(typesInAsm);
            }

            if (filter == null)
            {
                filter = type => true;
            }

            var types = typesCollected.Where(type => TypeIsPublicClass(type) && filter(type));

            return types;
        }

        private static bool TypeIsPublicClass(Type type)
        {
            return (type != null && type.IsPublic && type.IsClass && !type.IsAbstract);
        }
    }
}