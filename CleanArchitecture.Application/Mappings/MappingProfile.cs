using AutoMapper;
using CleanArchitecture.Application.Features.Actors.Commands.CreateActor;
using CleanArchitecture.Application.Features.Actors.Commands.DeleteActor;
using CleanArchitecture.Application.Features.Actors.Commands.UpdateActor;
using CleanArchitecture.Application.Features.Directors.Commands.CreateDirector;
using CleanArchitecture.Application.Features.Streamers.Commands;
using CleanArchitecture.Application.Features.Streamers.Commands.DeleteStreamer;
using CleanArchitecture.Application.Features.Streamers.Commands.UpdateStreamer;
using CleanArchitecture.Application.Features.Videos.Queries.GetVideosList;
using CleanArchitecture.Domain;

namespace CleanArchitecture.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Video, VideosVm>();

            CreateMap<CreateStreamerCommand, Streamer>();
            CreateMap<UpdateStreamerCommand, Streamer>();
            CreateMap<DeleteStreamerCommand, Streamer>();

            CreateMap<CreateDirectorCommand, Director>();

            CreateMap<CreateActorCommand, Actor>();
            CreateMap<UpdateActorCommand, Actor>();
            CreateMap<DeleteActorCommand, Actor>();    
        }
    }
}
