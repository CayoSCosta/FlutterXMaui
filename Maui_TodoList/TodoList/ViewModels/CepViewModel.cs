using System.ComponentModel;
using System.Windows.Input;
using TodoList.Services;

namespace TodoList.ViewModels
{
    public class CepViewModel : INotifyPropertyChanged
    {
        private readonly CepService _cepService;

        private string _cep;
        private string _resultado;

        public CepViewModel(CepService cepService)
        {
            _cepService = cepService;
            BuscarCepCommand = new Command(async () => await BuscarCep());
        }

        public string Cep
        {
            get => _cep;
            set
            {
                _cep = value;
                OnPropertyChanged(nameof(Cep));
            }
        }

        public string Resultado
        {
            get => _resultado;
            set
            {
                _resultado = value;
                OnPropertyChanged(nameof(Resultado));
            }
        }

        public ICommand BuscarCepCommand { get; }

        public CepViewModel()
        {
            _cepService = new CepService();
            BuscarCepCommand = new Command(async () => await BuscarCep());
        }

        private async Task BuscarCep()
        {
            if (string.IsNullOrWhiteSpace(Cep) || Cep.Length != 8)
            {
                Resultado = "CEP inválido";
                return;
            }

            var response = await _cepService.BuscarCepAsync(Cep);
            Resultado = $"{response.Logradouro}, {response.Bairro}, {response.Localidade} - {response.Uf}";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
