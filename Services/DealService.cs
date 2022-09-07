using System.Diagnostics;
using System;
using CRM.Models;

namespace CRM.Services;

public class DealService
{
    private static readonly List<Deal> deals = new();

    public async Task<Deal> GetDealAsync(int id)
    {
        await Task.CompletedTask;
        Deal deal = deals.First(deal => deal.Id == id);
        if (deal == null) throw new Exception("not found");
        return deal;
    }

    private static async Task UpdateDealAsync(int id, Deal newDeal)
    {
        await Task.CompletedTask;
        int index = deals.FindIndex(deal => deal.Id == id);
        if (index < 0) throw new Exception("not found");
        deals[index] = newDeal;
    }

    public async Task<Deal> CreateDealAsync(CreateDealDTO dto)
    {
        await Task.CompletedTask;
        var deal = Deal.Create(dto.Name, dto.Amount);
        deals.Add(deal);
        return deal;
    }

    public async Task SheduleDeal(ScheduleDealDTO dto)
    {
        var deal = await GetDealAsync(dto.Id);
        deal.Schedule(dto.Date);
        await UpdateDealAsync(dto.Id, deal);
    }

    public async Task CloseDeal(int id)
    {
        var deal = await GetDealAsync(id);
        deal.Close();
        await UpdateDealAsync(id, deal);
    }

    public async Task CancelDeal(int id)
    {
        var deal = await GetDealAsync(id);
        deal.Cancel();
        await UpdateDealAsync(id, deal);
    }
}

////////////////////////////////////

public class CreateDealDTO
{
    public string Name { get; set; } = null!;
    public int Amount { get; set; }
}

public class ScheduleDealDTO
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
}