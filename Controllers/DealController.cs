using CRM.Models;
using CRM.Services;
using Dapr;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers;

[ApiController]
[Route("deals")]
public class DealController : ControllerBase
{
    private readonly DealService _dealService;

    public DealController(DealService dealService)
    {
        _dealService = dealService;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Deal>> Get(int id)
    {
        var deal = await _dealService.GetDealAsync(id);
        if (deal == null)
        {
            return NotFound();
        }
        return Ok(deal);
    }

    [HttpPost]
    public async Task<ActionResult<Deal>> Create(CreateDealDTO dto)
    {
        Deal createdDeal = await _dealService.CreateDealAsync(dto);
        return Ok(createdDeal);
    }

    [HttpPost("{id:int}/schedule")]
    [Topic(SuperConfig.pubSubName, "deals/schedule")]
    public async Task<ActionResult> SheduleDeal(ScheduleDealDTO dto)
    {
        Console.WriteLine(dto);
        await _dealService.SheduleDeal(dto);
        return Ok();
    }

    [HttpPost("{id:int}/cancel")]
    public async Task<ActionResult> CancelDeal(int id)
    {
        await _dealService.CancelDeal(id);
        return Ok();
    }

    [HttpPost("{id:int}/close")]
    public async Task<ActionResult> CloseDeal(int id)
    {
        await _dealService.CloseDeal(id);
        return Ok();
    }
}