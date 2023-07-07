using Devon4Net.Application.Dtos;
using Devon4Net.Domain.Entities;

namespace Devon4Net.Application.Converters
{
    /// <summary>
    /// EmployeeConverter
    /// </summary>
    public static class EmployeeConverter
    {
        /// <summary>
        /// ModelToDto transformation
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static EmployeeDto ModelToDto(Employee? item)
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
