namespace Devon4Net.Application.Lambda.Business.SqsManagement.Dto
{
    public class SqsFunctionResult
    {
        public int NumberOfMessages { get; set; }

        public SqsFunctionResult()
        {
            NumberOfMessages = 0;
        }
    }
}
