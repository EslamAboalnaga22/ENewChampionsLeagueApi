namespace ChampionsLeague.Application.MappingProfile
{
    public class RequestToDomain : Profile
    {
        public RequestToDomain()
        {
            // ( Source , Destination )

            CreateMap<AddTeamRequest, Team>();

            CreateMap<UpdateTeamRequest, Team>();
            
            CreateMap<ResultGameRequest, Game>();

        }
    }
}
