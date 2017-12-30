using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UVEdgeNetwork
{
    /// <summary>
    /// エッジの始点から終点までの
    /// </summary>
    public Dictionary<int, List<int>> Network { get; private set; }



    public UVEdgeNetwork(Mesh mesh)
    {
        this.Network = new Dictionary<int, List<int>>(mesh.vertexCount);

        int[] triangles = mesh.triangles;
        int triangleCount = mesh.vertexCount / 3;
        for (int i = 0; i < triangleCount; ++i)
        {

        }
    }
}
