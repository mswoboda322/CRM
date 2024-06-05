using System.ComponentModel.DataAnnotations;

namespace Application.Features.Reports.DTOs;
public class ReportUpdateDTO
{
    [Required]
    public long Id { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public string ContractorName { get; set; }
}
