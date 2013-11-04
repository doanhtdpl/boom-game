using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using SCSEngine.Services;
using Microsoft.Xna.Framework;

namespace BoomGame.Debuger
{
    public class StringDebuger : DrawableGameComponent
    {
        private SpriteFont font;
        private SCSServices services;

        private static Dictionary<String, Vector2> dict;

        public StringDebuger(Game game)
            : base(game)
        {
            dict = new Dictionary<String, Vector2>();

            services = (SCSServices)game.Services.GetService(typeof(SCSServices));
            font = game.Content.Load<SpriteFont>("debug");
        }

        public static void setString(String text, Vector2 position)
        {
            dict.Add(text, position);
        }

        public override void Draw(GameTime gameTime)
        {
            for (int i = 0; i < dict.Count; ++i)
            {
                String text = (String)dict.Keys.ElementAt(i);
                Vector2 pos = (Vector2)dict[text];
                services.SpriteBatch.DrawString(font, text, pos, Color.White);
            }
            base.Draw(gameTime);
        }
    }
}
