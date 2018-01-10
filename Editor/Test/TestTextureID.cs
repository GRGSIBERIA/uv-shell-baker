using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using System.Linq;
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

    bool HasContaints(string path, string[] texPathes)
    {
        if (texPathes.Select(x => path == x).Count() > 0)
            return true;
        return false;
    }

    [Test]
    public void TestTextureIDPathes()
    {
        var textureid = GetTextureID();
        var pathes = new string[] { "gray", "red", "blue" };
        foreach (var path in pathes)
        {
            Assert.AreEqual(true, HasContaints(path, textureid.TexturePathes));
        }
    }

    [Test]
    public void TestTextureIDFromTriangle()
    {
        var textureid = GetTextureID();
        for (int i = 0; i < 3; ++i)
        {
            for (int j = 0; j < 12; ++j)
            {
                Assert.AreEqual(textureid.TextureIDs[i * 12 + j], i);
            }
        }
    }
}
