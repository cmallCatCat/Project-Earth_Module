using Core.Save_And_Load.Systems;
using Core.Save_And_Load.Utilities;
using QFramework;

namespace Core.Architectures
{
    public class InGame : Architecture<InGame>
    {
        protected override void Init()
        {
            RegisterUtility<SaveUtility>(new JsonSaveUtility());
            // RegisterSystem(new InventoryArchiveSystem());
            RegisterSystem(new GameObjectSaveSystem());
        }
    }
}