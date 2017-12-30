using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class TestTextureID {

	[Test]
	public void TestTextureIDSimplePasses() {
		// Use the Assert class to test conditions.
	}

    SkinnedMeshRenderer GetSkinnedMeshRenderer(GameObject utc, string goPath)
    {
        return utc.transform.Find(goPath).gameObject.GetComponent<SkinnedMeshRenderer>();
    }

    [Test]
    public void TestTexturedMaterial()
    {
        var utc = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Models/UnityChan/Prefabs/unitychan.prefab");
        var mesh = GetSkinnedMeshRenderer(utc, "mesh_root/hair_front");
        Assert.That(mesh.sharedMaterials.Length, Is.EqualTo(1));
    }

	// A UnityTest behaves like a coroutine in PlayMode
	// and allows you to yield null to skip a frame in EditMode
	[UnityTest]
	public IEnumerator TestTextureIDWithEnumeratorPasses() {
		// Use the Assert class to test conditions.
		// yield to skip a frame
		yield return null;
	}
}
