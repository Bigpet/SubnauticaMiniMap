using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using UnityEngine.UI;

namespace SubnauticaMiniMap
{
    public class MiniMap : MonoBehaviour
    {
        public static void Load()
        {
            MainPatcher.sw.WriteLine("MiniMap Load");
            MainPatcher.sw.Flush();
            objs.Add(new GameObject("SubnauticaMiniMap.MiniMap").AddComponent<MiniMap>().gameObject);
        }

        private void Awake()
        {
            MainPatcher.sw.WriteLine("MiniMap Awake");
            MainPatcher.sw.Flush();
            MiniMap.Instance = this;
        }

        private Graphic g;

        public static List<UnityEngine.Object> objs;

        public static MiniMap Instance { get; private set; }
    }
}
