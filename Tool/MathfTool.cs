using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFrameworkPure
{
    public class MathfTool
    {
        /// <summary>
        /// 计算2个最小的整数,要求其乘积不小于num
        /// </summary>
        /// <param name="num"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        public static void CalculateRowColumn(int num,out int row,out int column)
        {
            float sqrt = Mathf.Sqrt(num);
            if (IsInteger(sqrt))
            {
                column = row = (int)sqrt;
            }
            else
            {
                int min = (int)sqrt;
                int max = (int)sqrt + 1;
                if (min * max >= num)
                {
                    column = max;
                    row = min;
                }
                else
                {
                    column = row = max;
                }
            }

        }

        /// <summary>
        /// 是否为整数
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static bool IsInteger(float num)
        {
            return Mathf.Approximately(num, (int) num);
        }
    }
}

