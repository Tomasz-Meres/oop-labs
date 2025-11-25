namespace Simulator;

public static class Validator
{
    public static int Limiter(int value, int min, int max) 
    {
        if (value < min)
            value = min;
        if (value > max)
            value = max;
        return value;
    }

    public static string
        Shortener(string value, int min, int max, char placeholder)
        {
            if (value == null)
                value = "Unknown";
            value = value.Trim();
            if (value.Length > max)
                value = value.Substring(0, max);
            value = value.Trim();
            if (value.Length < min)
                value = value.PadRight(min, placeholder);
            if (char.IsLetter(value[0]) && char.IsLower(value[0]))
                value = char.ToUpper(value[0]) + value.Substring(1);
            return value;
    }
}
