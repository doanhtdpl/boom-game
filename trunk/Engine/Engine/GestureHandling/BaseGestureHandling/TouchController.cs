using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSCEngine.GestureHandling.BaseGestureHandling
{
    internal class TouchController : ITouchController
    {
        private const int MaxGestures = 4;
        private static readonly Vector2 InvalidPosition = new Vector2(int.MinValue, int.MinValue);

        public TouchController()
        {
            this.Touches = new List<ITouch>(MaxGestures);
        }

        public ICollection<ITouch> Touches { get; private set; }

        public void Update(TouchCollection toucheCollection, GameTime gameTime)
        {
            List<ITouch> touchesCopy = new List<ITouch>(Touches);
            this.Touches.Clear();

            foreach (TouchLocation touchLocation in toucheCollection)
            {
                bool isBegin = true;
                
                foreach (ITouch lastTouch in touchesCopy)
                {
                    if (lastTouch.SystemTouch.Id == touchLocation.Id)
                    {
                        Touches.Add(new Touch(touchLocation, new TouchPositions(touchLocation.Position, lastTouch.Positions.Current, lastTouch.Positions.Begin)));

                        isBegin = false;
                        break;
                    }
                }

                if (isBegin)
                {
                    Touches.Add(new Touch(touchLocation, new TouchPositions(touchLocation.Position, InvalidPosition, touchLocation.Position)));
                }
            }
        }
    }
}
