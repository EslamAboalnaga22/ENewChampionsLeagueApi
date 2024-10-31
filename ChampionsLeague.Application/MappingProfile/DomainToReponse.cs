namespace ChampionsLeague.Application.MappingProfile
{
    public class DomainToReponse : Profile
    {
        public DomainToReponse()
        {
            // ( Source , Destination )

            CreateMap<Team , TeamDetailsResponse>();
            
            CreateMap<Game, GameDetailsResponse>()
                .ForMember(dest => dest.TeamOne,
                           src => src.MapFrom(x => x.TOne.TeamName))
                .ForMember(dest => dest.TeamTwo,
                           src => src.MapFrom(x => x.TTwo.TeamName));

            CreateMap<Table, TableDetailsResponse>()
                .ForMember(dest => dest.TeamName,
                           src => src.MapFrom(x => x.Team.TeamName));
        }
    }
}
