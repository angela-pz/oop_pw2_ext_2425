using System;
using System.Runtime.CompilerServices;
using System;

namespace pw2;

public partial class MainPage : ContentPage
{

	public MainPage()
	{
		InitializeComponent();
	}


	//when the exit is clicked, it closes the window
	private async void clicked_exit(object sender, EventArgs e)
	{
		bool answer = await DisplayAlert("Confirm Exit", "click 'yes' to exit", "Yes", "No");
		if (answer)
		{
			System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
		}
	}

	//when the log in button is clicked, it takes the user to the calculator page (if the user is already registered)
	private async void login_clicked(object sender, EventArgs e)
	{
		string path = "files/user.csv";
		if (string.IsNullOrEmpty(enterusername.Text) || string.IsNullOrEmpty(enterpassword.Text))
		{
			await DisplayAlert("Error", "enter an username and a password", "OK");
		}
		else
		{
			if (File.Exists(path))
			{
				foreach (string line in File.ReadAllLines(path))
				{
					string[] part = line.Split(';');
					if (part[1] == enterusername.Text && part[3] == enterpassword.Text)
					{
						await Shell.Current.GoToAsync($"{nameof(Conversor)}?username={enterusername.Text}");
					}
				}
			}
			else
			{
				await DisplayAlert("ERROR", "Not registered or it does not match", "OK");
			}
		}
	}

	//when the register text is clicked, it takes the user to the register page
	private async void clicked_register(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new Register());
	}

	//when the forgot password text is clicked, it taked the user to the forgot password page
	private async void clicked_fp(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new ForgotPassword());
	}
}
