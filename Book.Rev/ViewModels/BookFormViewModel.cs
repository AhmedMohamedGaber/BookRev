using BookRev.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace BookRev.ViewModels
{
    public class BookFormViewModel
    {
        public int Id { get; set; }

        [Required, StringLength(250)]
        public string Title { get; set; }

        public int Year { get; set; }

        [Range(1,10)]
        public double Rate { get; set; }

        [Required, StringLength(2500)]
        public string Description { get; set; }
         
		//[Required(ErrorMessage = "Please choose image")]
		[Display(Name = "Picture")]
        [ValidateNever]
		public IFormFile? Image { get; set; }
        [ValidateNever]
        public string? ImageName { get; set; }

        [Display(Name ="Category")]
        public byte CategoryId { get; set; }

        //public Category Category { get; set; }

        [Display(Name = "Author")]
        public byte AuthorId { get; set; }
        //public Author Author { get; set; }

        [Display(Name = "Publisher")]
        public byte PublisherId { get; set; }
        //public Publisher Publisher { get; set; }
        [ValidateNever]
        public IEnumerable<Category> Categories{ get; set; }
        [ValidateNever]
        public IEnumerable<Author> Authors { get; set; }
        [ValidateNever]
        public IEnumerable<Publisher> Publishers { get; set; }


    }
}
