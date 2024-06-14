using BookClub.API.Data;
using BookClub.API.Models.Domain;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookClub.API.Repository
{
    public class BooksRepository : IBooksRepository
    {
        private readonly BookClubDbContext _bookClubDbContext;
        public BooksRepository(BookClubDbContext bookClubDbContext)
        {
            _bookClubDbContext = bookClubDbContext;
        }

        public async Task<Book> AddBookAsync(Book book)
        {
            await _bookClubDbContext.Books.AddAsync(book);
            await _bookClubDbContext.SaveChangesAsync();

            return book;
        }

        public async Task<Book?> DeleteBookAsync(Guid id)
        {
            var deleteBook = await _bookClubDbContext.Books.FirstOrDefaultAsync(x => x.BookId == id);

            if (deleteBook == null)
            {
                return null;
            }

            _bookClubDbContext.Books.Remove(deleteBook);
            await _bookClubDbContext.SaveChangesAsync();

            return deleteBook;
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            return await _bookClubDbContext.Books.Include("Category").ToListAsync();
        }

        public async Task<Book> GetBookByIdAsync(Guid id)
        {
            return await _bookClubDbContext.Books.Include("Category").FirstOrDefaultAsync(x => x.BookId == id);
        }

        public async Task<Book?> UpdateBookAsync(Guid id, Book book)
        {
            var updatedBook = await _bookClubDbContext.Books.FirstOrDefaultAsync(x => x.BookId == id);

            if(updatedBook == null)
            {
                return null;
            }

            updatedBook.Name = book.Name;
            updatedBook.Description = book.Description;
            updatedBook.CategoryId = book.CategoryId;
            updatedBook.BookImageUrl = book.BookImageUrl;

            await _bookClubDbContext.SaveChangesAsync();

            return updatedBook;
        }
    }
}
