using Application.Features.Tasks;
using Application.Features.Tasks.DTOs;
using Application.Features.Users.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpPost("Create")]
    public async Task<ActionResult<long>> Create(TaskCreateDTO createDTO)
    {
        var userId = await _taskService.CreateAsync(createDTO);
        return Ok(userId);
    }

    [HttpGet("List")]
    public async Task<ActionResult<List<TaskDTO>>> List()
    {
        return Ok(await _taskService.ListAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TaskDTO>> Get(long id)
    {
        return Ok(await _taskService.Get(id));
    }

    [HttpPut("Update")]
    public async Task<IActionResult> Update(TaskUpdateDTO updateDTO)
    {
        await _taskService.Update(updateDTO);
        return Ok();
    }

    [HttpPut("CompleteTask")]
    public async Task<ActionResult<bool>> CompleteTask(long id)
    {
        return Ok(await _taskService.CompleteTask(id));
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete(long id)
    {
        await _taskService.Delete(id);
        return Ok();
    }
}
