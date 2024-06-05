using Application.Abstractions;
using Application.Features.Tasks.DTOs;

namespace Application.Features.Tasks;

public interface ITaskService : IScopedApplicationService
{
    Task<bool> CompleteTask(long id);
    Task<long> CreateAsync(TaskCreateDTO createDTO);
    Task Delete(long id);
    Task<TaskDTO> Get(long id);
    Task<List<TaskDTO>> ListAsync();
    Task Update(TaskUpdateDTO updateDTO);
}