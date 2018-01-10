using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LoadTestModel
{
    public enum TestCase
    {
        Cube
    }

    static string GetPath(TestCase testcase)
    {
        switch (testcase)
        {
            case TestCase.Cube:
                return "Assets/Scripts/uv-shell-baker/Editor/Test/Models/cube.fbx";
            default:
                throw new System.Exception("Unknown TestCase");
        }
    }

    public static GameObject Load(TestCase testcase)
    {
        return AssetDatabase.LoadAssetAtPath<GameObject>(GetPath(testcase));
    }
}