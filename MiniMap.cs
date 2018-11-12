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
            var inst = new GameObject("SubnauticaMiniMap.MiniMap").AddComponent<MiniMap>();
            printOnce("MiniMap instantiated");
            try
            {
                objs.Add(inst.gameObject);
                printOnce("MiniMap inst added");
                GameObject obj = new GameObject("SubnauticaMiniMap.Overlay");
                obj.AddComponent<UIOverlay>().color = new Color(1.0f, 0.0f, 0.0f);
                printOnce("Overlay instantiated");
                objs.Add(obj);
                inst.overlay = obj;
                printOnce("Overlay instance assigned");
            }
            catch (Exception e)
            {
                printOnce(e.Message);
            }
            inst.assets = AssetBundle.LoadFromFile(@"H:\DEV\2018\UnitySubntest\SubnauticaModdingTest\Assets\AssetBundles\subnauticaminimap");
            if (inst.assets == null)
            {
                printOnce("Failed to load assets");
            }
            else
            {
                printOnce("loaded assets");
            }
        }

        private void Awake()
        {
            MainPatcher.sw.WriteLine("MiniMap Awake");
            MainPatcher.sw.Flush();
            MiniMap.Instance = this;
        }

        public void Update()
        {
            try
            {
                if (Input.GetKeyDown("g"))
                {
                    if (instMinimap)
                    {
                        var rto5 = instMinimap.GetComponent<RectTransform>();
                        this.inctest *= 10.0f;
                        rto5.localPosition += (new Vector3(1.0f, 1.0f, 0.0f) * inctest);
                    }
                }
                if (Input.GetKeyDown("h"))
                {
                    if (instMinimap)
                    {
                        var rto5 = instMinimap.GetComponent<RectTransform>();
                        rto5.localPosition -= (new Vector3(1.0f, 0.0f, 0.0f) * inctest);
                    }
                }
                if (Input.GetKeyDown("j"))
                {
                    if (instMinimap)
                    {
                        var rto5 = instMinimap.GetComponent<RectTransform>();
                        rto5.localPosition += (new Vector3(1.0f, 0.0f, 0.0f) * inctest);
                    }
                }
                if (Input.GetKeyDown("n"))
                {
                    if (instMinimap)
                    {
                        var rto5 = instMinimap.GetComponent<RectTransform>();
                        rto5.localPosition -= (new Vector3(0.0f, 1.0f, 0.0f) * inctest);
                    }
                }
                if (Input.GetKeyDown("m"))
                {
                    if (instMinimap)
                    {
                        var rto5 = instMinimap.GetComponent<RectTransform>();
                        rto5.localPosition += (new Vector3(0.0f, 1.0f, 0.0f) * inctest);
                    }
                }
                if (Input.GetKeyDown("k"))
                {
                    if (instMinimap)
                    {
                        var rto5 = instMinimap.GetComponent<RectTransform>();
                        this.inctest *= (1/10.0f);
                        rto5.localPosition += (new Vector3(1.0f, 1.0f, 0.0f) * inctest);
                    }
                }
                if (Input.GetKeyDown("u"))
                {
                    if (instMinimap)
                    {
                        var rto5 = instMinimap.GetComponent<RectTransform>();
                        rto5.localScale *= 2.0f;
                    }
                }
                if (Input.GetKeyDown("i"))
                {
                    if (instMinimap)
                    {
                        var rto5 = instMinimap.GetComponent<RectTransform>();
                        rto5.localScale /= 2.0f;
                    }
                }

                printOnce("update called");
                if (Input.GetMouseButtonDown(1))
                {
                    printOnce("right mouse down");
                    if (!initialized && assets != null)
                    {
                        printOnce("check assets");
                        if (prefabMinimap == null)
                        {
                            prefabMinimap = assets.LoadAsset<GameObject>("MiniMapOverlay");
                            if (prefabMinimap == null)
                            {
                                printOnce("prefab not loaded");
                            }
                            else
                            {
                                printOnce("prefab loaded");
                                instMinimap = UnityEngine.Object.Instantiate<GameObject>(this.prefabMinimap);
                                if (instMinimap == null)
                                {
                                    printOnce("inst minimap not loaded");
                                }
                                else
                                {
                                    printOnce("inst minimap loaded");
                                }
                            }
                        }
                        else
                        {
                            printOnce("prefabMinimap wasn not null");
                        }
                    }
                    else
                    {
                        printOnce("assets were null");
                    }
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
                        var canv = barOxygen.bar.canvas;//rootCanvas
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


                                GameObject obj2 = new GameObject("SubnauticaMiniMap.Overlay2");
                                printOnce("c1");
                                GameObject obj3 = new GameObject("SubnauticaMiniMap.Overlay3");
                                printOnce("c2");
                                var img2 = obj2.AddComponent<RawImage>();
                                printOnce("c3");
                                var img3 = obj3.AddComponent<RawImage>();
                                printOnce("c4");
                                img2.color = new Color(1.0f, 1.0f, 0.0f);
                                printOnce("c5");
                                img3.color = new Color(0.0f, 1.0f, 0.0f);
                                printOnce("Overlay instantiated2");
                                objs.Add(obj2);
                                printOnce("c6");
                                objs.Add(obj3);
                                printOnce("c7");
                                var rto2 = obj2.GetComponent<RectTransform>();
                                printOnce("c8");
                                var rto3 = obj3.GetComponent<RectTransform>();
                                if (rto2 != null)
                                {
                                    printOnce("c9");
                                }
                                //var to2 = obj2.GetComponent<Transform>();
                                if (rto3 != null)
                                {
                                    printOnce("c10");
                                }
                                //var to3 = obj3.GetComponent<Transform>();
                                printOnce("c12");
                                rto2.localScale = new Vector3(2.0f, 0.5f, 1.0f);
                                printOnce("c13");
                                var mcanv = mainGUI.screenCanvas;
                                printOnce("c13a");
                                if (mcanv != null)
                                {
                                    var mcanv2 = mcanv.GetComponent<Transform>();
                                    printOnce("c13b");
                                    if (mcanv2 != null)
                                    {
                                        printOnce("c13c");
                                        rto2.SetParent(mcanv2);
                                    }
                                }
                                printOnce("c14");
                                rto3.SetParent(canvTrans);
                                rto3.position += new Vector3(0, 0, 0.1f);
                                printOnce("c11");
                                rto3.localScale = new Vector3(0.001f, 0.001f, 1.0f);
                                GameObject obj4 = new GameObject("SubnauticaMiniMap.Overlay4");
                                var img4 = obj4.AddComponent<UnityEngine.UI.RawImage>();
                                img4.color = new Color(0.0f, 1.0f, 1.0f);
                                objs.Add(obj4);
                                var rto4 = obj4.GetComponent<RectTransform>();
                                if (instMinimap)
                                {
                                    printOnce("inst2 minimap loaded");
                                    var rto5 = instMinimap.GetComponent<RectTransform>();
                                    rto5.SetParent(canvTrans);
                                    rto5.position += new Vector3(0, 0, 0.1f);
                                    rto5.localScale *= .0001f;
                                }
                                rto4.SetParent(canvTrans);
                                rto4.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                                rto4.position = new Vector3(1.0f, 0.0f, 0.0f);
                                printOnce("rto4 set");
                                //rto4.anchorMax = new Vector2(Screen.width / 4, Screen.height / 4);
                                printOnce("rto4 anchormax");
                                //rto4.anchorMin = new Vector2(0, 0);
                                printOnce("rto4 anchormin");
                                rto4.position += new Vector3(0, 0, 0.1f);
                                printOnce("rto4 position");

                                transf.SetParent(canvTrans);
                                printOnce("transform set");
                                initialized = true;
                                printOnce("initialized minimap");
                            }
                            catch (Exception e)
                            {
                                printOnce("exception occured");
                                printOnce(e.Message);
                                printOnce(e.ToString());
                                printOnce(e.StackTrace);
                            }
                        }
                        else
                        {
                            printOnce("overlay was null");
                        }
                    }
                }

            }
            catch (Exception e)
            {
                printOnce("exception occured2");
                printOnce(e.Message);
                printOnce(e.ToString());
                printOnce(e.StackTrace);
            }
        }

        public static void printOnce(string message)
        {
            bool is_new = printed.Add(message);
            if (is_new)
            {
                MainPatcher.sw.WriteLine(message);
                MainPatcher.sw.Flush();
            }
        }

        public static HashSet<string> printed = new HashSet<string>();

        private bool initialized = false;
        private Graphic g;

        private static List<UnityEngine.Object> objs = new List<UnityEngine.Object>();
        public GameObject overlay;

        public AssetBundle assets;
        public GameObject prefabMinimap;
        public GameObject instMinimap;

        public float inctest = 0.001f;
        public static MiniMap Instance { get; private set; }
    }
}
