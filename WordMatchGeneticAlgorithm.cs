namespace GAConsoleApp;
/// <summary>
/// 文字匹配遗传算法
/// </summary>
public class WordMatchGeneticAlgorithm
{
    /// <summary>
    /// 种群数量
    /// </summary>
    private readonly int _populationSize;
    /// <summary>
    /// 种群
    /// </summary>
    public List<Chromosome> Population { get; set; }
    /// <summary>
    /// DNA长度
    /// </summary>
    private readonly int _dnaSize;
    /// <summary>
    /// 从匹配度中选中匹配度最高的多少个作为下一代的父母
    /// </summary>
    private readonly int _selectionCount;
    private readonly Random random;
    private readonly char[] _target;
    /// <summary>
    /// 文字匹配遗传算法
    /// </summary>
    /// <param name="populationSize">种群数量，越多匹配出的越快（人多力量大），但是速度越慢（人多不好统计）</param>
    /// <param name="target">要匹配出的目标字符串</param>
    /// <param name="selectionCount">每次进化选出多少个作为父母种群</param>
    public WordMatchGeneticAlgorithm(int populationSize, string target, int selectionCount = 50)
    {
        _populationSize = populationSize;
        Population = new List<Chromosome>();
        _dnaSize = target.Length;
        _target = target.ToArray();
        random = new Random();
        _selectionCount = selectionCount;
        InitialPopulation();
    }


    /// <summary>
    /// 种群初始化
    /// </summary>
    private void InitialPopulation()
    {
        for (int i = 0; i < _populationSize; i++)
            Population.Add(new Chromosome(_dnaSize)
            {
                Genes = Enumerable.Range(0, _dnaSize).Select((t, i) => Gene).ToArray()
            });
    }
    private char Gene => (char)random.Next(32, 127);
    /// <summary>
    /// 计算匹配度
    /// </summary>
    /// <param name="nPopulation"></param>
    /// <returns></returns>
    public List<Chromosome> Fitness()
    {
        for (int i = 0; i < _populationSize; i++)
            Population[i].CalcFitness(t => t.Genes.Select((d, idx) => d == _target[idx]).Where(t => t).Count());
        return Population;
    }

    /// <summary>
    /// 选择最优种群
    /// </summary>
    /// <param name="fitness"></param>
    /// <returns></returns>
    public List<Chromosome> Selection()
    {
        return Fitness().OrderByDescending(t => t.Fitness).Skip(0).Take(_selectionCount).ToList();
    }

    /// <summary>
    /// 随机交叉（杂交）
    /// </summary>
    /// <param name="parents">筛选出的最优种群</param>
    /// <returns></returns>
    public List<Chromosome> CrossOver(List<Chromosome> parents)
    {
        List<Chromosome> children = new();
        for (int k = 0; k < _populationSize; k++)
        {
            //if (random.NextDouble() < 0.04) //交叉概率
            //{
            Chromosome f = parents[random.Next(_selectionCount)];
            Chromosome m = parents[random.Next(_selectionCount)];

            Chromosome newChild = new(_dnaSize);
            for (int i = 0; i < _dnaSize; i++)
                newChild.Genes[i] = random.Next(0, 2) == 1 ? f.Genes[i] : m.Genes[i];
            children.Add(newChild);
            //}
            //else
            //{
            //    children.Add(Population[k]);
            //}
        }
        return children;
    }


    /// <summary>
    /// 变异
    /// </summary>
    /// <param name="children">杂交后的新种群</param>
    /// <returns></returns>
    public List<Chromosome> Mutation(List<Chromosome> children)
    {
        for (int i = 0; i < children.Count; i++)
            if (random.NextDouble() <= 0.1)
                children[i].Genes[random.Next(_dnaSize)] = Gene;
        return children;
    }
    /// <summary>
    /// 最优解
    /// </summary>
    /// <returns></returns>
    public string Best()
    {
        var fitness = Population.OrderByDescending(x => x.Fitness).First();
        return string.Concat(fitness.Genes);
    }
    /// <summary>
    /// 进化
    /// </summary>
    public void Evolve()
    {
        var parents = Selection();
        var children = CrossOver(parents);
        Population = Mutation(children);
    }
}