using System;
using Sirius.CaesarCipher.Interfaces;

namespace Sirius.CaesarCipher.Providers;

public sealed class CaesarEncoder : ICaesarEncoder
{
    private readonly char[] _alphabet = "abcdefghijklmnopqrstuvwxyz".ToArray();
    
    public string Encode(string text, int rot)
    {
        var result = "";
        foreach (var ch in text)
        {
            var isUpper = ch == char.ToUpper(ch);
            var a = isUpper ? 'A' : 'a';
            var z = isUpper ? 'Z' : 'z';
            
            if (a <= ch && ch <= z)
            {
                var newch = (char)(a + (ch - a + rot) % 26);
                result += newch;
            }
            else
            {
                result += ch;
            }
        }

        return result;
    }

    public string Decode(string? text, int rot)
    {
        var result = "";
        foreach (var ch in text)
        {
            var isUpper = ch == char.ToUpper(ch);
            var a = isUpper ? 'A' : 'a';
            var z = isUpper ? 'Z' : 'z';
            
            if (a <= ch && ch <= z)
            {
                var r = ch - a - rot;
                var newch = '\0';
                if (r < 0)
                { 
                    newch = (char)(z + (r % 26) + 1);
                }
                else
                {
                    newch = (char)(a+r%26);
                }
                
                result += newch;
            }
            else
            {
                result += ch;
            }
        }

        return result;
    }
}