using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor.Pages.BookList
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }
           
        //return a list or IEnumerable of book
        public IEnumerable<Book> Books { get; set; }
        public async Task OnGet()
        {
            //we will assign this Books, all of the books from the database
            // we are retriving all of the books
            //Async will basically let you run multiple tasks at a time, until it is awaited 
            Books = await _db.Book.ToListAsync(); //we need to await because we need to assign all the books that we found
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            //ne book ruji krejt librat ne baze te id-se
            var book = await _db.Book.FindAsync(id);

            //nese nuk ekziston libri
            if(book == null)
            {
                return NotFound();
            }
            _db.Book.Remove(book);
            _db.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}
