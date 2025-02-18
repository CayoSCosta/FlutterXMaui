using TodoList.ViewModels;

namespace TodoList.Views;

public partial class MainPage : ContentPage
{
    private MainViewModel _viewModel = new();
    public MainPage()
    {
        InitializeComponent();
        BindingContext = ((AppShell)App.Current.MainPage).BindingContext as MainViewModel;
    }
    // Evento para concluir a tarefa
    private void OnArchiveTaskInvoked(object sender, EventArgs e)
    {
        if (sender is SwipeItem swipeItem)
        {
            if (swipeItem.BindingContext is TaskItem task)
            {
                var viewModel = BindingContext as MainViewModel;
                viewModel?.ArchiveTask(task);
            }
        }
    }


    private void OnCompleteTaskInvoked(object sender, EventArgs e)
    {
        if (sender is SwipeItem swipeItem)
        {
            if (swipeItem.BindingContext is TaskItem task)
            {
                var viewModel = BindingContext as MainViewModel;
                viewModel?.CompleteTask(task);
            }
        }
    }

}
