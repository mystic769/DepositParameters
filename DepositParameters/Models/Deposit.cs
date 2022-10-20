namespace DepositParameters.Models;

public class Deposit
{
    public int Id { get; set; }

    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Замеры
    /// </summary>
    public List<Data> DataList { get; set; }
}
