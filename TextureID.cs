using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

public class TextureID
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

    string[] InitTexturePath(Material[] materials)
    {
        return materials.Select(mat => AssetDatabase.GetAssetPath(mat.mainTexture)).Distinct().ToArray();
    }

    /// <summary>
    /// ポリゴンごとのテクスチャのIDを割り振る
    /// </summary>
    /// <param name="mesh"></param>
    /// <param name="materials"></param>
    /// <returns></returns>
    int[] MakeTextureIDsForTriangle(Mesh mesh, Material[] materials)
    {
        var texPathDict = new Dictionary<string, int>();
        var submeshCount = mesh.subMeshCount;
        int texCount = 0;
        int uvCount = 0;
        int triangleCount = mesh.triangles.Length / 3;

        var ids = new int[mesh.triangles.Length / 3];

        for (var submesh = 0; submesh < submeshCount; ++submesh)
        {
            var submeshIds = mesh.GetIndices(submesh);

            // UVごとにテクスチャを分ける
            var texPath = AssetDatabase.GetAssetPath(materials[submesh].mainTexture);
            if (!texPathDict.ContainsKey(texPath))
                texPathDict[texPath] = texCount++;

            for (int i = 0; i < triangleCount; ++i)
            {
                ids[uvCount++] = texPathDict[texPath];
            }
        }

        return ids;
    }

    /// <summary>
    /// ポリゴンごとに対応したテクスチャIDを割り振る
    /// </summary>
    /// <param name="mesh"></param>
    /// <param name="materials"></param>
    /// <returns></returns>
    int[] Make(Mesh mesh, Material[] materials)
    {
        // UVごとにテクスチャIDを割り振る
        int[] triangle2textureIds = MakeTextureIDsForTriangle(mesh, materials);
        return triangle2textureIds;
    }

    /// <summary>
    /// UVに対応したTextureID
    /// </summary>
    public int[] TextureIDs { get; private set; }

    /// <summary>
    /// モデルに含まれる一意なテクスチャの数を返す
    /// </summary>
    public int TextureCount { get; private set; }

    /// <summary>
    /// 一意なテクスチャのパス名を返す
    /// </summary>
    public string[] TexturePathes { get; private set; }

    public TextureID(Mesh mesh, Material[] materials)
    {
        this.TextureIDs = this.Make(mesh, materials);
        this.TextureCount = this.InitTextureCount(materials);
        this.TexturePathes = this.InitTexturePath(materials);
    }
}
