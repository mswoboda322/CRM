using Application.Features.Reports.DTOs;
using Application.Features.Users.DTOs;

namespace Application.Features.Tasks.DTOs;
public class TaskDTO
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DateOfCreation { get; set; }
    public DateTime? DateOfCompletion { get; set; }
    public UserDTO? User { get; set; }
    public ReportSimpleDTO? Report { get; set; }
}
