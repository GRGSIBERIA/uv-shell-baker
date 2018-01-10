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
        var network = new UVEdgeNetwork(render.sharedMesh.triangles, render.sharedMesh.vertexCount);
        return new UVShellBuilder(network.UVNetwork, render.sharedMesh.uv.Length);
    }

	[Test]
	public void TestUVShellBuilderShellCount() {
        var builder = GetUVShellBuilder();

        Assert.AreEqual(builder.ShellCount, 18);
    }
}
