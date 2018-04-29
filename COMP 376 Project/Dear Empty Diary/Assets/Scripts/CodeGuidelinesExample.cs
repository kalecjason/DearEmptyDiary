using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeGuidelinesExample : MonoBehaviour {

    #region Useful Tips
    /*
     * Unity has a lot of nifty tools, but C# has an equal amount of awesome tools!
     * To add a tooltip to any public variable so it appears on the Unity inspector, use the following:
     */
     
        [Tooltip ("Minimum sale price.")]
        public float minPrice = 50.50f;
     
     /* To require components (prevent compilation until said component is attached to an object), use the following:
     
        [RequireComponent (typeof (RigidBody2D))]
        public class DummyObject : MonoBehaviour
     
     * This will make it so that the class requires a component of type RigidBody2D on its object.
     */

    /*
     * To change curly brace style, go to Tools > Options > Text Editor > C# > Code Style > Formatting > New Lines
     * Then untick all the new lining for braces.
     * For this project, we will be using the open braces on the same line.
     */
    #endregion

    #region Code Standards
    // -- Code Standards for Consistency

    // Example function 
    // -- NAMING CONVENTION: Always start with capitals, and each 'word' of the function starts with a capital letter
    public static void ExampleFunction() {
        return;
    }

    // Example use of get/set functionality
    // -- NAMING CONVENTION: Always use a capital first letter for these types of variables
    public int Total { get; set; }

    // Example plain old variable
    // -- NAMING CONVENTION: Always start with a lowercase letter. AVOID USING UNDERSCORE ( _ )!
    public int myTotal;

    // Example layout for a mixture of private and public variables
    // -- CONVENTION: privates first, then publics, separated by a space.
    // -- CONVENTION: group variables if applicable, as shown below.
    private int myInt, myInt2, myInt3;
    private string myName;

    public int theirInt;
    public string theirNames, herName, hisName;

    // Example use of if-else statements for curly braces.
    // -- CONVENTION: Always include curly braces for consistency!
    /*
     if ( doSomething() ){

     }
     else {
     
     }

    * THIS CONVENTION APPLIES TO: IF statements, Switch statements, try-catch-finally blocks, etc.
     */

    // Example namespace convention (we should be using them if we are going to be packaging features together)
    // -- NAMING CONVENTION: Always start with a capital letter, and each 'word' starts with one as well.
    /* 
    namespace DummySpace {

    }
    */

    /*
    * Use the TODO: [description] feature above functions as it will alert in the editor what is to be done.
    */

    /*
     * Always start comments with a white space, i.e. // My Comment
     */

    /*
     * Use #region and #endregion (as shown here) to group functionalities that belong together in a file.
     */
    #endregion

    #region Inspector Component Layout
    /*
     * In Unity, we attach components to objects.
     * By convention, the Scripts should be at the lowest point, UNLESS they serve as a 'tag' for the object, in which case they are right under the transform.
     * (The transform component is by default at the top, and should stay there.)
     * RigidBody as well as any Collider components should be one after the other, preferably the RigidBody followed by the Collider.
     * Any other relevant groupings should be considered.
     */
    #endregion
}