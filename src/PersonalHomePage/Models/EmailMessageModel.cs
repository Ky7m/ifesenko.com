using System.ComponentModel.DataAnnotations;

namespace PersonalHomePage.Models
{
    public class EmailMessageModel
    {
        [Required]
        public string Name { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string Message { get; set; }
    }
}