using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSCEngine.GestureHandling.BaseGestureHandling
{
    class BaseGestureDispatcher : Dictionary<Type, List<IGestureTarget>>, IGestureDispatcher
    {
        private Dictionary<Type, IDispatchAdapter> adapters = new Dictionary<Type,IDispatchAdapter>();
        public BaseGestureDispatcher()
        {
            this.Enabled = true;
        }

        public void AddTarget<GE>(IGestureTarget<GE> gTarget) where GE : IGestureEvent
        {
            if (!this.ContainsKey(typeof(GE)))
            {
                this.Add(typeof(GE), new List<IGestureTarget>());
                this.adapters.Add(typeof(GE), new DispatchAdapter<GE>());
            }

            this[typeof(GE)].Add(gTarget);
        }

        public void RemoveTarget<GE>(IGestureTarget<GE> gTarget) where GE : IGestureEvent
        {
            if (this.ContainsKey(typeof(GE)))
            {
                this[typeof(GE)].Remove(gTarget);

                if (this[typeof(GE)].Count == 0)
                {
                    this.Remove(typeof(GE));
                    this.adapters.Remove(typeof(GE));
                }
            }
        }

        public void Dispatch(IGestureEvent gEvent)
        {
            if (this.ContainsKey(gEvent.GetType()) && this.adapters.ContainsKey(gEvent.GetType()))
            {
                // dispatch gesture
                var dispatchAdapter = this.adapters[gEvent.GetType()];
                IEnumerable<IGestureTarget> targets = new List<IGestureTarget>(this[gEvent.GetType()]);
                dispatchAdapter.Dispatch(gEvent, targets);

                // remove completed targets
                var realTargets = this[gEvent.GetType()];
                realTargets.Clear();
                foreach (var target in targets)
                {
                    if (!target.IsGestureCompleted)
                    {
                        realTargets.Add(target);
                    }
                }
            }
        }

        private class DispatchAdapter<GE> : IDispatchAdapter where GE : IGestureEvent
        {
            private Dictionary<int, IGestureTarget<GE>> handledTargets = new Dictionary<int, IGestureTarget<GE>>();

            public void Dispatch(IGestureEvent e, IEnumerable<IGestureTarget> targets)
            {
                var ge = (GE) e;
                if (ge == null)
                    return;

                if (this.handledTargets.ContainsKey(e.Touch.SystemTouch.Id))
                {
                    IGestureTarget<GE> target = this.handledTargets[e.Touch.SystemTouch.Id];
                    if (!target.ReceivedGesture(ge))
                    {
                        this.handledTargets.Remove(ge.Touch.SystemTouch.Id);
                    }
                }
                else
                {
                    foreach (var target in targets)
                    {
                        var specTarget = target as IGestureTarget<GE>;
                        if (specTarget.IsHandleGesture(ge))
                        {
                            if (specTarget.ReceivedGesture(ge))
                            {
                                this.handledTargets.Add(ge.Touch.SystemTouch.Id, specTarget);
                                break;
                            }
                        }
                    }
                }
            }
        }

        private interface IDispatchAdapter
        {
            void Dispatch(IGestureEvent e, IEnumerable<IGestureTarget> targets);
        }


        public bool Enabled { get; set; }
    }
}
