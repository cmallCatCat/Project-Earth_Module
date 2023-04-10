using Core.Save_And_Load.Event;
using QFramework;

namespace Core.Save_And_Load.Command
{
    public class SaveCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            this.SendEvent<SaveEvent>();
        }
    }
}