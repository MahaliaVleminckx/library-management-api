using Microsoft.AspNetCore.SignalR.Protocol;

namespace Pri.Ee.Client.Models
{
    public class RegisterViewModel
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
