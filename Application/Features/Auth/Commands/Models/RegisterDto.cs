namespace Application.Features.Auth.Commands.Models;
public class RegisterDto
{
    public string UserName { get; set; }
    public string EmailAddress { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PassWord { get; set; }
    public List<int> Roles { get; set; }
}
