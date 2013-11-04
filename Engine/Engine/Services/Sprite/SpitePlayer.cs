using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SCSEngine.Sprite;

namespace SCSEngine.Services.Sprite
{
    public class SpritePlayer
    {
        protected SpriteBatch spriteBatch;

        public SpritePlayer(SpriteBatch sprBatch)
        {
            this.spriteBatch = sprBatch;
        }

        public virtual void Draw(ISprite sprite, Rectangle dest, Color color)
        {
            this.spriteBatch.Draw(sprite.SpriteData.Data.Texture, dest, sprite.CurrentFrame, color);
        }

        public virtual void Draw(ISprite sprite, Vector2 position, Color color)
        {
            this.spriteBatch.Draw(sprite.SpriteData.Data.Texture, position, sprite.CurrentFrame, color);
        }

        public virtual void Draw(ISprite sprite, Rectangle dest, float angle, Vector2 origin, Color color, SpriteEffects effect, float depth)
        {
            this.spriteBatch.Draw(sprite.SpriteData.Data.Texture, dest, sprite.CurrentFrame, color, angle, origin, effect, depth);
        }

        public virtual void Draw(ISprite sprite, Vector2 position, float angle, float scale, Vector2 origin, Color color, SpriteEffects effect, float depth)
        {
            this.spriteBatch.Draw(sprite.SpriteData.Data.Texture, position, sprite.CurrentFrame, color, angle, origin, scale, effect, depth);
        }

        public virtual void Draw(ISprite sprite, ISpriteModel spriteModel)
        {
            this.spriteBatch.Draw(sprite.SpriteData.Data.Texture, spriteModel.Position, sprite.CurrentFrame, spriteModel.Color, spriteModel.Rotation,
                                  spriteModel.Origin, spriteModel.Scale, spriteModel.Effect, spriteModel.Depth);
        }
    }
}
