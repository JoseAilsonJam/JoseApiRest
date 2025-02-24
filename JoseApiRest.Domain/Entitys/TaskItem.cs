using System.ComponentModel.DataAnnotations;
using System.Net;

namespace JoseApiRest.Domain.Entitys;

public class TaskItem
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O título é obrigatório.")]
    [StringLength((int)HttpStatusCode.Continue, ErrorMessage = "O título deve ter no máximo 100 caracteres.")]
    public string Title { get; set; } = string.Empty;

    [StringLength((int)HttpStatusCode.InternalServerError, ErrorMessage = "A descrição deve ter no máximo 500 caracteres.")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "A data de conclusão é obrigatória.")]
    public DateTime CompletionDate { get; set; }
    public bool IsCompleted { get; set; } = false;

    public TaskItem()
    {
        Id = Guid.NewGuid();
    }

    public TaskItem(Guid id, string title, string description, DateTime completionDate, bool isCompleted)
    {
        Id = id;
        Title = title;
        Description = description;
        CompletionDate = completionDate;
        IsCompleted = isCompleted;
    }
}
