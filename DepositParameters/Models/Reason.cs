namespace DepositParameters.Models;

public class Reason
{
    public int Id { get; set; }

    /// <summary>
    /// Текст причины отклонения
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Замеры
    /// </summary>
    public List<Data> DataList { get; set; }
}
