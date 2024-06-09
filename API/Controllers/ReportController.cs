using Application.Features.Reports;
using Application.Features.Reports.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ReportController : ControllerBase
{
    private readonly IReportService _reportService;

    public ReportController(IReportService reportService)
    {
        _reportService = reportService;
    }

    [HttpPost("Create")]
    public async Task<ActionResult<long>> Create(ReportCreateDTO createDTO)
    {
        var userId = await _reportService.CreateAsync(createDTO);
        return Ok(userId);
    }

    [HttpGet("List")]
    public async Task<ActionResult<List<ReportDTO>>> List()
    {
        return Ok(await _reportService.ListAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ReportDTO>> Get(long id)
    {
        return Ok(await _reportService.Get(id));
    }

    [HttpPut("Update")]
    public async Task<IActionResult> Update(ReportUpdateDTO updateDTO)
    {
        await _reportService.Update(updateDTO);
        return Ok();
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete(long id)
    {
        await _reportService.Delete(id);
        return Ok();
    }
}
