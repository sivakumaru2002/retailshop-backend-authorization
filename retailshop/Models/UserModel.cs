using System.ComponentModel.DataAnnotations;

namespace retailshop.Models
{
    public class UserModel
    {
        [Key]
        public string Username { get; set; }
        public required string  EmailAddress { get; set; }
        public string Password { get; set; }
        public required string ? DateOfJoing {  get; set; }
    }
}
