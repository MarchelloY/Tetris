using System.Text;
using UnityEngine;

namespace Utils
{
    public static class ScoreExtensions
    {
        public static string ConvertScore(this string score, int numberOfDigits)
        {
            if (score[0].Equals('-'))
            {
                score = score.Replace('-', ' ').Trim();
            }

            var builder = new StringBuilder();

            for (var i = 0; i < numberOfDigits - score.Length; i++)
            {
                builder.Append("0");
            }

            builder.Append(score);

            return builder.ToString();
        }

        public static int[] GetComboScore(this int[] comboScore)
        {
            for (var i = 0; i < comboScore.Length; i++)
            {
                comboScore[i] = (int) (100 * (Mathf.Pow(2,i + 1) - 1));
            }

            return comboScore;
        }
    }
}