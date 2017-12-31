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

    public List<List<int>> InitNetwork(int[] triangles)
    {
        List<List<int>> network = Enumerable.Repeat<List<int>>(new List<int>(), triangles.Length / 3).ToList();
        
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

    public UVEdgeNetwork(int[] triangles)
    {
        this.UVNetwork = this.InitNetwork(triangles);
    }
}
