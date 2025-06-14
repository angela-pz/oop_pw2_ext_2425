using System;
using Microsoft.Maui.Controls;

namespace pw2;

public partial class UserInfo : ContentPage, IQueryAttributable
{
    public string entername { get; set; }
    public string enterusername{ get; set; }
    public string enteremail { get; set;}
    public string enterpassword { get; set; }
    public string enterconfirmpassword { get; set; }
    public int operations { get; set; }
    public string inputcalc { get; set;}
    public string conversiontype { get; set;}
    public string outputcalc { get; set;}

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
    protected override void OnAppearing()
    {
        base.OnAppearing();
        string path = "files/user.csv";
        string operationsPath = "files/operations.csv";

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

                    if (File.Exists(operationsPath))
                    {
                        foreach (string lineFile in File.ReadAllLines(operationsPath))
                        {
                            //string[] parts = line.Split(';');

                            showinput.Text = "Input: " + part[1];
                            showconversion.Text = "Conversion Type: " + part[2];
                            showoutput.Text = "Output: " + part[3];
                        }
                    }
                }
            }
        }

/*
        if (File.Exists(operationsPath))
        {
            foreach (string line in File.ReadAllLines(operationsPath))
            {
                string[] part = line.Split(';');
                if (part[1] == actualuser) //read the info of the actual user and display it 
                {
                    showinput.Text = "Input: " + part[1];
                    showconversion.Text = "Conversion Type: " + part[2];
                    showoutput.Text = "Output: " + part[3];
                }
            }
        }
*/
    }

    //when the user click 'log out' the user is eliminated and get taken to the main page again
    private async void clicked_logout(object sender, EventArgs e)
    {
        string path = "files/user.csv";

        if (File.Exists(path))
        {
            string[] lines = File.ReadAllLines(path);
            bool found = false;

            for (int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(';');

                if (parts[1] == actualuser)
                {
                    parts[1] = ""; //modify the user to empty
                    lines[i] = string.Join(";", parts);

                    found = true;
                }
            }
            if (found)
            {
                File.WriteAllLines(path, lines);
                await DisplayAlert("User Eliminated !", "click 'ok' to get to the main page", "OK");
                await Navigation.PushAsync(new MainPage());
            }
        }
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

}