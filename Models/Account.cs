namespace BankWeb.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public Bank Bank { get; set; }
    }
}
