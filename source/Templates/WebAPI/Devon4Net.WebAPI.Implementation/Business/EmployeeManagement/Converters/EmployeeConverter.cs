using Devon4Net.WebAPI.Implementation.Business.EmployeeManagement.Dto;
using Devon4Net.WebAPI.Implementation.Domain.Entities;

namespace Devon4Net.WebAPI.Implementation.Business.EmployeeManagement.Converters
{
    /// <summary>
    /// TodoConverter
    /// </summary>
    public static class EmployeeConverter
    {
        /// <summary>
        /// ModelToDto TODO transformation
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static EmployeeDto ModelToDto(Employee item)
        {
            if (item == null) return new EmployeeDto();

            return new EmployeeDto
            {
                Id = item.Id,
                Name = item.Name,
                Surname = item.Surname,
                Mail = item.Mail
            };
        }

    }
}
