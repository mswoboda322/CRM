using Application.Features.Tasks.DTOs;
using AutoMapper;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Tasks;
public class TaskService : ITaskService
{
    private readonly DatabaseContext _databaseContext;
    private readonly IMapper _mapper;

    public TaskService(DatabaseContext databaseContext,
                         IMapper mapper)
    {
        _databaseContext = databaseContext;
        _mapper = mapper;
    }

    public async Task<long> CreateAsync(TaskCreateDTO createDTO)
    {
        var task = Domain.Entities.Task.Factory.Create(createDTO.Title, createDTO.Description, createDTO.UserId);
        await _databaseContext.Tasks.AddAsync(task);
        await _databaseContext.SaveChangesAsync();
        return task.Id;
    }

    public async Task<List<TaskDTO>> ListAsync()
    {
        var tasks = await _databaseContext.Tasks.Include(x => x.Report).Include(x => x.User).ToListAsync();
        return _mapper.Map<List<TaskDTO>>(tasks);
    }

    public async Task<TaskDTO> Get(long id)
    {
        var task = await GetTask(id);
        return _mapper.Map<TaskDTO>(task);
    }

    public async Task Update(TaskUpdateDTO updateDTO)
    {
        var task = await GetTask(updateDTO.Id);
        task.Update(updateDTO.Title, updateDTO.Description, updateDTO.UserId);
        await _databaseContext.SaveChangesAsync();
    }

    public async Task Delete(long id)
    {
        var task = await GetTask(id);
        _databaseContext.Tasks.Remove(task);
        await _databaseContext.SaveChangesAsync();
    }

    public async Task<bool> CompleteTask(long id)
    {
        var task = await GetTask(id);
        task.Complete();
        await _databaseContext.SaveChangesAsync();
        return true;
    }

    // ---Private

    private async Task<Domain.Entities.Task> GetTask(long id)
    {
        return await _databaseContext.Tasks.Include(x => x.Report).Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id) ??
            throw new Exception("Nie znaleziono zadania");
    }
}
