using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public static class ObjectsExtensions
    {
        public static void SetEnabled(this IEnumerable<Renderer> objects, bool isEnabled = true)
        {
            foreach (var obj in objects)
            {
                obj.enabled = isEnabled;
            }
        }
    }
}