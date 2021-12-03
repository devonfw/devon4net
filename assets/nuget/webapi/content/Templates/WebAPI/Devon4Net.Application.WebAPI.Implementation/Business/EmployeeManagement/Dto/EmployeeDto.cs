using System.ComponentModel.DataAnnotations;

namespace Devon4Net.Application.WebAPI.Implementation.Business.EmployeeManagement.Dto
{
    /// <summary>
    /// Employee definition
    /// </summary>
    public class EmployeeDto
    {
        /// <summary>
        /// the Id
        /// </summary>
        public long Id { get; set; }
        
        /// <summary>
        /// the Name
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// the Surname
        /// </summary>
        [Required]
        public string Surname { get; set; }

        /// <summary>
        /// the Mail
        /// </summary>
        [Required]
        public string Mail { get; set; }
    }
}
