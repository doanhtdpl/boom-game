//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Input.Touch;
//using Microsoft.Xna.Framework.Graphics;
//namespace ListView
//{
//    public class ListView
//    {
//        public List<IListViewItem> ListItem { get; set; }
//        public Vector2 Position { get; set; }
//        public Vector2 Offset { get; set; }
//        public Vector2 Velocity { get; set; }
//        public Vector2 LineSpace { get; set; }
//        public float MoveAlpha { get; set; }
//        public bool IsVertical { get; set; }
//        public Vector2 Range { get; set; }
//        public bool Enabled { get; set; }
//        public bool Visibled { get; set; }

//        public ListView(Vector2 range)
//        {
//            Range = range;
//            Position = new Vector2(50, 50);
//            Offset = new Vector2(0, 0);
//            Velocity = new Vector2(0, 0);
//            LineSpace = new Vector2(0, 80);
//            MoveAlpha = 0.0005f;
//            ListItem = new List<IListViewItem>();
//        }
//        public ListView(Vector2 range,Vector2 _position, Vector2 _velocity, Vector2 _lineSpace, float _moveAlpha,bool _IsVertical)
//        {
//            Range = range;
//            Position = _position;
//            Velocity = _velocity;
//            LineSpace = _lineSpace;
//            MoveAlpha = _moveAlpha;
//            ListItem = new List<IListViewItem>();
//            IsVertical = _IsVertical;

//            this.Enabled = false;
//            this.Visibled = false;
//        }

//        public void Add(IListViewItem item)
//        {
//            try
//            {
//                ListItem.Add(item);
//            }
//            catch (Exception)
//            {

//            }
//        }

//        public void Remove(IListViewItem item)
//        {
//            try
//            {
//                ListItem.Remove(item);
//            }
//            catch(Exception)
//            {

//            }
//        }

//        public void RemoveAt(int index)
//        {
//            try
//            {
//                ListItem.RemoveAt(index);
//            }
//            catch (Exception)
//            {

//            }
//        }

//        public void Update(GameTime gameTime)
//        {
//            if (Enabled)
//            {
//                Vector2 size = Position + ListItem.Count * LineSpace;
//                if (IsVertical)
//                {
//                    Offset += new Vector2(Offset.X, Velocity.Y * gameTime.ElapsedGameTime.Milliseconds * MoveAlpha);
//                    Velocity += new Vector2(Velocity.X, -Velocity.Y * gameTime.ElapsedGameTime.Milliseconds * MoveAlpha);

//                    // Kiem tra gioi han
//                    if (Offset.Y < Position.Y - size.Y + Range.Y)
//                    {
//                        Velocity = new Vector2(Velocity.X, (Position.Y - size.Y + Range.Y - Offset.Y));
//                    }

//                    if (Offset.Y > 0)
//                    {
//                        Velocity = new Vector2(Velocity.X, -Offset.Y);
//                    }

//                    for (int i = 0; i < ListItem.Count; i++)
//                    {
//                        ListItem.ElementAt(i).Update(Position+Offset+i*LineSpace);
//                    }
//                }
//                else
//                {
//                    Offset += new Vector2(Velocity.X * gameTime.ElapsedGameTime.Milliseconds * MoveAlpha, Offset.Y);
//                    Velocity += new Vector2(-Velocity.X * gameTime.ElapsedGameTime.Milliseconds * MoveAlpha, Velocity.Y);

//                    // Kiem tra gioi han
//                    if (Offset.X < Position.X - size.X + Range.X)
//                    {
//                        Velocity = new Vector2((Position.X - size.X + Range.X - Offset.X*5), Velocity.Y);
//                    }

//                    if (Offset.X > 0)
//                    {
//                        Velocity = new Vector2(-Offset.X*5, Velocity.Y);
//                    }

//                    for (int i = 0; i < ListItem.Count; i++)
//                    {
//                        ListItem.ElementAt(i).Update(Position + Offset + i * LineSpace);
//                    }
//                }
//            }
//        }

//        public void Draw(GameTime gameTime)
//        {
//            if (Visibled)
//            {
//                foreach (IListViewItem item in ListItem)
//                {
//                    item.Draw(gameTime);
//                }
//            }
//        }

//        public void HandleInput(List<GestureSample> Gestures)
//        { 
//            if (!TouchPanel.IsGestureAvailable && TouchPanel.GetState().Count!=0)
//            {
//                Velocity = Vector2.Zero;
//            }
//            foreach (var gesture in Gestures)
//            {
//                if (IsVertical)
//                {
//                    switch (gesture.GestureType)
//                    {
//                        case GestureType.VerticalDrag: // Keo tha
//                            Offset += new Vector2(Offset.X, gesture.Delta.Y);
//                            break;
//                        case GestureType.Flick: // Keo tha trong luc do
//                            Velocity += new Vector2(Velocity.X, gesture.Delta.Y);
//                            break;
//                        case GestureType.Tap:
//                            foreach (IListViewItem item in ListItem)
//                            {
//                                if(item.Bound.Contains(new Point((int)gesture.Position.X,(int)gesture.Position.Y)))
//                                    item.Tap(gesture);
//                            }
//                            break;
//                    }
//                }
//                else 
//                {
//                    switch (gesture.GestureType)
//                    {
//                        case GestureType.HorizontalDrag: // Keo tha
//                            Offset += new Vector2(gesture.Delta.X,Offset.Y);
//                            break;
//                        case GestureType.Flick: // Keo tha trong luc do
//                            Velocity += new Vector2(gesture.Delta.X,Velocity.Y);
//                            break;
//                        case GestureType.Tap:
//                            foreach (IListViewItem item in ListItem)
//                            {
//                                if (item.Bound.Contains(new Point((int)gesture.Position.X, (int)gesture.Position.Y)))
//                                    item.Tap(gesture);
//                            }
//                            break;
//                    }
//                }

//             }
//        }
//    }
//}
