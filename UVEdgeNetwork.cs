using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UVEdgeNetwork
{
    /// <summary>
    /// 始点と終点の頂点番号で構成されるエッジ，重複あり
    /// p_k \in {p_n, p_n+1, ..., p_n+m}
    /// </summary>
    public readonly List<List<int>> UVNetwork;

    public List<List<int>> InitNetwork(int[] triangles, int vertexCount)
    {
        List<List<int>> network = new List<List<int>>(vertexCount);

        for (int i = 0; i < vertexCount; ++i)
        {
            network.Add(new List<int>());
        }

        int triangleCount = triangles.Length / 3;
        for (int i = 0; i < triangleCount; ++i)
        {
            int a = i;
            int b = i + 1;
            int c = i + 2;

            network[a].Add(b);
            network[b].Add(c);
        }
        return network;
    }

    public UVEdgeNetwork(Mesh mesh)
    {
        this.UVNetwork = this.InitNetwork(mesh.triangles, mesh.vertexCount);
    }
}
