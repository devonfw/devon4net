using System.Collections.Generic;
using System.Linq;
using MvvmCross.Platform.IoC;
using MvvmCross.Plugins.JsonLocalization;

namespace Excalibur.Cross.Language
{
    public class ExTextProviderBuilder : MvxTextProviderBuilder
    {

        public ExTextProviderBuilder()
            : base(ExTextProvider.GeneralNamespace, ExTextProvider.RootFolderForResources)
        {
        }

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
