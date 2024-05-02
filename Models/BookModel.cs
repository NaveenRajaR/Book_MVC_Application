using System.ComponentModel.DataAnnotations;

namespace Books.Models
{
    public class BookModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? Publisher { get; set; }

        [Required]
        [StringLength(100)]
        public string? Title { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Author First Name")]
        public string? AuthorFirstName { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Author Last Name")]
        public string? AuthorLastName { get; set; }

        [Required]
        [Range(0, 10000)]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Publication Year")]
        public int PublicationYear { get; set; }

        [Required]
        [Display(Name = "City Of Publication")]
        public string? CityOfPublication { get; set; }

        public string? Medium { get; set; }

        public string MLACitation
        {
            get
            {
                return $"{AuthorLastName}, {AuthorFirstName}. \"{Title}\". {Publisher}, {PublicationYear}. {Medium}.";
            }
        }

        public string ChicagoCitation
        {
            get
            {
                return $"{AuthorLastName}, {AuthorFirstName}. {Title}. {CityOfPublication}: {Publisher}, {PublicationYear}.";
            }
        }
    }
}
