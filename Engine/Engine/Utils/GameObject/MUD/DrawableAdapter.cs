using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCSEngine.Utils.GameObject.MPB;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace SCSEngine.Utils.GameObject.MUD
{
    public static class DrawableAdapter
    {
        public static DrawableGameComponent Adapt(Game game, ISpriteBatchDrawable sd)
        {
            return new SpriteBatchDrawableAdapter(game, sd);
        }

        private class SpriteBatchDrawableAdapter : DrawableGameComponent
        {
            private ISpriteBatchDrawable sd;
            private SpriteBatch sprBatch;

            public SpriteBatchDrawableAdapter(Game game, ISpriteBatchDrawable adaptee)
                : base(game)
            {
                this.sd = adaptee;
                this.sprBatch = (SpriteBatch)game.Services.GetService(typeof(SpriteBatch));
            }

            public override void Draw(GameTime gameTime)
            {
                this.sd.Draw(sprBatch, gameTime);

                base.Draw(gameTime);
            }
        }

        public static DrawableGameComponent Adapt<E>(Game game, E model, IBrush<E> sd)
        {
            return new BrushDrawableAdapter<E>(game, model, sd);
        }

        private class BrushDrawableAdapter<E> : DrawableGameComponent
        {
            private E model;
            private IBrush<E> brush;

            public BrushDrawableAdapter(Game game, E model, IBrush<E> adaptee)
                : base(game)
            {
                this.model = model;
                this.brush = adaptee;
            }

            public override void Draw(GameTime gameTime)
            {
                this.brush.Draw(model, gameTime);

                base.Draw(gameTime);
            }
        }
    }
}
