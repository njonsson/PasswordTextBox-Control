using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;
using System.Text.RegularExpressions;

namespace CopyToNuGetTargetFrameworkFolder
{
    /// <summary>
    /// Provides the main entry point for the application.
    /// </summary>
    public static class Program
    {
        private static void AssemblyFileAndDestinationFolder(string[] arguments,
                                                             out FileInfo assemblyFile,
                                                             out DirectoryInfo destinationFolder)
        {
            if (arguments.Length == 2)
            {
                assemblyFile = new FileInfo(arguments[0]);
                destinationFolder = new DirectoryInfo(arguments[1]);
                if (!destinationFolder.Exists)
                {
                    throw new Exception($"Folder '{destinationFolder.FullName}' does not exist");
                }

                destinationFolder = destinationFolder.CreateSubdirectory(NuGetFrameworkFolder(assemblyFile));
                return;
            }

            var thisExe = Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            Console.WriteLine($"Usage: {thisExe} path\\to\\assembly.dll path\\to\\destination");
            throw new Exception($"Expected 2 arguments but received {arguments.Length}");
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static int Main(string[] arguments)
        {
            try
            {
                FileInfo assemblyFile;
                DirectoryInfo destinationFolder;
                AssemblyFileAndDestinationFolder(arguments,
                                                 out assemblyFile,
                                                 out destinationFolder);
                var destinationFile = Path.Combine(destinationFolder.FullName,
                                                   assemblyFile.Name);
                assemblyFile.CopyTo(destinationFile, true);
                Console.Write(destinationFile);
            }
            catch (Exception e)
            {
                Console.Error.Write($"{e.GetType().FullName}: \"{e.Message}\"");
                return 1;
            }

            return 0;
        }

        private static string NuGetFrameworkFolder(FileInfo assemblyFile)
        {
            var assembly = Assembly.LoadFile(assemblyFile.FullName);
            var targetFrameworkAttribute = TargetFrameworkAttribute(assembly);
            FrameworkName frameworkName;
            if (ReferenceEquals(targetFrameworkAttribute, null))
            {
                // ReSharper disable PossibleNullReferenceException
                var csprojFilePath = Path.Combine(assemblyFile.Directory.Parent.Parent.FullName,
                                                  Path.GetFileNameWithoutExtension(assemblyFile.Name) + ".csproj");
                // ReSharper restore PossibleNullReferenceException
                var csproj = File.ReadAllText(csprojFilePath);
                var frameworkVersionMatch = Regex.Match(csproj,
                                                        "<TargetFrameworkVersion>(.*?)</TargetFrameworkVersion>");
                var frameworkNameExpr = $".NETFramework,Version={frameworkVersionMatch.Groups[1]}";
                var frameworkProfileMatch = Regex.Match(csproj,
                                                        "<TargetFrameworkProfile>(.*?)</TargetFrameworkProfile>");
                if (frameworkProfileMatch.Success)
                {
                    frameworkNameExpr += $",Profile={frameworkProfileMatch.Groups[1]}";
                }
                frameworkName = new FrameworkName(frameworkNameExpr);
            }
            else
            {
                Console.WriteLine(targetFrameworkAttribute.FrameworkName);
                frameworkName = new FrameworkName(targetFrameworkAttribute.FrameworkName);
            }
            var identifier = NuGetFrameworkIdentifier(frameworkName);
            var version = NuGetFrameworkVersion(frameworkName);
            var profile = NuGetFrameworkProfile(frameworkName);
            return $"{identifier}{version}{profile}";
        }

        private static string NuGetFrameworkIdentifier(FrameworkName frameworkName)
        {
            switch (frameworkName.Identifier)
            {
                case ".NETFramework":
                    return "Net";
                default:
                    throw new Exception($"Unrecognized target framework identifier \"{frameworkName.Identifier}\"");
            }
        }

        private static string NuGetFrameworkProfile(FrameworkName frameworkName)
        {
            var profile = frameworkName.Profile.ToLower();
            if (string.IsNullOrEmpty(profile) || (profile == "full"))
            {
                return null;
            }

            return $"-{profile}";
        }

        private static string NuGetFrameworkVersion(FrameworkName frameworkName)
        {
            return Regex.Replace(frameworkName.Version.ToString(), @"\D", "");
        }

        private static TargetFrameworkAttribute TargetFrameworkAttribute(Assembly assembly)
        {
            var targetFrameworks = (TargetFrameworkAttribute[])assembly.GetCustomAttributes(typeof(TargetFrameworkAttribute),
                                                                                            true);
            if (1 < targetFrameworks.Length)
            {
                throw new Exception($"Expected one {typeof(TargetFrameworkAttribute).FullName} but found {targetFrameworks.Length}");
            }

            return targetFrameworks.FirstOrDefault();
        }
    }
}
