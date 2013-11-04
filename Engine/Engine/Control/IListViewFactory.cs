using SSCEngine.Control.Clipping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSCEngine.Control
{
    public interface IListViewFactory
    {
        IListViewArrange CreateArranger();
        RectangleClipping CreateClipper();
        IListViewGestureHandler CreateGesturer();
    }

    public abstract class BaseListViewFactory : IListViewFactory
    {
        public virtual RectangleClipping CreateClipper()
        {
            return new RectangleClipping();
        }

        public abstract IListViewArrange CreateArranger();

        public abstract IListViewGestureHandler CreateGesturer();
    }

    public class VerticalListViewFactory : BaseListViewFactory
    {
        private VerticalListViewFactory()
        {
        }

        public static VerticalListViewFactory Instance { get; private set; }

        static VerticalListViewFactory()
        {
            Instance = new VerticalListViewFactory();
        }

        public override IListViewArrange CreateArranger()
        {
            return new VerticalListViewArrange(100f, 10f);
        }

        public override IListViewGestureHandler CreateGesturer()
        {
            return new VerticalListViewGestureHandler(2f);
        }
    }

    public class HorizontalListViewFactory : BaseListViewFactory
    {
        private HorizontalListViewFactory()
        {
        }

        public static HorizontalListViewFactory Instance { get; private set; }

        static HorizontalListViewFactory()
        {
            Instance = new HorizontalListViewFactory();
        }

        public override IListViewArrange CreateArranger()
        {
            return new HorizontalListViewArrange(100f, 10f);
        }

        public override IListViewGestureHandler CreateGesturer()
        {
            return new HorizontalListViewGestureHandler(1.7f);
        }
    }
}
