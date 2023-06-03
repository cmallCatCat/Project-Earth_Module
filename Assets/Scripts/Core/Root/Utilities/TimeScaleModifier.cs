namespace Core.Root.Utilities
{
    /// <summary>
    /// TimeScaleModifier 类表示一个时间缩放影响。可以通过创建此类的实例并添加到 TimeScaleManager 来实现多重时间缩放效果。
    /// </summary>
    public class TimeScaleModifier
    {
        /// <summary>
        /// 唯一标识符，用于区分不同的时间缩放影响。
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// 表示此影响应用的时间缩放系数。默认值为 1f。
        /// </summary>
        public float TimeScaleFactor { get; set; }

        /// <summary>
        /// 创建一个新的 TimeScaleModifier 实例。需要提供一个唯一标识符 id 和初始的时间缩放系数 timeScaleFactor。
        /// </summary>
        public TimeScaleModifier(string id, float timeScaleFactor)
        {
            Id              = id;
            TimeScaleFactor = timeScaleFactor;
        }
    }
}