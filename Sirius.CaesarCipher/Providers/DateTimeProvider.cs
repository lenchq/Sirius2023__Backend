using Sirius.CaesarCipher.Interfaces;

namespace Sirius.CaesarCipher.Providers;

public sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}