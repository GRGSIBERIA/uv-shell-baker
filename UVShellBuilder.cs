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
    int SearchNotUsedIndexForNextShell(int[] usedUVs)
    {
        for (int i = 0; i < usedUVs.Length; ++i)
        {
            if (usedUVs[i] == 0)
            {
                ShellNetwork.Add(new List<int>());
                return i;
            }
        }
        return -1;
    }

    /// <summary>
    /// 幅優先でUVの島を探索する
    /// </summary>
    /// <param name="usedUVs"></param>
    /// <param name="network"></param>
    /// <param name="start"></param>
    void BreadthFirstSearch(int[] usedUVs, List<List<int>> network, int start)
    {
        // UV点を踏んだので使用する
        usedUVs[start] = 1;
        ShellNetwork.Last().Add(start);

        var ends = network[start];
        foreach (var end in ends)
        {
            // 遷移先のUVが使用済みなら無視する
            if (usedUVs[end] == 1)
                continue;
            BreadthFirstSearch(usedUVs, network, end);  // 未使用の末端に移動
        }
    }

    public UVShellBuilder(List<List<int>> network, int vertexCount)
    {
        int[] usedUVs = Enumerable.Repeat<int>(0, vertexCount).ToArray();
        this.ShellNetwork = new List<List<int>>();

        int nextId;
        while ((nextId = SearchNotUsedIndexForNextShell(usedUVs)) != -1)    // 次の島を探索する
        {
            BreadthFirstSearch(usedUVs, network, nextId);
            // 恐らく島の探索は終了したので次の島を探索する
        }
    }
}
