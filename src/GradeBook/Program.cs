using System;
using System.Collections.Generic;

namespace GradeBook
{
    class Program
    {
        static void Main(string[] args)
        {

            // book.AddGrade(args[0]);
            var input = Console.ReadLine();
            //book.AddGrade(99.9);


            if (args.Length > 0)
            {
                IBook book = new DiskBook(args[0].ToUpper());
                book.GradeAdded += OnGradeAdded;
                System.Console.WriteLine($"Your started a new gradebook named {book.Name}.");
                System.Console.WriteLine(input);
                EnterGrades(book, input);
                

            }
            else
            {
                try
                {
                    System.Console.WriteLine($"Name needed for a new book!");
                }
                catch (ArgumentException ex)
                {
                    System.Console.WriteLine(ex.Message);

                    System.Console.WriteLine($"Name needed for a new book!");
                }
                finally
                {
                    System.Console.WriteLine(":-)");
                }

            }

        }
        private static void EnterGrades(IBook book, string input)
        {

            do
            {  // System.Console.WriteLine(IBook.CATEGORY);

                System.Console.WriteLine($"Enter a new grade. Hit 'q' when done.");
                input = Console.ReadLine();
               // System.Console.WriteLine(input);
                if (input == "q" || input == "Q")
                {
                    var stats = book.GetStatistics();
                    Console.WriteLine($"The average grade is {stats.Average:N1}");
                    Console.WriteLine($"The highest grade is {stats.High:N1}");
                    Console.WriteLine($"The lowest grade is {stats.Low:N1}");
                    System.Console.WriteLine($"The letter grade is {stats.Letter}");
                    System.Console.WriteLine("You have exited.");
                    return;
                }
                if (input == "A" || input == "B" || input == "C" || input == "D" || input == "F")
                {
                    System.Console.WriteLine("You have entered a letter grade. This is now converted to its equivalent number.");
                    char character = char.Parse(input);
                    System.Console.WriteLine(character);
                    book.AddGrade(character);
                    //var stats = book.GetStatistics();
                    //System.Console.WriteLine(stats.High);
                    //System.Console.WriteLine(stats.Low);
                    continue;
                }
                try
                {
                    var grade = double.Parse(input);

                    if (grade <= 100 && grade >= 0)
                    {
                        book.AddGrade(grade);
                        //var stats = book.GetStatistics();
                        //System.Console.WriteLine(stats);
                        
                    }
                    else
                    {

                        throw new ArgumentException($"{nameof(grade)} is not within range (0-100)");
                    }
                }
                catch (ArgumentException ex)
                {

                    System.Console.WriteLine(ex.Message);
                }
                catch (FormatException ex)
                {
                    System.Console.WriteLine(ex.Message);
                }
                finally
                {
                    System.Console.WriteLine(":-)");
                }




            }
            while (true);



            //var book = new Book(input);
        }
        static void OnGradeAdded(object sender, EventArgs e)
        {
            System.Console.WriteLine("Your grade was added!");
        }
    }
}

// TIPS
// comment: select + ctrl + k + c , uncomment: select + ctrl + k + u
// place cursor on a member, field or method, press F12 => go to the metadata view on anything
// Everything inherits from System.Object => shortcut is 'object' with lowercase 'o'
// try to keep the 'Main' method as simple as possible (put code in other areas)
// Select code, click lightbul on the left, choose 'Extract Method' is one way to refactor (rearrange) code.
// dotnet analysis code and puts it into a new method.
// 'Abstract Base Class' - 
// 'interface' Class - a pure function, naming convention is to name with an uppercase 'I' example: IBook
// 'virtual' keyword - 