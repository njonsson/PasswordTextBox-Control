using System.Reflection;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Copy to NuGet Target Framework Folder")]
[assembly: AssemblyDescription("Copies a .NET assembly to a folder named for the assembly′s target framework (in NuGet-abbreviated format)")]
#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif
[assembly: AssemblyCompany("Nils Jonsson <passwordtextbox@nilsjonsson.com>")]
[assembly: AssemblyProduct("Copy to NuGet Target Framework Folder")]
[assembly: AssemblyCopyright("Copyright © 2016 Nils Jonsson and PasswordTextBox Control contributors")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("7acd3cb8-a274-49e9-9c4f-760542155d6f")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("0.2.0.*")]
[assembly: AssemblyFileVersion("0.2.0")]
