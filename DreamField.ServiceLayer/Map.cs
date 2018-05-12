
namespace DreamField.ServiceLayer
{
    using AutoMapper;
    using Model;
    using Dto;
    using System.Linq;


    public class Map : Profile
    {
        public Map()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Ration, RationDto>()
                .ForMember(dest => dest.Animal,
                    opts => opts.MapFrom(src => src.Animal.ToString()))
                .ForMember(dest => dest.FarmName,
                    opts => opts.MapFrom(src => src.Farm.name))
                .ForMember(dest => dest.EnergyFeedUnit,
                    opts => opts.MapFrom(src =>
                        src.RationFeeds.Sum(feed => feed.Feed.FeedElement.EnergyFeedUnit * feed.amount)))
                .ForMember(dest => dest.DigestibleProtein,
                    opts => opts.MapFrom(src =>
                        src.RationFeeds.Sum(feed => feed.Feed.FeedElement.DigestibleProtein * feed.amount))));
        }

    }
}