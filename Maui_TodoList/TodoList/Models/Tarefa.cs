namespace TodoList.Models;

public class Tarefa
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string SwipeViewId => $"SwipeView_{Id}";

    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public DateTime DataCriacao { get; set; }
    public bool Arquivada { get; set; }
    public bool Excluida { get; set; }
}

