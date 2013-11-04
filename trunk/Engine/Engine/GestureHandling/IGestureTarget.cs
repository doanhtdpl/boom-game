using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSCEngine.GestureHandling
{
    public class GestureTargetComparer : IComparer<IGestureTarget>
    {
        private GestureTargetComparer()
        {
        }

        public static GestureTargetComparer Instance { get; private set; }

        static GestureTargetComparer()
        {
            Instance = new GestureTargetComparer();
        }

        #region IComparer<IGestureTarget> Members

        public int Compare(IGestureTarget x, IGestureTarget y)
        {
            if (x.Priority > y.Priority)
                return 1;
            else if (x.Priority == y.Priority)
                return 0;

            return -1;
        }

        #endregion
    }

    public interface IGestureTarget
    {
        uint Priority { get; }

        bool IsGestureCompleted { get; }
    }

    //public enum GestureHandleType
    //{
    //    None,
    //    Handled,
    //    Exclusive
    //}

    public interface IGestureTarget<GestureEvent> : IGestureTarget where GestureEvent : IGestureEvent
    {
        /// <summary>
        /// Target received a gesture
        /// </summary>
        /// <param name="gEvent">Gesture event</param>
        /// <returns>True if target is exclusive handle gesture</returns>
        bool ReceivedGesture(GestureEvent gEvent);
        /// <summary>
        /// Check if target handled a gesture
        /// </summary>
        /// <param name="gEvent">Gesture event</param>
        /// <returns>True if target handle gesture</returns>
        bool IsHandleGesture(GestureEvent gEvent);
    }
}
