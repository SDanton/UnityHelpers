using UnityEngine;
using UnityEditor;
using System.Text;

[InitializeOnLoad]
public class LabelIconMatcher
{
    private static Vector2 offset = new Vector2(20, 2);
    private static Vector2 iconOffset = new Vector2(3, 2);
    private static Vector2 padding = new Vector2(50, 0);

    private static Color UI_base = GLB.ColorFromHex("383838");
    private static Color UI_hover = GLB.ColorFromHex("303030");
    private static Color UI_selected = GLB.ColorFromHex("3E5F96");

    static LabelIconMatcher()
    {
        EditorApplication.hierarchyWindowItemOnGUI += HierarchyWindowItemOnGUI;
    }
    
    static Color ColorFromHex(string hex, float alpha = 1.0f)
    {
        Color newColor = new Color(StrToFloat(hex.Substring(0, 2)), StrToFloat(hex.Substring(2, 2)), StrToFloat(hex.Substring(4, 2)), alpha);

        return newColor;
    }
    
    static float StrToFloat(string str)
    {
        return ((float)int.Parse(str, System.Globalization.NumberStyles.HexNumber) / 255f);
    }

    static Color getColourForTooltip(string label, bool selected = false)
    {
        Color ret = Color.white;

        if (selected == false)
        {
            switch (label)
            {
                // gray
                case "sv_label_0":
                    ret = ColorFromHex("A58E7A");
                    break;

                // blue
                case "sv_label_1":
                    ret = ColorFromHex("609BE2");
                    break;

                // teal
                case "sv_label_2":
                    ret = ColorFromHex("50D6A5");
                    break;

                // green
                case "sv_label_3":
                    ret = ColorFromHex("4FE247");
                    break;

                // yellow
                case "sv_label_4":
                    ret = ColorFromHex("F4E33E");
                    break;

                // orange
                case "sv_label_5":
                    ret = ColorFromHex("F28840");
                    break;

                // red
                case "sv_label_6":
                    ret = ColorFromHex("DD4A4A");
                    break;

                // purple
                case "sv_label_7":
                    ret = ColorFromHex("B066D3");
                    break;
            }
        }

        return ret;
    }

    private static int _hoveredInstance;

    static Color getBackgroundColorForMouseMove(int instanceID, Rect selectionRect)
    {
        Color ret = UI_base;

        if (instanceID == _hoveredInstance)
        {
            ret = UI_hover;
        }

        var current = Event.current;

        switch (current.type)
        {
            case EventType.MouseDown:

                if (selectionRect.Contains(current.mousePosition))
                {
                    ret = UI_selected;
                }

                break;

            case EventType.Layout:

                if (selectionRect.Contains(current.mousePosition))
                {
                    if (_hoveredInstance != instanceID)
                    {
                        _hoveredInstance = instanceID;
                        ret = UI_hover;
                    }
                }
                else
                {
                    if (_hoveredInstance == instanceID)
                    {
                        _hoveredInstance = 0;
                    }
                }

                break;
        }

        return ret;
    }

    static void HierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
    {
        GameObject gameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

        Color fontColor = Color.red;
        Color iconColor = Color.white;
        FontStyle style = FontStyle.Normal;
        Color backgroundColor = UI_base;
        bool selected = false;

        var obj = EditorUtility.InstanceIDToObject(instanceID);
        if (obj != null)
        {
            for (int i = 0; i < UnityEditor.Selection.gameObjects.Length; i++)
            {
                if (UnityEditor.Selection.gameObjects[i] == gameObject)
                {
                    backgroundColor = UI_selected;
                    selected = true;
                    break;
                }
            }

            if (selected == false)
            {
                backgroundColor = getBackgroundColorForMouseMove(instanceID, selectionRect);
            }

            var prefabType = PrefabUtility.GetPrefabType(obj);
            if (prefabType == PrefabType.PrefabInstance)
            {
                // Texture2D tex = PrefabUtility.GetIconForGameObject (gameObject);

                GUIContent content = EditorGUIUtility.ObjectContent(obj, null);

                iconColor = getColourForTooltip(content.image.name, false);
                fontColor = getColourForTooltip(content.image.name, selected);                

                Rect offsetRect = new Rect(selectionRect.position + offset, selectionRect.size + padding);
                EditorGUI.DrawRect(selectionRect, backgroundColor);

                EditorGUI.LabelField(offsetRect, obj.name, new GUIStyle()
                {
                    normal = new GUIStyleState() { textColor = fontColor },
                    fontStyle = style
                }
                );

                Rect iconRect = new Rect(selectionRect.position + iconOffset, new Vector2(12, 12));
                EditorGUI.DrawRect(iconRect, iconColor);
            }
        }
    }
}
