using strange.extensions.mediation.impl;
using TMPro;
using UnityEngine;

namespace Views
{
    public class UIView : View
    {
        [SerializeField] public TMP_Text scoreText;
        [SerializeField] public TMP_Text linesText;
        [SerializeField] public TMP_Text levelsText;

        [SerializeField] private TMP_Text highScoreText;
        [SerializeField] private TMP_Text highLinesText;

        public void UpdateMainUI(int[] values)
        {
            scoreText.text = values[0].ToString().ConvertScore(9);
            linesText.text = values[1].ToString();
            levelsText.text = values[2].ToString();
        }

        public void UpdatePauseUI(int[] values)
        {
            highScoreText.text = "HIGH SCORE: " + values[0];
            highLinesText.text = "HIGH LINES: " + values[1];
        }
    }
}