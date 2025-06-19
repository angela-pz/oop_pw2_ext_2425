using System.Text.RegularExpressions;

namespace pw2
{
    public class UserStore
    {
        private string name;
        private string username;
        private string email;
        private string password;
        private string confirmPassword;
        private int operations;
        private string inputvalue;
        private string conversiontype;
        private string outputcalc;

        public UserStore(string name, string username, string email, string password, string confirmPassword)
        {
            this.name = name;
            this.username = username;
            this.email = email;
            this.password = password;
            this.confirmPassword = confirmPassword;
            this.operations = 0; //start with 0 when you register a new user
            this.inputvalue = "";
            this.conversiontype = "";
            this.outputcalc = "";
        }

        //save the user into the file
        public void StoreUser(string path)
        {
            using (StreamWriter writer = new StreamWriter(path, append: true))
            {
                writer.WriteLine($"{this.name};{this.username};{this.email};{this.password};{this.confirmPassword};{this.operations}");
            }
        }

        public void StoreList(string operationsPath)
        {
            using (StreamWriter writer = new StreamWriter(operationsPath, append: true))
            {
                writer.WriteLine($"{this.inputvalue};{this.conversiontype};{this.outputcalc}");
            }
        }
    }
}