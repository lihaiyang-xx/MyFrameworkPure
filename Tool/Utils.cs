using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFrameworkPure
{
    public static class Utils
    {
        public static string GenerateUID()
        {
            return Guid.NewGuid().ToString("N");
        }

    }
}
