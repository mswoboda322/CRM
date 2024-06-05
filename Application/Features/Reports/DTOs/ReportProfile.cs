using AutoMapper;
using Domain.Entities;

namespace Application.Features.Reports.DTOs;
public class ReportProfile : Profile
{
    public ReportProfile()
    {
        CreateMap<Report,  ReportDTO>()
            .PreserveReferences();

        CreateMap<Report, ReportSimpleDTO>()
            .PreserveReferences();
    }
}
