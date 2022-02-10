namespace Devon4Net.Infrastructure.AnsibleTower.Dto.Common
{
    public class PaginatedResultDto<T>
    {
        public int? count { get; set; }
        public string next { get; set; }
        public string previous { get; set; }
        public List<T> results { get; set; }

    }
}
