using Microsoft.Data.Sqlite;

namespace Sirius.CaesarCipher.Model;

internal sealed class SqliteConfiguration
{
    public string DataSource { get; set; }
    public string Password { get; set; }
    public string Mode { get; set; }
}