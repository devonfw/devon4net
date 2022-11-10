using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devon4Net.Infrastructure.AWS.Common.Options
{
    public class AwsLogOptions
    {
        public string LogLevel { get; set; }
        public string LogGroup { get; set; }
        public string LogRegion { get; set; }
        
    }
}
