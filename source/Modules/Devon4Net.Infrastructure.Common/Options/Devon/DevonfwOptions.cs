namespace Devon4Net.Infrastructure.Common.Options.Devon
{
    public class DevonfwOptions
    {
        public bool UseDetailedErrorsKey { get; set; }
        public bool UseIIS { get; set; }
        public bool UseSwagger { get; set; }
        public bool UseXsrf { get; set; }
        public bool UseModelStateValidation { get; set; }
        public string Environment { get; set; }
        public KestrelDevonOptions Kestrel { get; set; }
        public IisDevonOptions IIS { get; set; }
        public ExtraSettingsOptions ExtraSettings { get; set; }
    }

}