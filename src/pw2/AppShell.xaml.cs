namespace pw2;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
		Routing.RegisterRoute(nameof(Register), typeof(Register));
		Routing.RegisterRoute(nameof(Conversor), typeof(Conversor));
		Routing.RegisterRoute(nameof(ForgotPassword), typeof(ForgotPassword));
		Routing.RegisterRoute(nameof(UserInfo), typeof(UserInfo));
	}
}
