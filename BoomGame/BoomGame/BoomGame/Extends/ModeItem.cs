using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SSCEngine.GestureHandling;
using SSCEngine.GestureHandling.Implements.Events;
using BoomGame.Shared;

namespace BoomGame.Extends
{
    public class ModeItem : DrawableGameComponent, IGestureTarget<Tap>
    {
        private Texture2D background;
        private Vector2 position;

        private SpriteFont font;
        private String text;
        private Vector2 fontPosition;
        private Rectangle bound;

        private SpriteBatch sprBatch;

        public bool IsLock
        {
            get;
            set;
        }

        public ModeItem(Game game)
            : base(game)
        {
        }

        public void onInit(SpriteBatch sprBatch, Texture2D background, Vector2 position, SpriteFont font, String text)
        {
            this.sprBatch = sprBatch;
            this.background = background;
            this.position = position;
            this.font = font;
            this.text = text;

            bound = new Rectangle((int)position.X, (int)position.Y, background.Width, background.Height);

            fontPosition = new Vector2(position.X, position.Y);
        }

        public override void Draw(GameTime gameTime)
        {
            sprBatch.Draw(background, position, Color.White);
            sprBatch.DrawString(font, text, fontPosition, Color.Red);

            base.Draw(gameTime);
        }

        public bool IsGestureCompleted
        {
            get { return false; }
        }

        public uint Priority
        {
            get { return 0; }
        }

        public bool IsHandleGesture(Tap gEvent)
        {
            return bound.Contains((int)gEvent.Touch.Positions.Current.X, (int)gEvent.Touch.Positions.Current.Y);
        }

        public bool ReceivedGesture(Tap gEvent)
        {
            if (!this.IsLock)
            {
                Global.CreateCurrentMap(Convert.ToInt32(text));
            }
            return false;
        }
    }
}
