using System.Collections.Generic;
using ClassicScroller.Core.Math;
using Microsoft.Xna.Framework.Graphics;


/*
 MIT License
Copyright (c) 2023 Filipe Lopes | FMFCLPRO

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
 */

namespace ClassicScroller.Core.Graphics;

public class GameRender
{
    public List<SpriteRenderer> SpriteRenderers = new List<SpriteRenderer>();

    public void Add(SpriteRenderer spriteRenderer)
    {
        if (spriteRenderer == null) return;

        SpriteRenderers.Add(spriteRenderer);
    }

    public void Remove(SpriteRenderer renderer)
    {
        SpriteRenderers.Remove(renderer);
    }

    public void RenderAll(SpriteBatch spriteBatch)
    {
        for (var i = 0; i < SpriteRenderers.Count; i++)
        {
            SpriteRenderer spriteRenderer = SpriteRenderers[i];
            if (!spriteRenderer.EnableRender) continue;
            Transform transform = spriteRenderer.Attached.Transform;
            spriteBatch.Draw(spriteRenderer.Sprite.Texture2D, transform.Position, spriteRenderer.Color);
        }
    }
}