namespace CleaningSaboms.Dto
{
    public class UpdateUserDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; } = null!;
        public string Id { get; set; } = null!;
    }
}
