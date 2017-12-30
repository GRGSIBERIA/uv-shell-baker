﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;


public class UV2TextureID
{
    /// <summary>
    /// ベイクするテクスチャの数を返す
    /// </summary>
    /// <param name="materials"></param>
    /// <returns></returns>
    int InitTextureCount(Material[] materials)
    {
        return materials.Select(mat => AssetDatabase.GetAssetPath(mat.mainTexture)).Distinct().Count();
    }

    /// <summary>
    /// UVに対してテクスチャのIDを割り振る
    /// </summary>
    /// <param name="mesh"></param>
    /// <param name="materials"></param>
    /// <returns></returns>
    int[] MakeTextureIDs(Mesh mesh, Material[] materials)
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

    /// <summary>
    /// UVごとに対応したテクスチャIDを割り振る
    /// </summary>
    /// <param name="mesh"></param>
    /// <param name="materials"></param>
    /// <returns></returns>
    int[] Make(Mesh mesh, Material[] materials)
    {
        // UVごとにテクスチャIDを割り振る
        int[] uv2textureIds = MakeTextureIDs(mesh, materials);
        return uv2textureIds;
    }

    /// <summary>
    /// UVに対応したTextureID
    /// </summary>
    public readonly int[] TextureIDs;

    /// <summary>
    /// モデルに含まれる一意なテクスチャの数を返す
    /// </summary>
    public readonly int TextureCount;

    public UV2TextureID(Mesh mesh, Material[] materials)
    {
        this.TextureIDs = this.Make(mesh, materials);
        this.TextureCount = this.InitTextureCount(materials);
    }
}
