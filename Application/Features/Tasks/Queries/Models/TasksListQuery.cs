using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Responses;

namespace Application.Features.Tasks.Queries.Models;
public   class TasksListQuery:IRequest<BaseResponse<List<TaskListItemDto>>>
{
    public int ProjectId { get; set; }
}
