using FlashQuizz.Services;
using FlashQuizz.Views;
using FlashQuizz.ViewModels;
using Microsoft.Maui.Controls;
using Microsoft.Extensions.Logging;

namespace FlashQuizz
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
                })
                .RegisterServices()
                .RegisterViewModels()
                .RegisterViews(); 

            return builder.Build();
        }

        private static MauiAppBuilder RegisterServices(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<FlashCardDbContext>();
            builder.Services.AddSingleton<CardService>();
            return builder;
        }
        private static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
        {
            builder.Services.AddTransient<MainViewModel>();
            builder.Services.AddTransient<LearningViewModel>();
            builder.Services.AddTransient<SessionSummaryViewModel>();
            builder.Services.AddTransient<MyCardsViewModel>();
            return builder;
        }

        private static MauiAppBuilder RegisterViews(this MauiAppBuilder builder)
        {
            builder.Services.AddTransient<MainPage>();  //AddTransient means that a new instance is created on each request  ; ViewModels are registered as services that can be injected via the constructor
            builder.Services.AddTransient<MyCardsPage>();
            builder.Services.AddTransient<AddCardPage>();
            builder.Services.AddTransient<EditCardPage>();
            builder.Services.AddTransient<LearningPage>();
            builder.Services.AddTransient<SessionSummaryPage>();
            return builder;
        }
    }
}
/*MauiProgram.cs is the starting point of the .NET MAUI application configuration, where:

The application host is created and configured

All services, ViewModels and pages are registered

Fonts and other application settings are configured*/