using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
namespace SCSEngine.Utils.GameObject.MUD
{
    public class MUDObject<M> : DrawableGameComponent, ICompleteable where M : IGameComponent
    {
        public M Model { get; private set; }
        public IUpdateable UpdateComponent { get; private set; }
        public IDrawable DrawComponent { get; private set; }

        private ICompleteable iLim;

        public MUDObject(Game game, M model, IUpdateable update, IDrawable draw)
            : base(game)
        {
            iLim = model as ICompleteable;
        }

        public override void Initialize()
        {
            this.Model.Initialize();

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (this.UpdateComponent != null)
            {
                this.UpdateComponent.Update(gameTime);
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (this.DrawComponent != null)
            {
                this.DrawComponent.Draw(gameTime);
            }

            base.Draw(gameTime);
        }

        #region ICompleteable Members

        public bool IsCompleted
        {
            get { if (iLim != null) return false; return iLim.IsCompleted; }
        }

        public event OnCompletedHandler OnCompleted;

        #endregion
    }
}
