using Core.Save_And_Load.Event;
using Framework;

namespace Core.Save_And_Load.Command
{
    public class SaveCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            this.SendEvent<SaveEvent>();
        }
    }
    
    public class LoadCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            this.SendEvent<LoadEvent>();
        }
    }
}