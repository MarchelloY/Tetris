using System.Text;

public static class Utils
{
    public static string ConvertScore(this string score, int numberOfDigits)
    {
        if (score[0].Equals('-')) score = score.Replace('-', ' ').Trim();
        var stringBuilder = new StringBuilder();
        for (var i = 0; i < numberOfDigits - score.Length; i++)
            stringBuilder.Append("0");
        stringBuilder.Append(score);
        
        return stringBuilder.ToString();
    }
}