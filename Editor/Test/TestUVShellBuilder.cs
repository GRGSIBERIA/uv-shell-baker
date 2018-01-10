using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class TestUVShellBuilder
{
    UVShellBuilder GetUVShellBuilder()
    {
        var go = LoadTestModel.Load(LoadTestModel.TestCase.Cube);
        var render = go.GetComponent<MeshFilter>();
        var network = new UVEdgeNetwork(render.sharedMesh.triangles);
        return new UVShellBuilder(network.UVNetwork, render.sharedMesh.vertexCount);
    }

	[Test]
	public void TestUVShellBuilderSimplePasses() {
        // Use the Assert class to test conditions.
        var builder = GetUVShellBuilder();

        
    }
}
