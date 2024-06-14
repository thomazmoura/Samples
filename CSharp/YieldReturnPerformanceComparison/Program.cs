namespace YieldReturnPerformanceComparison;

public class ReturnListVsYieldReturnBenchMark
{
    private const int AmountOfUsersToBeGenerated = 1000;
    private readonly Faker<User> _faker;

    public ReturnListVsYieldReturnBenchMark()
    {
        Randomizer.Seed = new Random(42);
        _faker = new Faker<User>()
            .RuleFor(user => user.Name, (faker, user) => faker.Name.FullName())
            .RuleFor(user => user.BirthDate, (faker, user) => faker.Date.Past());
    }

    // The point of this particular test is show that if iterate twice on an actual IEnumerable
    // that uses yield return it will execute twice
    [Benchmark]
    public (List<User>, int) WithYieldReturnAndToListAfter()
    {
        var users = GenerateWithYieldReturn();
        return (users.ToList(), users.Count());
    }

    [Benchmark]
    public (List<User>, int) WithAListAndToListAfter()
    {
        var result = GenerateWithAList();
        return (result.ToList(), result.Count());
    }

    [Benchmark]
    public (List<User>, int) WithYieldReturnAndToListBefore()
    {
        var result = GenerateWithYieldReturn().ToList();
        return (result, result.Count);
    }

    [Benchmark]
    public (List<User>, int) WithAListAndToListBefore()
    {
        var result = GenerateWithAList().ToList();
        return (result, result.Count);
    }

    public IEnumerable<User> GenerateWithAList()
    {
        List<User> users = [];
        for (var i = 0; i <= AmountOfUsersToBeGenerated; i++)
        {
            users.Add(_faker.Generate());
        }
        return users;
    }

    public IEnumerable<User> GenerateWithYieldReturn()
    {
        for (var i = 0; i <= AmountOfUsersToBeGenerated; i++)
        {
            yield return _faker.Generate();
        }
    }
}

public class User
{
    private static int s_idSeed = 1;

    public int Id { get; } = s_idSeed++;
    public required string Name { get; set; }
    public required DateTime BirthDate { get; set; }
}

public class Program
{
    public static void Main(string[] args)
    {
        var config = ManualConfig.Create(DefaultConfig.Instance)
                                         .AddDiagnoser(MemoryDiagnoser.Default);
        var summary = BenchmarkRunner.Run<ReturnListVsYieldReturnBenchMark>(config);
    }
}
