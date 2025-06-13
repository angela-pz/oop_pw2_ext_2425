using System;

namespace pw2
{
    public abstract class Conversion
    {
        protected string name;
        protected string defintion;

        public Conversion(string name, string definition)
        {
            this.name = name;
            this.defintion = definition;
        }

        public void PrintConversion(string input, string output) 
        {
            Console.Clear();
            Console.WriteLine($"{this.name} representation of {input} is {output}");
            Console.ReadLine();
        }

        public abstract string Change(string input);

        public string GetName()
        {
            return this.name;
        }

        public string GetDefinition() 
        {
            return this.defintion;
        }
    }
}