using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCSEngine.ScreenManagement.Implement;
using SCSEngine.Services;
using SCSEngine.ResourceManagement;
using Microsoft.Xna.Framework.Graphics;
using SSCEngine.Control;
using SCSEngine.ScreenManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BoomGame.Scene
{
    public class AboutScene : BaseGameScreen
    {
        private SCSServices services;
        private IResourceManager resourceManager;

        private Texture2D background;

        private Texture2D aboutBackground;

        public AboutScene(IGameScreenManager manager)
            : base(manager)
        {
            services = (SCSServices)manager.Game.Services.GetService(typeof(SCSServices));

            resourceManager = (IResourceManager)manager.Game.Services.GetService(typeof(IResourceManager));
        }

        protected void onInit()
        {
            background = resourceManager.GetResource<Texture2D>(Shared.Resources.Menu_Background);
            aboutBackground = resourceManager.GetResource<Texture2D>(Shared.Resources.About_Background);
        }

        public override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                onBackButton_pressed();
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            services.SpriteBatch.Draw(background, Vector2.Zero, Color.White);
            services.SpriteBatch.Draw(aboutBackground, Vector2.Zero, Color.White);

            base.Draw(gameTime);
        }

        void onBackButton_pressed()
        {
            // Call to menu

        }
    }
}
