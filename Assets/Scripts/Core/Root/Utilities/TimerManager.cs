using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

namespace Core.Root.Utilities
{
    public class TimerManager : MonoSingleton<TimerManager>
    {

        private class TimerData
        {
            public float         Duration;
            public float         ElapsedTime;
            public bool          IsPaused;
            public Coroutine     Coroutine;
            public System.Action Callback;

            public TimerData(float duration, System.Action callback)
            {
                Duration    = duration;
                ElapsedTime = 0f;
                IsPaused    = false;
                Callback    = callback;
            }
        }

        private Dictionary<string, TimerData> timerDataDict = new Dictionary<string, TimerData>();

        public void StartTimer(string timerID, float delay, System.Action callback, bool ignoreTimeScale = false)
        {
            if (timerDataDict.ContainsKey(timerID))
            {
                Debug.LogWarning($"Timer with ID '{timerID}' is already running. Stopping existing timer.");
                StopTimer(timerID);
            }
            TimerData timerData = new TimerData(delay, callback);
            timerDataDict[timerID] = timerData;
            timerData.Coroutine    = StartCoroutine(TimerCoroutine(timerID, ignoreTimeScale));
        }
        
        public void TryToStopTimer(string timerID)
        {
            if (timerDataDict.ContainsKey(timerID))
            {
                StopTimer(timerID);
            }
        }

        public void StopTimer(string timerID)
        {
            if (timerDataDict.ContainsKey(timerID))
            {
                TimerData timerData = timerDataDict[timerID];
                StopCoroutine(timerData.Coroutine);
                timerDataDict.Remove(timerID);
            }
            else
            {
                Debug.LogWarning($"No timer with ID '{timerID}' found.");
            }
        }

        public void PauseTimer(string timerID)
        {
            if (timerDataDict.TryGetValue(timerID, out TimerData value))
            {
                value.IsPaused = true;
            }
            else
            {
                Debug.LogWarning($"No timer with ID '{timerID}' found.");
            }
        }

        public void ResumeTimer(string timerID)
        {
            if (timerDataDict.TryGetValue(timerID, out TimerData value))
            {
                value.IsPaused = false;
            }
            else
            {
                Debug.LogWarning($"No timer with ID '{timerID}' found.");
            }
        }

        public float GetRemainingTime(string timerID)
        {
            if (timerDataDict.ContainsKey(timerID))
            {
                return timerDataDict[timerID].Duration - timerDataDict[timerID].ElapsedTime;
            }
            else
            {
                Debug.LogWarning($"No timer with ID '{timerID}' found.");
                return 0f;
            }
        }
        
        public bool HasTimer(string timerID)
        {
            return timerDataDict.ContainsKey(timerID);
        }

        public float GetElapsedTime(string timerID)
        {
            if (timerDataDict.TryGetValue(timerID, out TimerData value))
            {
                return value.ElapsedTime;
            }
            else
            {
                Debug.LogWarning($"No timer with ID '{timerID}' found.");
                return 0f;
            }
        }

        public float GetCompletionPercentage(string timerID)
        {
            if (timerDataDict.ContainsKey(timerID))
            {
                return timerDataDict[timerID].ElapsedTime / timerDataDict[timerID].Duration;
            }
            else
            {
                Debug.LogWarning($"No timer with ID '{timerID}' found.");
                return 0f;
            }
        }

        private IEnumerator TimerCoroutine(string timerID, bool ignoreTimeScale)
        {
            TimerData timerData = timerDataDict[timerID];
            while (timerData.ElapsedTime < timerData.Duration)
            {
                if (!timerData.IsPaused)
                {
                    timerData.ElapsedTime += ignoreTimeScale ? Time.unscaledDeltaTime : Time.deltaTime;
                }
                yield return null;
            }

            timerData.Callback?.Invoke();
            timerDataDict.Remove(timerID);
        }
    }
}
