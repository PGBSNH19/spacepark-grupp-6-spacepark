using AutoMapper;
using spaceparkapi.Dto;
using spaceparkapi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace spaceparkapi.Configuration
{
    public class MappedProfile : Profile
    {
        public MappedProfile()
        {
            CreateMap<Spaceport, SpaceportDto>()
                    .ReverseMap();
            CreateMap<Spaceship, SpaceshipDto>()
                   .ReverseMap();
        }

    }
}