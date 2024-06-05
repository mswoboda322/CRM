using System.ComponentModel.DataAnnotations;

namespace Application.Features.Tasks.DTOs;
public class TaskCreateDTO
{
    [Required]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }
    public long? UserId { get; set; }
}
