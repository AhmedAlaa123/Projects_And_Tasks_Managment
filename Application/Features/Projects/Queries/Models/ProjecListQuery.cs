using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Responses;

namespace Application.Features.Projects.Queries.Models;
public class ProjecListQuery: IRequest<BaseResponse<ProjectPageDto>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; } = 10;

}
