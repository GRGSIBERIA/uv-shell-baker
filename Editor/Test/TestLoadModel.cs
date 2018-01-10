using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class TestLoadModel
{
    GameObject Load()
    {
        return LoadTestModel.Load("Assets/Scripts/uv-shell-baker/Editor/Test/Models/cube.fbx");
    }

    [Test]
    public void LoadModel()
    {
        var go = Load();
        Assert.AreEqual("cube", go.name);
    }
}
