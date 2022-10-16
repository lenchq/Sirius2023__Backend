namespace Sirius.CaesarCipher.Interfaces;

public interface ICaesarEncoder
{
    public string Encode(string text, int rot);
    public string Decode(string? text, int rot);
}