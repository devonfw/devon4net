namespace Devon4Net.Application.WebAPI.Domain.Entities
{
    /// <summary>
    /// Entity class for Employee
    /// </summary>
    public partial class Employee
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Surname
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// mail
        /// </summary>
        public string Mail { get; set; }
    }
}
