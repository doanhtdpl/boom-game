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
using BoomGame.Debuger;
using SSCEngine.GestureHandling;
using BoomGame.Shared;
using BoomGame.Factory;
using SCSEngine.Sprite.Implements;

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

        public TBBoomMission()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = true;
            Content.RootDirectory = "Content";

            // Frame rate is 30 fps by default for Windows Phone.
            TargetElapsedTime = TimeSpan.FromTicks(333333);

            // Extend battery life under lock.
            InactiveSleepTime = TimeSpan.FromSeconds(1);

            this.IsFixedTimeStep = true;
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

            Global.isMusicOff = SaveLoadGame.LoadSoundVolume();

            // Global
            Global.Initialize(this);
            Components.Add(Global.BoomMissionManager);

            // Init Grid
            Grid.Grid.game = this;

            // Init Gesture Manager and initialize detector
            Global.GestureManager = DefaultGestureHandlingFactory.Instance.CreateManager(this);
            DefaultGestureHandlingFactory.Instance.InitDetectors(Global.GestureManager);

            // Init all game factory
            InitFactory();
            AddSpriteData();

            // Begin with Choose Scene
            BoomGame.Scene.MenuScene menu = Global.BoomMissionManager.Bank.GetScreen(Shared.Macros.S_MENU, true) as BoomGame.Scene.MenuScene;
            menu.onInit();
            Global.BoomMissionManager.AddExclusive(menu);
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
            Global.GestureManager.Update(gameTime);
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
            SpriteFramesBank.Instance.Add(Shared.Resources.BomberMoveLeft, FramesGenerator.Generate(50, 50, 50, 1));
            SpriteFramesBank.Instance.Add(Shared.Resources.BomberMoveRight, FramesGenerator.Generate(50, 50, 50, 1));
            SpriteFramesBank.Instance.Add(Shared.Resources.BomberMoveUp, FramesGenerator.Generate(50, 50, 50, 1));
            SpriteFramesBank.Instance.Add(Shared.Resources.BomberMoveDown, FramesGenerator.Generate(50, 50, 50, 1));

            SpriteFramesBank.Instance.Add(Shared.Resources.EnemyMoveLeft, FramesGenerator.Generate(52, 51, 208, 30));
            SpriteFramesBank.Instance.Add(Shared.Resources.EnemyMoveRight, FramesGenerator.Generate(52, 51, 208, 30));
            SpriteFramesBank.Instance.Add(Shared.Resources.EnemyMoveUp, FramesGenerator.Generate(57, 53, 228, 32));
            SpriteFramesBank.Instance.Add(Shared.Resources.EnemyMoveDown, FramesGenerator.Generate(54, 53, 216, 32));
            SpriteFramesBank.Instance.Add(Shared.Resources.Boss_3, FramesGenerator.Generate(150, 150, 150, 1));

            SpriteFramesBank.Instance.Add(Shared.Resources.box, FramesGenerator.Generate(50, 50, 50, 1));

            SpriteFramesBank.Instance.Add(Shared.Resources.Bomb, FramesGenerator.Generate(51, 51,255, 24));
            SpriteFramesBank.Instance.Add(Shared.Resources.Bomb_1, FramesGenerator.Generate(51, 52, 255, 25));
            SpriteFramesBank.Instance.Add(Shared.Resources.Bomb_2, FramesGenerator.Generate(51, 52, 255, 25));
        }

        protected void InitFactory()
        {
            EnemyFactory.setGame(this);
            BombFactory.setGame(this);
            ObstacleFactory.setGame(this);
            ExplodeFactory.setGame(this);
            WaterEffectFactory.setGame(this);
            ItemFactory.setGame(this);
        }
    }
}
    