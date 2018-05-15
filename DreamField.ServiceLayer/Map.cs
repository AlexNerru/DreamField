
namespace DreamField.ServiceLayer
{
    using AutoMapper;
    using Model;
    using Dto;
    using System.Linq;


    public static class AutoMapperWebConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(
                cfg =>
                {
                    cfg.CreateMap<Ration, RationInfoDto>()
                        .ForMember(dest => dest.Animal,
                            opts => opts.MapFrom(src => src.Animal))
                        .ForMember(dest => dest.FarmName,
                            opts => opts.MapFrom(src => src.Farm.name))
                        .ForMember(dest => dest.EnergyFeedUnit,
                            opts => opts.MapFrom(src =>
                                src.RationFeeds.Sum(feed => feed.Feed.FeedElement.EnergyFeedUnit * feed.amount)))
                        .ForMember(dest => dest.DigestibleProtein,
                            opts => opts.MapFrom(src =>
                                src.RationFeeds.Sum(feed => feed.Feed.FeedElement.DigestibleProtein * feed.amount)))
                        .ForMember(dest => dest.RoughPercent,
                            opts => opts.MapFrom(src => src.RationStructure.roughage))
                        .ForMember(dest => dest.JuicyPercent,
                            opts => opts.MapFrom(src => src.RationStructure.juicy_food))
                        .ForMember(dest => dest.Consentrates,
                            opts => opts.MapFrom(src => src.RationStructure.concentrates))
                        .ForMember(dest => dest.Feeds,
                            opts => opts.MapFrom(src => src.RationFeeds));

                    cfg.CreateMap<RationFeed, RationFeedsDto>()
                            .ForMember(dest => dest.Feed,
                                opts => opts.MapFrom(src => src.Feed.name))
                            .ForMember(dest => dest.Id,
                                opts => opts.MapFrom(src => src.Ration.Id))
                            .ForMember(dest => dest.Amount,
                                opts => opts.MapFrom(src => src.amount));
                });




        }
    }
}