namespace OrderManagement.Application.Contract.Accounts
{
    public class LoginResponseDto
    {
        public LoginResponseDto(string token, string role, string firstName, string surname, string email, DateTime expiration)
        {
            Token = token;
            Role = role;
            FirstName = firstName;
            Surname = surname;
            Email = email;
            Expiration = expiration;
        }

        public string Token { get; set; }
        public string Role { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public DateTime Expiration { get; set; }
    }
}
