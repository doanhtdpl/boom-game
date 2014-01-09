using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SSCEngine.GestureHandling;
using SSCEngine.GestureHandling.Implements.Events;
using BoomGame.Shared;
using BoomGame.Scene;

namespace BoomGame.Extends
{
    public class ModeItem : DrawableGameComponent, IGestureTarget<Tap>
    {
        private Texture2D background;
        private Vector2 position;

        private SpriteFont font;
        public String Text;
        private Vector2 fontPosition;
        private Rectangle bound;

        private SpriteBatch sprBatch;

        public bool IsLock
        {
            get;
            set;
        }

        private double delayBeginScene = 500;

        public ModeItem(Game game)
            : base(game)
        {
        }

        public void onInit(SpriteBatch sprBatch, Texture2D background, Vector2 position, SpriteFont font, String text, bool islock)
        {
            this.sprBatch = sprBatch;
            this.background = background;
            this.position = position;
            this.font = font;
            this.Text = text;
            this.IsLock = islock;

            bound = new Rectangle((int)position.X, (int)position.Y, background.Width, background.Height);

            fontPosition = new Vector2(position.X + 37, position.Y + 32);
        }

        public override void Update(GameTime gameTime)
        {
            if (delayBeginScene > 0)
                delayBeginScene -= gameTime.ElapsedGameTime.TotalMilliseconds;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (!IsLock)
            {
                sprBatch.Draw(background, position, Color.White);
                sprBatch.DrawString(font, Convert.ToInt32(Text) < 10 ? "0" + Text : Text, fontPosition, Color.Black);
            }
            else
            {
                sprBatch.Draw(background, position, Color.Red);
            }

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
            if (delayBeginScene <= 0 && !this.IsLock)
            {
                // Play sound effect
                Global.PlaySound_Button_Effect();

                ChooseGame chooseGame = Global.BoomMissionManager.Current as ChooseGame;
                chooseGame.Pause();
                Global.BoomMissionManager.RemoveCurrent();
                Global.CurrentMap = Text;
                Global.CreateCurrentMap(Convert.ToInt32(Text));
            }
            return false;
        }
    }
}
