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
using BoomGame.Utilities;

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
                        int gridX = Convert.ToInt32(reader.ReadLine());
                        int gridY = Convert.ToInt32(reader.ReadLine());
                        int row = Convert.ToInt32(reader.ReadLine());
                        int colum = Convert.ToInt32(reader.ReadLine());

                        Grid.Grid.getInst().onInit(new Vector2(gridX, gridY), row, colum, 50f, 50f);
                        AStar.getInst().onInit(row, colum, gridX, gridY, 50f, 50f);

                        Shared.Constants.GAME_SIZE_X = gridX;
                        Shared.Constants.GAME_SIZE_Y = gridY;
                        Shared.Constants.GAME_SIZE_WIDTH = colum * 50;
                        Shared.Constants.GAME_SIZE_HEIGHT = row * 50;

                        int numberObj = Convert.ToInt32(reader.ReadLine());
                        int id = 0;
                        float posX = 0f, posY = 0f;
                        int itemType = 0;

                        for (int i = 0; i <= numberObj; ++i)
                        {
                            String line = reader.ReadLine();
                            String[] arr = line.Split(',');

                            id = Convert.ToInt32(arr[0]);
                            posX = (float)Convert.ToDouble(arr[1]);
                            posY = (float)Convert.ToDouble(arr[2]);
                            itemType = Convert.ToInt32(arr[3]);

                            IGameEntity entity = CreateEntity(id, posX, posY, itemType);
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

        protected void putWallAt(Vector2 position)
        {
            AStar.getInst().SetWall(position);
        }

        protected IGameEntity CreateEntity(int id, float posX, float posY, int itemType)
        {
            IGameEntity entity = null;
            try
            {
                switch (id)
                {
                    case Localize.ID_bomber:
                        Global.Bomber_Start_Position_X = posX;
                        Global.Bomber_Start_Position_Y = posY;
                        break;

                    case Localize.ID_barricade_blue:
                        entity = ObstacleFactory.getInst().create(new ObstacleInfo(new Vector2(posX, posY), Shared.Constants.OBSTACLE_IDLE, (Sprite)resourceManager.GetResource<ISprite>(Shared.Resources.barricade_blue), true, false));
                        break;

                    case Localize.ID_barricade_green:
                        entity = ObstacleFactory.getInst().create(new ObstacleInfo(new Vector2(posX, posY), Shared.Constants.OBSTACLE_IDLE, (Sprite)resourceManager.GetResource<ISprite>(Shared.Resources.barricade_green), true, false));
                        break;

                    case Localize.ID_barricade_orange:
                        entity = ObstacleFactory.getInst().create(new ObstacleInfo(new Vector2(posX, posY), Shared.Constants.OBSTACLE_IDLE, (Sprite)resourceManager.GetResource<ISprite>(Shared.Resources.barricade_orange), true, false));
                        break;

                    case Localize.ID_barricade_red:
                        entity = ObstacleFactory.getInst().create(new ObstacleInfo(new Vector2(posX, posY), Shared.Constants.OBSTACLE_IDLE, (Sprite)resourceManager.GetResource<ISprite>(Shared.Resources.barricade_red), true, false));
                        break;

                    case Localize.ID_basic_box:
                        Vector2 boxPos = new Vector2(posX, posY);
                        entity = ObstacleFactory.getInst().create(new ObstacleInfo(boxPos, Shared.Constants.OBSTACLE_CANMOVE, (Sprite)resourceManager.GetResource<ISprite>(Shared.Resources.box), false, false));
                        (entity.LogicalObj as ObstacleLogical).ItemTypeContained = itemType;

                        // Set wall
                        putWallAt(boxPos);

                        break;

                    case Localize.ID_basic_enemy:
                        entity = EnemyFactory.getInst().create(new EnemyInfo(new Vector2(posX, posY), new Vector2(3f, 3f), false));
                        Global.Counter_Enemy++;
                        break;

                    case Localize.ID_boss_1:
                    case Localize.ID_boss_2:
                    case Localize.ID_boss_3:
                    case Localize.ID_boss_4:
                        entity = EnemyFactory.getInst().create(new EnemyInfo(new Vector2(posX, posY), new Vector2(3f, 3f), true));
                        Global.Counter_Enemy++;
                        break;

                    case Localize.ID_green_shrub:
                        entity = ObstacleFactory.getInst().create(new ObstacleInfo(new Vector2(posX, posY), Shared.Constants.OBSTACLE_IDLE, (Sprite)resourceManager.GetResource<ISprite>(Shared.Resources.green_shrub), false, true));
                        break;

                    case Localize.ID_green_tree:
                        entity = ObstacleFactory.getInst().create(new ObstacleInfo(new Vector2(posX, posY), Shared.Constants.OBSTACLE_IDLE, (Sprite)resourceManager.GetResource<ISprite>(Shared.Resources.green_tree), true, false));
                        break;

                    case Localize.ID_house_blue:
                        entity = ObstacleFactory.getInst().create(new ObstacleInfo(new Vector2(posX, posY), Shared.Constants.OBSTACLE_IDLE, (Sprite)resourceManager.GetResource<ISprite>(Shared.Resources.house_blue), true, false));
                        break;

                    case Localize.ID_house_orange:
                        entity = ObstacleFactory.getInst().create(new ObstacleInfo(new Vector2(posX, posY), Shared.Constants.OBSTACLE_IDLE, (Sprite)resourceManager.GetResource<ISprite>(Shared.Resources.house_orange), true, false));
                        break;

                    case Localize.ID_house_red:
                        entity = ObstacleFactory.getInst().create(new ObstacleInfo(new Vector2(posX, posY), Shared.Constants.OBSTACLE_IDLE, (Sprite)resourceManager.GetResource<ISprite>(Shared.Resources.house_red), true, false));
                        break;

                    case Localize.ID_house_yellow:
                        entity = ObstacleFactory.getInst().create(new ObstacleInfo(new Vector2(posX, posY), Shared.Constants.OBSTACLE_IDLE, (Sprite)resourceManager.GetResource<ISprite>(Shared.Resources.house_yellow), true, false));
                        break;

                    case Localize.ID_scenery_tree:
                        entity = ObstacleFactory.getInst().create(new ObstacleInfo(new Vector2(posX, posY), Shared.Constants.OBSTACLE_IDLE, (Sprite)resourceManager.GetResource<ISprite>(Shared.Resources.scenery_tree), true, false));
                        break;

                    case Localize.ID_wall_blue:
                        entity = ObstacleFactory.getInst().create(new ObstacleInfo(new Vector2(posX, posY), Shared.Constants.OBSTACLE_IDLE, (Sprite)resourceManager.GetResource<ISprite>(Shared.Resources.wall_blue), true, false));
                        break;

                    case Localize.ID_wall_orange:
                        entity = ObstacleFactory.getInst().create(new ObstacleInfo(new Vector2(posX, posY), Shared.Constants.OBSTACLE_IDLE, (Sprite)resourceManager.GetResource<ISprite>(Shared.Resources.wall_orange), true, false));
                        break;

                    case Localize.ID_wall_red:
                        entity = ObstacleFactory.getInst().create(new ObstacleInfo(new Vector2(posX, posY), Shared.Constants.OBSTACLE_IDLE, (Sprite)resourceManager.GetResource<ISprite>(Shared.Resources.wall_red), true, false));
                        break;

                    case Localize.ID_wall_violet:
                        entity = ObstacleFactory.getInst().create(new ObstacleInfo(new Vector2(posX, posY), Shared.Constants.OBSTACLE_IDLE, (Sprite)resourceManager.GetResource<ISprite>(Shared.Resources.wall_violet), true, false));
                        break;

                    case Localize.ID_yellow_flower:
                        entity = ObstacleFactory.getInst().create(new ObstacleInfo(new Vector2(posX, posY), Shared.Constants.OBSTACLE_IDLE, (Sprite)resourceManager.GetResource<ISprite>(Shared.Resources.yellow_flower), true, false));
                        break;
                    // Item
                    case Localize.ID_item_Ball:
                        entity = ItemFactory.getInst().create(new ItemInfo(new Vector2(posX, posY), Shared.Localize.ID_item_Ball));
                        break;

                    case Localize.ID_item_Wheel:
                        entity = ItemFactory.getInst().create(new ItemInfo(new Vector2(posX, posY), Shared.Localize.ID_item_Wheel));
                        break;

                    case Localize.ID_item_Bottle:
                        entity = ItemFactory.getInst().create(new ItemInfo(new Vector2(posX, posY), Shared.Localize.ID_item_Bottle));
                        break;

                    case Localize.ID_item_Coin:
                        entity = ItemFactory.getInst().create(new ItemInfo(new Vector2(posX, posY), Shared.Localize.ID_item_Coin));
                        break;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            
            return entity;
        }
    }
}
