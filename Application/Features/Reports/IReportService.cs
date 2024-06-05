using Application.Abstractions;
using Application.Features.Reports.DTOs;
using Application.Features.Tasks.DTOs;

namespace Application.Features.Reports;

public interface IReportService : IScopedApplicationService
{
    Task<long> CreateAsync(ReportCreateDTO createDTO);
    Task Delete(long id);
    Task<ReportDTO> Get(long id);
    Task<List<ReportDTO>> ListAsync();
    Task Update(ReportUpdateDTO updateDTO);
}