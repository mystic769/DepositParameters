namespace DepositParameters.Models;
public class Data
{
    public Data()
    {

    }

    /// <summary>
    /// Создать новый замер
    /// </summary>
    /// <param name="deposit">Месторождение</param>
    /// <param name="previousData">Предыдущий замер</param>
    public Data(Deposit deposit, Data previousData = null)
    {
        DepositId = deposit.Id;
        Deposit = deposit;
        PreviousData = previousData;
    }

    public int Id { get; set; }

    /// <summary>
    /// Статус
    /// </summary>
    public Status Status { get; set; } = Status.None;

    /// <summary>
    /// Id месторождения
    /// </summary>
    public int DepositId { get; set; }

    /// <summary>
    /// Месторождение
    /// </summary>
    public Deposit Deposit { get; set; }

    /// <summary>
    /// Дата замера
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Id причины отклонения
    /// </summary>
    public int? ReasonId { get; set; }

    /// <summary>
    /// Причина отклонения
    /// </summary>
    public Reason Reason { get; set; }

    /// <summary>
    /// Мероприятия по возврату снижений
    /// </summary>
    public string Measures { get; set; }

    /// <summary>
    /// Параметр Qж
    /// </summary>
    public Parameter Qj { get; set; }

    /// <summary>
    /// Параметр % воды
    /// </summary>
    public Parameter Wp { get; set; }

    /// <summary>
    /// Параметр Qн
    /// </summary>
    public Parameter Qn { get; set; }

    /// <summary>
    /// Параметр Нд
    /// </summary>
    public Parameter Nd { get; set; }

    /// <summary>
    /// Параметр Рлин
    /// </summary>
    public Parameter Rlin { get; set; }

    /// <summary>
    /// Параметр Рбуф
    /// </summary>
    public Parameter Rbuf { get; set; }

    /// <summary>
    /// Параметр Рзатр
    /// </summary>
    public Parameter Rzatr { get; set; }

    /// <summary>
    /// Id предыдущего замера
    /// </summary>
    public int? PreviousDataId { get; set; }

    /// <summary>
    /// Предыдущий замер
    /// </summary>
    public Data PreviousData { get; set; }

    /// <summary>
    /// Считаем параметр Qn
    /// </summary>
    public void CalculateQn()
    {
        Qn = new Parameter(Qj?.Value * (1 - Wp?.Value / 100));
    }

    /// <summary>
    /// Получаем параметры для таблицы
    /// </summary>
    /// <returns></returns>
    public List<ParameterModel> GetParameters()
    {
        return new List<ParameterModel>()
        {
            new ParameterModel(1, "Qж", Qj, PreviousData?.Qj),
            new ParameterModel(2, "% воды", Wp, PreviousData?.Wp),
            new ParameterModel(3, "Qн", Qn, PreviousData?.Qn),
            new ParameterModel(4, "Нд", Nd, PreviousData?.Nd),
            new ParameterModel(5, "Рлин", Rlin, PreviousData?.Rlin),
            new ParameterModel(6, "Рбуф", Rbuf, PreviousData?.Rbuf),
            new ParameterModel(7, "Рзатр", Rzatr, PreviousData?.Rzatr)
        };
    }
}
