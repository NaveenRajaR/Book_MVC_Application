using Books.Data;
using Books.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Books.Controllers
{
    public class HomeController : Controller
    {
        private readonly BookDbContext _books;

        private List<BookModel> _booksobject;
        public HomeController(BookDbContext _books)
        {
            this._booksobject = new List<BookModel>{ 
                new BookModel{Publisher = "Oxford",Title = "Engineering",AuthorFirstName = "Mark",AuthorLastName = "Zuckerbeg",Price = 199,PublicationYear = 2022,CityOfPublication = "Newyork",Medium = "Print"},
                new BookModel{Publisher = "IIT",Title = "Machine Learnig",AuthorFirstName = "Elon",AuthorLastName = "Musk",Price = 349,PublicationYear = 2020,CityOfPublication = "Cigago",Medium = "Digital"},
                new BookModel{Publisher = "MIT",Title = "Artificial Intelligence",AuthorFirstName = "Avatar",AuthorLastName = "Raigh",Price = 599,PublicationYear = 2019,CityOfPublication = "Las vegas",Medium = "Print"}};
            this._books = _books;
        }

        public IActionResult Index()
        {
            return View(this._booksobject);
        }

        public IActionResult GetBooksSortedByPublisher()
        {
            List<BookModel> sortedBooks = this._booksobject.OrderBy(book => book.Publisher)
                                    .ThenBy(book => book.AuthorLastName)
                                    .ThenBy(book => book.AuthorFirstName)
                                    .ThenBy(book => book.Title)
                                    .ToList();
            return Ok(sortedBooks);
        }

        public IActionResult GetBooksSortedByAuthor()
        {
            List<BookModel> sortedBooks = this._booksobject.OrderBy(book => book.AuthorLastName)
                                    .ThenBy(book => book.AuthorFirstName)
                                    .ThenBy(book => book.Title)
                                    .ToList();
            return Ok(sortedBooks);
        }

        public IActionResult GetBooksSortedByPublisherSP()
        {
            //Query used to create StoredProcedure based on table.

            //CREATE PROCEDURE GetBooksSortedByPublisherSP
            //AS
            //BEGIN
            //SELECT*
            //FROM Books
            //ORDER BY Publisher, AuthorLastName, AuthorFirstName, Title;
            //END

            try
            {
                List<BookModel> sortedBooks = this.GetSortedBooks("GetBooksSortedByPublisherSP");
                return Ok(sortedBooks);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IActionResult GetBooksSortedByAuthorSP()
        {
            //Query used to create StoredProcedure based on table.

            //CREATE PROCEDURE GetBooksSortedByAuthorSP
            //AS
            //BEGIN
            //SELECT*
            //FROM Books
            //ORDER BY AuthorLastName, AuthorFirstName, Title;
            //END

            try
            {
                List<BookModel> sortedBooks = this.GetSortedBooks("GetBooksSortedByAuthorSP");
                return Ok(sortedBooks);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IActionResult GetTotalPrice()
        {
            var totalPrice = this._booksobject.Sum(book => book.Price);
            return Ok(new { Value = "The total price of books is :" + totalPrice });
        }


        [HttpPost]
        public async Task<IActionResult> BulkInsertBooks(string Book)
        {
            try
            {
                List<BookModel> bookList = new List<BookModel>();
                List<BookModel>? sortedBooks = !string.IsNullOrEmpty(Book) ? JsonConvert.DeserializeObject<List<BookModel>>(Book) : this._booksobject;
                if (sortedBooks != null)
                {
                    foreach(var book in sortedBooks)
                    {
                        var existingBook = await _books.books.FirstOrDefaultAsync(b => b.Title == book.Title && b.AuthorFirstName == book.AuthorFirstName && b.AuthorLastName == book.AuthorLastName);
                        if(existingBook == null)
                        {
                            bookList.Add(book);
                        }

                    }
                    if(bookList.Count > 0)
                    {
                        this._books.AddRange(bookList);
                        await _books.SaveChangesAsync();
                        return Ok(new { Value = "Successfully inserted books" });
                    }
                    else
                    {
                        return Ok(new { Value = "Books exists in DB." });
                    }
                }
                else
                {
                    return BadRequest("Empty Data");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        private List<BookModel> GetSortedBooks(string sortingOption)
        {
            if (_books == null || string.IsNullOrEmpty(sortingOption))
            {
                throw new ArgumentException("The book object was empty.", nameof(GetSortedBooks));
            }

            try
            {
                return _books.books.FromSqlRaw($"EXEC dbo.{sortingOption}").ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching sorted books.", ex);
            }

        }
    }
}