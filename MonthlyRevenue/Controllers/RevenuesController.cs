using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MonthlyRevenue.Application.Commands;
using MonthlyRevenue.Application.Queries;
using MonthlyRevenue.Domain;

namespace MonthlyRevenue.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RevenuesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    public RevenuesController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>查詢：用公司代號（可帶期間）</summary>
    [HttpGet("{companyCode}")]
    public async Task<ActionResult<IEnumerable<RevenueResponse>>> Get(
        [FromRoute] string companyCode,
        [FromQuery] string? fromYM,
        [FromQuery] string? toYM)
    {
        var rows = await _mediator.Send(new GetRevenuesByCompanyQuery(companyCode, fromYM, toYM));
        var vm = rows.Select(r => _mapper.Map<RevenueResponse>(r));
        return Ok(vm);
    }

    /// <summary>新增/更新：單筆（CSV 解析後逐筆打）</summary>
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] RevenueUpsertRequest req)
    {
        var cmd = _mapper.Map<RevenueUpsertCommand>(req);
        var affected = await _mediator.Send(cmd);
        return affected > 0 ? Ok() : StatusCode(500);
    }
}