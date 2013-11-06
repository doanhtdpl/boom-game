using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using SCSEngine.Services;
using SCSEngine.ResourceManagement;
using BoomGame.Interface.Manager;
using BoomGame.Manager;
using SCSEngine.Sprite;
using BoomGame.Entity;
using BoomGame.Debuger;
using BoomGame.Layers;

namespace BoomGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class TBBoomMission : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        SCSServices scsServices;

        GameManagerImpl gameManager;

        BomberEntity bomberEntity;

        public TBBoomMission()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = true;
            Content.RootDirectory = "Content";

            // Frame rate is 30 fps by default for Windows Phone.
            TargetElapsedTime = TimeSpan.FromTicks(333333);

            // Extend battery life under lock.
            InactiveSleepTime = TimeSpan.FromSeconds(1);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 480;
            graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            scsServices = new SCSServices(this, spriteBatch);
            Services.AddService(typeof(SCSServices), scsServices);

            IResourceManager resourceManager = new SCSResourceManager(this.Content);
            this.Services.AddService(typeof(IResourceManager), resourceManager);

            StringDebuger debuger = new StringDebuger(this);
            Components.Add(debuger);

            gameManager = new GameManagerImpl(this);
            Components.Add(gameManager);

            bomberEntity = new BomberEntity(this);
            bomberEntity.onInit();
            gameManager.Add(bomberEntity);

            DefaultInputLayer layer = new DefaultInputLayer(this);
            layer.Add(bomberEntity);
            layer.onInit();
            Components.Add(layer);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            base.Draw(gameTime);

            spriteBatch.End();
        }

        protected void AddSpriteData()
        {
            SpriteFramesBank.Instance.Add(Shared.Resources.BomberMoveLeft, FramesGenerator.Generate(55, 55, 55, 1));
            SpriteFramesBank.Instance.Add(Shared.Resources.BomberMoveRight, FramesGenerator.Generate(55, 55, 55, 1));
            SpriteFramesBank.Instance.Add(Shared.Resources.BomberMoveUp, FramesGenerator.Generate(55, 55, 55, 1));
            SpriteFramesBank.Instance.Add(Shared.Resources.BomberMoveDown, FramesGenerator.Generate(55, 55, 55, 1));
        }
    }
}
