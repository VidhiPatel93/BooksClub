namespace BookClub.API.Models.Domain
{
    public class Book
    {
        public Guid BookId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string? BookImageUrl { get; set; }

        public Guid CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
