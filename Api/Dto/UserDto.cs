namespace CleaningSaboms.Dto
{
    public class UserDto
    {
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public List<string> Roles { get; set; } = null!;
    }
}
