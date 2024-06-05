namespace Domain.Entities;
public class Task
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DateOfCreation { get; set; }
    public DateTime? DateOfCompletion { get; set; }
    public long? UserId { get; set; }
    public User? User { get; set; }
    public long? ReportId { get; set; }
    public Report? Report { get; set; }

    public void Update(string title,
                       string description,
                       long userId)
    {
        Title = title;
        Description = description;
        UserId = userId;
    }

    public void Complete()
    {
        DateOfCompletion = DateTime.Now;
        if(Report is not null)
            Report.Complete();
    }

    public static class Factory
    {
        public static Task Create(string title,
                                  string description,
                                  long? userId)
        {
            return new Task()
            {
                Title = title,
                Description = description,
                DateOfCreation = DateTime.Now,
                UserId = userId
            };
        }

        public static Task CreateFromReport(string title,
                                 string description)
        {
            return new Task()
            {
                Title = title,
                Description = description,
                DateOfCreation = DateTime.Now,
            };
        }
    }

    private Task()
    {
        
    }
}
