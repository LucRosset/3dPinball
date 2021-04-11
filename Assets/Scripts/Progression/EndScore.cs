using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Pinball.Progression
{
    public class EndScore : MonoBehaviour
    {
        private TextMeshProUGUI scoreText;

        void Start()
        {
            scoreText = GetComponent<TextMeshProUGUI>();
        }

        public void SetScore(string text) { scoreText.text = text; }

        public void SetScore(int score) { scoreText.text = score.ToString(); }
    }
}