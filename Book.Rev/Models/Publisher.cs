using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookRev.Models
{
    public class Publisher
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte Id { get; set; }
        public string Name { get; set; }

        [Required, MaxLength(100)]
        public string Address { get; set; }

        [MaxLength(11)]
        public string PhoneNumber { get; set; }

        public string Url { get; set; }


    }
}
