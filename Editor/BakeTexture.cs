using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Drawing;

public class BakeTexture
{
    List<Image> Textures;

    List<Image> CreateTextures(string[] texturePathes)
    {
        List<Image> textures = new List<Image>();
        foreach (var path in texturePathes)
        {
            var img = new Bitmap(path);
            textures.Add(img);
        }
        return textures;
    }

    public BakeTexture(Vector2[] UVpos, int[] triangles, TextureID textureId, UVShellBuilder shell)
    {
        this.Textures = CreateTextures(textureId.TexturePathes);
    }
}
