using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Application.Features.Users.Commands
{
    public class CreateUserVm:IRequest<string>
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public string? FirstName
        {
            get; set;
        }
        public string? LastName
        {
            get; set;
        }
        public string? Email
        {
            get; set;
        }
        public List<int> RoleId { get; set; }

    }
}
