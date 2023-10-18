using System.Collections.Generic;
using System.Diagnostics;
using ClassicScroller.Core.Composite;
using ClassicScroller.Core.Graphics;
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

namespace ClassicScroller.Core;

public class World
{
    public List<CompositeObject> Objects { get; }

    private GameRender _gameRender;

    private bool HasInitiaized { get; set; }

    public World()
    {
        _gameRender = new GameRender();
        Objects = new List<CompositeObject>();
    }


    /// <summary>
    /// Searches through all objects and tries to find the component. Returns the first found.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T FindComponentInWorld<T>() where T : Component
    {
        for (var i = 0; i < Objects.Count; i++)
        {
            var obj = Objects[i];
            bool found = obj.TryGetComponent<T>(out T component);
            if (found) return component;
        }

        return null;
    }

    public void RenderWorld(SpriteBatch spriteBatch)
    {
        _gameRender.RenderAll(spriteBatch);
    }

    public void AddToWorld(CompositeObject compositeObject)
    {
        if (HasInitiaized)
        {
            compositeObject.PreStart(this);
            compositeObject.Start(this);

            SpriteRenderer spriteRenderer = compositeObject.GetComponent<SpriteRenderer>();

            if (spriteRenderer != null)
            {
                _gameRender.Add(spriteRenderer);
            }
        }

        Objects.Add(compositeObject);
        Input.Update();
        Debug.WriteLine(Objects.Count);
    }

    public void UpdateWorld(float deltaTime)
    {
        for (var i = 0; i < Objects.Count; i++)
        {
            CompositeObject obj = Objects[i];
            obj?.Update(this, deltaTime);
        }
    }

    public void PreStart()
    {
        for (var i = 0; i < Objects.Count; i++)
        {
            CompositeObject obj = Objects[i];
            obj?.PreStart(this);
        }
    }

    public void Start()
    {
        for (var i = 0; i < Objects.Count; i++)
        {
            CompositeObject obj = Objects[i];
            obj?.Start(this);

            SpriteRenderer spriteRenderer = obj?.GetComponent<SpriteRenderer>();

            if (spriteRenderer != null)
            {
                _gameRender.Add(spriteRenderer);
            }
        }

        HasInitiaized = true;
    }
}