using AutoMapper;
using MonthlyRevenue.Application.Commands;
using MonthlyRevenue.Domain;

namespace MonthlyRevenue.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RevenueUpsertRequest, RevenueUpsertCommand>();
            CreateMap<MonthlyRevenueFromCsv, RevenueResponse>();
        }
    }
}
