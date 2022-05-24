using UnityEngine;
using System;
using System.Collections;

/* This code is not originally mine.
 * Source : https://www.brechtos.com/hiding-or-disabling-inspector-properties-using-propertydrawers-within-unity-5/
 * I have made modifications to allow for a second condition
 * 
 * If at any point, more then 2 conditions are required, inform Scott (Scordi8#0001, me, who wrote this comment) and I'll re-write code for unlimited conditions.
 */

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property |
    AttributeTargets.Class | AttributeTargets.Struct, Inherited = true)]
public class ConditionalHideAttribute : PropertyAttribute
{
#if UNITY_EDITOR
    //The name of the bool fields that will be in control
    public string ConditionalSourceField = "";
    public string ConditionalSourceField2 = "";
    public string[] MultiConditionalField;
    public bool usemulti = false;
    //TRUE = Hide in inspector / FALSE = Disable in inspector 
    public bool HideInInspector = false;

    // Base ConditionalHideAttribute
    public ConditionalHideAttribute(string conditionalSourceField)
    {
        this.ConditionalSourceField = conditionalSourceField;
        this.HideInInspector = false;
    }

    // Overload #1, includes hide in inspecter bool. this overload is the typically used method
    public ConditionalHideAttribute(string conditionalSourceField, bool hideInInspector)
    {
        this.ConditionalSourceField = conditionalSourceField;
        this.HideInInspector = hideInInspector;
    }

    // Overload #2, adds a second conditional field, otherwise identical to overload #1
    public ConditionalHideAttribute(string conditionalSourceField, string conditionalSourceField2, bool hideInInspector)
    {
        this.ConditionalSourceField = conditionalSourceField;
        this.ConditionalSourceField2 = conditionalSourceField2;
        this.HideInInspector = hideInInspector;
    }

    // Overload #3, uses a string[] instead of set values. can use any amount
    public ConditionalHideAttribute(string[] ConditionalSourceFields, bool hideInInspector)
    {

        this.MultiConditionalField = ConditionalSourceFields;
        this.usemulti = true;
        this.HideInInspector = hideInInspector;
    }
#endif
}