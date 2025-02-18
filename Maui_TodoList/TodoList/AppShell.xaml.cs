using Microsoft.Maui.Controls;
using TodoList.ViewModels;
using TodoList.Views;

namespace TodoList;

public partial class AppShell : Shell
{
    private MainViewModel _mainViewModel = new MainViewModel();
    public AppShell()
    {
        InitializeComponent();
        BindingContext = _mainViewModel;

        //// Passando o ViewModel para todas as páginas
        //Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        //Routing.RegisterRoute(nameof(Arquivadas), typeof(Arquivadas));
        //Routing.RegisterRoute(nameof(Concluidas), typeof(Concluidas));
    }
}
