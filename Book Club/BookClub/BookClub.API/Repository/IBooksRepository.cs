using BookClub.API.Models.Domain;

namespace BookClub.API.Repository
{
    public interface IBooksRepository
    {
        Task<List<Book>> GetAllBooksAsync();

        Task<Book> GetBookByIdAsync(Guid id);

        Task<Book> AddBookAsync(Book book);

        Task<Book?> UpdateBookAsync(Guid id, Book book);

        Task<Book?> DeleteBookAsync(Guid id);

    }
}
