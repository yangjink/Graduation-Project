//using UnityEngine;
//using System.Collections;
//using GameDefine;
//using System.Collections.Generic;
//using System.IO;
//using System.Threading;

//public class GameManager : MonoBehaviour
//{
//    private static GameManager m_instance = null;           //GameManager以单件形式存在
//    public static GameManager Instance
//    {
//        get
//        {
//            return m_instance;
//        }
//    }

//    //private NetManager m_NetManager;
//    //public NetManager netManager { get { return m_NetManager; } }

//    public int RunningSceneID { get; set; }

//    public List<LightmapData> SceneLightMapAttr = new List<LightmapData>();

//    public List<string> SceneLightMapAttrBundleName = new List<string>();



//    public static bool m_bStartMain = true;

//    public static UIPathData friendZone_returnPath;

//    public static int shop_openType = 0;//0.抽卡 1.商店

//    public bool isLogin = true;//活动公告开游戏弹第一次

//    public static int seeType = 0;  // 那个界面打开的战斗详情 0：模块本身  1：聊天界面的开的详情

//    private int m_InitTableState = 0;

//    public static UIPathData GuildWarRecord_returnPath;   //联盟战UI上层目录

//    public static UIPathData GuildGuardian_returnPath;   //联盟守护兽UI上层目录

//    public static UIPathData GuildGuardianRecover_returnPath;   //联盟守护兽治疗UI上层目录

//    public static bool isMainUIShow = false;
//    public static bool isUIWorldMapShow = false;

//    public bool isleagueShow = false;


//    void OnApplicationFocus()
//    {
//#if !UNITY_EDITOR
//        //游戏恢复焦点状态请求数据
//        if(!GameManager.Instance.PlayerDataPool.IsLogin)
//            return;
//        CSPauseToFocus request = PacketDistributed.CreatePacket(MessageId.CSPauseToFocus) as CSPauseToFocus;
//        LoadingProxy.WaitingForNetRequest(request);
//#endif
//    }
//    public void CleanUp()
//    {
//        RunningSceneID = -1;
//        GlobeVar.IsExistClearResourceNum = 0;
//        SceneLightMapAttr.Clear();
//        SceneLightMapAttrBundleName.Clear();
//    }

//    void Awake()
//    {
//        if (null != m_instance)
//        {
//            Destroy(m_instance.gameObject);
//        }
//        m_instance = this;
//        DontDestroyOnLoad(this.gameObject);

//        UIInfo.Init();

//        // 重新加载UIPathConfig
//       // UIInfo.ReUpdateUIPathData();
//        //最后CleanUp一下
//        CleanUp();

//    }

//    private bool isSetSomeInfo = false;
//    /// <summary>
//    /// 可以后加载的一些内容
//    /// </summary>
//    public void SetGameManagerInfo()
//    {
//        if (isSetSomeInfo)
//            return;
//        isSetSomeInfo = true;

//        GameObject loadingUI = Instantiate(Resources.Load("Prefab/Logic/LoadingUIRoot")) as GameObject;
//        if (loadingUI != null)
//        {
//            loadingUI.transform.parent = gameObject.transform;
//            loadingUI.name = "LoadingUI";
//        }
//    }



//    public void LoadTable(bool isAgain = false)
//    {
//        if (LoadTableState == 1 && isAgain == false)
//        {
//            return;
//        }
//        try
//        {
//            m_InitTableState = 0;
//            m_InitTableState = 1;
//        }
//        catch (System.Exception ex)
//        {
//            m_InitTableState = 2;
//            Debug.LogException(ex);
//        }
//    }

//    public int LoadTableState { get { return m_InitTableState; } }

//    public bool InBackGround = false;


//    void Update()
//    {
//        //super limit backmainui
//        if (GlobeVar.isBackMainClickOther >= 0)
//            GlobeVar.isBackMainClickOther -= Time.deltaTime;
//        if (GlobeVar.isMoveCameraClickOther >= 0)
//        {
//            GlobeVar.isMoveCameraClickOther -= Time.deltaTime;
//        }
//    }

//}
