using System;
using Microsoft.Maui.Controls;

namespace pw2;

public partial class UserInfo : ContentPage, IQueryAttributable
{
    private string entername { get; set; }
    private string enterusername{ get; set; }
    private string enteremail { get; set;}
    private string enterpassword { get; set; }
    private string enterconfirmpassword { get; set; }
    private int operations { get; set; }
    private string inputcalc { get; set;}
    private string conversiontype { get; set;}
    private string outputcalc { get; set;}

    private string actualuser;
    public UserInfo()
    {
        InitializeComponent();
    }
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("username"))
        {
            this.actualuser = query["username"].ToString();
            OnAppearing();
        }
    }
    protected async override void OnAppearing()
    {
        base.OnAppearing();
        string path = "files/user.csv";
        string operationsPath = "files/operations.csv";

        try
        {
            if (File.Exists(path))
            {
                foreach (string line in File.ReadAllLines(path))
                {
                    string[] part = line.Split(';');
                    if (part[1] == actualuser) //read the info of the actual user and display it 
                    {
                        showname.Text = "Name: " + part[0];
                        showusername.Text = "Username: " + part[1];
                        showemail.Text = "Email: " + part[2];
                        showpw.Text = "Password: " + part[3];
                        numop.Text = "Number of operations: " + part[5];

                        try
                        {
                            if (File.Exists(operationsPath))
                            {
                                foreach (string lineFile in File.ReadAllLines(operationsPath))
                                {
                                    string[] parts = lineFile.Split(';');
                                    if (parts[0] == actualuser)
                                    {
                                        showinput.Text = "Input: " + parts[1];
                                        showconversion.Text = "Conversion Type: " + parts[2];
                                        showoutput.Text = "Output: " + parts[3];
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            await DisplayAlert("Error", "This username is not registerd", "OK");
                            return;
                        }
                    }
                }
            }
        }
        catch
        {
            await DisplayAlert("Error", "This username is not registerd", "OK");
            return;
        }
    }

    //when the user click 'log out' the user is eliminated and get taken to the main page again
    private async void clicked_logout(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///MainPage");
    }

    //when the conversor is clicked, it takes the user back to the calculator
    private async void clicked_con(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(Conversor)}?username={this.actualuser}");
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
}