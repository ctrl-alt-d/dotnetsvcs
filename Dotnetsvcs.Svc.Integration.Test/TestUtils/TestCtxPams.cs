using Microsoft.EntityFrameworkCore;

namespace Dotnetsvcs.Svc.Integration.Test.TestUtils;

public static class TestCtxPams
{
    public static DbContextOptionsBuilder ConfigureDbOptions(
        this DbContextOptionsBuilder options,
        Action<string>? customLogger = null
        )
        =>
        options
        .LogTo(customLogger ?? DoNothing)
        .UseSqlite(GetNewConnectionString());

    private static string GetNewConnectionString()
        =>
        $"Data Source=file:{DataSource}?mode=memory&cache=shared";

    private static string DataSource
        =>
        string.Concat("Esborrar", Guid.NewGuid().ToString().AsSpan(4, 8), ".db");

    private static void DoNothing(string _) { }

}

