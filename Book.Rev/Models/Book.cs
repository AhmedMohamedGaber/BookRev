using System.ComponentModel.DataAnnotations;

namespace BookRev.Models
{
    public class Book
    {

        public int Id { get; set; }

        [Required,MaxLength(250)]
        public string Title { get; set; }

        public int Year { get; set; }

        public double Rate { get; set; }

        [Required,MaxLength(2500)]
        public string Description { get; set; }


		[Required(ErrorMessage = "Please choose  image")]
		public string Image { get; set; }

		public byte CategoryId { get; set; }

        public Category Category { get; set; }

        public byte AuthorId { get; set; }
        public Author Author { get; set; }

        public byte PublisherId { get; set; }
        public Publisher Publisher { get; set; }

    }
}
