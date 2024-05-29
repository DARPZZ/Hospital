using ZXing.Net.Maui.Controls;
using ZXing.Net.Maui;
using Hospital.Services;
namespace Hospital;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseBarcodeReader()
            .UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("FontAwesome6FreeBrands.otf", "FontAwesomeBrands");
				fonts.AddFont("FontAwesome6FreeRegular.otf", "FontAwesomeRegular");
				fonts.AddFont("FontAwesome6FreeSolid.otf", "FontAwesomeSolid");
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddSingleton<MainViewModel>();

		builder.Services.AddSingleton<MainPage>();

		builder.Services.AddSingleton<SignupViewModel>();

		builder.Services.AddSingleton<SignupPage>();

		builder.Services.AddSingleton<LoginnViewModel>();

		builder.Services.AddSingleton<LoginnPage>();

		builder.Services.AddSingleton<OpeningViewModel>();

		builder.Services.AddSingleton<OpeningPage>();

		builder.Services.AddSingleton<QrViewmodel>();

		builder.Services.AddSingleton<QrPage>();

        builder.Services.AddSingleton<DrawerService>();

        builder.Services.AddSingleton<ForgotPasswordPage>();

        builder.Services.AddSingleton<UserService>();

		builder.Services.AddSingleton<RegistrationForm>();

        builder.Services.AddSingleton<ForgotPasswordViewModel>();

		return builder.Build();
	}
}
