using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace PersonalHomePage.Models
{
    public sealed class EmailMessageModel
    {
        [Required]
        public string Name { get; set; }

        [Required, DataType(DataType.EmailAddress), EmailAddress]
        public string Email { get; set; }

        [Required, AllowHtml]
        public string Message { get; set; }
    }
}