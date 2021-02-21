using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFrameworkPure
{
    public static class VectorExtension
    {
        /// <summary>
        /// 设置Vector3的分量
        /// </summary>
        /// <param name="v"></param>
        /// <param name="pivot"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Vector3 Set(this Vector3 v, char pivot, float value)
        {
            switch (pivot)
            {
                case 'x':
                    v.x = value;
                    break;
                case 'y':
                    v.y = value;
                    break;
                case 'z':
                    v.z = value;
                    break;
            }

            return v;
        }
    }
}

