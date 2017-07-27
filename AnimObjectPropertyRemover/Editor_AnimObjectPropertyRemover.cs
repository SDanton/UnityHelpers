#if UNITY_EDITOR

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEditor;
using System.Reflection;

[CustomEditor(typeof(AnimObjectPropertyRemover))]
public class Editor_AnimObjectPropertyRemover : Editor
{      
    AnimObjectPropertyRemover _updater;

    AnimationClip[] _clips;

    bool _clearConsole;

    void OnEnable ()
    {
        if (EditorApplication.isPlaying) return;

        EditorApplication.update += Update;
        Debug.Log("OnEnable");

        wAnimationWindowHelper.init();
    }

    void Update ()
    {
        if (EditorApplication.isPlaying) return;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        _updater = target as AnimObjectPropertyRemover;

        EditorGUI.BeginDisabledGroup(EditorApplication.isPlaying);

        _clearConsole = EditorGUILayout.Toggle("Clear Console", _clearConsole);
        
        if (GUILayout.Button("(1) Test Path"))
        {
            processCurves (false);
        }

        if (GUILayout.Button("(2) See Property Names"))
        {
            processCurves (false, true);
        }        

        if (GUILayout.Button("(2) Update Curves"))
        {
            processCurves (true);
        }

        EditorGUI.EndDisabledGroup();
    }

    void processCurves (bool setCurve = false, bool justSeeProperties = false)
    {
        if (_updater.Controller != null && _updater.RelativePathToObject != "")
        {

            if (_clearConsole)
            {
                Debug.ClearDeveloperConsole();
            }
            
            _clips = _updater.Controller.animationClips;

            Debug.Log ("Clip count: " + _clips.Length);

            int properties = 0;

            for (int i = 0; i < _clips.Length; i++)
            {
                EditorCurveBinding[] bindings = AnimationUtility.GetCurveBindings (_clips[i]);

                foreach (EditorCurveBinding binding in bindings)
                {                    
                    if (binding.path == _updater.RelativePathToObject)
                    {
                        if (justSeeProperties == true)
                        {
                            Debug.Log ("-------- " + _clips[i].ToString() + " / " + binding.propertyName);

                            properties++;
                        }
                        else if (binding.propertyName.Contains ("m_LocalPosition"))
                        {
                            Debug.Log ("-------- " + _clips[i].ToString() + " / " + binding.propertyName);

                            properties++;

                            if (setCurve == true)
                            {
                                AnimationUtility.SetEditorCurve(_clips[i], binding, null);
                            }
                        }
                    }
                }
            }

            Debug.Log ("Properties: " + properties);
        }
    }
}

#endif
