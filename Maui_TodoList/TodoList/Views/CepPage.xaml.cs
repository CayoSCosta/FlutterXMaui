using TodoList.ViewModels;

namespace TodoList.Views;

public partial class CepPage : ContentPage
{
	public CepPage(CepViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}