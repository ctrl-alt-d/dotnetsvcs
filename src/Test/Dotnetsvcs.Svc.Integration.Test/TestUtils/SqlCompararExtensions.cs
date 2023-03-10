namespace Dotnetsvcs.Svc.Integration.Test.TestUtils;
public static class SqlCompararExtensions {
    public static string ComparableSql(this string sql_raw) {
        var sql_lines =
            sql_raw
            .Split(Environment.NewLine)
            .SelectMany(l => l.Split(" "))
            .Select(l => l.Trim())
            .Where(l => !string.IsNullOrEmpty(l))
            .Select(l => l.ToLower())
            ;

        var sql = string.Join("", sql_lines);

        return sql;
    }


}
