using AutoMapper;
using BookClub.API.Data;
using BookClub.API.Models.Domain;
using BookClub.API.Models.DTO;
using BookClub.API.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookClub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBooksRepository booksRepository;
        private readonly IMapper mapper;

        public BooksController(IBooksRepository booksRepository, IMapper mapper)
        {
            this.booksRepository = booksRepository;
            this.mapper = mapper;
        }


        [HttpGet]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAllBooks()
        {
            var booksDomain = await booksRepository.GetAllBooksAsync();

            var bookDTO = mapper.Map<List<BookDTO>>(booksDomain);
            return Ok(bookDTO);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetBookById(Guid id)
        {
            var bookDomain = await booksRepository.GetBookByIdAsync(id);

            if (bookDomain == null) 
            { 
                return NotFound();
            }

            var bookDTO = mapper.Map<BookDTO>(bookDomain);

            return Ok(bookDTO);
        }

        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> AddBook(AddBookRequestDTO addBookRequestDTO)
        {
            var book = mapper.Map<Book>(addBookRequestDTO);
            await booksRepository.AddBookAsync(book);

            var bookDTO = mapper.Map<BookDTO>(book);
            return Ok(bookDTO);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> UpdateBook(Guid id, UpdateBookRequestDTO updateBookRequestDTO)
        {
            var book = mapper.Map<Book>(updateBookRequestDTO);
            var updatedBook = await booksRepository.UpdateBookAsync(id, book);

            if (updatedBook == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<BookDTO>(updatedBook));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            var deleteBook = await booksRepository.DeleteBookAsync(id);

            if (deleteBook == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<BookDTO>(deleteBook));
        }
    }
}
