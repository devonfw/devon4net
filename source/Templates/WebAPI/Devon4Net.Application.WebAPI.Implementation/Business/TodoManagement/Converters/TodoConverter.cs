using Devon4Net.Application.WebAPI.Implementation.Business.TodoManagement.Dto;
using Devon4Net.Application.WebAPI.Implementation.Domain.Entities;

namespace Devon4Net.Application.WebAPI.Implementation.Business.TodoManagement.Converters
{
    /// <summary>
    /// TodoConverter
    /// </summary>
    public static class TodoConverter
    {
        /// <summary>
        /// ModelToDto transformation
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static TodoDto ModelToDto(Todos item)
        {
            if (item == null) return new TodoDto();

            return new TodoDto
            {
                Id = item.Id,
                Description = item.Description
            };
        }

    }
}
