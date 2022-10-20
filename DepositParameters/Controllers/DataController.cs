using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DepositParameters.Models;

namespace DepositParameters.Controllers;

[ApiController]
[Route("[controller]")]
public class DataController : ControllerBase
{
    private readonly AppDbContext _context;

    public DataController(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Получить последний замер месторождения
    /// </summary>
    /// <param name="depositId">Id месторождения</param>
    /// <returns></returns>
    private async Task<Data> GetLastData(int depositId)
    {
        return await _context.Data
            .Include(x => x.Deposit)
            .Include(x => x.PreviousData)
            .Where(x => x.DepositId == depositId)
            .OrderByDescending(x => x.Date)
            .FirstOrDefaultAsync();
    }


    /// <summary>
    /// Получить модель замера
    /// </summary>
    /// <param name="depositId">Id месторождения</param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Get(int depositId)
    {
        var data = await GetLastData(depositId);

        if (data == null)
        {
            return Ok(new DataModel(new Data(data.Deposit)));
        }

        if (data.Status == Status.Approved)
        {
            return Ok(new DataModel(new Data(data.Deposit, data)));
        }
        else
        {
            return Ok(new DataModel(data));
        }
    }

    /// <summary>
    /// Получить и сохранить данные замера
    /// </summary>
    /// <param name="data">Данные замера</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Post(Data data)
    {
        if (data.Qj.Value == null || data.Wp.Value == null || data.ReasonId == null)
        {
            return BadRequest();
        }

        data.CalculateQn();
        data.PreviousData = await GetLastData(data.DepositId);
        data.Status = Status.OnApproval;
        data.Date = DateTime.Now;

        Random rnd = new();
        data.Qj.Status = (Status)rnd.Next(1, 3);
        data.Wp.Status = (Status)rnd.Next(1, 3);

        _context.Add(data);
        await _context.SaveChangesAsync();

        return Ok(new DataModel(data));
    }
}
