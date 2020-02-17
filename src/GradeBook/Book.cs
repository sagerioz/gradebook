using System;
using System.IO;
using System.Collections.Generic;

namespace GradeBook
{
    // Each class is in a different dedicated file named as such ~ Book.cs contains the class "Book"
    // Each class has both STATE and the BEHAVIOR which typically acts on the state data. Like Properties and methods.

    // convention is only one type per file. This delegate should typically be in it's own file:
    // this DELGATE is designed to define an event as a part of the Book class:
    public delegate void GradeAddedDelegate(object sender, EventArgs args);

    
    public class NamedObject
    {
        public NamedObject(string name)
        {
            Name = name;
        }

        public string Name
        {
            get;
            set;
        }
    }
    // this interface shows everyone what is going on with this object.
    // interfaces are more common than abstract classes.
    // convention is the name of an interface will start with an "I".

    public interface IBook
    {
      void AddGrade(double grade);
      Statistics GetStatistics();
      string Name { get; }
      event GradeAddedDelegate GradeAdded;
    }
    public abstract class Book : NamedObject, IBook
    {
        // this constructor takes the name parameter and forwards it to the NamedObject constructor
        public Book(string name) : base(name) 
        {
        }

        public abstract event GradeAddedDelegate GradeAdded;

        public abstract void AddGrade(double grade);

        public abstract Statistics GetStatistics();
       
    };
    // inside of an abstract class, you can have an abstract method.
    // Using abstract classes you can now start more types of books,
    // i.e. a book that is stored in memory, or one that's stored in a
    // file somewhere.
    public class DiskBook : Book
    {
        public DiskBook(string name) : base(name)
        {
        }

        public override event GradeAddedDelegate GradeAdded;
        public void AddGrade(char letter){
            switch(letter)
            {
                case 'A':
                    AddGrade(190);
                    break;
                case 'B':
                    AddGrade(180);
                    break;
                case 'C':
                    AddGrade(170);
                    break;
                case 'D':
                    AddGrade(160);
                    break;
                default:
                    AddGrade(120);
                    break;
            }
        }

        public override void AddGrade(double grade)
        {
            // throw new NotImplementedException();
            // this using statement essentially makes sure the code gets cleaned up
            // via a try/finally statement that will guarantee to use the dispose() method
            // so the file will remain open for writing to
  
            using(var write = File.AppendText($"{Name}.txt"))
            {
                write.WriteLine(grade);
                if(GradeAdded != null)
                {
                    GradeAdded(this, new EventArgs());
                }
            } 
             
            if(grade > 100 || grade < 0)
            {
                System.Console.WriteLine($"{grade} is not within range (0-100)");
                return;
            }
            else
            {
             System.Console.WriteLine($"{Name} was updated with a score of {grade}. ");
            }
               

        }

        public override Statistics GetStatistics()
        {
           var result = new Statistics();
           using(var reader = File.OpenText($"{Name}.txt"))
           {
               var line = reader.ReadLine();
               while(line != null)
               {
                   var number = double.Parse(line);
                   result.Add(number);
                   line  = reader.ReadLine();

               }
           }
           return result;
        }
    }
    public class InMemoryBook : Book
    {
        public InMemoryBook(string nameforbook) : base(nameforbook) 
        
        {
            // This is a constructor. does not have a return type.

            grades =  new List<double>(); // properly initialize 'grades' list
            Name = nameforbook; // 'this' keyword is always available on a member of the class
         
        }
        public void AddGrade(char letter){
            switch(letter)
            {
                case 'A':
                    AddGrade(190);
                    break;
                case 'B':
                    AddGrade(180);
                    break;
                case 'C':
                    AddGrade(170);
                    break;
                case 'D':
                    AddGrade(160);
                    break;
                default:
                    AddGrade(110);
                    break;
            }
        }

        // 'override' - will override whatever the base class is providing.
        // this is how you achieve polymorphism - overriding whatever my base class is providing.
        // Cannot override any method you inherit; only can override abstract and virtual methods.
        public override void AddGrade(double grade)
        {
            if(grade <= 100 && grade >= 0){
                grades.Add(grade);
                if(GradeAdded != null)
                {
                    GradeAdded(this, new EventArgs()); // <= new instance of the EventArgs class. Additional info comes through here
                    // with this object
                }
            }else{
                System.Console.WriteLine($"{grade} is not within range (0-100)");
                return;
            }
                System.Console.WriteLine($"{Name} was updated with a score of {grade}. Total number of grades in the system is {grades.Count}");

        }
        public override event GradeAddedDelegate GradeAdded; // <= this is a field that now exists on the Book class.
        // this code creates an event
        public override Statistics GetStatistics()
        {
            var result = new Statistics();
           
            foreach(var grade in grades)
            {
               result.Add(grade);
              
            }
                         
             
             
             
             return result;
        }

       
        // Instantiate this list, one way:
        // List<double> grades =  new List<double>();
        // How to add state to your class? Use a field definition, i.e. "grades"
        // this is a FIELD. Cannot use implicit typing here. No var keyword allowed.
        // Another way to instantiate this List - create a constructor method as seen at the top of this class.
        private List<double> grades; // FIELD

        //public string Name; <= FIELD - viewable member outside of the class - uppercase indicates a public member 
        // However, exposing thie piece of state to the outside world will allow anyone to set this field to a null value or an empty string.
        // Properties can restrict these unwanted entries. you still want to allow access to this state but
        // you want to control this access. See the property member 'Name' below to encapsulate STATE:


       // private string name; // <= the 'private' keyword controls and protects this state

        //public string Name
       // {
           // get // read the property
           // {
                //return name.ToUpper(); // <= this is your GETTER
           // }
          //  set // this writes a value into this property
           // {
              //  if(!String.IsNullOrEmpty(value))
              //  {
               // name = value; // <= this is your SETTER. There is always an implicit variable available in a setter
                // named 'value'.
                //}
               // else
               // {
                  //  throw new ArgumentException($"{nameof(value)} is not a valid name.");
               // }
           // }
       // }
         // How to effectively create an ummutable field or variable:
        // readonly string category = "Science";
            public const string CATEGORY = "Science"; // all-caps indicate that this is a CONSTANT
       
    }
}

// another way to write the code for defining the Name property, READ ONLY. Shorthand and most common:

//  public string Name 
//    {
        // get; <= this is public.
        // private set; <= this is private. state cannot be overwritten. One of the benefits of using a
                            // property over a field.
//    }
// =======================================================================================================

// TIP: Want to rename/refactor a class? 
// RIGHT click on the symbol, ie, 'Book', choose 
// 'rename symbol'.

