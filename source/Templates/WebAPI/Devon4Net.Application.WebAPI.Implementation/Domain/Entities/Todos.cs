namespace Devon4Net.Application.WebAPI.Implementation.Domain.Entities
{
    /// <summary>
    /// Entity class for Todos
    /// </summary>
    public partial class Todos
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
