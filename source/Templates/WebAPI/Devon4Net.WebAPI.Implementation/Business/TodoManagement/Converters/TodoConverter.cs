using Devon4Net.WebAPI.Implementation.Business.TodoManagement.Dto;
using Devon4Net.WebAPI.Implementation.Domain.Entities;

namespace Devon4Net.WebAPI.Implementation.Business.TodoManagement.Converters
{
    /// <summary>
    /// TodoConverter
    /// </summary>
    public static class TodoConverter
    {
        /// <summary>
        /// ModelToDto TODO transformation
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
