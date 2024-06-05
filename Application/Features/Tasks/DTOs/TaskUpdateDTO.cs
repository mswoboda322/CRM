using System.ComponentModel.DataAnnotations;

namespace Application.Features.Tasks.DTOs;
public class TaskUpdateDTO
{
    [Required]
    public long Id { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public long UserId { get; set; }
}
