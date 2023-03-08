using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Dotnetsvcs.Svc.Integration.Test.StackElements.ModelConfiguration.Helpers;

class SqliteTimestampConverter : ValueConverter<byte[]?, int?>
{
    public SqliteTimestampConverter() : base(
        convertToProviderExpression: v => v == null ? null : ToDb(v),
        convertFromProviderExpression: v => v == null ? null : FromDb(v))
    { }
    static byte[] FromDb(int? v) =>
        BitConverter.GetBytes(v!.Value);
    static int ToDb(byte[] v) =>
        BitConverter.ToInt32(v);
}

