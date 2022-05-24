using UnityEngine;
using UnityEditor;

/* This code is not originally mine.
 * Source : https://www.brechtos.com/hiding-or-disabling-inspector-properties-using-propertydrawers-within-unity-5/
 * I have made modifications to allow for a second condition
 * 
 * If at any point, more then 2 conditions are required, inform Scott (Scordi8#0001, me, who wrote this comment) and I'll re-write code for unlimited conditions.
 */

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(ConditionalHideAttribute))]
public class ConditionalHidePropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        ConditionalHideAttribute condHAtt = (ConditionalHideAttribute)attribute;
        bool enabled = GetConditionalHideAttributeResult(condHAtt, property);

        bool wasEnabled = GUI.enabled;
        GUI.enabled = enabled;
        if (!condHAtt.HideInInspector || enabled)
        {
            EditorGUI.PropertyField(position, property, label, true);
        }

        GUI.enabled = wasEnabled;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        ConditionalHideAttribute condHAtt = (ConditionalHideAttribute)attribute;
        bool enabled = GetConditionalHideAttributeResult(condHAtt, property);

        if (!condHAtt.HideInInspector || enabled)
        {
            return EditorGUI.GetPropertyHeight(property, label);
        }
        else
        {
            return -EditorGUIUtility.standardVerticalSpacing;
        }
    }

    private bool GetConditionalHideAttributeResult(ConditionalHideAttribute condHAtt, SerializedProperty property)
    {
        bool enabled = true;
        string propertyPath = property.propertyPath; //returns the property path of the property we want to apply the attribute to
        
        string conditionPath = propertyPath.Replace(property.name, condHAtt.ConditionalSourceField); //changes the path to the conditionalsource property path
        string conditionPath2 = propertyPath.Replace(property.name, condHAtt.ConditionalSourceField2);

        if (condHAtt.usemulti)
        {
            bool use = true;
            SerializedProperty loopproperties;
            foreach (string arg in condHAtt.MultiConditionalField)
            {
                string argpath = propertyPath.Replace(property.name, arg);
                loopproperties = property.serializedObject.FindProperty(argpath);
                use = use && loopproperties.boolValue;
            }
            enabled = use;
        }
        else

        {
            SerializedProperty sourcePropertyValue = property.serializedObject.FindProperty(conditionPath);
            SerializedProperty sourcePropertyValue2 = property.serializedObject.FindProperty(conditionPath2);

            if (sourcePropertyValue != null)
            {
                enabled = sourcePropertyValue.boolValue;
            }
            else
            {
                Debug.LogWarning("Attempting to use a ConditionalHideAttribute but no matching SourcePropertyValue found in object: " + condHAtt.ConditionalSourceField);
            }
            if (sourcePropertyValue2 != null)
            {
                enabled = sourcePropertyValue2.boolValue && enabled;
            }
        }
        return enabled;
    }
}
#endif