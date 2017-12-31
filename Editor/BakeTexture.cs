using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Drawing;

public class BakeTexture
{
    public List<Image> Textures { get; private set; }

    List<Image> CreateTextures(string[] texturePathes)
    {
        List<Image> textures = new List<Image>();
        foreach (var path in texturePathes)
        {
            var source = new Bitmap(path);
            var graph = System.Drawing.Graphics.FromImage(source);
            graph.FillRectangle(Brushes.Black, new Rectangle(0, 0, source.Width, source.Height));
            graph.Dispose();
            textures.Add(source);
        }
        return textures;
    }

    public BakeTexture(Vector2[] UVpos, int[] triangles, TextureID textureId, UVShellBuilder shell)
    {
        this.Textures = CreateTextures(textureId.TexturePathes);
    }
}
