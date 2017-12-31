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

    void CheckShellAndTextureOnTriangle(UVShellBuilder shell, TextureID textureId, int[] id)
    {
        for (int i = 0; i < 2; ++i)
        {
            if (shell.AssignedUVToShell[id[i]] == shell.AssignedUVToShell[id[i + 1]])
                throw new System.Exception("Don't match a shell on a triangle!!! why???");
            if (textureId.TextureIDs[id[i]] == textureId.TextureIDs[id[i + 1]])
                throw new System.Exception("Don't match a texture on a triangle!!! why???");
        }
    }

    void PaintingShell(Vector2[] UVpos, int[] triangles, UVShellBuilder shell, TextureID textureId)
    {
        for (int i = 0; i < triangles.Length; i += 3)
        {
            int[] id = { triangles[i], triangles[i + 1], triangles[i + 2] };

            CheckShellAndTextureOnTriangle(shell, textureId, id);


        }
    }

    public BakeTexture(Vector2[] UVpos, int[] triangles, TextureID textureId, UVShellBuilder shell)
    {
        this.Textures = CreateTextures(textureId.TexturePathes);

        
    }
}
