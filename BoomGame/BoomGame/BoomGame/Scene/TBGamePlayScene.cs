using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCSEngine.ScreenManagement.Implement;
using SCSEngine.Services;
using SCSEngine.ResourceManagement;
using SCSEngine.ScreenManagement;
using BoomGame.Interface.Manager;
using BoomGame.Manager;
using BoomGame.Layers;

namespace BoomGame.Scene
{
    public class TBGamePlayScene : BaseGameScreen
    {
        protected SCSServices services;

        protected IResourceManager resourceManager;

        public GameManagerImpl GameManager
        {
            get;
            set;
        }

        public DefaultInputLayer InputLayer
        {
            get;
            set;
        }

        public TBGamePlayScene(IGameScreenManager manager)
            : base(manager)
        {
            services = (SCSServices)manager.Game.Services.GetService(typeof(SCSServices));

            resourceManager = (IResourceManager)manager.Game.Services.GetService(typeof(IResourceManager));

            GameManager = new GameManagerImpl(manager.Game);
            Components.Add(GameManager);

            InputLayer = new DefaultInputLayer(manager.Game);
            InputLayer.onInit();
            Components.Add(InputLayer);
        }
    }
}
