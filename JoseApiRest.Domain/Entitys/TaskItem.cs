using System.ComponentModel.DataAnnotations;

namespace JoseApiRest.Domain.Entitys;

public class TaskItem
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O título é obrigatório.")]
    [StringLength(100, ErrorMessage = "O título deve ter no máximo 100 caracteres.")]
    public string Title { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "A descrição deve ter no máximo 500 caracteres.")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "A data de conclusão é obrigatória.")]
    public DateTime CompletionDate { get; set; }
    public bool IsCompleted { get; set; } = false;

    public TaskItem()
    {
        Id = Guid.NewGuid();
    }
}
