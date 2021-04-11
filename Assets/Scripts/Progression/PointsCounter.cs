using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Pinball.Progression{
    public class PointsCounter : MonoBehaviour
    {
        [Tooltip("Text displaying the points total.")]
        [SerializeField] TextMeshProUGUI displayText = null;

        public int pointsTotal {get; private set;}

        // Start is called before the first frame update
        void Start()
        {
            pointsTotal = 0;
            displayText.text = pointsTotal.ToString();
        }

        // Public methods

        ///<summary>
        /// Add points to the total.
        ///</summary>
        ///<param name="points">
        /// Ammount of points to add.
        ///</param>
        public void AddPoints(int points)
        {
            pointsTotal += points;
            displayText.text = pointsTotal.ToString();
        }

        ///<summary>
        /// Set the points total to the specified value.
        ///</summary>
        ///<param name="points">
        /// Point value to be set.
        ///</param>
        public void SetPoints(int points)
        {
            pointsTotal = points;
            displayText.text = pointsTotal.ToString();
        }
    }
}