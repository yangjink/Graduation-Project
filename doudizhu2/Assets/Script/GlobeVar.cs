using System.Collections;
using UnityEngine;
//using GCGame.Table;

namespace GameDefine
{
    public class GlobeVar
    {
        //Const 定义
        public const int INVALID_ID = -1;

        public const System.UInt64 INVALID_GUID = 0xFFFFFFFFFFFFFFFFul;        //非法GUID


        public const int LAYER_DEFAULT = 0;                //默认的层
        public const int LAYER_UI = 5;                     //ui层
        public const int LAYER_3DUI = 10;                   //3D UI层
        public const int LAYER_MAINPANELUI = 11;
        //透明色
        private static Color transparentcolor = new Color(1, 1, 1, 0.005f);
        public static Color TRANSPARENT_COLOR { get { return transparentcolor; } }
		public static Color buildPanelRed = new Color (0.984f, 0.265f, 0.042f);
		public static Color buildPanelGreen = new Color (0.0625f, 0.953f, 0.48f);
		public static Color blue = new Color (0.04f,0.294f,0.45f);
		public static Color yellow = new Color (0.43f,0.21f,0.04f);

        //材质变色Material
        public const float BLUEMATERIAL_R = 0.208f;
        public const float BLUEMATERIAL_G = 0.565f;
        public const float BLUEMATERIAL_B = 0.741f;
		public const string KLocalConfigName = "LocalConfig.txt";
        public const string KConfigIsFullPackage = "bIsFullPackage";
        public const string KConfigIsTranslate = "bIsTranslate";
        public const string KConfigIsRate = "bIsRate";
        public const string KConfigIsOpenUpdate = "bIsOpenUpdate";
        public const string KDownLoadSize = "DownLoadSize";

   //登陆过程每个流程等待最长时间
//        public const float LOGIN_STATUS_TIMEOUT = 30.0f;         // 登陆每个流程等待最长时间


        //
//        public const int MAX_SHOW_STAGE_NUM = 3; //主城展示平台最大展示位

        //
//        public const int MAX_PVE_HERO_BATTLE_NUM = 3;               //PVE阵容最大英雄数


        //////////////////////////////////////////////////////////////////////////
        //队伍相关定义
        //////////////////////////////////////////////////////////////////////////
//        public const int MAX_USER_TEAM_MEMBER = 3;               //队伍最大人数

//        public const int MAX_SHOW_ONLINE_USER_COUNT = 20;               //在线显示玩家数量

        //是否启用NavAgent托管移动
//        public static bool OpenNavAgentFlag = false;

        //最小StopRange
        public const float MinStopRange = 0.001f;

        //该值限定最大追赶加速度，此值不宜过大，过大会导致客户端逻辑加速过猛，影响视觉感受
        public const float Max_Speed_Acceleration = 0.5f;
        //(单位：秒) 平均同步时间，设置越小同步越快，会受到 Max_Speed_Acceleration 值的限制
        public const float Avg_Synchrotron_Time = 5f;
        //物品背包相关
//        public const int MAX_BAG_TEAMLIST = 50;               //背包最大格子数量

//        private static int[] s_CostSoulNum = new int[5] { 20, 50, 100, 200, 300 };
//        public static int[] CostSoulNum { get { return s_CostSoulNum; } }

//        public const int MAX_ITEM_NUM = 99999999;
//        public const int MAX_STAMINA_VALUE = 10000;
//
//        public const float CONNECT_TIMEOUT = 30.0f;         // 等待超时时间，超过时间可继续发包

//        //领主属性
//        public enum Limited
//        {
//            MOBA_REWARD = 0,   //moba奖励 这个是表格的索引 Limited表格金币和钻石的上限。
//            MOBA_REWARD_INFO = 1,//moba奖励 各个项计算的数值。
//
////        }
//        public const int UserNameLenMax = 18;
//        //开启英雄升星功能的等级(领主等级)
//        public const int LEVEL_CAN_StarUpgrade = 8;
//        //开启装备升级功能的等级(领主等级)
//        public const int LEVEL_CAN_EquipEnhance = 6;

		public static float pixelY =  2*Screen.height;
		public static float pixelX = 2*Screen.width;
		public static float cameraMoveMaxX = 2;

		public static bool isClickNpcFun = false;
        public static float isBackMainClickOther = 0;
        public static float isMoveCameraClickOther = 0;

		public static bool IsMoveNpcTalk = false;       //场景UI是否跟随物体移动
        public static int IsExistClearResourceNum = 0;            //是否有场景UI需要卸载

        public static bool CameraHeightMove = false;
        //		public static bool LotteryShowHeroTips = false;		//抽卡判断

        public static int ItemEffectNum = 0;

        public static int buyPanelOpen = 0;                     //0,正常购买，1代表打折卷购买

        public static int OpenVip = 0;//0，打开领主  1打开VIP

        public static int OpenScientific_GuardianCub = 0;//0打开默认界面 1打开科研守护兽
    }
}

