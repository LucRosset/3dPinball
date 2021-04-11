using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pinball.Props
{
    abstract public class Display : MonoBehaviour
    {
        ///<summary>
        /// Set or flip the display's state.
        ///</summary>
        ///<param name="state">
        /// State the display should be set to.
        ///</param>
        abstract public void SetDisplayState(bool state);
    }
}