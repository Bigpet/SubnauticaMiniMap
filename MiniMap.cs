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
            var inst = new GameObject("SubnauticaMiniMap.MiniMap").AddComponent<MiniMap>().gameObject;
            objs.Add(inst);
            GameObject obj = new GameObject("SubnauticaMiniMap.Overlay").AddComponent<UIOverlay>().gameObject;
            objs.Add(obj);
            inst.GetComponent<MiniMap>().overlay = obj;
        }

        private void Awake()
        {
            MainPatcher.sw.WriteLine("MiniMap Awake");
            MainPatcher.sw.Flush();
            MiniMap.Instance = this;
        }

        public void Update()
        {
            printOnce("update called");
            if (!initialized)
            {
                var mainGUI = uGUI.main;
                if (mainGUI == null) return;

                printOnce("initializedGUI");
                var hud = mainGUI.hud;
                if (hud == null) return;

                printOnce("initializedHUD");
                var barOxygen = hud.barOxygen.GetComponent<uGUI_OxygenBar>();
                if (barOxygen == null) return;

                printOnce("initialized oxygen bar");
                var canv = barOxygen.bar.canvas;
                if (canv == null) return;

                printOnce("initializedCANVAS");
                if (overlay != null)
                {
                    try
                    {
                        printOnce("getting transform");
                        var transf = overlay.GetComponent<Transform>();
                        if (transf == null) return;
                        printOnce("transform got");
                        var canvTrans = canv.GetComponent<Transform>();
                        if (canvTrans == null) return;
                        printOnce("canv transform got");
                        transf.SetParent(canvTrans);
                        printOnce("transform set");
                        initialized = true;
                        printOnce("initialized minimap");
                    }catch
                    {
                        printOnce("exception occured");
                    }
                }
                else
                {
                    printOnce("overlay was null");
                }
            }
        }

        public static void printOnce(string message)
        {
            bool is_new = printed.Add(message);
            if(is_new)
            {
                MainPatcher.sw.WriteLine(message);
                MainPatcher.sw.Flush();
            }
        }

        public static HashSet<string>printed = new HashSet<string>();

        private bool initialized = false;
        private Graphic g;

        public static List<UnityEngine.Object> objs;
        public GameObject overlay;

        public static MiniMap Instance { get; private set; }
    }
}
