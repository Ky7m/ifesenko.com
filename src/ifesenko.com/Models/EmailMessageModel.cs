using System.ComponentModel.DataAnnotations;

namespace IfesenkoDotCom.Models
{
    public sealed class EmailMessageModel
    {
        [Required]
        public string Name { get; set; }

        [Required, DataType(DataType.EmailAddress), EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Message { get; set; }
    }
}