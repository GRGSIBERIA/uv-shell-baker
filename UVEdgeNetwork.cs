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
    public List<List<int>> UVNetwork { get; private set; }

    public List<List<int>> InitNetwork(int[] triangles, int vertexCount)
    {
        int triangleLength = triangles.Length / 3;
        List<List<int>> network = Enumerable.Repeat<List<int>>(new List<int>(), vertexCount).ToList();

        for (int i = 0; i < triangleLength; ++i)
        {
            int a = triangles[i * 3];
            int b = triangles[i * 3 + 1];
            int c = triangles[i * 3 + 2];
            
            network[a].Add(b);
            network[b].Add(c);
        }
        return network;
    }

    public UVEdgeNetwork(int[] triangles, int vertexCount)
    {
        this.UVNetwork = this.InitNetwork(triangles, vertexCount);
    }
}
