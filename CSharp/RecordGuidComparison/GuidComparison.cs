namespace RecordGuidComparison;

public class GuidComparison
{
    [Fact]
    public void GuidComparison_WhenUsedTwoIdenticGuidReferences_IsTrueAndDistinctWorks()
    {
        var guid1 = new Guid("675b818f-f7fa-4f1d-b689-48d883690fdc");
        var guid2 = new Guid("675b818f-f7fa-4f1d-b689-48d883690fdc");

        (guid1 == guid2).Should().BeTrue();
        var unique = new Guid[] { guid1, guid2 };
        unique.Distinct().Count().Should().Be(1);
    }

    [Fact]
    public void GuidComparison_WhenInsideAnAnonymousObject_IsFalseButDistinctWorks()
    {
        var object1 = new { Id = new Guid("675b818f-f7fa-4f1d-b689-48d883690fdc") };
        var object2 = new { Id = new Guid("675b818f-f7fa-4f1d-b689-48d883690fdc") };

        (object1 == object2).Should().BeFalse();
        var unique = new [] { object1, object2 };
        unique.Distinct().Count().Should().Be(1);
    }

    [Fact]
    public void GuidComparison_WhenInsideAReferenceObject_IsFalseAndDistinctDoesNotWork()
    {
        var object1 = new IdClass(){ Id = new Guid("675b818f-f7fa-4f1d-b689-48d883690fdc") };
        var object2 = new IdClass(){ Id = new Guid("675b818f-f7fa-4f1d-b689-48d883690fdc") };

        (object1 == object2).Should().BeFalse();
        var unique = new IdClass[] { object1, object2 };
        unique.Distinct().Count().Should().Be(2);
    }

    [Fact]
    public void GuidComparison_WhenInsideARecord_IsTrueAndDistinctWorks()
    {
        var object1 = new IdRecord(new Guid("675b818f-f7fa-4f1d-b689-48d883690fdc"));
        var object2 = new IdRecord(new Guid("675b818f-f7fa-4f1d-b689-48d883690fdc"));

        (object1 == object2).Should().BeTrue();
        var unique = new IdRecord[] { object1, object2 };
        unique.Distinct().Count().Should().Be(1);
    }
}

public class IdClass
{
    public Guid Id { get; set; }
}

public record IdRecord(Guid Id);
