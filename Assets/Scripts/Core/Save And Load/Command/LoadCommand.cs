using Core.Save_And_Load.Event;
using QFramework;

namespace Core.Save_And_Load.Command
{
    public class LoadCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            this.SendEvent<LoadEvent>();
        }
    }
}