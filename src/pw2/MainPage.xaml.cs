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
			//System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
			Environment.Exit(0);
		}
	}

	private bool Validcredentials(string username, string password)
	{
		string path = "files/user.csv";

		try
		{
			if (!File.Exists(path))
			{
				return false;
			}
			foreach (string line in File.ReadAllLines(path))
			{
				string[] part = line.Split(';');
				if (part[1] == enterusername.Text && part[3] == enterpassword.Text)
				{
					return true;
				}
			}
		}
		catch (Exception ex)
		{
			return false;
		}
		return false;
	}

		/*
		if (!File.Exists(path))
		{
			return false;
		}
		foreach (string line in File.ReadAllLines(path))
		{
			string[] part = line.Split(';');
			if (part[1] == enterusername.Text && part[3] == enterpassword.Text)
			{
				return true;
			}
		}
		return false;
		*/
	

	private async void login_clicked(object sender, EventArgs e)
	{
		string username = enterusername.Text;
		string password = enterpassword.Text;

		if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
		{
			await DisplayAlert("Error", "enter an username and a password", "OK");
			return;
		}

		bool valid = Validcredentials(username, password);

		if (valid)
		{
			await Shell.Current.GoToAsync($"{nameof(Conversor)}?username={enterusername.Text}");
		}
		else
		{
			await DisplayAlert("Error", "enter an username and a password", "OK");			
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
