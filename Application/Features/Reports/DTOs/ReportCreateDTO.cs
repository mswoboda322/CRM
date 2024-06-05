using System.ComponentModel.DataAnnotations;

namespace Application.Features.Reports.DTOs;
public class ReportCreateDTO
{
    [Required]
    public string Description { get; set; }
    [Required]
    public string ContractorName { get; set; }
}
