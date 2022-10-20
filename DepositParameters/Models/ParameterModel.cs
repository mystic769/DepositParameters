using System.ComponentModel.DataAnnotations.Schema;
using DepositParameters.Extensions;

namespace DepositParameters.Models;

[NotMapped]
public class ParameterModel
{
    /// <summary>
    /// Создать модель параметра
    /// </summary>
    /// <param name="id">Id для таблицы</param>
    /// <param name="name">Название параметра</param>
    /// <param name="parameter">Данные параметра</param>
    /// <param name="previousParameter">Данные параметра предыдущего замера</param>
    public ParameterModel(int id, string name, Parameter parameter, Parameter previousParameter)
    {
        Id = id;
        Name = name;
        Status = parameter?.Status;
        StatusName = parameter?.Status.GetDisplayName();
        Agreement = parameter?.Value;
        PreviousData = previousParameter?.Value;
    }

    public int Id { get; set; }


    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Статус
    /// </summary>
    public Status? Status { get; set; }

    /// <summary>
    /// Название статуса
    /// </summary>
    public string StatusName { get; set; }

    /// <summary>
    /// Значение параметра на согласование
    /// </summary>
    public decimal? Agreement { get; set; }

    /// <summary>
    /// Значение параметра предыдущего замера
    /// </summary>
    public decimal? PreviousData { get; set; }

    /// <summary>
    /// Разница между текущими данными и предыдущим замером
    /// </summary>
    public decimal? Difference => Agreement - PreviousData;
}
