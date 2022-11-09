using System.Collections.Immutable;

namespace Devon4Net.Infrastructure.AWS.CDK.Consts
{
    public static class DmsMigrationTypeConsts
    {
        public static readonly ImmutableArray<string> MigrationTypes = new ImmutableArray<string>()
        {
            "cdc",
            "full-load",
            "full-load-and-cdc"
        };
    }
}
