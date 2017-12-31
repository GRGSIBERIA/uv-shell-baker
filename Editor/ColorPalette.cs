using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Drawing;

/// <summary>
/// カラーパレット
/// </summary>
public class ColorPalette
{
    /// <summary>
    /// 偶数番目の色は鮮やか，奇数番目の色合いはコンストラクタに依存
    /// </summary>
    public System.Drawing.Color[] Colors { get; private set; }

    /// <summary>
    /// 島を塗りつぶす色をつくるためのカラーパレット
    /// </summary>
    /// <param name="shellCount"></param>
    /// <param name="sValue">奇数番目の彩度</param>
    /// <param name="vValue">奇数番目の明度</param>
    public ColorPalette(int shellCount, float sValue, float vValue)
    {
        this.Colors = new System.Drawing.Color[shellCount];

        // shellの数だけ適当な色分けを考える
        float diff = 1f / (float)Colors.Length;
        for (int i = 0; i < Colors.Length; ++i)
        {
            float s = (i & 1) == 0 ? 1f : sValue;
            float v = (i & 1) == 0 ? 1f : vValue;

            var c = UnityEngine.Color.HSVToRGB(diff * i, s, v);

            this.Colors[i] = System.Drawing.Color.FromArgb(255, (int)(c.r * 255), (int)(c.g * 255), (int)(c.b * 255));
        }
    }
}
