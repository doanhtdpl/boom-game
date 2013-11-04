using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSCEngine.Control
{
    public interface IListViewArrange
    {
        float Range { get; set; }
        float Padding { get; set; }

        void Arrange(ICanvas canvas, int index);
        float GetContentSize(ref Vector2 contentSize, int nElems);
    }

    public class VerticalListViewArrange : IListViewArrange
    {
        float Range { get; set; }
        float Padding { get; set; }

        public VerticalListViewArrange()
        {
        }

        public VerticalListViewArrange(float range, float padding)
        {
            this.Range = range;
            this.Padding = padding;
        }

        public void Arrange(ICanvas canvas, int index)
        {
            canvas.Bound.Position.Y = index * (Padding + Range);
        }

        public float GetContentSize(ref Vector2 contentSize, int nElems)
        {
            return contentSize.Y = nElems * (Padding + Range);
        }

        float IListViewArrange.Range { get; set; }

        float IListViewArrange.Padding { get; set; }
    }

    public class HorizontalListViewArrange : IListViewArrange
    {
        float Range { get; set; }
        float Padding { get; set; }

        public HorizontalListViewArrange()
        {
        }

        public HorizontalListViewArrange(float range, float padding)
        {
            this.Range = range;
            this.Padding = padding;
        }

        public void Arrange(ICanvas canvas, int index)
        {
            canvas.Bound.Position.X = index * (Padding + Range);
        }

        public float GetContentSize(ref Vector2 contentSize, int nElems)
        {
            return contentSize.X = nElems * (Padding + Range);
        }

        float IListViewArrange.Range { get; set; }

        float IListViewArrange.Padding { get; set; }
    }
}
