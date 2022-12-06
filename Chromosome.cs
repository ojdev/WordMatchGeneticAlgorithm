namespace GAConsoleApp;
/// <summary>
/// 染色体
/// </summary>
public class Chromosome
{
    /// <summary>
    /// 基因判断
    /// </summary>
    public char[] Genes { get; set; }
    /// <summary>
    /// 适应度
    /// </summary>
    public double Fitness { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="length">基因长度</param>
    public Chromosome(int length)
    {
        Genes = new char[length];
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="target"></param>
    public void CalcFitness(Func<Chromosome, double> action)
    {
        Fitness = action.Invoke(this);
    }

}