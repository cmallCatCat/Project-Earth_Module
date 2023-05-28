using UnityEngine;

namespace Make_It_Flow.Scripts.Core.Event_System.Event_Groups
{
    public class EventGroups : ScriptableObject
    {
        [System.Serializable]
        public struct EventGroup
        {
            public string groupName;
            public string[] events;
        }

        public EventGroup[] groups;
    }
}