using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using BoomGame.Entity.Collide;
using BoomGame.Entity.Logical;
using BoomGame.Entity.Renderer;
using BoomGame.Entity.Item;
using BoomGame.Shared;

namespace BoomGame.Entity
{
    public class ItemEntity : IGameEntity, ICollidable
    {
        protected List<ICollidable> collidableList = new List<ICollidable>();
        private bool isDead = false;

        protected IItem itemType;
        public IItem ItemType
        {
            set { this.itemType = value; }
        }

        public Logical.DefaultLogical LogicalObj
        {
            get;
            set;
        }

        public Renderer.DefaultRenderer RendererObj
        {
            get;
            set;
        }

        public ItemEntity(Game game)
        {
            this.LogicalObj = new ItemLogical(game, this);
            this.RendererObj = new ItemRenderer(game, this);
        }

        public void onInit()
        {
            this.RendererObj.onInit();
            this.LogicalObj.onInit();
        }

        public IGameEntity Owner
        {
            get { return this; }
        }

        public bool IsDead
        {
            get { return isDead; }
        }

        public List<ICollidable> CollidableList
        {
            get { return collidableList; }
        }

        public Rectangle Bound
        {
            get { return (LogicalObj as ItemLogical).Bound; }
        }

        public void Collision(ICollidable obj)
        {
            if (obj is BomberEntity || obj is WaterEffectEntity)
            {
                this.collidableList.Add(obj);
            }
        }

        public void ApplyCollision()
        {
            for (var i = 0; i < this.collidableList.Count; ++i)
            {
                if (this.collidableList[i] is BomberEntity)
                {
                    this.collisionWithBomber(this.collidableList[i] as BomberEntity);
                }
                else if (this.collidableList[i] is WaterEffectEntity)
                {
                    this.collisionWithWaterEffect(this.collidableList[i] as WaterEffectEntity);
                }
            }
            collidableList.Clear();
        }

        private void collisionWithBomber(BomberEntity bomber)
        {
            BomberRenderer renderer = bomber.RendererObj as BomberRenderer;
            BomberLogical logical = bomber.LogicalObj as BomberLogical;

            ItemRenderer obsRenderer = (this.RendererObj as ItemRenderer);
            ItemLogical obsLogical = (this.LogicalObj as ItemLogical);

            if (((Math.Abs(renderer.Position.X - obsRenderer.Position.X) <= 10 && (renderer.direction == Shared.Constants.DIRECTION_UP || renderer.direction == Shared.Constants.DIRECTION_DOWN))
                || (Math.Abs(renderer.Position.Y - obsRenderer.Position.Y) <= 10 && (renderer.direction == Shared.Constants.DIRECTION_LEFT || renderer.direction == Shared.Constants.DIRECTION_RIGHT))))
            {
                itemType.affect(bomber);
                this.isDead = true;

                // Play sound pick up item
                Global.PlaySoundEffect(Shared.Resources.Sound_Pick_Item);
            }
        }

        private void collisionWithWaterEffect(WaterEffectEntity wef)
        {
            this.isDead = true;
        }
    }
}
