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
            MainPatcher.writeDebugLine("MiniMap Load");
            MainPatcher.writeDebugLine(DateTime.Now.ToString("O"));
            var inst = new GameObject("SubnauticaMiniMap.MiniMap").AddComponent<MiniMap>();
            printOnce("MiniMap instantiated");
            try
            {
                objs.Add(inst.gameObject);
                GameObject obj = new GameObject("SubnauticaMiniMap.Overlay");
                //obj.AddComponent<UIOverlay>().color = new Color(1.0f, 0.0f, 0.0f);
                objs.Add(obj);
                inst.overlay = obj;
            }
            catch (Exception e)
            {
                printOnce("exception during MiniMap Load");
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
        }

        private void Awake()
        {
            MainPatcher.writeDebugLine("MiniMap Awake");
            MiniMap.Instance = this;
        }

        public void Update()
        {
            try
            {
                CheckHotkeys();
                UpdateMapPosition();
                //if (Input.GetMouseButtonDown(1))
                {
                    if (!initialized && asset_loaded)
                    {
                        InitializeMiniMap();
                    }
                    if (!asset_loaded)
                    {
                        LoadPrefabAsset();
                    }
                }

            }
            catch (Exception e)
            {
                printOnce("exception occured in update");
                printOnce(e.Message);
                printOnce(e.ToString());
                printOnce(e.StackTrace);
            }
        }

        private void CheckHotkeys()
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
        }

        /* To keep debug messages from spamming the log this function prints every unique string only once.
         */
        public static void printOnce(string message)
        {
            bool is_new = printed.Add(message);
            if (is_new)
            {
                MainPatcher.writeDebugLine(message);
            }
        }

        private void PositionTest(uGUI mainGUI, Transform canvTrans)
        {
            GameObject obj2 = new GameObject("SubnauticaMiniMap.Overlay2");
            var img2 = obj2.AddComponent<RawImage>();
            img2.color = new Color(1.0f, 1.0f, 0.0f);
            objs.Add(obj2);
            var rto2 = obj2.GetComponent<RectTransform>();
            if (rto2 != null)
            {
                printOnce("c9");
            }
            rto2.localScale = new Vector3(2.0f, 0.5f, 1.0f);
            var mcanv = mainGUI.screenCanvas;
            if (mcanv != null)
            {
                var mcanv2 = mcanv.GetComponent<Transform>();
                if (mcanv2 != null)
                {
                    rto2.SetParent(mcanv2);
                }
            }

            GameObject obj3 = new GameObject("SubnauticaMiniMap.Overlay3");
            var img3 = obj3.AddComponent<RawImage>();
            img3.color = new Color(0.0f, 1.0f, 0.0f);
            objs.Add(obj3);
            var rto3 = obj3.GetComponent<RectTransform>();
            //var to2 = obj2.GetComponent<Transform>();
            if (rto3 != null)
            {
                printOnce("c10");
            }
            //var to3 = obj3.GetComponent<Transform>();
            rto3.SetParent(canvTrans);
            rto3.position += new Vector3(0, 0, 0.1f);
            rto3.localScale = new Vector3(0.001f, 0.001f, 1.0f);


            GameObject obj4 = new GameObject("SubnauticaMiniMap.Overlay4");
            var img4 = obj4.AddComponent<UnityEngine.UI.RawImage>();
            img4.color = new Color(0.0f, 1.0f, 1.0f);
            objs.Add(obj4);
            var rto4 = obj4.GetComponent<RectTransform>();
            rto4.SetParent(canvTrans);
            rto4.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            rto4.position = new Vector3(1.0f, 0.0f, 0.0f);
            //rto4.anchorMax = new Vector2(Screen.width / 4, Screen.height / 4);
            //rto4.anchorMin = new Vector2(0, 0);
            rto4.position += new Vector3(0, 0, 0.1f);
        }

        private void InitializeMiniMap()
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
                    transf.SetParent(canvTrans);
                    initialized = true;
                    printOnce("initialized minimap");
                    if (instMinimap)
                    {
                        printOnce("inst2 minimap loaded");
                        var rto5 = instMinimap.GetComponent<RectTransform>();
                        rto5.SetParent(canvTrans);
                        rto5.localScale *= .0001f;
                        rto5.position += new Vector3(0.05f, -0.035f, 0.1f);
                    }

                    //PositionTest(mainGUI, canvTrans);
                }
                catch (Exception e)
                {
                    printOnce("exception during MiniMap initialization");
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

        private void LoadPrefabAsset()
        {
            if (assets != null)
            {
                printOnce("check assets");
                if (prefabMinimap == null)
                {
                    prefabMinimap = assets.LoadAsset<GameObject>("MiniMapOverlay2");
                    if (prefabMinimap == null)
                    {
                        printOnce("MiniMap Prefab could not be loaded");
                    }
                    else
                    {
                        instMinimap = UnityEngine.Object.Instantiate<GameObject>(this.prefabMinimap);
                        if (instMinimap == null)
                        {
                            printOnce("MiniMap Prefab could not be instantiated");
                            return;
                        }
                        asset_loaded = true;
                    }
                }
            }
        }

        /* Update the rotation/position of the player icon and the minimap */
        private void UpdateMapPosition()
        {
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
