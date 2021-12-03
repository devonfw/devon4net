namespace Devon4Net.Infrastructure.AnsibleTower.Dto.Inventories
{
    public class CreateInventoryRequestDto
    {
        public string host_filter { get; set; }
        public int? organization { get; set; }
        public string kind { get; set; }
        public string name { get; set; }
    }
}
