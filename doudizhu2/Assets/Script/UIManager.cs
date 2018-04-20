//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//using GameDefine;
//using System.Text;

//public partial class UIManager : MonoBehaviour
//{

//    public Transform NameBoardRoot;    //姓名板
//    private Transform BaseUIRoot;      // 位于UI最底层，常驻场景，基础交互
//    private Transform PopUIRoot;       // 位于UI上层，弹出式，互斥
//    private Transform StoryUIRoot;     // 故事背景层
//    private Transform TipUIRoot;       // 位于UI顶层，弹出重要提示信息等
//    private Transform MenuPopUIRoot;
//    private Transform MessageUIRoot;
//    private Transform DeathUIRoot;

//    private Dictionary<string, GameObject> m_dicTipUI = new Dictionary<string, GameObject>();
//    private Dictionary<string, GameObject> m_dicBaseUI = new Dictionary<string, GameObject>();
//    private Dictionary<string, GameObject> m_dicPopUI = new Dictionary<string, GameObject>();
//    private Dictionary<string, GameObject> m_dicStoryUI = new Dictionary<string, GameObject>();
//    private Dictionary<string, GameObject> m_dicMenuPopUI = new Dictionary<string, GameObject>();
//    private Dictionary<string, GameObject> m_dicMessageUI = new Dictionary<string, GameObject>();
//    private Dictionary<string, GameObject> m_dicDeathUI = new Dictionary<string, GameObject>();
//    private Dictionary<string, GameObject> m_dicCacheUI = new Dictionary<string, GameObject>();
//    private Dictionary<string, int> m_dicWaitLoad = new Dictionary<string, int>();

//    private List<string> m_listIconNameUI = new List<string>();
//    public UIAtlas m_IconAtlas = null;

//    public GameObject m_SkillIconAtlas = null;
//    public GameObject m_HeroIconAtlas = null;
//    public GameObject m_ItemIconAtlas = null;
//    public GameObject m_EquipIconAtlas = null;
//    public UIAtlas SkillIconAtlas = null;
//    public UIAtlas HeroIconAtlas = null;
//    public UIAtlas ItemIconAtlas = null;
//    public UIAtlas EquipIconAtlas = null;

//    //public static Dictionary<string, UIPathData> m_DicUIInfo = new Dictionary<string, UIPathData>();
//    public static Dictionary<string, UIPathData> m_DicUIName = new Dictionary<string, UIPathData>();

//    public static Dictionary<int, List<UIPathData>> m_DicSceneBaseUI = new Dictionary<int, List<UIPathData>>();

//    private static UIManager m_instance;

//    public static UIManager Instance() { return m_instance; }

//    public static UIPathData nowShowPopUI = UIInfo.MainUIPanel;
//    public static UIPathData lastShowPopUI = UIInfo.MainUIPanel;
//    //副本返回主城需提示文字
//    public List<string> m_RemaindTextList = new List<string>();

//    private bool BackOpenOtherUI = false;

//    void Awake()
//    {
//        m_dicTipUI.Clear();
//        m_dicBaseUI.Clear();
//        m_dicPopUI.Clear();
//        m_dicStoryUI.Clear();
//        m_dicMenuPopUI.Clear();
//        m_dicMessageUI.Clear();
//        m_dicDeathUI.Clear();
//        m_dicCacheUI.Clear();
//        m_instance = this;
//        m_listIconNameUI.Clear();
//        Transform camTrans = transform.GetChild(0);
//        if (camTrans != null)
//        {
//            camTrans.gameObject.DoSetActive(true);
//        }
//        gameObject.AddComponent<ClickScreenEffect>();
//        BaseUIRoot = gameObject.transform.Find("BaseUIRoot");
//        if (null == BaseUIRoot)
//        {
//            BaseUIRoot = AddObjToRoot("BaseUIRoot").transform;
//        }

//        PopUIRoot = gameObject.transform.Find("PopUIRoot");
//        if (null == PopUIRoot)
//        {
//            PopUIRoot = AddObjToRoot("PopUIRoot").transform;
//        }

//        StoryUIRoot = gameObject.transform.Find("StoryUIRoot");
//        if (null == StoryUIRoot)
//        {
//            StoryUIRoot = AddObjToRoot("StoryUIRoot").transform;
//        }

//        TipUIRoot = gameObject.transform.Find("TipUIRoot");
//        if (null == TipUIRoot)
//        {
//            TipUIRoot = AddObjToRoot("TipUIRoot").transform;

//        }

//        MenuPopUIRoot = gameObject.transform.Find("MenuPopUIRoot");
//        if (null == MenuPopUIRoot)
//        {
//            MenuPopUIRoot = AddObjToRoot("MenuPopUIRoot").transform;
//        }

//        MessageUIRoot = gameObject.transform.Find("MessageUIRoot");
//        if (null == MessageUIRoot)
//        {
//            MessageUIRoot = AddObjToRoot("MessageUIRoot").transform;
//        }

//        DeathUIRoot = gameObject.transform.Find("DeathUIRoot");
//        if (null == DeathUIRoot)
//        {
//            DeathUIRoot = AddObjToRoot("DeathUIRoot").transform;
//        }

//        NameBoardRoot = gameObject.transform.Find("NameBoardRoot");
//        if (null == NameBoardRoot)
//        {
//            NameBoardRoot = AddObjToRoot("NameBoardRoot").transform;
//        }

//        m_IconAtlas = gameObject.GetComponent<UIAtlas>();
//        if (m_IconAtlas == null)
//        {
//            Shader shader = Shader.Find("Unlit/Transparent Colored");
//            Material mat = new Material(shader);
//            m_IconAtlas = gameObject.AddComponent<UIAtlas>();
//            m_IconAtlas.spriteMaterial = mat;
//        }

//        BaseUIRoot.gameObject.SetActive(true);
//        TipUIRoot.gameObject.SetActive(true);
//        PopUIRoot.gameObject.SetActive(true);
//        StoryUIRoot.gameObject.SetActive(true);
//        MenuPopUIRoot.gameObject.SetActive(true);
//        MessageUIRoot.gameObject.SetActive(true);
//        DeathUIRoot.gameObject.SetActive(true);
//        NameBoardRoot.gameObject.SetActive(true);

//    }

//    void Update()
//    {
//    }

//    void OnDestroy()
//    {
//        m_instance = null;
//    }

//    /// <summary>
//    /// 添加面板级别的父节点对象
//    /// </summary>
//    /// <param name="name"></param>
//    /// <returns></returns>
//    private GameObject AddObjToRoot(string name)
//    {
//        GameObject obj = new GameObject();
//        if (UICamera.mainCamera != null)
//            obj.transform.parent = gameObject.transform.Find("Camera");// UICamera.mainCamera.transform;
//        else
//            obj.transform.parent = transform;
//        obj.transform.localPosition = Vector3.zero;
//        obj.transform.localScale = Vector3.one;
//        obj.name = name;
//        return obj;
//    }

//    /// <summary>
//    /// UI面板计数工具
//    /// </summary>
//    /// <param name="pathName"></param>
//    private bool LoadDicRefCount(string pathName, bool isAdd)
//    {
//        if (m_dicWaitLoad.ContainsKey(pathName))
//        {
//            if (isAdd)
//                m_dicWaitLoad[pathName]++;
//            else
//            {
//                m_dicWaitLoad[pathName]--;
//                if (m_dicWaitLoad[pathName] <= 0)
//                {
//                    m_dicWaitLoad.Remove(pathName);
//                }
//            }
//        }
//        else
//        {
//            if (isAdd)
//                m_dicWaitLoad.Add(pathName, 1);
//            else
//                return false;

//        }
//        return true;
//    }



//    public static void CloseUIByName(string pathDataName)
//    {
//        UIPathData pathData = null;
//        if (m_DicUIName.TryGetValue(pathDataName, out pathData))
//        {
//            if (pathData != null)
//            {
//                CloseUI(pathData);
//            }
//        }
//    }


//    // 关闭UI，根据类型不同，触发不同行为
//    public static void CloseUI(UIPathData pathData)
//    {
//        //        if (Input.touchCount > 1)
//        //            return;
//        if (null == m_instance)
//        {
//            return;
//        }
//        m_instance.UIPanelStateChange = true;
//        m_instance.RemoveLoadDicRefCount(pathData.name);
//        switch (pathData.uiType)
//        {
//            case UIPathData.UIType.TYPE_BASE:
//                m_instance.CloseBaseUI(pathData.name);
//                break;
//            case UIPathData.UIType.TYPE_POP:
//                m_instance.ClosePopUI(pathData.name);
//                GlobeVar.IsExistClearResourceNum++;
//                break;
//            case UIPathData.UIType.TYPE_STORY:
//                GlobeVar.IsExistClearResourceNum++;
//                m_instance.CloseStoryUI(pathData.name);
//                break;
//            case UIPathData.UIType.TYPE_TIP:
//                GlobeVar.IsExistClearResourceNum++;
//                m_instance.CloseTipUI(pathData.name);
//                break;
//            case UIPathData.UIType.TYPE_MENUPOP:
//                m_instance.CloseMenuPopUI(pathData.name);
//                break;
//            case UIPathData.UIType.TYPE_MESSAGE:
//                m_instance.CloseMessageUI(pathData.name);
//                break;
//            case UIPathData.UIType.TYPE_DEATH:
//                m_instance.CloseDeathUI(pathData.name);
//                break;
//            default:
//                break;
//        }
//    }


//    public static GameObject ShowUIByName(string pathDataName, bool CloseCamera = false, bool showSelf = true)
//    {
//        UIPathData pathData = null;
//        if (m_DicUIName.TryGetValue(pathDataName, out pathData))
//        {
//            if (pathData != null)
//            {
//                return ShowUI(pathData, CloseCamera, showSelf);
//            }
//        }
//        return null;
//    }
//    /// <summary>
//    /// 显示UI
//    /// </summary>
//    /// <param name="pathData"></param>
//    /// <returns></returns>
//    public static GameObject ShowUI(UIPathData pathData, bool CloseCamera = false, bool showSelf = true)
//    {
//        GameObject window = null;
//        if (null == m_instance)
//        {
            
//            return window;
//        }

//        if (pathData.uiType == UIPathData.UIType.TYPE_POP || pathData.uiType == UIPathData.UIType.TYPE_MENUPOP)
//        {
//            if (!pathData.name.Equals("FightBattle"))
//            {
//                m_instance.clearAtlasAuto();
//            }
//        }
//        //面板计数
//        m_instance.LoadDicRefCount(pathData.name, true);
//        m_instance.UIPanelStateChange = true;
//        if (pathData.uiType == UIPathData.UIType.TYPE_POP)
//        {
//        }
//        Dictionary<string, GameObject> curDic = null;
//        switch (pathData.uiType)
//        {
//            case UIPathData.UIType.TYPE_BASE:
//                //清空界面动效
//                GlobeVar.ItemEffectNum = 0;
//                curDic = m_instance.m_dicBaseUI;
//                break;
//            case UIPathData.UIType.TYPE_POP:
//                curDic = m_instance.m_dicPopUI;
//                lastShowPopUI = nowShowPopUI;
//                nowShowPopUI = pathData;
//                //清空界面动效
//                GlobeVar.ItemEffectNum = 0;
//                break;
//            case UIPathData.UIType.TYPE_STORY:
//                //return null;
//                curDic = m_instance.m_dicStoryUI;
//                break;
//            case UIPathData.UIType.TYPE_TIP:
//                curDic = m_instance.m_dicTipUI;
//                break;
//            case UIPathData.UIType.TYPE_MENUPOP:
//                curDic = m_instance.m_dicMenuPopUI;
//                break;
//            case UIPathData.UIType.TYPE_MESSAGE:
//                curDic = m_instance.m_dicMessageUI;
//                break;
//            case UIPathData.UIType.TYPE_DEATH:
//                curDic = m_instance.m_dicDeathUI;

//                break;
//            default:
//                return window;
//        }
//        if (null == curDic)
//        {
//            return window;
//        }
//        if (m_instance.m_dicCacheUI.ContainsKey(pathData.name))
//        {
//            if (!curDic.ContainsKey(pathData.name))
//            {
//                curDic.Add(pathData.name, m_instance.m_dicCacheUI[pathData.name]);
//            }
//            //m_instance.m_dicCacheUI.Remove(pathData.name);
//        }
//        int _index = 0;
//        if (curDic.ContainsKey(pathData.name))
//        {
//            curDic[pathData.name].SetActive(true);
//            window = m_instance.DoAddUI(pathData, curDic[pathData.name]);
//        }
//        else
//        {
//            window = m_instance.LoadUI(pathData);
//        }


//        if (pathData.uiType == UIPathData.UIType.TYPE_BASE && pathData.name.Equals("MainUIPanel"))
//        {
//            m_instance.TryDestroyUI(m_instance.m_dicBaseUI, "MainVisitPanel");
//        }
//        window.SetActive(showSelf);

//        return window;
//    }

//    private GameObject LoadUI(UIPathData uiData)
//    {
//        return DoAddUI(uiData, windowBundle);
//    }

//    /// <summary>
//    /// 添加UI对象实体
//    /// </summary>
//    /// <param name="uiData"></param>
//    /// <param name="curWindow"></param>
//    /// <returns></returns>
//    private GameObject DoAddUI(UIPathData uiData, GameObject curWindow)
//    {
//        GameObject newWindow = null;
//        if (!m_dicWaitLoad.Remove(uiData.name))
//        {
//            return newWindow;
//        }

//        if (null != curWindow)
//        {
//            Transform parentRoot = null;
//            Dictionary<string, GameObject> relativeDic = null;
//            switch (uiData.uiType)
//            {
//                case UIPathData.UIType.TYPE_BASE:
//                    parentRoot = BaseUIRoot;
//                    relativeDic = m_dicBaseUI;
//                    break;
//                case UIPathData.UIType.TYPE_POP:
//                    parentRoot = PopUIRoot;
//                    relativeDic = m_dicPopUI;
//                    break;
//                case UIPathData.UIType.TYPE_STORY:
//                    parentRoot = StoryUIRoot;
//                    relativeDic = m_dicStoryUI;
//                    break;
//                case UIPathData.UIType.TYPE_TIP:
//                    parentRoot = TipUIRoot;
//                    relativeDic = m_dicTipUI;
//                    break;
//                case UIPathData.UIType.TYPE_MENUPOP:
//                    parentRoot = MenuPopUIRoot;
//                    relativeDic = m_dicMenuPopUI;
//                    break;
//                case UIPathData.UIType.TYPE_MESSAGE:
//                    parentRoot = MessageUIRoot;
//                    relativeDic = m_dicMessageUI;
//                    break;
//                case UIPathData.UIType.TYPE_DEATH:
//                    parentRoot = DeathUIRoot;
//                    relativeDic = m_dicDeathUI;
//                    break;
//                default:
//                    break;

//            }
//            //先清除掉以前的UI
//            if (uiData.uiType == UIPathData.UIType.TYPE_POP || uiData.uiType == UIPathData.UIType.TYPE_MENUPOP)
//            {
//                OnLoadNewPopUI(m_dicPopUI, uiData.name);
//                //OnLoadNewPopUI(m_dicMenuPopUI, uiData.name);
//            }
//            //如果没有缓存在重新创建
//            if (null != relativeDic && relativeDic.ContainsKey(uiData.name))
//            {
//                relativeDic[uiData.name].SetActive(true);
//                newWindow = relativeDic[uiData.name];
//            }
//            else if (null != parentRoot && null != relativeDic)
//            {
//                newWindow = GameObject.Instantiate(curWindow) as GameObject;
//                if (null != newWindow)
//                {
//                    newWindow.name = uiData.name;
//                    newWindow.transform.parent = parentRoot;

//                    newWindow.transform.localScale = Vector3.one;
//                    UIWidget tWidget = newWindow.transform.GetComponent<UIWidget>();
//                    if (tWidget != null && UICamera.mainCamera != null)
//                    {//给UIWidget挂目标点
//                        tWidget.updateAnchors = UIRect.AnchorUpdate.OnEnable;
//                        tWidget.SetAnchor(UICamera.mainCamera.gameObject, 0, 0, 0, 0);
//                    }
//                    relativeDic.Add(uiData.name, newWindow);
//                    //做UI适配
//                    UIAdaptate(newWindow, uiData.uiType);
//                }
//            }

//            if (uiData.uiType == UIPathData.UIType.TYPE_STORY)
//            {
//            }
//            else if (uiData.uiType == UIPathData.UIType.TYPE_MENUPOP)
//            {

//            }
//            else if (uiData.uiType == UIPathData.UIType.TYPE_DEATH)
//            {
//                //ReliveCloseOtherSubUI();
//            }
//            else if (uiData.uiType == UIPathData.UIType.TYPE_POP)
//            {

//            }
//            else if (uiData.uiType == UIPathData.UIType.TYPE_MESSAGE)
//            {
//                if (uiData.name.Equals("MessageBox"))
//                {
//                    newWindow.GetComponent<UIWidget>().width = 720;
//                    newWindow.GetComponent<UIWidget>().height = 300;
//                }
//            }
//        }
//        newWindow.transform.localPosition = new Vector3(1, -1, newWindow.transform.position.z);
//        return newWindow;
//    }
//    /// <summary>
//    /// 根据屏幕分辨率返回合适的UI偏移量
//    /// </summary>
//    public NGUISafeAreaManager.FourDirOffset GetPositionUIAdaptate()
//    {
//        NGUISafeAreaManager.FourDirOffset Offset;
//        float swidth = GlobeVar.pixelX;
//        float shight = GlobeVar.pixelY;
//        float ratio = swidth / shight;
//        ratio = ((float)((int)((ratio + 0.005) * 100))) / 100;

//        if (ratio < 0.53f)
//        {
//            Offset.Bottom = 65;
//            Offset.Top = -150;
//            Offset.Left = 0;
//            Offset.Right = 0;
//        }
//        else if (ratio > 0.56f && ratio < 0.75)
//        {
//            Offset.Bottom = 65;
//            Offset.Top = -100;
//            Offset.Left = 0;
//            Offset.Right = 0;
//        }
//        else
//        {
//            Offset.Bottom = 0;
//            Offset.Top = 0;
//            Offset.Left = 0;
//            Offset.Right = 0;
//        }
//        return Offset;
//    }
//    /// <summary>
//    /// UI适配
//    /// </summary>
//    public void UIAdaptate(GameObject newWindow, UIPathData.UIType type)
//    {
//        GameObject uisafearea = GameObject.FindWithTag("uisafearea");
//        if (uisafearea == null)
//        {
//            return;
//        }
//        UIRect mUIRect = uisafearea.GetComponent<UIRect>();
//        if (mUIRect == null)
//        {
//            return;
//        }
//        NGUISafeAreaManager.FourDirOffset Offset = GetPositionUIAdaptate();
//        NGUISafeAreaManager.Init(Offset, mUIRect);
//        var rect = NGUISafeAreaManager.GetSafeArea();
//        List<UIAnchor> allAnchors = new List<UIAnchor>();
//        List<UIRect> allRects = new List<UIRect>();
//        allAnchors.AddRange(newWindow.GetComponentsInChildren<UIAnchor>(true));
//        foreach (var anchor in allAnchors)
//        {
//            if (anchor != null && anchor.container == null)
//            {
//                anchor.container = rect.gameObject;
//            }
//            anchor.enabled = true;
//        }
//    }
//    /// <summary>
//    /// 获取UI
//    /// </summary>
//    /// <returns>The U.</returns>
//    /// <param name="pathData">Path data.</param>
//    public static GameObject GetUI(UIPathData pathData)
//    {
//        GameObject window = null;
//        if (null == m_instance)
//        {
//            Logger.WarningLog("game manager is not init");
//            return window;
//        }
//        Dictionary<string, GameObject> curDic = null;
//        switch (pathData.uiType)
//        {
//            case UIPathData.UIType.TYPE_BASE:
//                curDic = m_instance.m_dicBaseUI;
//                break;
//            case UIPathData.UIType.TYPE_POP:
//                curDic = m_instance.m_dicPopUI;
//                break;
//            case UIPathData.UIType.TYPE_STORY:
//                curDic = m_instance.m_dicStoryUI;
//                break;
//            case UIPathData.UIType.TYPE_TIP:
//                curDic = m_instance.m_dicTipUI;
//                break;
//            case UIPathData.UIType.TYPE_MENUPOP:
//                curDic = m_instance.m_dicMenuPopUI;
//                break;
//            case UIPathData.UIType.TYPE_MESSAGE:
//                curDic = m_instance.m_dicMessageUI;
//                break;
//            case UIPathData.UIType.TYPE_DEATH:
//                curDic = m_instance.m_dicDeathUI;

//                break;
//            default:
//                return window;
//        }

//        if (null == curDic)
//        {
//            return window;
//        }
//        if (curDic.ContainsKey(pathData.name))
//        {
//            return curDic[pathData.name];
//        }
//        else
//        {
//            return null;
//        }
//    }
//    // 查找UI是否已经Show出来了
//    public static bool IsUiShow(UIPathData pathData)
//    {
//        if (null == m_instance)
//        {
//            Logger.ErrorLog("game manager is not init");
//            return false;
//        }

//        Dictionary<string, GameObject> curDic = null;
//        switch (pathData.uiType)
//        {
//            case UIPathData.UIType.TYPE_BASE:
//                curDic = m_instance.m_dicBaseUI;
//                break;
//            case UIPathData.UIType.TYPE_POP:
//                curDic = m_instance.m_dicPopUI;
//                break;
//            case UIPathData.UIType.TYPE_STORY:
//                curDic = m_instance.m_dicStoryUI;
//                break;
//            case UIPathData.UIType.TYPE_TIP:
//                curDic = m_instance.m_dicTipUI;
//                break;
//            case UIPathData.UIType.TYPE_MENUPOP:
//                curDic = m_instance.m_dicMenuPopUI;
//                break;
//            case UIPathData.UIType.TYPE_MESSAGE:
//                curDic = m_instance.m_dicMessageUI;
//                break;
//            case UIPathData.UIType.TYPE_DEATH:
//                curDic = m_instance.m_dicDeathUI;

//                break;
//            default:
//                return false;
//        }
//        if (curDic.ContainsKey(pathData.name))
//        {
//            return curDic[pathData.name].activeSelf;
//        }

//        return false;

//    }
//    private void ClosePopUI(string name)
//    {
//        if (m_dicPopUI.ContainsKey(name))
//        {
//            m_dicPopUI[name].SetActive(false);
//        }
//        //OnClosePopUI(m_dicPopUI, name);
//    }

//    private void CloseStoryUI(string name)
//    {
//        if (TryDestroyUI(m_dicStoryUI, name))
//        {
//            BaseUIRoot.gameObject.SetActive(true);
//            TipUIRoot.gameObject.SetActive(true);
//            PopUIRoot.gameObject.SetActive(true);
//            MenuPopUIRoot.gameObject.SetActive(true);
//            MessageUIRoot.gameObject.SetActive(true);
//            StoryUIRoot.gameObject.SetActive(true);
//        }
//    }

//    private void CloseBaseUI(string name)
//    {
//        if (m_dicBaseUI.ContainsKey(name))
//        {
//            m_dicBaseUI[name].SetActive(false);
//        }
//    }

//    private void CloseTipUI(string name)
//    {
//        TryDestroyUI(m_dicTipUI, name);
//    }

//    private void CloseMenuPopUI(string name)
//    {
//        TryDestroyUI(m_dicMenuPopUI, name);
//    }

//    private void CloseMessageUI(string name)
//    {
//        TryDestroyUI(m_dicMessageUI, name);
//    }

//    private void CloseDeathUI(string name)
//    {
//        if (TryDestroyUI(m_dicDeathUI, name))
//        {
//            // 关闭复活界面时 恢复节点的显示
//            m_instance.PopUIRoot.gameObject.SetActive(true);
//            m_instance.MenuPopUIRoot.gameObject.SetActive(true);
//            m_instance.TipUIRoot.gameObject.SetActive(true);
//        }
//    }
//    private bool TryDestroyUI(Dictionary<string, GameObject> curList, string curName)
//    {
//        if (curList == null)
//        {
//            return false;
//        }

//        if (!curList.ContainsKey(curName))
//        {
//            return false;
//        }

//        if (m_DicUIName.ContainsKey(curName) && !m_DicUIName[curName].isDestroyOnUnload)
//        {
//            curList[curName].SetActive(false);
//            if (m_dicCacheUI.ContainsKey(curName) == false && IsContainsData(curName) == false)
//            {
//                m_dicCacheUI.Add(curName, curList[curName]);
//            }
//        }
//        else
//        {
//            DestroyUI(curName, curList[curName]);
//        }

//        curList.Remove(curName);

//        return true;
//    }
//    /// <summary>
//    /// 
//    /// </summary>
//    /// <param name="curList"></param>
//    /// <param name="curName"></param>
//    private void OnLoadNewPopUI(Dictionary<string, GameObject> curList, string curName)
//    {
//        if (curList == null)
//        {
//            return;
//        }
//        if ("ChatPanel".Equals(curName))
//        {
//            return;
//        }
//        List<string> objToRemove = new List<string>();

//        if (curList.Count > 0)
//        {
//            objToRemove.Clear();
//            foreach (KeyValuePair<string, GameObject> objs in curList)
//            {
//                if (curName == objs.Key || objs.Value == null)
//                {
//                    continue;
//                }
//                objs.Value.SetActive(false);
//                objToRemove.Add(objs.Key);
//                if (m_DicUIName.ContainsKey(objs.Key) && m_DicUIName[objs.Key].isDestroyOnUnload)
//                {
//                    DestroyUI(objs.Key, objs.Value);
//                }
//                else
//                {
//                    if (m_dicCacheUI.ContainsKey(objs.Key) == false && IsContainsData(curName) == false)
//                    {
//                        m_dicCacheUI.Add(objs.Key, objs.Value);
//                    }

//                }
//            }

//            for (int i = 0; i < objToRemove.Count; i++)
//            {
//                if (curList.ContainsKey(objToRemove[i]))
//                {
//                    curList.Remove(objToRemove[i]);
//                }
//            }
//        }
//    }
//    //加载item 数据
//    public static GameObject LoadItem(UIPathData pathData)
//    {
//        if (null == m_instance)
//        {
//            Logger.ErrorLog("game manager is not init");
//            return null;
//        }

//        return m_instance.LoadUIItem(pathData);
//    }
//    /// <summary>
//    /// 获取 gameObjec 数据
//    /// </summary>
//    /// <param name="uiData"></param>
//    /// <returns></returns>
//    private GameObject LoadUIItem(UIPathData uiData)
//    {
//        GameObject retItem = null;
//        GameObject bundleObj = null;
//        if (uiData.uiGroupName != null)
//        {
//            bundleObj = BundleManager.GetGroupUIItem(uiData);
//        }
//        if (bundleObj == null)
//        {
//            bundleObj = BundleManager.LoadGroupNameUI(uiData);
//        }
//        if (bundleObj != null)
//        {
//            retItem = GameObject.Instantiate(bundleObj) as GameObject;
//        }
//        return retItem;
//    }
//    /// <summary>
//    /// 删除UI对象实体
//    /// </summary>
//    /// <param name="name"></param>
//    /// <param name="obj"></param>
//    private void DestroyUI(string name, GameObject obj)
//    {
//        //	Debug.Log ("ui destroyed");
//        BundleManager.OnUIDestroy(name, obj);
//        //DestroyImmediate(obj,true);
//        Destroy(obj);
//        obj = null;
//    }

//    private bool RemoveLoadDicRefCount(string pathName)
//    {
//        if (!m_dicWaitLoad.ContainsKey(pathName))
//        {
//            return false;
//        }

//        m_dicWaitLoad[pathName]--;
//        if (m_dicWaitLoad[pathName] <= 0)
//        {
//            m_dicWaitLoad.Remove(pathName);
//        }

//        return true;
//    }

//    /// <summary>
//    /// 创建atlas的maintexture
//    /// </summary>
//    public static void makeAtlas(bool _isClearList = true)
//    {
//        if (m_instance == null) return;
//        //CreateUIAtlas.CreateAtlasWithFileNameList(m_instance.m_listIconNameUI, ref m_instance.m_IconAtlas);
//    }

//    /// <summary>
//    /// 清除atlas的maintexture
//    /// </summary>
//    public void clearAtlasAuto()
//    {
//        if (m_instance == null) return;
//        m_listIconNameUI.Clear();
//        //CreateUIAtlas.RemoveAtlasTexture(ref m_IconAtlas);
//    }

//    /// <summary>
//    /// 清除atlas的maintexture
//    /// </summary>
//    public static void clearAtlas()
//    {
//        //if (m_instance == null) return;
//        //m_instance.m_listIconNameUI.Clear();
//        //CreateUIAtlas.RemoveAtlasTexture(ref m_instance.m_IconAtlas);
//    }

//    /// <summary>
//    /// 添加atlas图片
//    /// </summary>
//    /// <param name="iconName">图片路径和名称</param>
//    public void addAtlasIconWithItemName(string iconName)
//    {

//        if (m_listIconNameUI.Contains(iconName) == false)
//        {
//            m_listIconNameUI.Add(iconName);
//        }
//    }

//    public static void AddAtlasIcon(string iconName)
//    {
//        if (Instance() != null && !string.IsNullOrEmpty(iconName))
//        {
//            Instance().addAtlasIconWithItemName(iconName);
//        }
//    }

//    public static Texture LoadTexture(string resName, UIPathData uiData, Texture oldTexture)
//    {

//        if (null == m_instance)
//        {
//            Logger.ErrorLog("game manager is not init");
//            return null;
//        }

//        if (oldTexture != null)
//        {
//            BundleManager.ReleaseUITextureByTextureName(uiData.name, oldTexture.name);
//        }
//        return BundleManager.LoadTexture(resName, uiData.name);
//    }

//    private bool UIPanelStateChange = false;			//ui panel State
//    private bool ExistState = false;		//ui panel exist State
//    public static bool IsExistNotBaseUI()
//    {
//        if (m_instance.UIPanelStateChange)
//        {
//            m_instance.UIPanelStateChange = false;
//            return m_instance.ExistState;
//        }
//        m_instance.ExistState = false;
//        foreach (GameObject obj in m_instance.m_dicPopUI.Values)
//        {
//            if (obj.activeInHierarchy)
//            {
//                return false;
//            }

//        }
//        foreach (GameObject obj in m_instance.m_dicStoryUI.Values)
//        {
//            if (obj.activeInHierarchy)
//                return false;
//        } foreach (GameObject obj in m_instance.m_dicMenuPopUI.Values)
//        {
//            if (obj.activeInHierarchy)
//                return false;
//        }
//        m_instance.ExistState = true;
//        return m_instance.ExistState;
//    }

//    public UIAtlas makeStaticIconAtlas(string iconName)
//    {
//        if (ItemIconAtlas.GetSprite(iconName) != null)
//        {
//            return ItemIconAtlas;
//        }
//        else if (SkillIconAtlas.GetSprite(iconName) != null)
//        {
//            return SkillIconAtlas;
//        }
//        else if (HeroIconAtlas.GetSprite(iconName) != null)
//        {
//            return HeroIconAtlas;
//        }
//        else if (EquipIconAtlas.GetSprite(iconName) != null)
//        {
//            return EquipIconAtlas;
//        }

//        return m_IconAtlas;
//    }
//    public bool IsHaveBaseMainPanelUI()
//    {
//        bool signal = false;
//        if (BaseUIRoot.childCount == 0)
//        {
//            return false;
//        }
//        else
//        {
//            for (int i = 0; i < BaseUIRoot.childCount; i++)
//            {
//                if (BaseUIRoot.GetChild(i).name == "MainUIPanel")
//                {
//                    signal = true;
//                }
//            }
//        }
//        return signal;
//    }

//    //退出联盟界面崩溃异常处理 其他人可忽略 start//
//    public static void IsHaveLegionUI()
//    {
//        if (m_instance.PopUIRoot.childCount == 0 && m_instance.TipUIRoot.childCount == 0)
//        {
//            return;
//        }
//        for (int i = 0; i < m_instance.TipUIRoot.childCount; i++)
//        {
//            IsLegionUI(m_instance.TipUIRoot.GetChild(i).name);//先关TipUI

//        }
//        for (int j = 0; j < m_instance.PopUIRoot.childCount; j++)
//        {
//            if (IsLegionUI(m_instance.PopUIRoot.GetChild(j).name))//再关PopUI  然后跳转到大世界或者主城堡
//            {
//                if (GameManager.Instance.RunningSceneID == (int)GameDefine.SceneDefine.SCENE_DEFINE.SCENE_WorldMap)
//                {
//                    if (!IsUiShow(UIInfo.Panel_WorldMapUI))
//                        ShowUI(UIInfo.Panel_WorldMapUI);
//                }
//                else if (GameManager.Instance.RunningSceneID == (int)GameDefine.SceneDefine.SCENE_DEFINE.SCENE_MAINSCENE)
//                {
//                    if (!IsUiShow(UIInfo.MainUIPanel))
//                        ShowUI(UIInfo.MainUIPanel);
//                }
//            }
//        }
//    }
//    private static bool IsLegionUI(string name)
//    {
//        bool signal = false;
//        GameObject panel = null;
//        switch (name)
//        {
//            case "Panel_LMCorps"://军团界面
//                panel = GameObject.Find(name);
//                if (panel != null && panel.activeSelf)
//                {
//                    CloseUI(UIInfo.Panel_LMCorps);
//                    signal = true;
//                }
//                break;
//            case "Panel_LMMain"://联盟主界面
//                panel = GameObject.Find(name);
//                if (panel != null && panel.activeSelf)
//                {
//                    CloseUI(UIInfo.Panel_GuildMain);
//                    signal = true;
//                }
//                break;
//            case "Panel_LMHospital"://联盟捐献界面
//                panel = GameObject.Find(name);
//                if (panel != null && panel.activeSelf)
//                {
//                    CloseUI(UIInfo.Panel_GuildBloodBank);
//                    signal = true;
//                }
//                break;
//            case "Panel_LMStageSelect"://联盟副本选择章节界面
//                panel = GameObject.Find(name);
//                if (panel != null && panel.activeSelf)
//                {
//                    CloseUI(UIInfo.Panel_LMStageSelect);
//                    signal = true;
//                }
//                break;
//            case "Panel_LMPK"://联盟战界面
//                panel = GameObject.Find(name);
//                if (panel != null && panel.activeSelf)
//                {
//                    CloseUI(UIInfo.Panel_GuildWar);
//                    signal = true;
//                }
//                break;
//            case "Pop_LMMail"://联盟全员邮件
//                panel = GameObject.Find(name);
//                if (panel != null && panel.activeSelf)
//                {
//                    CloseUI(UIInfo.Panel_GuildMailBox);
//                    signal = true;
//                }
//                break;
//            case "Panel_LMSupport"://联盟援助界面
//                panel = GameObject.Find(name);
//                if (panel != null && panel.activeSelf)
//                {
//                    CloseUI(UIInfo.Panel_GuildSupport);
//                    signal = true;
//                }
//                break;
//            case "Panel_LMPresent"://联盟礼物界面
//                panel = GameObject.Find(name);
//                if (panel != null && panel.activeSelf)
//                {
//                    CloseUI(UIInfo.Panel_GuildGift);
//                    signal = true;
//                }
//                break;
//            case "Panel_LMShop"://联盟商店
//                panel = GameObject.Find(name);
//                if (panel != null && panel.activeSelf)
//                {
//                    CloseUI(UIInfo.Panel_GuildShop);
//                    signal = true;
//                }
//                break;
//            case "Panel_LMWhatNew"://联盟动态界面
//                panel = GameObject.Find(name);
//                if (panel != null && panel.activeSelf)
//                {
//                    CloseUI(UIInfo.Panel_GuildMessage);
//                    signal = true;
//                }
//                break;
//            case "Panel_LMSign"://联盟签到界面
//                panel = GameObject.Find(name);
//                if (panel != null && panel.activeSelf)
//                {
//                    CloseUI(UIInfo.Panel_GuildDonation);
//                    signal = true;
//                }
//                break;
//            case "Panel_LMBuilding"://公会建筑界面
//                panel = GameObject.Find(name);
//                if (panel != null && panel.activeSelf)
//                {
//                    CloseUI(UIInfo.Panel_GuildBuild);
//                    signal = true;
//                }
//                break;
//            case "Panel_LMDiplomacy"://联盟外交界面
//                panel = GameObject.Find(name);
//                if (panel != null && panel.activeSelf)
//                {
//                    CloseUI(UIInfo.Panel_GuildDiplomacy);
//                    signal = true;
//                }
//                break;
//            case "Panel_LMStage"://公会副本界面
//                panel = GameObject.Find(name);
//                if (panel != null && panel.activeSelf)
//                {
//                    CloseUI(UIInfo.Panel_LMStage);
//                    signal = true;
//                }
//                break;
//            case "Panel_LMInfo"://公会信息界面
//                panel = GameObject.Find(name);
//                if (panel != null && panel.activeSelf)
//                {
//                    CloseUI(UIInfo.Panel_GuildProperty);
//                    signal = true;
//                }
//                break;
//            case "Panel_LMMem"://成员界面
//                panel = GameObject.Find(name);
//                if (panel != null && panel.activeSelf)
//                {
//                    CloseUI(UIInfo.Panel_LeagueMember);
//                    signal = true;
//                }
//                break;
//            case "Panel_LMPet"://守护兽界面
//                panel = GameObject.Find(name);
//                if (panel != null && panel.activeSelf)
//                {
//                    CloseUI(UIInfo.Panel_GuardianInfo);
//                    signal = true;
//                }
//                break;
//            case "Panel_LMPetRecover"://守护兽治疗界面
//                panel = GameObject.Find(name);
//                if (panel != null && panel.activeSelf)
//                {
//                    CloseUI(UIInfo.Panel_GuildPetRecoverPanel);
//                    signal = true;
//                }
//                break;
//            case "Panel_LMRenming"://联盟任命界面
//                panel = GameObject.Find(name);
//                if (panel != null && panel.activeSelf)
//                {
//                    CloseUI(UIInfo.Panel_ApointDialog);
//                    signal = true;
//                }
//                break;
//            case "Panel_LMSettings"://联盟设置界面
//                panel = GameObject.Find(name);
//                if (panel != null && panel.activeSelf)
//                {
//                    CloseUI(UIInfo.Panel_GuildSetting);
//                    signal = true;
//                }
//                break;
//            case "Panel_LMStageEnter"://进入联盟副本布阵界面
//                panel = GameObject.Find(name);
//                if (panel != null && panel.activeSelf)
//                {
//                    CloseUI(UIInfo.Panel_LMStageEnter);
//                    signal = true;
//                }
//                break;
//            case "Panel_LMYuanjun"://联盟援军界面
//                panel = GameObject.Find(name);
//                if (panel != null && panel.activeSelf)
//                {
//                    CloseUI(UIInfo.Panel_GuildReinforceInfo);
//                    signal = true;
//                }
//                break;
//            case "Panel_LMJJ"://集结界面
//                panel = GameObject.Find(name);
//                if (panel != null && panel.activeSelf)
//                {
//                    CloseUI(UIInfo.Panel_LMMusterDetal);
//                    signal = true;
//                }
//                break;
//            default:
//                break;
//        }
//        return signal;
//    }
//    //退出联盟界面崩溃异常处理 其他人可忽略 end//

//}
