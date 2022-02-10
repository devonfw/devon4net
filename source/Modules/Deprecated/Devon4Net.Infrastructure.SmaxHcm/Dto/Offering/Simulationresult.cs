namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Offering
{
    public class Simulationresult
    {
        public List<string> TriggeringFields { get; set; }
        public List<string> MandatoryFields { get; set; }
        public Changedfields ChangedFields { get; set; }
        public List<object> Errors { get; set; }
        public List<Renderingrule> RenderingRules { get; set; }
        public List<Cascadingrule> CascadingRules { get; set; }
    }
}