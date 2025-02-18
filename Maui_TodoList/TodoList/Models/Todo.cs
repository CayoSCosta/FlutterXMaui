namespace TodoList.Models;

public class Todo
{
    public string Description { get; set; }
    public bool IsDone { get; set; }

    public Todo(string description)
    {
        Description = description;
        IsDone = false;
    }
}
