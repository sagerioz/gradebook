using System;
using Xunit;

namespace GradeBook.Tests
{
    public delegate string WriteLogDelegate(string logMessage);
    // Delegates can invoke multiple methods, they are "multicast"
    public class TypeTests
    {
      [Fact]
      public void WriteLogDelegateCanPointToMethod()
      {
        WriteLogDelegate log;
        // log = new WriteLogDelegate(ReturnMessage); <= this is longhand for line #14
        log = ReturnMessage;
        var result = log("Hello!");
        Assert.Equal("Hello!", result);
      }
      
      string ReturnMessage(string message)
      {
        return message;
      }
      
      
       [Fact]
       public void GradeIsWithinBounds()
       // value types are immutable. Strings behave like value types.
       {
         var book = GetBook("testingGrades");
         SetGrade(book, 105);

        // Assert.Null(book.grades);

       }

        private void SetGrade(InMemoryBook book, int grade)
        {
            book.AddGrade(grade);
        }

       [Fact]
       public void StringsBehaveLikeValueTypes()
       // value types are immutable. Strings behave like value types.
       {
         string name  = "Erica";
         var upper = MakeUpperCase(name);

         Assert.Equal("Erica", name);
         Assert.Equal("ERICA", upper);

       }

        private string MakeUpperCase(string parameter)
        {
            return parameter.ToUpper();
        }

        [Fact]
        public void Test1()
        {
            int v = GetInt();
            int v1 = SetInt(v);
            int z = v1;
                     
          Assert.Equal(3, v);
          Assert.Equal(44, v1);
                  
        }

        private int SetInt( int g)
        {
           return  g = 44;
        }

        private int GetInt()
        {
            return 3;
        }

        [Fact]
        public void CSharpCanPassByRef()
        { 
          var book1 = GetBook("Book 1");
          var book2 = book1;
          var book3 = book2;
          GetBookSetName(ref book1, "New name");
          GetBookSetName(ref book2, "New name for book");
          GetBookSetName(ref book3, "All have changed");
         
          Assert.Equal("ALL HAVE CHANGED", book1.Name);
          Assert.Equal("ALL HAVE CHANGED", book2.Name);
          Assert.Equal("ALL HAVE CHANGED", book2.Name);
         
        }
        [Fact]
        public void CSharpCanPassByValue()
        { 
          var book1 = GetBook("Book 1");
          var book2 = book1;
          GetBookSetName(ref book1, "New Name");
          GetBookSetName(ref book2, "New Name for Book");

          Assert.Equal("NEW NAME FOR BOOK", book1.Name);
          Assert.Equal("NEW NAME FOR BOOK", book2.Name);
         
        }
          private void GetBookSetName(ref InMemoryBook book, string name)
          {
            book.Name = name;
          }

        [Fact] // this is an attribute
        public void GetBookReturnsDifferentObjects() // method
        {
          var book1 = GetBook("Book 1");
          var book2 = GetBook("Book 2");

          Assert.Equal("BOOK 1", book1.Name);
          Assert.Equal("BOOK 2", book2.Name);
          Assert.NotSame(book1 , book2);
        }

        [Fact] // this is an attribute
        public void TwoVarsCanReferenceSameObject() // method
        {
          var book1 = GetBook("Book 1");
          var book2 = book1;

          Assert.Same(book1, book2);
          Assert.True(Object.ReferenceEquals(book1, book2));

          
        }

        InMemoryBook GetBook(string name) // by default this is private. "Book" is the return type.
        {
            return new InMemoryBook(name);
        }
    }
}
