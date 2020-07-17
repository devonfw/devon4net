using System;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Common;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Providers;

namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Designer
{

    public class GetDesignResponseDto
    {
        public string self { get; set; }
        public string content_version { get; set; }
        public DateTime created { get; set; }
        public DateTime modified { get; set; }
        public string global_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
        public Ext ext { get; set; }
        public string url { get; set; }
        public string version { get; set; }
        public bool published { get; set; }
        public string containerId { get; set; }
        public Optionmodel optionModel { get; set; }
        public string type { get; set; }
        public object[] upgrades_from { get; set; }
        public object[] upgrades_to { get; set; }
    }


    public class Optionmodel
    {
        public string self { get; set; }
    }
}
