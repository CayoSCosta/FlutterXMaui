using Microsoft.Extensions.Logging;
using TodoList.Services;
using TodoList.ViewModels;
using TodoList.Views;

namespace TodoList
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<CepService>();
            builder.Services.AddTransient<CepViewModel>();
            builder.Services.AddTransient<MainViewModel>();
            builder.Services.AddTransient<Arquivadas>();
            builder.Services.AddTransient<Concluidas>();


            return builder.Build();
        }
    }
}
