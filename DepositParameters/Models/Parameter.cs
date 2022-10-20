using Microsoft.EntityFrameworkCore;

namespace DepositParameters.Models;

[Owned]
public class Parameter
{
    public Parameter()
    {
        
    }

    public Parameter(decimal? value)
    {
        Value = value;
    }

    /// <summary>
    /// Значение
    /// </summary>
    public decimal? Value { get; set; }

    /// <summary>
    /// Статус
    /// </summary>
    public Status Status { get; set; } = Status.None;
}
