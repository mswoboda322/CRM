namespace Application.Features.Tasks.DTOs;
public class TaskSimpleDTO
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DateOfCreation { get; set; }
    public DateTime? DateOfCompletion { get; set; }
}
