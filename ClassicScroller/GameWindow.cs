using System;
using System.Diagnostics;
using System.IO;
using ClassicScroller.Core;
using ClassicScroller.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

namespace ClassicScroller;

public class GameWindow : Microsoft.Xna.Framework.Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private World _world;
    private Camera2D _camera;

    public GameWindow()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        base.Initialize();
    }

    Texture2D LoadTexture(string name)
    {
        string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources/Pngs");

        Texture2D texture = null;

        string final = Path.Combine(path, name + ".png");
        if (File.Exists(final))
        {
            Debug.WriteLine("YPIEEEEEE");
            using (FileStream fileStream = new FileStream(final, FileMode.Open))
            {
                texture = Texture2D.FromStream(GraphicsDevice, fileStream);
            }
        }

        return texture;
    }

    protected override void LoadContent()
    {
        _camera = new Camera2D(GraphicsDevice.Viewport);
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _world.PreStart();
        _world.Start();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        Input.Update();

        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        _world.UpdateWorld(deltaTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _camera.Update();
        _spriteBatch.Begin(transformMatrix: _camera.Transform);
        _world.RenderWorld(_spriteBatch);
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}