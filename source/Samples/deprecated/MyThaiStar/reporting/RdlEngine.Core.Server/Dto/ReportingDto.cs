using fyiReporting.RDL;
using Newtonsoft.Json;
using RdlEngine.Core.Server.Controllers;
using System.Collections.Generic;

namespace RdlEngine.Core.Server.Dto
{
    public class ReportingDto
    {
        [JsonProperty(PropertyName = "template")]
        public string TemplateName { get; set; }

        //[JsonProperty(PropertyName = "templateContent")]
        //public string TemplateContent { get; set; }

        [JsonProperty(PropertyName = "params")]
        public List<Param> ParamList { get; set; }

        //[JsonProperty(PropertyName = "connectionString")]
        //public string ConnectionString { get; set; }

        [JsonProperty(PropertyName = "reportType")]
        public int? ReportType { get; set; }
    }
}
