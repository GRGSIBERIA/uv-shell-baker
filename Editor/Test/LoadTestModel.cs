using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LoadTestModel
{
    public static GameObject Load(string path)
    {
        return AssetDatabase.LoadAssetAtPath<GameObject>(path);
    }
}