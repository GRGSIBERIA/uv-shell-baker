using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// UVのネットワークからUVシェルを構築する
/// </summary>
public class UVShellBuilder
{
    public readonly List<List<int>> ShellNetwork;

    /// <summary>
    /// 未使用のUVのインデックスを探す
    /// </summary>
    /// <param name="usedUVs"></param>
    /// <returns></returns>
    int SearchNotUsedIndex(int[] usedUVs)
    {
        return usedUVs.Where(id => id == 0).First();
    }

    /// <summary>
    /// 幅優先でUVの島を探索する
    /// </summary>
    /// <param name="usedUVs"></param>
    /// <param name="network"></param>
    /// <param name="start"></param>
    void BreadthFirstSearch(int[] usedUVs, List<List<int>> network, int start)
    {
        var ends = network[start];

        foreach (var end in ends)
        {
            // 遷移先のUVが使用済みなら無視する
            if (usedUVs[end] == 1)
                continue;
            usedUVs[end] = 1;
        }
        
    }

    public UVShellBuilder(List<List<int>> network, int vertexCount)
    {
        int[] usedUVs = Enumerable.Repeat<int>(0, vertexCount).ToArray();
        this.ShellNetwork = new List<List<int>>();

        BreadthFirstSearch(usedUVs, network, 0);
    }
}
