using ClassicScroller.Core;
using ClassicScroller.Core.Composite;
using ClassicScroller.Core.Graphics;
using ClassicScroller.Core.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


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

namespace ClassicScroller.Game.Components;

public class PlayerComponent : Component,IUpdatable
{
    private SpriteRenderer _renderer;
    public void Update(World world,float deltaTime)
    {
        if (Input.IsPressing(Keys.D))
        {
            Attached.Transform.Position += new Vector2(1, 0);
        }
        if (Input.IsPressing(Keys.A))
        {
            Attached.Transform.Position += new Vector2(-1, 0);
        }
        
        if (Input.IsPressing(Keys.S))
        {
            Attached.Transform.Position += new Vector2(0, 1);
        }
        if (Input.IsPressing(Keys.W))
        {
            Attached.Transform.Position += new Vector2(0, -1);
        }
        
        if (Input.WasPressed(Keys.C))
        {
            _renderer.EnableRender = !_renderer.EnableRender;
        }

    }

    public void PreStart(World world)
    {
        _renderer = Attached.AddComponent<SpriteRenderer>();
    }

    public void Start(World world)
    {
     
    }

}