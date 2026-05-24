using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Roles.Command;

public class CreateRolevm:IRequest<int>
{
    public string RoleName { get; set; }
}
