using Core.Architectures;
using QFramework;
using UnityEngine;

namespace Core
{
    public abstract class GameController: MonoBehaviour,IController
    {
        public IArchitecture GetArchitecture()
        {
            return Game.Interface;
        }
    }
}