using Amazon.CDK.AWS.RDS;
using Amazon.JSII.Runtime.Deputy;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Management.RDS
{
    public class SQLserverVersionFinder : DeputyBase
    {
        public static SqlServerEngineVersion GetSqlServerVersion(string sqlServerVersion)
        {
            return GetStaticProperty<SqlServerEngineVersion>(typeof(SqlServerEngineVersion), sqlServerVersion.ToUpper());
        }
    }
}
