using Make_It_Flow.Scripts.Core.Behavior;
using Make_It_Flow.Scripts.Core.Event_System.Input_Manager;
using Make_It_Flow.Scripts.Core.Objects;
using UnityEngine;

namespace MeadowGames.MakeItFlow.Samples
{
    public class CheckSelectedText : MonoBehaviour
    {
        MFObject currentGO;
        public void Check(MFObject mfObject, Behavior mfBehavior)
        {
            foreach (MFObject obj in InputManager.eventsHandler.objectsUnderPointer)
            {
                if (obj.MFTag == "text")
                {
                    if (obj != currentGO)
                    {
                        currentGO = obj;
                        mfBehavior.behaviorEvents.OnBehaviorEnd.Invoke();
                    }
                }
            }
        }
    }
}