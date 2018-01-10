using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// UVのネットワークからUVシェルを構築する
/// </summary>
public class UVShellBuilder
{
    /// <summary>
    /// 島番号->UVID
    /// </summary>
    public List<List<int>> ShellNetwork { get; private set; }

    /// <summary>
    /// UVID->島番号
    /// </summary>
    public int[] AssignedUVToShell { get; private set; }

    /// <summary>
    /// 島の数
    /// </summary>
    public int ShellCount { get; private set; }

    /// <summary>
    /// 未使用のUVのインデックスを探す
    /// </summary>
    /// <param name="usedUVs"></param>
    /// <returns></returns>
    int SearchNotUsedIndexForNextShell(int[] usedUVs, List<List<int>> shellNetwork)
    {
        for (int i = 0; i < usedUVs.Length; ++i)
        {
            if (usedUVs[i] == 0)
            {
                shellNetwork.Add(new List<int>());
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
    /// <param name="startPoint"></param>
    void BreadthFirstSearch(int[] usedUVs, List<List<int>> network, List<List<int>> shellNetwork, int startPoint)
    {
        // UV点を踏んだので使用する
        usedUVs[startPoint] = 1;
        shellNetwork.Last().Add(startPoint);

        var endPoints = network[startPoint];
        foreach (var endPoint in endPoints)
        {
            // 遷移先のUVが使用済みなら無視する
            if (usedUVs[endPoint] == 1)
                continue;
            BreadthFirstSearch(usedUVs, network, shellNetwork, endPoint);  // 未使用の末端に移動
        }
    }

    List<List<int>> BuildShellNetwork(List<List<int>> network, int uvCount)
    {
        int[] usedUVs = Enumerable.Repeat<int>(0, uvCount).ToArray();
        var shellNetwork = new List<List<int>>();

        int nextId;
        while ((nextId = SearchNotUsedIndexForNextShell(usedUVs, shellNetwork)) != -1)    // 次の島を探索する
        {
            BreadthFirstSearch(usedUVs, network, shellNetwork, nextId);
            // 恐らく島の探索は終了したので次の島を探索する
        }
        return shellNetwork;
    }

    int[] BuildAssignedShell(int uvCount)
    {
        int[] uv2shell = new int[uvCount];

        for (int i = 0; i < this.ShellNetwork.Count; ++i)
        {
            for (int j = 0; j < this.ShellNetwork[i].Count; ++j)
                uv2shell[this.ShellNetwork[i][j]] = i;
        }

        return uv2shell;
    }

    int InitShellCount()
    {
        return this.ShellNetwork.Count;
    }

    public UVShellBuilder(List<List<int>> network, int uvCount)
    {
        this.ShellNetwork = BuildShellNetwork(network, uvCount);
        this.AssignedUVToShell = BuildAssignedShell(uvCount);
        this.ShellCount = InitShellCount();
    }
}
