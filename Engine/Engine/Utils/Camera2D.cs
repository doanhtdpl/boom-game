//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;

//namespace TriUan.SCSEngine.Utils
//{
//    public class Camera2D
//    {
//       private const float minZoom = 0.5f;
//       private const float maxZoom = 2.5f;

//       private static float _zoom;
//       private Matrix _transform;
//       private static Vector2 _pos;
//       private static  int _viewportWidth;
//       private static  int _viewportHeight;
//       private static int _worldWidth;
//       private static int _worldHeight;

//       public Camera2D(int  vp_width, int vp_height, int worldWidth, int worldHeight, float initialZoom)
//       {
//           _zoom = initialZoom;
//           Rotation = 0.0f;
//           _viewportWidth = vp_width;
//           _viewportHeight = vp_height;
//           _worldWidth = worldWidth;
//           _worldHeight = worldHeight;
//           Pos = new Vector2(0, 0);
//       }

//       #region Properties

//       public static float Zoom
//       {
//           get { return _zoom; }
//           set
//           {
//               _zoom = MathHelper.Clamp(value,minZoom,maxZoom);
//           }
//       }

//       public float Rotation { get; set; }

//       public void Move(Vector2 amount)
//       {
//           Pos += amount;
//       }

//       public static Vector2 Pos
//       {
//           get { return _pos; }
//           set
//           {
//               _pos.X = MathHelper.Clamp(value.X, -0, -0);
//               _pos.Y = MathHelper.Clamp(value.Y, -480, 0);
              
//            }
//       }

//       #endregion

//       public Matrix GetTransformation()
//       {
//         _transform =
//                Matrix.CreateRotationZ(Rotation) *
//                Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
//               Matrix.CreateTranslation(new Vector3(_pos.X, _pos.Y, 0));
           
//           return _transform;
//       }

//       public static float Scale(Vector2 positiion1, Vector2 position2, Vector2 delta1, Vector2 delta2)
//       {
//           Vector2 old1 = positiion1 - delta1;
//           Vector2 old2 = position2 - delta2;
//           float distance = Vector2.Distance(positiion1, position2);
//           float olddis = Vector2.Distance(old1, old2);
//           if (distance * olddis == 0)
//               return 1;
//           return distance / olddis;

//       }
//    }
//}
