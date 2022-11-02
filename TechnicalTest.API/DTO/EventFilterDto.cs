namespace TechnicalTest.API.DTO;

public class EventFilterDto
{
    public DateTime? Date { get; set; }
    public bool? HasDocument { get; set; }
    public string? Order { get; set; }
}