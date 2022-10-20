using System.ComponentModel.DataAnnotations;

namespace DepositParameters.Models;

public enum Status
{
    /// <summary>
    /// На согласовании
    /// </summary>
    [Display(Name = "На согласовании")]
    OnApproval,
    /// <summary>
    /// Согласовано
    /// </summary>
    [Display(Name = "Согласовано")]
    Approved,
    /// <summary>
    /// Отклонено
    /// </summary>
    [Display(Name = "Отклонено")]
    Rejected,
    /// <summary>
    /// Нет статуса
    /// </summary>
    [Display(Name = "Нет статуса")]
    None
}
