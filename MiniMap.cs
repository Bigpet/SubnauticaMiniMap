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
            MainPatcher.dbg_log.WriteLine("MiniMap Load");
            MainPatcher.dbg_log.Flush();
            var inst = new GameObject("SubnauticaMiniMap.MiniMap").AddComponent<MiniMap>();
            printOnce("MiniMap instantiated");
            try
            {
                objs.Add(inst.gameObject);
                printOnce("MiniMap instance added");
                GameObject obj = new GameObject("SubnauticaMiniMap.Overlay");
                //obj.AddComponent<UIOverlay>().color = new Color(1.0f, 0.0f, 0.0f);
                printOnce("Overlay instantiated");
                objs.Add(obj);
                inst.overlay = obj;
                printOnce("Overlay instance assigned");
            }
            catch (Exception e)
            {
                printOnce(e.Message);
            }
            string minimapassetfile = string.Format("{0}\\QMods\\MiniMap\\subnauticaminimap", Environment.CurrentDirectory);
            printOnce(minimapassetfile);
            inst.assets = AssetBundle.LoadFromFile(minimapassetfile);
            //inst.assets = AssetBundle.LoadFromFile(@"H:\DEV\2018\UnitySubntest\SubnauticaModdingTest\Assets\AssetBundles\subnauticaminimap");
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
            MainPatcher.dbg_log.WriteLine("MiniMap Awake");
            MainPatcher.dbg_log.Flush();
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
                        this.inctest *= (1 / 10.0f);
                        rto5.localPosition += (new Vector3(1.0f, 1.0f, 0.0f) * inctest);
                    }
                }
                if (Input.GetKeyDown("u"))
                {
                    if (instMinimap)
                    {
                        var rto5 = instMinimap.GetComponent<RectTransform>();
                        rto5.localScale *= 2.0f;
                        mapScale /= 2.0f;
                    }
                }
                if (Input.GetKeyDown("i"))
                {
                    if (instMinimap)
                    {
                        var rto5 = instMinimap.GetComponent<RectTransform>();
                        rto5.localScale /= 2.0f;
                        mapScale *= 2.0f;
                    }
                }
                if (Input.GetKeyDown("b"))
                {
                    this.rotateMap = !this.rotateMap;
                }
                if (Input.GetKeyDown("c"))
                {
                    this.flipX = !this.flipX;
                }
                if (Input.GetKeyDown("v"))
                {
                    this.flipY = !this.flipY;
                }
                if (Input.GetKeyDown("x"))
                {
                    this.flipS = !this.flipS;
                }
                if (Input.GetKeyDown("y"))
                {
                    this.flipT = !this.flipT;
                }
                if (Input.GetKeyDown("p"))
                {
                    this.flipR = !this.flipR;
                }

                if (prefabMinimap != null)
                {
                    var mapimg = GameObject.Find("MapImage");
                    var playerIcon = GameObject.Find("PlayerIcon");
                    if (mapimg)
                    {
                        var transf = mapimg.GetComponent<RectTransform>();
                        RectTransform transf2;
                        if (playerIcon)
                        {
                            transf2 = playerIcon.GetComponent<RectTransform>();
                        }
                        else
                        {
                            transf2 = new RectTransform();
                        }
                        var angles = Player.main.transform.rotation.eulerAngles;
                        //transf.rotation.SetAxisAngle(new Vector3(0.0f,0.0f,1.0f), angles.magnitude);
                        //-Player.main.camRoot.transform.rotation.eulerAngles.y

                        ////transf.rotation = Quaternion.AngleAxis(-Player.main.camRoot.transform.rotation.eulerAngles.y + ((int)rotated)* variableRot), new Vector3(0.0f, 0.0f, 1.0f));
                        //transf.localRotation = Quaternion.AngleAxis(-Player.main.camRoot.transform.rotation.eulerAngles.y + variableRot, new Vector3(0.0f, 0.0f, 1.0f));
                        //transf.localPosition.Scale(new Vector3(0.0f, 0.0f, 1.0f));
                        //transf.localPosition += new Vector3(
                        //    Player.main.transform.position.x * 1.0f / mapScale, 
                        //    Player.main.transform.position.y * 1.0f / mapScale, 
                        //    0.0f);
                        ////transf.localPosition.y = Player.main.transform.position.y * 1.0f / mapScale;
                        ///
                        transf.rotation = Quaternion.identity;
                        transf.position = Vector3.zero;
                        transf.localRotation = Quaternion.identity;
                        transf.localPosition = Vector3.zero;
                        transf.localScale = Vector3.one;
                        transf2.rotation = Quaternion.identity;
                        transf2.position = Vector3.zero;
                        transf2.localRotation = Quaternion.identity;
                        transf2.localPosition = Vector3.zero;
                        transf2.localScale = Vector3.one;

                        if (this.rotateMap)
                        {
                            transf.Rotate(0.0f, 0.0f, (flipR ? 1.0f : -1.0f) * Player.main.camRoot.transform.rotation.eulerAngles.y);
                        }
                        else
                        {
                            transf2.Rotate(0.0f, 0.0f, (flipR ? -1.0f : 1.0f) * Player.main.camRoot.transform.rotation.eulerAngles.y);
                        }
                        transf.Translate(
                                Player.main.transform.position.x * (flipX ? -1.0f : 1.0f) / mapScale,
                                Player.main.transform.position.z * (flipY ? -1.0f : 1.0f) / mapScale,
                                0.0f
                                );
                        transf.localScale = new Vector3((flipS ? -1.0f : 1.0f), (flipT ? -1.0f : 1.0f), 1.0f);
                    }
                }
                printOnce("update called");
                //if (Input.GetMouseButtonDown(1))
                {
                    printOnce("left mouse down");

                    if (!initialized && asset_loaded)
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
                                    rto5.localScale *= .0001f;
                                    rto5.position += new Vector3(0.05f, -0.035f, 0.1f);
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

                    if (!asset_loaded)
                    {
                        if (assets != null)
                        {
                            printOnce("check assets");
                            if (prefabMinimap == null)
                            {
                                prefabMinimap = assets.LoadAsset<GameObject>("MiniMapOverlay2");
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
                                        asset_loaded = true;
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
                MainPatcher.dbg_log.WriteLine(message);
                MainPatcher.dbg_log.Flush();
            }
        }

        public static HashSet<string> printed = new HashSet<string>();

        private bool initialized = false;
        private bool asset_loaded = false;
        private Graphic g;

        //A list of the objects that were created to be used later to potentially clean them up again (inspired by the Map mod)
        private static List<UnityEngine.Object> objs = new List<UnityEngine.Object>();
        public GameObject overlay;

        public AssetBundle assets;
        public GameObject prefabMinimap;
        public GameObject instMinimap;

        public float inctest = 0.001f;
        public static MiniMap Instance { get; private set; }

        public bool rotated = false;
        public bool rotateMap = false;
        public bool flipX = true;
        public bool flipY = true;
        public bool flipS = false;
        public bool flipT = false;
        public bool flipR = true;
        public float variableRot = 0.01f;
        public float mapScale = 20000.0f;
    }
}
