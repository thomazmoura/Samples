namespace AsyncExceptionHandlers;

public class AsyncExceptionHandlersTests
{
    private readonly ExceptionHandler _exceptionHandler;
    public AsyncExceptionHandlersTests()
    {
        _exceptionHandler = new ExceptionHandler();
    }


    [Fact]
    public void ThrowExceptionOnSyncMethod_ShouldThrowException()
    {
        var callToMethod = () => _exceptionHandler.ThrowExceptionOnSyncMethod();
        callToMethod.Should().Throw<TestException>();
    }

    [Fact]
    public void ThrowExceptionOnSyncMethodThatReturnsTask_WhenNotUsingAwait_ShouldThrowException()
    {
        var callToMethod = () => _exceptionHandler.ThrowExceptionOnSyncMethodThatReturnsTask().GetAwaiter().GetResult();
        callToMethod.Should().Throw<TestException>();
    }

    [Fact]
    public async Task ThrowExceptionOnSyncMethodThatReturnsTask_WhenUsingAwait_ShouldThrowException()
    {
        var callToMethod = async () => await _exceptionHandler.ThrowExceptionOnSyncMethodThatReturnsTask();
        await callToMethod.Should().ThrowAsync<TestException>();
    }

    [Fact]
    public void ThrowExceptionOnASyncMethodThatReturnsTask_WhenNotUsingAwait_ShouldThrowException()
    {
        var callToMethod = () => _exceptionHandler.ThrowExceptionOnAsyncMethodThatReturnsTask().GetAwaiter().GetResult();
        callToMethod.Should().Throw<TestException>();
    }

    [Fact]
    public async Task ThrowExceptionOnASyncMethodThatReturnsTask_WhenUsingAwait_ShouldThrowException()
    {
        var callToMethod = async () => await _exceptionHandler.ThrowExceptionOnAsyncMethodThatReturnsTask();
        await callToMethod.Should().ThrowAsync<TestException>();
    }

}

public class ExceptionHandler
{
    public void ThrowExceptionOnSyncMethod()
    {
        throw new TestException("Throwing exception");
    }

    public Task ThrowExceptionOnSyncMethodThatReturnsTask()
    {
        throw new TestException("Throwing exception");
    }

    public async Task ThrowExceptionOnAsyncMethodThatReturnsTask()
    {
        throw new TestException("Throwing exception");
    }
}

public class TestException: Exception {
    public TestException() { }
    public TestException(string message): base(message) { }
}
