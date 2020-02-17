using System;
using Xunit;

namespace GradeBook.Tests
{
    public class BookTests
    {
        [Fact] // this is an attribute
        public void BookCalculatesAnAverageGrade() // method
        {
            // arrange
                var book = new InMemoryBook("unittest");
                book.AddGrade(25);
                book.AddGrade(10);
                book.AddGrade(65);
               
               
            // act
             var result = book.GetStatistics();   


                Assert.Equal(65, result.High);
                Assert.Equal(10, result.Low);
                Assert.Equal(33.33, result.Average);
                Assert.Equal('F', result.Letter);

        }
    }
}
