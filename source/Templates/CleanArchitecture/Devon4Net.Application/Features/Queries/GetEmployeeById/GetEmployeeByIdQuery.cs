using Devon4Net.Application.Dtos;
using Devon4Net.Infrastructure.MediatR.Query;

namespace Devon4Net.Application.Features.Queries.GetEmployeeById;

public record GetEmployeeByIdQuery(Guid Id) : QueryBase<EmployeeDto>;