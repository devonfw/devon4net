namespace Devon4Net.Application.Lambda.Business.SnsManagement.Dto
{
    public class SnsFunctionResult
    {
        public int NumberOfMessages { get; set; }

        public SnsFunctionResult()
        {
            NumberOfMessages = 0;
        }
    }
}
