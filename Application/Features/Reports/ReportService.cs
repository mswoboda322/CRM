using Application.Features.Reports.DTOs;
using Application.Features.Tasks.DTOs;
using AutoMapper;
using Domain.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Reports;
public class ReportService : IReportService
{
    private readonly DatabaseContext _databaseContext;
    private readonly IMapper _mapper;

    public ReportService(DatabaseContext databaseContext,
                         IMapper mapper)
    {
        _databaseContext = databaseContext;
        _mapper = mapper;
    }

    public async Task<long> CreateAsync(ReportCreateDTO createDTO)
    {
        var report = Report.Factory.Create(createDTO.Description, createDTO.Description);
        await _databaseContext.Reports.AddAsync(report);
        await _databaseContext.SaveChangesAsync();
        return report.Id;
    }

    public async Task<List<ReportDTO>> ListAsync()
    {
        var reports = await _databaseContext.Reports.Include(x => x.Task).ToListAsync();
        return _mapper.Map<List<ReportDTO>>(reports);
    }

    public async Task<ReportDTO> Get(long id)
    {
        var task = await GetReport(id);
        return _mapper.Map<ReportDTO>(task);
    }

    public async System.Threading.Tasks.Task Update(ReportUpdateDTO updateDTO)
    {
        var report = await GetReport(updateDTO.Id);
        report.Update(updateDTO.Description, updateDTO.ContractorName);
        await _databaseContext.SaveChangesAsync();
    }

    public async System.Threading.Tasks.Task Delete(long id)
    {
        var task = await GetReport(id);
        _databaseContext.Reports.Remove(task);
        await _databaseContext.SaveChangesAsync();
    }

    // ---Private

    private async Task<Report> GetReport(long id)
    {
        return await _databaseContext.Reports.Include(x => x.Task).FirstOrDefaultAsync(x => x.Id == id) ??
            throw new Exception("Nie znaleziono zadania");
    }
}
