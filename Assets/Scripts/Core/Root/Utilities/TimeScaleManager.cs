using System;
using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

namespace Core.Root.Utilities
{
    /// <summary>
    /// TimeScaleManager 是一个用于管理 Unity 游戏中时间缩放的高级管理器类。它提供了对游戏时间缩放的全局和局部控制，支持多重影响管理、持续时间控制、事件驱动等功能。
    /// </summary>
    public class TimeScaleManager : MonoSingleton<TimeScaleManager>
    {
        /// <summary>
        /// 用于表示时间缩放曲线的 AnimationCurve 对象。通过编辑此曲线，可以实现复杂的非线性时间缩放效果。
        /// </summary>
        public AnimationCurve TimeScaleCurve;
        /// <summary>
        /// 表示允许的最小时间缩放值。默认值为 0f。
        /// </summary>
        public float MinTimeScale;
        /// <summary>
        /// 表示允许的最大时间缩放值。默认值为 4f。
        /// </summary>
        public float MaxTimeScale = 4f;

        private List<TimeScaleModifier> timeScaleModifiers = new List<TimeScaleModifier>();

        private TimeScaleModifier pause;

        private void Awake()
        {
            pause                     =  new TimeScaleModifier("Pause", 0f);
            InputReader.openBackpack  += () => AddModifier(pause);
            InputReader.closeBackpack += () => RemoveModifier(pause);
        }

        /// <summary>
        /// 向管理器添加一个新的 TimeScaleModifier 实例。这将影响当前的时间缩放值。
        /// </summary>
        public void AddModifier(TimeScaleModifier modifier)
        {
            timeScaleModifiers.Add(modifier);
            ApplyTimeScale();
        }

        /// <summary>
        /// 在指定的持续时间内逐渐将时间缩放应用。在调整过程中会根据 TimeScaleCurve 曲线来计算缩放值。完成调整后，可以选择性地执行 onComplete() 回调函数。
        /// </summary>
        /// <param name="timeScaleModifier">需要应用的时间缩放影响</param>
        /// <param name="duration">应用过程的持续时间，默认为 1 秒</param>
        /// <param name="onComplete">可选参数，在应用过程完成后执行的回调函数。</param>
        /// <returns></returns>
        public IEnumerator ApplySlowMotion(TimeScaleModifier timeScaleModifier, float duration = 1f, Action onComplete = null)
        {
            yield return ApplyTimeScaleWithDuration(timeScaleModifier.TimeScaleFactor, duration,
                () => { AddModifier(timeScaleModifier); });
            onComplete?.Invoke();
        }

        /// <summary>
        /// 在指定的持续时间内逐渐将时间缩放移除。在调整过程中会根据 TimeScaleCurve 曲线来计算缩放值。完成调整后，可以选择性地执行 onComplete() 回调函数。
        /// </summary>
        /// <param name="timeScaleModifier">需要移除的时间缩放影响</param>
        /// <param name="duration">应用过程的持续时间，默认为 1 秒</param>
        /// <param name="onComplete">可选参数，在应用过程完成后执行的回调函数。</param>
        /// <returns></returns>
        public IEnumerator RemoveSlowMotion(TimeScaleModifier timeScaleModifier, float duration = 1f, Action onComplete = null)
        {
            if (timeScaleModifiers.Contains(timeScaleModifier))
            {
                yield return ApplyTimeScaleWithDuration(1 / timeScaleModifier.TimeScaleFactor, duration,
                    () => { RemoveModifier(timeScaleModifier); });
                onComplete?.Invoke();
            }
            else
            {
                Debug.LogWarning("RemoveSlowMotion: TimeScaleModifier not found");
            }
        }

        /// <summary>
        /// 从管理器中移除指定的 TimeScaleModifier 实例。这将影响当前的时间缩放值。
        /// </summary>
        public void RemoveModifier(TimeScaleModifier modifier)
        {
            if (timeScaleModifiers.Contains(modifier))
            {
                timeScaleModifiers.Remove(modifier);
                ApplyTimeScale();
            }
            else
            {
                Debug.LogWarning("RemoveModifier: TimeScaleModifier not found");
            }
        }

        /// <summary>
        /// 在指定的持续时间内逐渐将时间缩放值调整为目标值后直接复原。在调整过程中会根据 TimeScaleCurve 曲线来计算缩放值。完成调整后，可以选择性地执行 onComplete() 回调函数。
        /// </summary>
        public IEnumerator ApplyTimeScaleWithDuration(float targetScale, float duration, Action onComplete = null)
        {
            TimeScaleModifier temporaryModifier = new TimeScaleModifier("Temporary", targetScale);
            AddModifier(temporaryModifier);

            float timer = 0f;

            while (timer < duration)
            {
                timer += Time.unscaledDeltaTime;
                float t = TimeScaleCurve.Evaluate(timer / duration);
                temporaryModifier.TimeScaleFactor = Mathf.Lerp(1f, targetScale, t);
                ApplyTimeScale();
                yield return null;
            }

            RemoveModifier(temporaryModifier);

            onComplete?.Invoke();
        }

        private void ApplyTimeScale()
        {
            float finalTimeScale = 1f;

            foreach (var modifier in timeScaleModifiers)
            {
                finalTimeScale *= modifier.TimeScaleFactor;
            }

            finalTimeScale = Mathf.Clamp(finalTimeScale, MinTimeScale, MaxTimeScale);

            Time.timeScale      = finalTimeScale;
            Time.fixedDeltaTime = 0.02f * finalTimeScale;
        }
    }
}