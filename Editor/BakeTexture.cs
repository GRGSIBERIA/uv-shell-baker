using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Drawing;

public class BakeTexture
{
    /// <summary>
    /// ベイク済みのテクスチャ
    /// </summary>
    public List<Bitmap> Textures { get; private set; }

    /// <summary>
    /// コンストラクタ以外で使用してはいけない
    /// </summary>
    List<System.Drawing.Graphics> graphes;

    ColorPalette palette;

    List<Bitmap> CreateTextures(string[] texturePathes)
    {
        List<Bitmap> textures = new List<Bitmap>();
        graphes = new List<System.Drawing.Graphics>();      // あとでDisposeしないといけない
        foreach (var path in texturePathes)
        {
            var source = new Bitmap(path);
            var graph = System.Drawing.Graphics.FromImage(source);
            graph.FillRectangle(Brushes.Black, new Rectangle(0, 0, source.Width, source.Height));
            graphes.Add(graph);
            textures.Add(source);
        }
        return textures;
    }

    /// <summary>
    /// Graphicsの解放処理，graphesは二度と使用しない
    /// </summary>
    void DisposeGraphics()
    {
        foreach (var g in this.graphes)
            g.Dispose();
        graphes = null;
    }

    /// <summary>
    /// 島とテクスチャIDがポリゴンに紐付いていないケースをチェックする
    /// </summary>
    /// <param name="shell"></param>
    /// <param name="textureId"></param>
    /// <param name="id"></param>
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

    void DrawTriangle(int[] id, Vector2[] uvpos, Size texSize, System.Drawing.Graphics graph)
    {
        Point[] realpos = {
            new Point((int)(uvpos[id[0]].x * texSize.Width), (int)(uvpos[id[0]].y * texSize.Height)),
            new Point((int)(uvpos[id[1]].x * texSize.Width), (int)(uvpos[id[1]].y * texSize.Height)),
            new Point((int)(uvpos[id[2]].x * texSize.Width), (int)(uvpos[id[2]].y * texSize.Height)) };

        graph.FillPolygon(Brushes.Black, realpos);      // 島ごとに色を変えたい！
    }

    void PaintingShell(Vector2[] UVpos, int[] triangles, UVShellBuilder shell, TextureID textureId)
    {
        for (int i = 0; i < triangles.Length; i += 3)
        {
            int[] id = { triangles[i], triangles[i + 1], triangles[i + 2] };

            CheckShellAndTextureOnTriangle(shell, textureId, id);

            // id[0]が通用するのは，三角形のすべてで値が一致しているから
            DrawTriangle(id, UVpos, Textures[id[0]].Size, graphes[id[0]]);
        }
    }

    public BakeTexture(Vector2[] UVpos, int[] triangles, TextureID textureId, UVShellBuilder shell)
    {
        this.palette = new ColorPalette(shell.ShellCount, .7f, .7f);    // ここで彩度を調整
        this.Textures = CreateTextures(textureId.TexturePathes);
        DisposeGraphics();      // Graphicsを解放，以降は絶対に使用しない
    }
}
