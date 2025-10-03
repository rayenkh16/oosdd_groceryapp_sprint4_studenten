namespace Grocery.Core.Models
{
    public enum Role
    {
        None,
        Admin
    }

    public partial class Client : Model
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }

        // Nieuwe property voor rolbeheer
        public Role Role { get; set; } = Role.None;

        public Client(int id, string name, string emailAddress, string password)
            : base(id, name)
        {
            EmailAddress = emailAddress;
            Password = password;
            Role = Role.None; // default waarde
        }
    }
}
