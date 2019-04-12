using UnityEngine;
using UnityEditor;
using System.Text;

[InitializeOnLoad]
public class LabelIconMatcher
{
    private static Vector2 offset = new Vector2(20, 2);
    private static Vector2 iconOffset = new Vector2 (3, 2);
    private static Vector2 padding = new Vector2 (50, 0);
    
    static LabelIconMatcher()
    {
        EditorApplication.hierarchyWindowItemOnGUI += HierarchyWindowItemOnGUI;
    }

    static Color getColourForTooltip(string label)
    {
        Color ret = Color.white;

        switch (label)
        {
            // gray
            case "sv_label_0":
                ret = GLB.ColorFromHex ("A58E7A");
                break;

            // blue
            case "sv_label_1":
                ret = GLB.ColorFromHex("609BE2");
                break;

            // teal
            case "sv_label_2":
                ret = GLB.ColorFromHex("609BE2");
                break;

            // green
            case "sv_label_3":
                ret = GLB.ColorFromHex("4FE247");
                break;

            // yellow
            case "sv_label_4":
                ret = GLB.ColorFromHex("F4E33E");
                break;

            // orange
            case "sv_label_5":
                ret = GLB.ColorFromHex("F28840");
                break;

            // red
            case "sv_label_6":
                ret = GLB.ColorFromHex("DD4A4A");
                break;

            // purple
            case "sv_label_7":
                ret = GLB.ColorFromHex("B066D3");
                break;
        }

        return ret;
    }

    static void HierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
    {
        GameObject gameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

        Color fontColor = Color.red;
        FontStyle style = FontStyle.Normal;
        Color backgroundColor = new Color(0.22f, 0.22f, 0.22f); // dark theme colour

        var obj = EditorUtility.InstanceIDToObject(instanceID);
        if (obj != null)
        {
            var prefabType = PrefabUtility.GetPrefabType(obj);
            if (prefabType == PrefabType.PrefabInstance)
            {                
                GUIContent content = EditorGUIUtility.ObjectContent (obj, null);
                fontColor = getColourForTooltip (content.image.name);

                if (gameObject.name == "Father")
                {
                    fontColor = Color.cyan;
                    style = FontStyle.Bold;
                }

                Rect offsetRect = new Rect(selectionRect.position + offset, selectionRect.size + padding);
                EditorGUI.DrawRect(selectionRect, backgroundColor);                

                EditorGUI.LabelField(offsetRect, obj.name, new GUIStyle()
                {
                    normal = new GUIStyleState() { textColor = fontColor },
                    fontStyle = style
                }
                );

                Rect iconRect = new Rect(selectionRect.position + iconOffset, new Vector2(12, 12));
                EditorGUI.DrawRect(iconRect, fontColor);
            }
            else if (gameObject.name.Contains("++"))
            {
                fontColor = Color.white;
                style = FontStyle.Bold;

                Rect offsetRect = new Rect(selectionRect.position + offset, selectionRect.size + padding);
                EditorGUI.DrawRect(selectionRect, backgroundColor);
                EditorGUI.LabelField(offsetRect, obj.name, new GUIStyle()
                {
                    normal = new GUIStyleState() { textColor = fontColor },
                    fontStyle = style
                }
                );
            }
        }
    }
}
