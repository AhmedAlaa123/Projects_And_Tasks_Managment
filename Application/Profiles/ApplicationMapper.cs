using Application.Features.Projects.Commands.Models;
using Application.Features.Projects.Queries.Models;
using Application.Features.Tasks.Commands.Models;
using AutoMapper;
using Domain.Models;

namespace Application.Profiles;
public class ApplicationMapper : Profile
{
    public ApplicationMapper()
    {
        CreateMap<ProjectCreateDto,Project >()
            .ForMember(x => x.CreatedAt,option=>option.MapFrom(ele=>DateTime.UtcNow))
            .ReverseMap();
        CreateMap<ProjectUpdateDto, Project>().ReverseMap();
        CreateMap<Project, ProjectInfoDto>().ReverseMap();
        CreateMap<Project, ProjectDetailDto>().ReverseMap();
        CreateMap<TaskCreateDto,Domain.Models.Task>().ReverseMap();
        CreateMap<TaskUpdateDto, Domain.Models.Task>().ReverseMap();
        CreateMap<TaskInfoDto, Domain.Models.Task>().ReverseMap();
        
    }
}
