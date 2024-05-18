using AutoMapper;
using Collectify.Application.Identity.Models.Dtos;
using Collectify.Domain.Entities;

namespace Collectify.Infrastructure.Identity.Mappers;

public class AccessTokenMapper : Profile
{
    public AccessTokenMapper()
    {
        CreateMap<AccessToken, AccessTokenDto>();
    }
}