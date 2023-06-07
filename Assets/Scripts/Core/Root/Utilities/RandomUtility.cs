using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.Root.Utilities
{
    public static class RandomUtility
    {
        // Basic random number generation
        public static int Range(int min, int max)
        {
            return UnityEngine.Random.Range(min, max);
        }

        public static float Range(float min, float max)
        {
            return UnityEngine.Random.Range(min, max);
        }

        // Random boolean value generation
        public static bool RandomBool(float probability = 0.5f)
        {
            return UnityEngine.Random.value < probability;
        }

        // Random color generation
        public static Color RandomColor()
        {
            return new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
        }

        public static Color RandomColorA()
        {
            return new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
        }

        // Random vector generation
        public static Vector2 RandomVector2(float minX, float maxX, float minY, float maxY)
        {
            return new Vector2(Range(minX, maxX), Range(minY, maxY));
        }
    
        public static Vector3 RandomVector3(float minX, float maxX, float minY, float maxY, float minZ, float maxZ)
        {
            return new Vector3(Range(minX, maxX), Range(minY, maxY), Range(minZ, maxZ));
        }
        
        public static Vector2 RandomVector2InCircle(float radius)
        {
            float angle    = Range(0.0f, 360.0f);
            float distance = Mathf.Sqrt(Range(0.0f, 1.0f)) * radius;
            float x        = distance * Mathf.Cos(angle * Mathf.Deg2Rad);
            float y        = distance * Mathf.Sin(angle * Mathf.Deg2Rad);

            return new Vector2(x, y);
        }

        // Random rotation generation
        public static Quaternion RandomRotation()
        {
            return Quaternion.Euler(RandomVector3(0, 360, 0, 360, 0, 360));
        }

        // Random array/list operations
        public static T RandomElement<T>(T[] array)
        {
            return array[Range(0, array.Length)];
        }

        public static T RandomElement<T>(List<T> list)
        {
            return list[Range(0, list.Count)];
        }
        
        /// <summary>
        /// 根据权重随机返回数组/列表中的一个元素。
        /// </summary>
        /// <typeparam name="T">数组/列表元素的类型。</typeparam>
        /// <param name="list">包含键值对的列表，每个键值对由一个浮点型权重和一个元素组成。</param>
        /// <returns>根据权重随机选择的元素。</returns>
        public static T RandomElementWithWeight<T>(List<KeyValueStruct<float, T>> list)
        {
            if (list == null || list.Count == 0)
            {
                throw new ArgumentException("列表不能为空或长度不能为零。");
            }

            // 计算总权重
            float totalWeight = list.Sum(pair => pair.Key);

            // 在0到totalWeight之间生成一个随机数
            float randomValue = Range(0, totalWeight);

            // 遍历列表，并依次减去每个元素的权重，直到randomValue小于等于0
            foreach (KeyValueStruct<float, T> pair in list)
            {
                randomValue -= pair.Key;

                if (randomValue <= 0)
                {
                    return pair.Value;
                }
            }

            // 如果因为浮点数精度问题没有返回任何值，则返回列表中最后一个元素
            return list.Last().Value;
        }

        /// <summary>
        /// 对给定的列表进行洗牌，即对其进行乱序处理。
        /// </summary>
        /// <param name="list">要进行洗牌操作的列表</param>
        public static void Shuffle<T>(IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k     = Range(0, n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }

        /// <summary>
        /// 根据正态分布生成随机浮点数。
        /// </summary>
        /// <param name="mean"></param>
        /// <param name="standardDeviation">正态分布的标准差</param>
        /// <returns>根据指定分布生成的随机浮点数。</returns>
        public static float NormalDistribution(float mean, float standardDeviation)
        {
            double u1            = 1.0 - UnityEngine.Random.value;
            double u2            = 1.0 - UnityEngine.Random.value;
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2); 
            return (float)(mean + standardDeviation * randStdNormal);
        }

        /// <summary>
        /// 根据指数分布生成随机浮点数。
        /// </summary>
        /// <param name="lambda">指数分布的比率参数</param>
        /// <returns>根据指定分布生成的随机浮点数。</returns>
        public static float ExponentialDistribution(float lambda)
        {
            double u = UnityEngine.Random.value;
            return (float)(-Math.Log(1.0 - u) / lambda);
        }

        // Seed setting and control
        public static int GetSeed()
        {
            return UnityEngine.Random.state.GetHashCode();
        }

        public static void SetSeed(int seed)
        {
            UnityEngine.Random.InitState(seed);
        }
    }
}
