namespace Devon4Net.Infrastructure.AnsibleTower.Dto.JobTemplates
{

    public class CreateJobTemplateRequestDto
    {
        public int project { get; set; }
        public int organization { get; set; }
        public bool ask_inventory_on_launch { get; set; }
        public string name { get; set; }
        public string playbook { get; set; }
    }
}
