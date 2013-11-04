using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCSEngine.ScreenManagement.Implement
{
    public class BaseGameScreenManagerFactory : IGameScreenManagerFactory
    {
        private BaseGameScreenManagerFactory()
        {

        }

        public static BaseGameScreenManagerFactory Instance { get; private set; }

        static BaseGameScreenManagerFactory()
        {
            Instance = new BaseGameScreenManagerFactory();
        }

        #region IGameScreenManagerFactory Members

        public IGameScreenBank CreateScreenBank()
        {
            return new BaseGameScreenBank();
        }

        #endregion
    }
}