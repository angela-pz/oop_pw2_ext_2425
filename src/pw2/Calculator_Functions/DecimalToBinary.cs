using System;

namespace pw2
{
    public class DecimalToBinary : Conversion
    {

        public DecimalToBinary(string name, string definition) : base(name, definition)
        {

        }

        public override string Change(string input) 
        {
            int number = Int32.Parse(input);

            if (number == 0) return "0";
            
            string binaryString = "";
            
            while (number > 0)
            {
                int remainder = number % 2;
                binaryString = remainder + binaryString;
                number /= 2;
            }
            
            return binaryString;
        }
    }
}