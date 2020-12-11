using System;
using System.Collections.Generic;
using System.Reflection;
using MvvmCross.Plugin.JsonLocalization;

namespace Excalibur.Cross.Language
{
    /// <summary>
    /// Text provider that will register various things required for making json based localization work within an app.
    /// </summary>
    public class ExTextProvider
    {
        internal static IList<Assembly> ProjectAssemblies = new List<Assembly>();
        internal static string RootFolderForResources = String.Empty;
        internal static string GeneralNamespace = String.Empty;
        internal static string SharedNamespace = String.Empty;

        /// <summary>
        /// Method for initializing and creating the <see cref="ExTextProviderBuilder"/>
        /// </summary>
        /// <param name="projectAssemblies">List of assemblies to look through when creating language files based on ViewModels</param>
        /// <param name="rootFolderForResources">Folder where it can find the localization files</param>
        /// <param name="generalNamespace">Namespace of the app</param>
        /// <param name="sharedNamespace">Namespace for shared localization file</param>
        public static void InitializeAndCreateBuilder(IList<Assembly> projectAssemblies, string rootFolderForResources, string generalNamespace, string sharedNamespace)
        {
            ProjectAssemblies = projectAssemblies;
            RootFolderForResources = rootFolderForResources;
            GeneralNamespace = generalNamespace;
            SharedNamespace = sharedNamespace;

            var builder = new ExTextProviderBuilder();

            MvvmCross.Mvx.RegisterSingleton<IMvxTextProviderBuilder>(builder);
            MvvmCross.Mvx.RegisterSingleton(builder.TextProvider);
        }
    }
}
