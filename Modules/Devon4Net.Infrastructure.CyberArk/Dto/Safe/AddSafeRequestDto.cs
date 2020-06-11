namespace Devon4Net.Infrastructure.CyberArk.Dto.Safe
{
    public class AddSafeRequestDto
    {
        public Safe safe { get; set; }
    }

    public class Safe
    {
        public string SafeName { get; set; }
        public string Description { get; set; }
        public bool OLACEnabled { get; set; }
        public string ManagingCPM { get; set; }
        public int NumberOfVersionsRetention { get; set; }
    }
}
