namespace BankManagement.API.DTOs
{

    public class AccountDto
    {
        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public double Balance { get; set; }
    }

}