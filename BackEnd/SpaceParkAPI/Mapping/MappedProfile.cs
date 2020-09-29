using AutoMapper;
using spaceparkapi.Dto;
using spaceparkapi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace spaceparkapi.Mapping
{
    public class MappedProfile : Profile
    {
        public MappedProfile()
        {
            CreateMap<Spaceport, SpaceportDto>()
                    .ReverseMap();
            CreateMap<Spaceship, SpaceshipDto>()
                   .ReverseMap();
            CreateMap<Traveller, TravellerDto>()
                   .ReverseMap();
            CreateMap<Parkingspot, ParkingspotDto>()
                   .ReverseMap();
        }

    }
}