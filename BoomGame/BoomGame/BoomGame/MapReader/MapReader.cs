using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoomGame.Entity;
using System.IO;
using System.Diagnostics;
using BoomGame.Shared;
using BoomGame.Entity.Logical;
using BoomGame.Factory;
using BoomGame.FactoryElement;
using Microsoft.Xna.Framework;
using SCSEngine.Sprite.Implements;
using SCSEngine.ResourceManagement;
using SCSEngine.Sprite;
using System.IO.IsolatedStorage;

namespace BoomGame.MapReader
{
    public class MapReader
    {
        private String mapPath;
        private IResourceManager resourceManager;

        public MapReader(String path)
        {
            this.mapPath = path;
        }

        public bool onInit(IResourceManager resourceManager)
        {
            this.resourceManager = resourceManager;
            return true;
        }

        public List<IGameEntity> Read()
        {
            try
            {
                List<IGameEntity> entities = new List<IGameEntity>();

                using (System.IO.Stream stream = TitleContainer.OpenStream(mapPath))
                {
                    using (System.IO.StreamReader reader = new System.IO.StreamReader(stream))
                    {
                        int row = Convert.ToInt32(reader.ReadLine());
                        int colum = Convert.ToInt32(reader.ReadLine());
                        Grid.Grid.getInst().onInit(Vector2.Zero, row, colum, 50f, 50f);

                        Shared.Constants.GAME_SIZE_X = 0;
                        Shared.Constants.GAME_SIZE_Y = 0;
                        Shared.Constants.GAME_SIZE_WIDTH = colum * 50;
                        Shared.Constants.GAME_SIZE_HEIGHT = row * 50;

                        int numberObj = Convert.ToInt32(reader.ReadLine());
                        int id = 0;
                        float posX = 0f, posY = 0f;

                        for (int i = 0; i < numberObj; ++i)
                        {
                            String line = reader.ReadLine();
                            String[] arr = line.Split(',');

                            id = Convert.ToInt32(arr[0]);
                            posX = (float)Convert.ToDouble(arr[1]);
                            posY = (float)Convert.ToDouble(arr[2]);

                            IGameEntity entity = CreateEntity(id, posX, posY);
                            if (entity != null)
                                entities.Add(entity);
                        }

                        reader.Close();
                        stream.Close();

                        return entities;
                    }
                }
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine("Read file error" + ex);
                return null;
            }
        }

        protected IGameEntity CreateEntity(int id, float posX, float posY)
        {
            IGameEntity entity = null;
            switch (id)
            {
                case Localize.ID_BASIC_BOX:
                    entity = ObstacleFactory.getInst().create(new ObstacleInfo(new Vector2(posX, posY), Shared.Constants.OBSTACLE_IDLE, (Sprite)resourceManager.GetResource<ISprite>(Shared.Resources.Obstacle)));
                    break;
                case Localize.ID_BASIC_ENEMY:
                    entity = EnemyFactory.getInst().create(new EnemyInfo(new Vector2(posX, posY), new Vector2(5f, 5f)));
                    Global.Counter_Enemy++;
                    break;
            }
            return entity;
        }
    }
}
