using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MonthlyRevenue.Application.Commands;
using MonthlyRevenue.Application.Queries;
using MonthlyRevenue.Domain;
using MonthlyRevenue.Models;

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

    [HttpPost("search")]
    public async Task<ActionResult<PagedResponse<RevenueResponse>>> Search([FromBody] RevenueSearchRequest req)
    {
        var page = await _mediator.Send(new SearchRevenuesQuery(
            req.CompanyCode, req.FromYM, req.ToYM, req.PageIndex, req.PageSize));

        var items = page.Items.Select(_mapper.Map<RevenueResponse>).ToList();

        var resp = new PagedResponse<RevenueResponse>
        {
            Items = items,
            PageIndex = req.PageIndex,
            PageSize = req.PageSize,
            TotalCount = page.TotalCount
        };

        return Ok(resp);
    }

    /// <summary>新增/更新：單筆</summary>
    [HttpPost("upsert")]
    public async Task<ActionResult> Post([FromBody] RevenueUpsertRequest req)
    {
        var cmd = _mapper.Map<RevenueUpsertCommand>(req);
        var affected = await _mediator.Send(cmd);
        return affected > 0 ? Ok() : StatusCode(500);
    }
}