using System.Collections.Generic;
using UnityEngine;

public static class UtilManager
{
    private static Dictionary<string, GameObject> objects = new Dictionary<string, GameObject>();

    public static void AddObject(string name, GameObject obj)
    {
        objects.Add(name, obj);
    }

    public static GameObject GetObject(string name)
    {
        if (objects.ContainsKey(name))
        {
            return objects[name];
        }
        else
        {
            Debug.LogWarning($"Object with name {name} not found in collection.");
            return null;
        }
    }


    public static string GetSceneName(this SceneID sceneID)
    {
        switch (sceneID)
        {
            case SceneID.StartScene:
                return "StartScene";
            case SceneID.MenuScene:
                return "MenuScene";
            case SceneID.GameScene:
                return "GameScene";
            default:
                return string.Empty;
        }
    }



}
