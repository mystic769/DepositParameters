using System.ComponentModel.DataAnnotations.Schema;

namespace DepositParameters.Models;

[NotMapped]
public class DataModel
{
    /// <summary>
    /// Создать модель замера
    /// </summary>
    /// <param name="data">Данные замера</param>
    public DataModel(Data data)
    {
        DepositName = data.Deposit.Name;
        ReasonId = data.ReasonId;
        Measures = data.Measures;
        Status = data.Status;
        DatePrevious = data.PreviousData?.Date.ToShortDateString();
        Parameters = data.GetParameters();

        if (Status == Status.OnApproval)
        {
            Info = $"Запрос на согласование отправлен {data.Date}";
        }
    }

    /// <summary>
    /// Название месторождения
    /// </summary>
    public string DepositName { get; set; }

    /// <summary>
    /// Дата предыдущего замера
    /// </summary>
    public string DatePrevious { get; set; }

    /// <summary>
    /// Id причины отклонения
    /// </summary>
    public int? ReasonId { get; set; }

    /// <summary>
    /// Мероприятия по возврату снижений
    /// </summary>
    public string Measures { get; set; }

    /// <summary>
    /// Статус замера
    /// </summary>
    public Status Status { get; set; }

    /// <summary>
    /// Информационное сообщение
    /// </summary>
    public string Info { get; set; }

    /// <summary>
    /// Параметры
    /// </summary>
    public List<ParameterModel> Parameters { get; set; }

}
