//using UnityEngine;
//using System.Collections.Generic;

//public class UIPathData
//{
//    public enum UIType
//    {
//        TYPE_ITEM,          // 只是用资源路径
//        TYPE_BASE,          // 常驻场景的UI，Close不销毁 一级UI
//        TYPE_POP,           // 弹出式UI，互斥，当前只能有一个弹出界面 二级弹出 在一级之上 阻止移动 无法操作后面UI
//        TYPE_STORY,         // 故事界面，故事界面出现，所有UI消失，故事界面关闭，其他界面恢复
//        TYPE_TIP,           // 三级弹出 在二级之上 不互斥 阻止移动 无法操作后面UI
//        TYPE_MENUPOP,       // TYPE_POP的一个分支 由主菜单MenuBar打开的二级UI 主要用于动态加载特殊屏蔽区域 其他和POPUI完全一致
//        TYPE_MESSAGE,       // 消息提示UI 在三级之上 一般是最高层级 不互斥 不阻止移动 可操作后面UI
//        TYPE_DEATH,         // 死亡UI 目前只有复活界面 用于添加复活特殊规则
//        TYPE_BLOOD,         //数字文字
//    };
	
	
	
//    // group捆绑打包名称，会将同一功能的UI打包在一起
//    // isMainAsset
//    // bDestroyOnUnload 只对PopUI起作用
//    public UIPathData(string _name, UIType _uiType, string groupName = null, string fistLoadInClientSceneType = null, bool bMainAsset = false, bool bDestroyOnUnload = true)
//    {
//        //if (string.IsNullOrEmpty(groupName))
//        //    path = "Prefab/UI/" + _name;
//        //else
//        //    path = "Prefab/UI/" + groupName + '/' + _name;
//        uiType = _uiType;
//        name = _name;
		
//        uiGroupName = groupName;
//        isMainAsset = bMainAsset;
//        isDestroyOnUnload = bDestroyOnUnload;
		
//        #if UNITY_ANDROID
		
//        // < 768M UI不进行缓存
//        if (SystemInfo.systemMemorySize < 768)
//        {
//            isDestroyOnUnload = true;
//        }
		
//        #endif

//        //UIManager.m_DicUIInfo.Add(path, this);
//        UIManager.m_DicUIName.Add(name, this);
//        if (!string.IsNullOrEmpty(fistLoadInClientSceneType))
//        {
//            string[] sceneTypeSplit = fistLoadInClientSceneType.Split(';');
//            foreach (string str in sceneTypeSplit)
//            {
//                try
//                {
//                    int type = int.Parse(str);
//                    if (UIManager.m_DicSceneBaseUI.ContainsKey(type))
//                    {
//                        UIManager.m_DicSceneBaseUI[type].Add(this);
//                    }
//                    else
//                    {
//                        List<UIPathData> pathDataList = new List<UIPathData>();
//                        pathDataList.Add(this);
//                        UIManager.m_DicSceneBaseUI.Add(type, pathDataList);
//                    }
//                }
//                catch (System.Exception ex)
//                {
//                    Debug.LogError(ex.Message + ex.StackTrace);                    
//                }
//            }
//        }
//    }
	
//    public string path;
//    public string name;
//    public UIType uiType;
//    public string uiGroupName;
//    public bool isMainAsset;            // 是否是主资源，如果主资源UI被关闭
//    public bool isDestroyOnUnload;
//}

//public class UIInfo
//{
//    private static UIInfo m_slefinit;   //这个不准用
//    public static void Init()
//    {//此函数不允许随便乱调用，也不允许在里面加任何东西
//        if (m_slefinit == null)
//            m_slefinit = new UIInfo();
//    }
//}