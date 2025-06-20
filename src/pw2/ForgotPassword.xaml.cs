using System;
using Microsoft.Maui.Controls;
using System.Text.RegularExpressions;

namespace pw2;

public partial class ForgotPassword : ContentPage
{
    public ForgotPassword()
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

    //validate password 
    public static bool PasswordValidation(string password)
    {
        var lower = new Regex(@"[a-z]+");
        var upper = new Regex(@"[A-Z]+");
        var num = new Regex(@"[0-9]+");
        var symbol = new Regex(@"[!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]+");

        if (password.Length < 8 || !lower.IsMatch(password) || !upper.IsMatch(password) || !num.IsMatch(password) || !symbol.IsMatch(password))
        {
            return false;
        }
        return true;
    }
    private async void changedPassword(object sender, EventArgs e)
    {
        string username = enterusername.Text?.Trim() ?? "";
        string password = enterpassword.Text?.Trim() ?? "";
        string confirmPassword = newconfirmpassword.Text?.Trim() ?? "";

        string path = "files/user.csv";

        //make sure all the sections arer filled out
        if (username == "" || password == "" || confirmPassword == "")
        {
            await DisplayAlert("Error", "Fill all the sections", "OK");
            return;
        }

        try
        {
            if (File.Exists(path))
            {
                string[] lines = File.ReadAllLines(path);
                bool found = false;

                for (int i = 0; i < lines.Length; i++)
                {
                    string[] parts = lines[i].Split(';');
                    if (parts[1] == enterusername.Text)
                    {
                        parts[3] = enterpassword.Text; //modify the password to the new one
                        lines[i] = string.Join(";", parts);
                        found = true;
                    }
                }

                if (!found)
                {
                    await DisplayAlert("Error", "This username is not registerd", "OK");
                    return;
                }
                else
                {
                    File.WriteAllLines(path, lines); //write the new password in the file
                }
            }
        }
        catch
        {
            await DisplayAlert("Error", "There is an error regarding the file", "OK");
            return;
        }

        //validation for the password
        if (!PasswordValidation(password))
        {
            await DisplayAlert("Error", "Make sure the password has at least 8 character, an upper letter, a lower letter, a number and a special character.", "OK");
            return;
        }

        //password and confirm password must be the same
        if (password != confirmPassword)
        {
            await DisplayAlert("Error", "Password must coincide", "OK");
            return;
        }

        //confirmed change and automatically takes it to the main page
        await DisplayAlert("Password changed :)", "click 'ok' to get to the main page", "Ok");
        await Shell.Current.GoToAsync("///MainPage");
    }
}
