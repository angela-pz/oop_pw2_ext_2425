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
			Environment.Exit(0);
		}
	}

	private async void login_clicked(object sender, EventArgs e)
	{
		string path = "files/user.csv";
		string username = enterusername.Text;
		string password = enterpassword.Text;
		bool valid = false;

		if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
		{
			await DisplayAlert("Error", "enter an username and a password", "OK");
			return;
		}
		try
		{
			if (!File.Exists(path))
			{
				await DisplayAlert("Error", "File does not exist", "OK");
				return;
			}
			foreach (string line in File.ReadAllLines(path))
			{
				string[] part = line.Split(';');
				if (part[1] == enterusername.Text && part[3] == enterpassword.Text)
				{
					await Shell.Current.GoToAsync($"{nameof(Conversor)}?username={enterusername.Text}");
				}
			}
		}
		catch
		{
			await DisplayAlert("Error", "Error related to the file", "OK");
		}
	}


	//when the register text is clicked, it takes the user to the register page
	private async void clicked_register(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync(nameof(Register));
	}

	//when the forgot password text is clicked, it taked the user to the forgot password page
	private async void clicked_fp(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync(nameof(ForgotPassword));
	}
}
