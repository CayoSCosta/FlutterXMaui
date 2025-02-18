using TodoList.ViewModels;

namespace TodoList.Views;

public partial class Concluidas : ContentPage
{
    public Concluidas()
    {
        InitializeComponent();
        BindingContext = ((AppShell)App.Current.MainPage).BindingContext as MainViewModel;
    }
}
