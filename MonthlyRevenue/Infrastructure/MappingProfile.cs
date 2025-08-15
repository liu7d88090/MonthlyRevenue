using AutoMapper;
using MonthlyRevenue.Domain;

namespace MonthlyRevenue.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RevenueUpsertRequest, RevenueUpsertCommand>();
            CreateMap<RevenueRow, RevenueResponse>();
        }
    }
}
