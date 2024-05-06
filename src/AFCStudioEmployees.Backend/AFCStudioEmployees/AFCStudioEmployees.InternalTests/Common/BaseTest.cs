using AFCStudioEmployees.Persistence;

namespace AFCStudioEmployees.InternalTests.Common;

// Base class for all test classes
public class BaseTest : IDisposable
{
    protected readonly ApplicationDbContext Context;

    public BaseTest()
    {
        Context = ApplicationContextFactory.Create();
    }
    
    public void Dispose()
    {
        ApplicationContextFactory.Destroy(Context);
    }
}