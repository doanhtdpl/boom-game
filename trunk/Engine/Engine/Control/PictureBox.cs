using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSCEngine.Control
{
    public class PictureBox : BaseUIControl
    {
        public Texture2D Image { get; set; }

        private SpriteBatch spriteBatch;
        public PictureBox(Game game, SpriteBatch sprBatch)
            : base(game)
        {
            this.spriteBatch = sprBatch;
        }

        public PictureBox(Game game, Texture2D img, SpriteBatch sprBatch)
            : base(game)
        {
            this.Image = img;
            this.spriteBatch = sprBatch;
        }

        public override void Draw(GameTime gameTime)
        {
            if (this.Image != null)
            {
                this.spriteBatch.Begin();
                this.spriteBatch.Draw(this.Image, this.Canvas.Bound.Rectangle, this.Canvas.Content.Rectangle, Color.White);
                this.spriteBatch.End();
            }

            base.Draw(gameTime);
        }

        public void FitSizeByImage()
        {
            Texture2D img = this.Image;
            if (img == null)
                return;

            this.Canvas.Content.Position = Vector2.Zero;
            this.Canvas.Content.Size = new Vector2(img.Width, img.Height);
            
            this.Canvas.Bound.Size = this.Canvas.Content.Size;
        }

        public override void RegisterGestures(GestureHandling.IGestureDispatcher dispatcher)
        {
        }

        public override void LeaveGestures(GestureHandling.IGestureDispatcher dispatcher)
        {
        }
    }
}