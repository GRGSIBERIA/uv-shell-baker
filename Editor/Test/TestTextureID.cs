﻿using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class TestTextureID {

    TextureID GetTextureID()
    {
        var go = LoadTestModel.Load(LoadTestModel.TestCase.Cube);
        var render = go.GetComponent<MeshFilter>();
        var mat = go.GetComponent<MeshRenderer>();
        return new TextureID(render.sharedMesh, mat.sharedMaterials);
    }


	[Test]
	public void TestTextureCount() {
        // Use the Assert class to test conditions.
        var textureid = GetTextureID();
        Assert.AreEqual(textureid.TextureCount, 3);
	}
    
}
