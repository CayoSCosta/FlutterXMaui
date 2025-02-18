namespace TodoList.ViewModels;

public class TaskItem
{
    public string Title { get; set; }
    public bool IsArchived { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsCompleted { get; set; }
}
