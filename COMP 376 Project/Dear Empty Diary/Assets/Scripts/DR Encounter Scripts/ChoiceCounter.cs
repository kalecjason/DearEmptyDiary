using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceCounter : MonoBehaviour
{
    #region ChoiceCounter Description
    /*
     * Keeps track of how many 'Good' and 'Bad' choices Ruby made during the encounter. 
     */
    #endregion

    #region Variables
    [Tooltip("The number of good choices made.")]
    public int goodCounter = 0;
    [Tooltip("The number of bad choices made.")]
    public int badCounter = 0;
    #endregion

    #region Functions
    public void IncrementGood()
    {
        goodCounter++;
    }

    public void IncrementBad()
    {
        badCounter++;
    }
    #endregion
}
