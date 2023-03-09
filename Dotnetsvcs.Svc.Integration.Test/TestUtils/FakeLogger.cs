

namespace Dotnetsvcs.Svc.Integration.Test.TestUtils;

public class FakeLogger {

    public void SaveLog(string log) {
        var lineat =
            log
            .Split(Environment.NewLine)
            .Select((c, i) => (c, i))
            .Where(x => x.c.Contains("Executed DbCommand"))
            .Select(t => (int?)t.i)
            .FirstOrDefault();
        if (lineat==null) return;

        var sani0 = log.Split("\n").Skip(lineat!.Value + 1);
        var sani1 = string.Join(" ", sani0).Split().Where(s => !string.IsNullOrWhiteSpace(s));
        var sani2 = string.Join(" ", sani1);
        var sani3 = sani2.Replace("\"", "").Trim();
        Log.Add(sani3);
        LogLastEntry = sani3;
    }

    internal void Clear() {
        Log.Clear();
    }

    public List<string> Log { get; } = new();
    public string LogLastEntry { get; private set; } = "";

    public string ComparableLastEntry =>
        LogLastEntry
        .ComparableSql();
}
