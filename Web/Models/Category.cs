using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace Web.Models
{
    public class Category
    {
        [Key]
        public int id { get; set; }
        [Required]

		[DisplayName("Name")]
		public string name { get; set; }

        [DisplayName("Display Order")]
        [Range(1,100,ErrorMessage ="Display order must be in 1 to 100 range")]
        public int DisplayOrder { get; set; }

        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}
