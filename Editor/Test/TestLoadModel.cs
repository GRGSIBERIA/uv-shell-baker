using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class TestLoadModel
{
    [Test]
    public void LoadModel()
    {
        var go = LoadTestModel.Load(LoadTestModel.TestCase.Cube);
        Assert.AreEqual("cube", go.name);
    }
}
