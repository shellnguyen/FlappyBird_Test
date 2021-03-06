﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class ScriptableSingleton<T> : ScriptableObject where T : ScriptableObject
{
    protected static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
#if UNITY_EDITOR
                if (!_instance)
                {
                    string[] configsGUIDs = UnityEditor.AssetDatabase.FindAssets("t:" + typeof(T).Name);
                    if (configsGUIDs.Length > 0)
                    {
                        _instance = UnityEditor.AssetDatabase.LoadAssetAtPath<T>(UnityEditor.AssetDatabase.GUIDToAssetPath(configsGUIDs[0]));
                    }
                }
#else
                var type = typeof(T);
                var instances = Resources.FindObjectsOfTypeAll<T>();
                _instance = instances.FirstOrDefault();
                if (_instance == null)
                {
                    Debug.Log("[ScriptableSingleton] No instance found!");
                }
                else if (instances.Length > 1)
                {
                    Debug.Log("[ScriptableSingleton] Multiple instances found!");
                }
                else
                {
                    Debug.Log("[ScriptableSingleton] An instance was found!");
                }
#endif
            }

            return _instance;
        }
    }
}
