using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pinball.Progression
{
    abstract public class Action : MonoBehaviour
    {
        ///<summary>
        /// Method to perform an action when called.
        ///</summary>
        abstract public void DoAction();

        ///<summary>
        /// Method to perform an action with an int parameter when called.
        ///</summary>
        ///<param name="parameter">
        /// Parameter containing any necessary value or object ID for the action to be performed.
        ///</param>
        abstract public void DoAction(int parameter);
    }
}