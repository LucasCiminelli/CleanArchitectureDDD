using AutoMapper;
using CleanArchitecture.Application.Features.Actors.Commands.CreateActor;
using CleanArchitecture.Application.Features.Actors.Commands.DeleteActor;
using CleanArchitecture.Application.Features.Actors.Commands.UpdateActor;
using CleanArchitecture.Application.Features.Actors.Queries.Vms;
using CleanArchitecture.Application.Features.Directors.Commands.CreateDirector;
using CleanArchitecture.Application.Features.Directors.Commands.DeleteDirector;
using CleanArchitecture.Application.Features.Directors.Commands.UpdateDirector;
using CleanArchitecture.Application.Features.Directors.Queries.Vms;
using CleanArchitecture.Application.Features.Streamers.Commands;
using CleanArchitecture.Application.Features.Streamers.Commands.DeleteStreamer;
using CleanArchitecture.Application.Features.Streamers.Commands.UpdateStreamer;
using CleanArchitecture.Application.Features.Streamers.Queries.Vms;
using CleanArchitecture.Application.Features.Videos.Commands.CreateVideo;
using CleanArchitecture.Application.Features.Videos.Commands.DeleteVideo;
using CleanArchitecture.Application.Features.Videos.Commands.UpdateVideo;
using CleanArchitecture.Application.Features.Videos.Queries.Vms;
using CleanArchitecture.Domain;

namespace CleanArchitecture.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Video, VideosVm>();
            CreateMap<Director, DirectorsVm>();
            CreateMap<Actor, ActorsVm>();
            CreateMap<Streamer, StreamersVm>();

            CreateMap<CreateVideoCommand, Video>();
            CreateMap<UpdateVideoCommand, Video>();
            CreateMap<DeleteVideoCommand, Video>();
            
            CreateMap<CreateStreamerCommand, Streamer>();
            CreateMap<UpdateStreamerCommand, Streamer>();
            CreateMap<DeleteStreamerCommand, Streamer>();

            CreateMap<CreateDirectorCommand, Director>();
            CreateMap<UpdateDirectorCommand, Director>();
            CreateMap<DeleteDirectorCommand, Director>();
            
            CreateMap<CreateActorCommand, Actor>();
            CreateMap<UpdateActorCommand, Actor>();
            CreateMap<DeleteActorCommand, Actor>();    
        }
    }
}
