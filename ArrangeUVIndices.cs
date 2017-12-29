using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ArrangeUVIndices
{
    static int CountTexture(Material[] materials)
    {
        Dictionary<string, int> texturePathes = new Dictionary<string, int>();
        foreach (var mat in materials)
        {
            var tex = AssetDatabase.GetAssetPath(mat.mainTexture);
            texturePathes[tex] = 1;
        }

        return texturePathes.Count;
    }


    public static void Arrange(Material[] materials)
    {
        int count = CountTexture(materials);

    }

}
