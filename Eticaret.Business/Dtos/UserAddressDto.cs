using Eticaret.SharedLibrary.Entity;

namespace Eticaret.Data.Entity;

public class UserAddressDto : BaseDto
{
    public int? UserId { get; set; }

    public string Name { get; set; }

    public string Address { get; set; }

    public string City { get; set; }

    public string PhoneNumber { get; set; }
}