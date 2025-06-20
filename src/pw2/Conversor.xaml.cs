using System;
using Microsoft.Maui.Controls;

namespace pw2;

public partial class Conversor : ContentPage, IQueryAttributable
{
    private List<Conversion> conversions;
    private string actualuser;
    private string inputvalue;
    private string conversiontype;
    private string outputcalc;

    public Conversor()
    {
        InitializeComponent();

        this.conversions = new List<Conversion>();

        //conversion
        this.conversions.Add(new DecimalToBinary("Binary", "Decimal to Binary"));
        this.conversions.Add(new DecimalToOctal("Octal", "Decimal to Octal"));
        this.conversions.Add(new DecimalToHexadecimal("Hexadecimal", "Decimal to Hexadecimal"));
        this.conversions.Add(new DecimalToTwoComplement("TwoComplement", "Decimal to Binary (TwoComplement)"));
        this.conversions.Add(new BinaryToDecimal("Decimal", "Binary to Decimal"));
        this.conversions.Add(new TwoComplementToDecimal("Decimal", "Binary (TwoComplement) to Decimal"));
        this.conversions.Add(new OctalToDecimal("Decimal", "Octal to Decimal"));
        this.conversions.Add(new HexadecimalToDecimal("Decimal", "Hexadecimal to Decimal"));

    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("username"))
        {
            this.actualuser = query["username"].ToString();
        }
    }

    //increments the number of operations 
    private async void SumOperations()
    {
        string path = "files/user.csv";

        try
        {
            if (File.Exists(path))
            {
                string[] lines = File.ReadAllLines(path);
                bool found = false;

                for (int i = 0; i < lines.Length; i++)
                {
                    string[] parts = lines[i].Split(';');
                    if (parts[1] == this.actualuser)
                    {
                        //sums the number of operations
                        int numoperations = Convert.ToInt32(parts[5]);
                        numoperations++;
                        parts[5] = numoperations.ToString(); //modifies the value in the file
                        lines[i] = string.Join(";", parts);
                        found = true;
                    }
                }

                if (found)
                {
                    File.WriteAllLines(path, lines); //writes it in the file
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "There is an error regarding the file", "OK");
            return;
        }
    }
    

    private async void ListInfo(string inputvalue, string conversionType, string outputcalc)
    {
        //creates a new line in the file and adds it
        string operationsPath = "files/operations.csv";
        string operationLineFile = $"{actualuser};{inputvalue};{conversionType};{outputcalc}";

        try
        {
            File.AppendAllText(operationsPath, operationLineFile + Environment.NewLine);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "There is an error regarding the file", "OK");
            return;
        }
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

    //when the user clicks 'user info' it takes the user to the user info page
    private async void clicked_ui(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(UserInfo)}?username={this.actualuser}");
    }

    //when the user click 'log out' the user is eliminated and get taken to the main page again
    private async void clicked_logout(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///MainPage");
    }

    //shows the digits pressed
    private void OnDigitButtonClicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        string text = button.Text;
        enterinput.Text += text;
    }

    //shows the hexadecimal letters pressed
    private void OnHexDigitButtonClicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        string hexChar = button.Text.ToUpper();
        enterinput.Text += hexChar;
    }

    //clear the display
    private void OnClearButtonClicked(object sender, EventArgs e)
    {
        enterinput.Text = string.Empty;
    }

    //change de sign of the number displayed
    private void OnSignButtonClicked(object sender, EventArgs e)
    {
        double.TryParse(enterinput.Text, out double currentNumber);
        currentNumber = -currentNumber;
        enterinput.Text = currentNumber.ToString();
    }

    //the validation functions from my activity workouts did not work perfectly so i decided to implement the validation individually

    //DECIMAL TO BINARY
    private async void DecimalToBinaryClicked(object sender, EventArgs e)
    {
        //validation to check if there has been data inputed to convert
        if (!string.IsNullOrEmpty(enterinput.Text))
        {
            //validation to check that only decimal numbers are inputed
            if (int.TryParse(enterinput.Text, out int decimalval))
            {
                inputvalue = enterinput.Text;
                DecimalToBinary converter = new DecimalToBinary("Binary", "Decimal to Binary");
                string numbinary = converter.Change(enterinput.Text);
                enterinput.Text = numbinary;

                conversiontype = "DecimalToBinary";
                outputcalc = enterinput.Text;
                SumOperations();
                ListInfo(inputvalue, conversiontype, outputcalc);
            }
            else
            {
                await DisplayAlert("Error", "Enter the correct data", "ok");
                enterinput.Text = string.Empty;
                return;
            }
        }
        else
        {
            await DisplayAlert("Error", "Enter a value to convert", "ok");
            return;
        }
    }

    //DECIMAL TO TWOS COMPLEMENT
    private async void DecimalToTwoComplementClicked(object sender, EventArgs e)
    {
        //validation to check if there has been data inputed to convert
        if (!string.IsNullOrEmpty(enterinput.Text))
        {
            //validation to check that only decimal numbers are inputed
            if (int.TryParse(enterinput.Text, out int decimalval))
            {
                inputvalue = enterinput.Text;
                DecimalToTwoComplement converter = new DecimalToTwoComplement("TwoComplement", "Decimal to Binary (TwoComplement)");
                string numtwocom = converter.Change(enterinput.Text);
                enterinput.Text = numtwocom;

                conversiontype = "DecimalToTwoComplement";
                outputcalc = numtwocom;
                SumOperations();
                ListInfo(inputvalue, conversiontype, outputcalc);
            }
            else
            {
                await DisplayAlert("Error", "Enter the correct data", "ok");
                enterinput.Text = string.Empty;
                return;
            }
        }
        else
        {
            await DisplayAlert("Error", "Enter a value to convert", "ok");
            return;
        }
    }

    //DECIMAL TO OCTAL 
    private async void DecimalToOctalClicked(object sender, EventArgs e)
    {
        //validation to check if there has been data inputed to convert
        if (!string.IsNullOrEmpty(enterinput.Text))
        {
            //validation to check that only decimal numbers are inputed
            if (int.TryParse(enterinput.Text, out int decimalval))
            {
                inputvalue = enterinput.Text;
                DecimalToOctal converter = new DecimalToOctal("Octal", "Decimal to Octal");
                string octalnum = converter.Change(enterinput.Text);
                enterinput.Text = octalnum;

                conversiontype = "DecimalToOctal";
                outputcalc = octalnum;
                SumOperations();
                ListInfo(inputvalue, conversiontype, outputcalc);
            }
            else
            {
                await DisplayAlert("Error", "Enter the correct data", "ok");
                enterinput.Text = string.Empty;
                return;
            }
        }
        else
        {
            await DisplayAlert("Error", "Enter a value to convert", "ok");
            return;
        }
    }
    
    //DECIMAL TO HEXADECIMAL
    private async void DecimalToHexadecimalClicked(object sender, EventArgs e)
    {
        //validation to check if there has been data inputed to convert
        if (!string.IsNullOrEmpty(enterinput.Text))
        {
            //validation to check that only decimal numbers are inputed
            if (int.TryParse(enterinput.Text, out int decimalval))
            {
                inputvalue = enterinput.Text;
                DecimalToHexadecimal converter = new DecimalToHexadecimal("Hexadecimal", "Decimal to Hexadecimal");
                string hexnum = converter.Change(enterinput.Text);
                enterinput.Text = hexnum;

                conversiontype = "DecimalToHexadecimal";
                outputcalc = hexnum;
                SumOperations();
                ListInfo(inputvalue, conversiontype, outputcalc);
            }
            else
            {
                await DisplayAlert("Error", "Enter the correct data", "ok");
                enterinput.Text = string.Empty;
                return;
            }
        }
        else
        {
            await DisplayAlert("Error", "Enter a value to convert", "ok");
            return;
        }
    }

    //BINARY TO DECIMAL
    private async void BinaryToDecimalClicked(object sender, EventArgs e)
    {
        //validation to check if there has been data inputed to convert
        if (!string.IsNullOrEmpty(enterinput.Text))
        {
            string bin = enterinput.Text.Trim();

            //validation to check that only binary numbers are inputed
            if (bin.All(c => c == '0' || c == '1'))
            {
                inputvalue = enterinput.Text;
                BinaryToDecimal converter = new BinaryToDecimal("Decimal", "Binary to Decimal");
                string decnum = converter.Change(enterinput.Text);
                enterinput.Text = decnum;

                conversiontype = "BinaryToDecimal";
                outputcalc = decnum;
                SumOperations();
                ListInfo(inputvalue, conversiontype, outputcalc);
            }
            else
            {
                await DisplayAlert("Error", "Enter the correct data", "ok");
                enterinput.Text = string.Empty;
                return;
            }
        }
        else
        {
            await DisplayAlert("Error", "Enter a value to convert", "ok");
            return;
        }
    }
    
    //TWOS COMPLEMENT TO DECIMAL
    private async void TwoComplementToDecimalClicked(object sender, EventArgs e)
    {
        //validation to check if there has been data inputed to convert
        if (!string.IsNullOrEmpty(enterinput.Text))
        {
            string bin = enterinput.Text.Trim();

            //validation to check that only twos compliment (binary) numbers are inputed
            if (bin.All(c => c == '0' || c == '1'))
            {
                inputvalue = enterinput.Text;
                TwoComplementToDecimal converter = new TwoComplementToDecimal("Decimal", "Binary (TwoComplement) to Decimal");
                string decnum = converter.Change(enterinput.Text);
                enterinput.Text = decnum;
                
                conversiontype = "TwoComplementToDecimal";
                outputcalc = decnum;
                SumOperations();
                ListInfo(inputvalue, conversiontype, outputcalc);
            }
            else
            {
                await DisplayAlert("Error", "Enter the correct data", "ok");
                enterinput.Text = string.Empty;
                return;
            }
        }
        else
        {
            await DisplayAlert("Error", "Enter a value to convert", "ok");
            return;
        }
    }
    
    //OCTAL TO DECIMAL
    private async void OctalToDecimalClicked(object sender, EventArgs e)
    {
        //validation to check if there has been data inputed to convert
        if (!string.IsNullOrEmpty(enterinput.Text))
        {
            //validation to check that only octal numbers are inputed
            if (int.TryParse(enterinput.Text, out int decimalval))
            {
                inputvalue = enterinput.Text;
                OctalToDecimal converter = new OctalToDecimal("Decimal", "Octal to Decimal");
                string decnum = converter.Change(enterinput.Text);
                enterinput.Text = decnum;

                conversiontype = "OctalToDecimal";
                outputcalc = decnum;
                SumOperations();
                ListInfo(inputvalue, conversiontype, outputcalc);
            }
            else
            {
                await DisplayAlert("Error", "Enter the correct data", "ok");
                enterinput.Text = string.Empty;
                return;
            }
        }
        else
        {
            await DisplayAlert("Error", "Enter a value to convert", "ok");
            return;
        }
    }
    
    //HEXADECIMAL TO DECIMAL
    private async void HexadecimalToDecimalClicked(object sender, EventArgs e)
    {
        //validation to check if there has been data inputed to convert
        if (!string.IsNullOrEmpty(enterinput.Text))
        {
            inputvalue = enterinput.Text;
            HexadecimalToDecimal converter = new HexadecimalToDecimal("Decimal", "Hexadecimal to Decimal");
            string decnum = converter.Change(enterinput.Text);
            enterinput.Text = decnum;

            conversiontype = "HexadecimalToDecimal";
            outputcalc = decnum;
            SumOperations();
            ListInfo(inputvalue, conversiontype, outputcalc);
        }
        else
        {
            await DisplayAlert("Error", "Enter a value to convert", "ok");
            return;
        }
    }

}

