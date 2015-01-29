﻿using Astrid.Core;
using Astrid.Framework;
using Astrid.Framework.Assets;
using Astrid.Framework.Graphics;

namespace AstridDemo
{
    public class DemoGame : GameBase
    {
        private SpriteBatch _spriteBatch;
        private Texture _texture;
        private Vector2 _position;

        public DemoGame(ApplicationBase application)
            : base(application)
        {
        }

        public override void Create()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _texture = AssetManager.Load<Texture>("AstridLogo.png");

            var x = GraphicsDevice.Width / 2 - _texture.Width / 2;
            var y = GraphicsDevice.Height / 2 - _texture.Height / 2;
            _position = new Vector2(x, y);
        }

        public override void Destroy()
        {
        }

        public override void Pause()
        {
        }

        public override void Resume()
        {
        }

        public override void Resize(int width, int height)
        {
        }

        public override void Update(float deltaTime)
        {
        }

        public override void Render(float deltaTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(_texture, _position);
            _spriteBatch.End();
        }
    }
}
