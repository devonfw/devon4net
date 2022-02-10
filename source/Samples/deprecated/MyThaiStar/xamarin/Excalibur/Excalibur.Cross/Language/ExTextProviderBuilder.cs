using System.Collections.Generic;
using System.Linq;
using MvvmCross.IoC;
using MvvmCross.Plugin.JsonLocalization;

namespace Excalibur.Cross.Language
{
    /// <summary>
    /// Custom text provider that provides localization based on json localization <see cref="MvvmCross.Plugin.JsonLocalization"/>
    /// </summary>
    public class ExTextProviderBuilder : MvxTextProviderBuilder
    {
        /// <summary>
        /// Constructor that will provide the MvvmCross GeneralNamespace and RootFolder to the base
        /// </summary>
        public ExTextProviderBuilder()
            : base(ExTextProvider.GeneralNamespace, ExTextProvider.RootFolderForResources)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Override for the loading of ResourceFiles.
        /// This will make sure that every ViewModel will have a json file for it registered.
        ///
        /// And lastly will register a Shared json file.
        /// </summary>
        protected override IDictionary<string, string> ResourceFiles
        {
            get
            {
                var dictionary = ExTextProvider.ProjectAssemblies
                                                .SelectMany(x => x.CreatableTypes()
                                                    .Where(t => t.Name.EndsWith("ViewModel")))
                                                .ToDictionary(t => t.Name, t => t.Name);

                dictionary[ExTextProvider.SharedNamespace] = ExTextProvider.SharedNamespace;
                return dictionary;
            }
        }
    }
}
