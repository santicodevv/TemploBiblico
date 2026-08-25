using AutoMapper;
using Iglesia.Application.DTOs.Members;
using Iglesia.Domain.Entities;

namespace Iglesia.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Member, MemberDto>();
    }
}