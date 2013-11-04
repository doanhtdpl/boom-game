using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
namespace SCSEngine.Utils.GameObject.MPB
{
    public class MPBObject<E> : DrawableGameComponent, ICompleteable
    {
        private E model;
        private IProcessor<E> processor;
        private IBrush<E> brush;
        private ICompleteable iLim;

        public MPBObject(Game game, E e, IProcessor<E> eProc, IBrush<E> eBrush)
            : base(game)
        {
            this.model = e;
            this.processor = eProc;
            this.brush = eBrush;
            iLim = e as ICompleteable;
        }

        public override void Update(GameTime gameTime)
        {
            if (this.processor != null)
            {
                this.processor.Update(model, gameTime);
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (this.brush != null)
            {
                this.brush.Draw(model, gameTime);
            }

            base.Draw(gameTime);
        }

        #region ICompleteable Members

        public bool IsCompleted
        {
            get { if (iLim == null) return false; return iLim.IsCompleted; }
        }

        public event OnCompletedHandler OnCompleted;

        #endregion
    }
}
