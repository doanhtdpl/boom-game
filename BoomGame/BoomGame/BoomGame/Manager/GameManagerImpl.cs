using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoomGame.Interface.Manager;
using Microsoft.Xna.Framework;
using BoomGame.Manager.Interface;
using BoomGame.Entity.Collide;

namespace BoomGame.Manager
{
    public class GameManagerImpl : DrawableGameComponent, IGameManager
    {
        protected RenderManager rendererManager;
        protected CollisionManager collisionManager;
        protected LogicManager logicManager;

        public GameManagerImpl(Game game)
            : base(game)
        {
            rendererManager = new RenderManager();
            collisionManager = new CollisionManager();
            logicManager = new LogicManager();
        }

        public override void Update(GameTime gameTime)
        {
            rendererManager.Update(gameTime);
            logicManager.Update(gameTime);
            collisionManager.UpdateRealTime();

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            rendererManager.Draw(gameTime);

            base.Draw(gameTime);
        }

        public void Add(Entity.IGameEntity element)
        {
            if (element is ICollidable)
            {
                collisionManager.Add(element as ICollidable);
            }

            if(element.LogicalObj != null)
                logicManager.Add(element.LogicalObj);
            if (element.RendererObj != null)
                rendererManager.Add(element.RendererObj);
        }

        public void Remove(Entity.IGameEntity element)
        {
            if (element is ICollidable)
            {
                collisionManager.Remove(element as ICollidable);
            }

            logicManager.Remove(element.LogicalObj);
            rendererManager.Remove(element.RendererObj);
        }
    }
}
