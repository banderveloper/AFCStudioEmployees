using AFCStudioEmployees.Persistence;
using Microsoft.Extensions.Logging;

namespace AFCStudioEmployees.Tests.Common;

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