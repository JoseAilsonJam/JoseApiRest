namespace JoseApiRest.Domain.Entitys;

public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CompletionDate { get; set; }
}
