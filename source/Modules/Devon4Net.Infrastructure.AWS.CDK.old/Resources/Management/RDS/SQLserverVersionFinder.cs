using Amazon.CDK.AWS.RDS;
using Amazon.JSII.Runtime.Deputy;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Management.RDS
{
    public class SQLserverVersionFinder : DeputyBase
    {
        public static SqlServerEngineVersion GetSqlServerVersion(string sqlServerVersion)
        {
            try
            {
                return GetStaticProperty<SqlServerEngineVersion>(typeof(SqlServerEngineVersion), sqlServerVersion.ToUpper());
            }
            catch(Exception ex)
            {
                throw new ArgumentException($"The method for returning the Version Engine of SQL Failed:{ex.Message} | {ex.InnerException} | {ex.StackTrace}");
            }
        }
    }
}
