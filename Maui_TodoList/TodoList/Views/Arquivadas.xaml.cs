using TodoList.ViewModels;

namespace TodoList.Views;

public partial class Arquivadas : ContentPage
{
    public Arquivadas()
    {
        InitializeComponent();
        BindingContext = ((AppShell)App.Current.MainPage).BindingContext as MainViewModel;
    }
}
