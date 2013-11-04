using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SCSEngine.Sprite;

namespace SCSEngine.Utils.Adapters
{
    public class DrawableGameComponentAsSpriteModel : DrawableGameComponent, ISpriteModel
    {
        public DrawableGameComponentAsSpriteModel(Game game)
            : base(game)
        {
            this.Position = Vector2.Zero;
            this.Origin = Vector2.Zero;
            this.Scale = 1f;
            this.Rotation = 0f;
            this.Effect = SpriteEffects.None;
            this.Color = Color.White;
            this.Depth = 1f;
        }

        public Vector2 Position { get; set; }
        public Vector2 Origin { get; set; }
        public float Scale { get; set; }
        public float Rotation { get; set; }
        public SpriteEffects Effect { get; set; }
        public Color Color { get; set; }
        public float Depth { get; set; }
    }
}
