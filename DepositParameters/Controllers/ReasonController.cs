using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DepositParameters.Controllers;

[ApiController]
[Route("[controller]")]
public class ReasonController : ControllerBase
{
    private readonly AppDbContext _context;

    public ReasonController(AppDbContext context)
    {
        _context = context;
    }

   /// <summary>
   /// ѕолучить справочник причин отклонений
   /// </summary>
   /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var reasons = await _context.Reasons.ToListAsync();

        return reasons.Any() ? Ok(reasons) : NotFound();
    }
}
