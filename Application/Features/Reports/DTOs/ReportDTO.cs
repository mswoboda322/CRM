using Application.Features.Tasks.DTOs;

namespace Application.Features.Reports.DTOs;
public class ReportDTO
{
    public long Id { get; set; }
    public string Description { get; set; }
    public DateTime DateOfCreation { get; set; }
    public DateTime? DateOfCompletion { get; set; }
    public string ContractorName { get; set; }
    public TaskSimpleDTO? Task { get; set; }
}
