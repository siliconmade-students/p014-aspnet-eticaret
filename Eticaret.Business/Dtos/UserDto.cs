namespace Eticaret.Business.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string EmailAddress { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? FullAddress { get; set; }
        public string? City { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Roles { get; set; }
        public bool IsActive { get; set; }
    }
}
