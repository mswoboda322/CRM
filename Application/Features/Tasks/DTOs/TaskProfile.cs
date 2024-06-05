using AutoMapper;
using Domain.Entities;

namespace Application.Features.Tasks.DTOs;
public class TaskProfile : Profile
{
    public TaskProfile()
    {
        CreateMap<Domain.Entities.Task, TaskDTO>()
           .PreserveReferences();

        CreateMap<Domain.Entities.Task, TaskSimpleDTO>()
           .PreserveReferences();
    }
}
