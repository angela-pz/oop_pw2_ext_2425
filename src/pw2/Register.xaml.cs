using System;
using System.Linq;
using Microsoft.Maui.Controls;
using System.Text.RegularExpressions;
//

namespace pw2;

public partial class Register : ContentPage
{
    public Register()
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

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        string path = "files/user.csv";

        string name = entername.Text?.Trim() ?? "";
        string username = enterusername.Text?.Trim() ?? "";
        string email = enteremail.Text?.Trim() ?? "";
        string password = enterpassword.Text?.Trim() ?? "";
        string confirmPassword = enterconfirmpassword.Text?.Trim() ?? "";

        //fill all the fields
        if (name == "" || username == "" || email == "" || password == "" || confirmPassword == "")
        {
            await DisplayAlert("Error", "Fill all the areas", "OK");
            return;
        }

        //name and username must be diffrent
        if (name == username)
        {
            await DisplayAlert("Error", "Name and username must be diffrent", "OK");
            return;
        }

        //check that the user name is not already registered
        if (File.Exists(path))
        {
            foreach (string line in File.ReadAllLines(path))
            {
                string[] part = line.Split(';');
                if (part[1] == enterusername.Text)
                {
                    await DisplayAlert("Error", "username already exists", "OK");
                    return;
                }
            }
        }

        //the email must contain @
        if (!email.Contains("@"))
        {
            await DisplayAlert("Error", "make sure the email contains a '@'", "OK");
            return;
        }

        //password must follow all the validations
        if (!PasswordValidation(password))
        {
            await DisplayAlert("Error", "make sure the password has at least 8 character, an upper letter, a lower letter, a number and a special character.", "OK");
            return;
        }

        //password and confirm password must coincide
        if (password != confirmPassword)
        {
            await DisplayAlert("Error", "Password must coincide", "OK");
            return;
        }

        //check the policy box
        if (!acceptpolicy.IsChecked)
        {
            await DisplayAlert("Accept the policy", "To continue you must accept the policy", "OK");
            return;
        }

        //creates new user and takes back to the main log in page
        UserStore user = new UserStore(entername.Text, enterusername.Text, enteremail.Text, enterpassword.Text, enterconfirmpassword.Text);
        user.StoreUser(path);
        await DisplayAlert("New user created :)", "click 'ok' to get to the main page", "OK");
        await Shell.Current.GoToAsync(nameof(MainPage));

    }

}
