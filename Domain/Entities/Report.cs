namespace Domain.Entities;
public class Report
{
    public long Id { get; set; }
    public string Description { get; set; }
    public DateTime DateOfCreation { get; set; }
    public DateTime? DateOfCompletion { get; set; }
    public string ContractorName { get; set; }
    public long? TaskId { get; set; }
    public Task? Task { get; set; }

    public void Update(string description,
                       string contractorName)
    {
        Description = description;
        ContractorName = contractorName;
    }

    public void Complete()
    {
        DateOfCompletion = DateTime.Now;
    }

    public static class Factory
    {
        public static Report Create(string description,
                                    string contractorName)
        {
            return new Report()
            {
                Description = description,
                ContractorName = contractorName,
                DateOfCreation = DateTime.Now,
                Task = Task.Factory.CreateFromReport($"Zgłoszenie kontrahenta: {contractorName}", description)
            };
        }
    }

    private Report()
    {

    }
}
