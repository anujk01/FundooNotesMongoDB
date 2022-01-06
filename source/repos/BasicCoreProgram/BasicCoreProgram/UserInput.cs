using System;
using System.Collections.Generic;
using System.Text;

namespace BasicCoreProgram
{
    class UserInput
    {
        public static void PrintTheName()
        {
            Console.WriteLine("Please enter the Name");

            string name = Console.ReadLine();

            int namelength = name.Length;

            if (namelength >= 3 && namelength < 5)
            {
                Console.WriteLine("The Length of string is " + namelength);

                Console.WriteLine("Hello " + name + " How are you?");

                Console.WriteLine("Hello {0} How are you?", name);

            }
            else if (namelength == 5)
            {
                Console.WriteLine("The name is having 5 characters");
            }
            else
            {
                Console.WriteLine("Please enter Valid Name");
            }
        }
    }
}