using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

public class ArrangeUVIndices
{
    /// <summary>
    /// ベイクするテクスチャの数を返す
    /// </summary>
    /// <param name="materials"></param>
    /// <returns></returns>
    static int CountTexture(Material[] materials)
    {
        return materials.Select(mat => AssetDatabase.GetAssetPath(mat.mainTexture)).Distinct().Count();
    }

    /// <summary>
    /// UVに対してテクスチャのIDを割り振る
    /// </summary>
    /// <param name="mesh"></param>
    /// <param name="materials"></param>
    /// <returns></returns>
    static int[] MakeTextureIDs(Mesh mesh, Material[] materials)
    {
        var texPathDict = new Dictionary<string, int>();
        var submeshCount = mesh.subMeshCount;
        int texCount = 0;
        int uvCount = 0;

        var ids = new int[mesh.uv.Count()];

        for (var submesh = 0; submesh < submeshCount; ++submesh)
        {
            var submeshIds = mesh.GetIndices(submesh);

            // UVごとにテクスチャを分ける
            var texPath = AssetDatabase.GetAssetPath(materials[submesh].mainTexture);
            if (!texPathDict.ContainsKey(texPath))
                texPathDict[texPath] = texCount++;

            // UVごとにテクスチャのIDを割り振る
            foreach (var id in submeshIds)
            {
                ids[uvCount++] = texPathDict[texPath];
            }
        }

        return ids;
    }

    public static void Arrange(SkinnedMeshRenderer renderer)
    {
        // UVごとにテクスチャIDを割り振る
        int[] uv2textureIds = MakeTextureIDs(renderer.sharedMesh, renderer.sharedMaterials);

        // テクスチャIDごとにUVShellIDを焼く
        int textureCount = CountTexture(renderer.sharedMaterials);
        for (int i = 0; i < textureCount; ++i)
        {
            
        }
    }

}
