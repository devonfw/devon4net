namespace Devon4Net.Application.WebAPI.Implementation.Business.TodoManagement.Dto
{
    /// <summary>
    /// TodoDto definition
    /// </summary>
    public class TodoDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Done
        /// </summary>
        public bool Done { get; set; }
    }
}
