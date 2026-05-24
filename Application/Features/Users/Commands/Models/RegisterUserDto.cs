using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
namespace Application.Features.Users.Commands.Models;
public class RegisterUserDto
{
    public string UserName { get; set; }
    public string EmailAddress { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PassWord { get; set; }
    public List<int> Roles { get; set; }
}

