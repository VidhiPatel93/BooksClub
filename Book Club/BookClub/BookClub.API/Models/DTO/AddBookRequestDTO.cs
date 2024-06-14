using BookClub.API.Models.Domain;

namespace BookClub.API.Models.DTO
{
    public class AddBookRequestDTO
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string? BookImageUrl { get; set; }

        public Guid CategoryId { get; set; }
    }
}
