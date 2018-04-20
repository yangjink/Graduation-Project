using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class BaseView : MonoBehaviour
{
    public enum TriggerType
    {
        OnClick,
        OnHoverOver,
        OnHoverOut,
        OnPress,
        OnRelease,
        OnDoubleClick,
        OnSelect,
        OnDeselect,
        OnDragStart,
        OnDragEnd,
        OnDragOver,
        OnDragOut,
        OnDrag,
    }
    protected bool isMove = false;


    /// Add a button event for a gameobject
    public void AddButtonEvent(GameObject button, UIButtonEvent.OnClickEvent callback, object sender = null)
    {
        if (null == button)
            return;
        UIButtonEvent btnEvent = button.GetComponent<UIButtonEvent>();
        if (null == btnEvent)
        {
            btnEvent = button.AddComponent<UIButtonEvent>();
        }
        btnEvent.Callback = callback;
        btnEvent.SenderParam = sender;
    }

    public void SetBtnToGray(GameObject btn, bool isToGray)
    {
        UISprite spriteButton = btn.GetComponent<UISprite>();

        UILabel label = btn.GetComponentInChildren<UILabel>();
        if (label != null)
        {
            if (isToGray)
                SetLabelToGray(label);
            else
                SetLabelToRed(label);
        }
    }

    #region 统一置灰 色号 3B3B3BFF
    Color colorGray = new Color(59f / 255, 59f / 255, 59f / 255);
    Color colorRed = new Color(88f / 255, 13f / 255, 0);
    public void SetBtnAndLabelToGray(GameObject btn)
    {
        SetButtonEnabled(btn, false);
        UILabel label = btn.GetComponentInChildren<UILabel>();
        if (label != null)
            label.effectColor = colorGray;//Color.gray;
    }
    public void SetEnabled(GameObject btn, bool enable)
    {
        BoxCollider collider = btn.GetComponent<BoxCollider>();
        if (null != collider) collider.enabled = enable;
    }
    public void SetLabelToGray(UILabel label)
    {
        if (label != null)
            label.effectColor = colorGray;
    }
    public void SetLabelToRed(UILabel label)
    {
        if (label != null)
            label.effectColor = colorRed;
    }
    #endregion

    Dictionary<GameObject, Color> recordedColor = new Dictionary<GameObject, Color>();
    public void RecordColor(GameObject btn)
    {
        if (!recordedColor.ContainsKey(btn))
        {
            if (btn.GetComponentInChildren<UILabel>() == null)
                return;
            Color tmpColor = btn.GetComponentInChildren<UILabel>().effectColor;
            recordedColor.Add(btn, new Color(tmpColor.r, tmpColor.g, tmpColor.b));
        }
    }
    public void ControlDisable(GameObject btn, bool colliderEnable = false)
    {
        if (btn == null)
            return;
        if (!recordedColor.ContainsKey(btn))
            RecordColor(btn);
        btn.GetComponent<BoxCollider>().enabled = colliderEnable;
        UISprite sprite = btn.GetComponent<UISprite>();

    }
    public void ControlEnable(GameObject btn)
    {
        if (!recordedColor.ContainsKey(btn))
            RecordColor(btn);
        if (btn == null)
            return;
        btn.GetComponent<BoxCollider>().enabled = true;
        UISprite sprite = btn.GetComponent<UISprite>();

        UILabel label = btn.GetComponentInChildren<UILabel>();
        if (label != null)
        {

            label.effectColor = recordedColor[btn];
        }
    }
    public void SetButtonEnabled(GameObject m_btn)
    {
        BoxCollider collider = m_btn.GetComponent<BoxCollider>();
        if (null != collider) collider.enabled = false;
    }
    public void SetButtonEnabled(GameObject m_btn, bool enable = true)
    {
        BoxCollider collider = m_btn.GetComponent<BoxCollider>();
        if (null != collider) collider.enabled = enable;

        UISprite sprite = m_btn.GetComponent<UISprite>();
        if (null != sprite) sprite.ColorGrey = !enable;

        UISprite[] sprite_childs = m_btn.GetComponentsInChildren<UISprite>();
        if (null != sprite_childs && sprite_childs.Length > 0)
        {
            foreach (var item in sprite_childs)
                if (null != item)
                    item.ColorGrey = !enable;
        }

        UIButton button = m_btn.GetComponent<UIButton>();
        if (null != button) button.ColorGrey = !enable;
    }
    public void SetSliderEnabled(UISlider m_Slider, bool enable = true)
    {
        BoxCollider collider = m_Slider.gameObject.GetComponent<BoxCollider>();
        if (null != collider) collider.enabled = enable;

    }
    public void AddButtonTriggerEvent(GameObject button, EventDelegate.Callback callback, TriggerType triggerType)
    {
        UIEventTrigger btnTgr = button.AddMissingComponent<UIEventTrigger>();
        List<EventDelegate> eventList = new List<EventDelegate>();
        eventList.Add(new EventDelegate(callback));
        switch (triggerType)
        {
            case TriggerType.OnClick:
                btnTgr.onClick = eventList;
                break;
            case TriggerType.OnHoverOver:
                btnTgr.onHoverOver = eventList;
                break;
            case TriggerType.OnHoverOut:
                btnTgr.onHoverOut = eventList;
                break;
            case TriggerType.OnPress:
                btnTgr.onPress = eventList;
                break;
            case TriggerType.OnRelease:
                btnTgr.onRelease = eventList;
                break;
            case TriggerType.OnDoubleClick:
                btnTgr.onDoubleClick = eventList;
                break;
            case TriggerType.OnSelect:
                btnTgr.onSelect = eventList;
                break;
            case TriggerType.OnDeselect:
                btnTgr.onDeselect = eventList;
                break;
            case TriggerType.OnDragStart:
                btnTgr.onDragStart = eventList;
                break;
            case TriggerType.OnDragEnd:
                btnTgr.onDragEnd = eventList;
                break;
            case TriggerType.OnDragOver:
                btnTgr.onDragOver = eventList;
                break;
            case TriggerType.OnDragOut:
                btnTgr.onDragOut = eventList;
                break;
            case TriggerType.OnDrag:
                btnTgr.onDrag = eventList;
                break;
            default:

                break;
        }

    }
    public void AddButtonEvent(GameObject button, UIButtonEvent.OnClickEvent callback, object sender, AudioClip clickSound)
    {
        AddButtonEvent(button, callback, sender);
        UIButtonEvent btnEvent = button.GetComponent<UIButtonEvent>();
        if (null == btnEvent)
        {
            return;
        }

        //btnEvent.ClickSound = clickSound;
    }

    /// Remove the button event.
    public void RemoveEvent(GameObject button)
    {
        UIButtonEvent btnEvent = button.GetComponent<UIButtonEvent>();
        if (null != btnEvent)
        {
            btnEvent.Callback = null;
            btnEvent.SenderParam = null;
        }
    }
    /// Remove the button event.
    public void RemoveTriggerEvent(GameObject button, EventDelegate.Callback callback, TriggerType triggerType)
    {
        UIEventTrigger btnTgr = button.AddMissingComponent<UIEventTrigger>();
        if (btnTgr == null)
            return;
        List<EventDelegate> eventList = new List<EventDelegate>();
        eventList.Add(new EventDelegate(null));
        switch (triggerType)
        {
            case TriggerType.OnClick:

                btnTgr.onClick = eventList;
                break;
            case TriggerType.OnHoverOver:
                btnTgr.onHoverOver = eventList;
                break;
            case TriggerType.OnHoverOut:
                btnTgr.onHoverOut = eventList;
                break;
            case TriggerType.OnPress:
                btnTgr.onPress = eventList;
                break;
            case TriggerType.OnRelease:
                btnTgr.onRelease = eventList;
                break;
            case TriggerType.OnDoubleClick:
                btnTgr.onDoubleClick = eventList;
                break;
            case TriggerType.OnSelect:
                btnTgr.onSelect = eventList;
                break;
            case TriggerType.OnDeselect:
                btnTgr.onDeselect = eventList;
                break;
            case TriggerType.OnDragStart:
                btnTgr.onDragStart = eventList;
                break;
            case TriggerType.OnDragEnd:
                btnTgr.onDragEnd = eventList;
                break;
            case TriggerType.OnDragOver:
                btnTgr.onDragOver = eventList;
                break;
            case TriggerType.OnDragOut:
                btnTgr.onDragOut = eventList;
                break;
            case TriggerType.OnDrag:
                btnTgr.onDrag = eventList;
                break;
            default:

                break;
        }

    }

    /// Add a button event for a gameobject
    public void AddButtonPressEvent(GameObject button, UIButtonEvent.OnPressEvent callback, object sender = null)
    {
        UIButtonEvent btnEvent = button.GetComponent<UIButtonEvent>();
        if (null == btnEvent)
        {
            btnEvent = button.AddComponent<UIButtonEvent>();
        }
        btnEvent.PressCallback = callback;
        btnEvent.SenderParam = sender;
    }

    public void AddButtonPressCallBack(GameObject button, UIPressEvent.OnPressCallback callback, object sender = null)
    {
        UIButtonEvent btnEvent = button.GetComponent<UIButtonEvent>();
        if (null == btnEvent)
        {
            btnEvent = button.AddComponent<UIButtonEvent>();
        }

        //         btnEvent.PressCallback = callback;
        //         btnEvent.SenderParam = sender;

        UIPressEvent press_event = NGUITools.AddMissingComponent<UIPressEvent>(button);
        if (press_event != null)
        {
            btnEvent.PressCallback = press_event.OnMyPress;
            btnEvent.SenderParam = null;

            press_event.m_PressCallback = callback;
            press_event.m_ObjCallback = sender;
        }
    }

    /// Remove the button event.
    public void RemovePressEvent(GameObject button)
    {
        UIButtonEvent btnEvent = button.GetComponent<UIButtonEvent>();
        if (null != btnEvent)
        {
            btnEvent.PressCallback = null;
            btnEvent.SenderParam = null;
        }
    }



    // Add a slide left over event for a gameobject
    public void AddSlideLeftOverEvent(GameObject obj, UISlideEvent.OnSlideEvent callback, object sender = null)
    {
        UISlideEvent slideEvent = obj.GetComponent<UISlideEvent>();
        if (null == slideEvent)
        {
            slideEvent = obj.AddComponent<UISlideEvent>();
        }
        slideEvent.SlideLeftOver = callback;
        slideEvent.SenderParam = sender;
    }

    // Add a slide right over event for a gameobject
    public void AddSlideRightOverEvent(GameObject obj, UISlideEvent.OnSlideEvent callback, object sender = null)
    {
        UISlideEvent slideEvent = obj.GetComponent<UISlideEvent>();
        if (null == slideEvent)
        {
            slideEvent = obj.AddComponent<UISlideEvent>();
        }
        slideEvent.SlideRightOver = callback;
        slideEvent.SenderParam = sender;
    }

    // Add a sliding event for a gameobject
    public void AddSlidingEvent(GameObject obj, UISlideEvent.OnSlidingEvent callback, object sender = null)
    {
        UISlideEvent slideEvent = obj.GetComponent<UISlideEvent>();
        if (null == slideEvent)
        {
            slideEvent = obj.AddComponent<UISlideEvent>();
        }
        slideEvent.Sliding = callback;
        slideEvent.SenderParam = sender;
    }

    // Add a sliding left event for a gameobject
    public void AddSlidingLeftEvent(GameObject obj, UISlideEvent.OnSlidingEvent callback, object sender = null)
    {
        UISlideEvent slideEvent = obj.GetComponent<UISlideEvent>();
        if (null == slideEvent)
        {
            slideEvent = obj.AddComponent<UISlideEvent>();
        }
        slideEvent.SlidingLeft = callback;
        slideEvent.SenderParam = sender;
    }

    // Add a sliding right event for a gameobject
    public void AddSlidingRightEvent(GameObject obj, UISlideEvent.OnSlidingEvent callback, object sender = null)
    {
        UISlideEvent slideEvent = obj.GetComponent<UISlideEvent>();
        if (null == slideEvent)
        {
            slideEvent = obj.AddComponent<UISlideEvent>();
        }
        slideEvent.SlidingRight = callback;
        slideEvent.SenderParam = sender;
    }

    //Remove all slide event
    public void RemoveAllSlideEvent(GameObject obj)
    {
        UISlideEvent slideEvent = obj.GetComponent<UISlideEvent>();
        if (null != slideEvent)
        {
            slideEvent.SlideStart = null;
            slideEvent.Click = null;
            slideEvent.SlideLeftOver = null;
            slideEvent.SlideRightOver = null;
            slideEvent.Sliding = null;
            slideEvent.SlidingLeft = null;
            slideEvent.SlidingRight = null;
            slideEvent.SenderParam = null;
            Destroy(slideEvent);
        }
    }

    /// Find GameObject named 'objName'. Use the Transform.FindChild instead of it if you can.
    public GameObject FindChild(string objName, GameObject fromParent = null, bool isRecursively = true)
    {
        GameObject parent = fromParent;
        if (parent == null)
        {
            parent = this.gameObject;
        }

        if (isRecursively)
        {
            Transform child = FindChild(parent.transform, objName);
            if (null != child)
            {
                return child.gameObject;
            }
            else
            {
                return null;
            }
        }
        else
        {
            for (int idx = 0; idx < parent.transform.childCount; idx++)
            {
                Transform child = parent.transform.GetChild(idx);
                if (child == null)
                {
                    continue;
                }
                if (child.name.Equals(objName))
                {
                    return child.gameObject;
                }
            }
        }

        return null;
    }

    /// Find Component named 'objName'
    public T FindComponent<T>(string objName, GameObject fromParent = null) where T : Component
    {
        GameObject obj = FindChild(objName, fromParent);
        if (null != obj)
        {
            return obj.GetComponent<T>();
        }
        else
        {
            return null;
        }
    }

    /// Destroy the Game Object and all it's children.
    public void DestroySelf(GameObject obj)
    {
        obj.name += "[delete]";
        Destroy(obj);
    }

    /// Destroy all children except itself.
    public void DestroyChildren(GameObject parent)
    {
        if (parent == null)
        {
            return;
        }

        for (int idx = 0; idx < parent.transform.childCount; idx++)
        {
            Transform child = parent.transform.GetChild(idx);
            if (child == null)
            {
                continue;
            }
            child.name += "[delete]";
            Destroy(child.gameObject);
        }
    }

    private Transform FindChild(Transform parent, string objName)
    {
        if (parent.name.Equals(objName))
        {
            return parent;
        }
        else
        {
            for (int idx = 0; idx < parent.transform.childCount; idx++)
            {
                Transform child = parent.transform.GetChild(idx);
                if (child == null)
                {
                    continue;
                }
                child = FindChild(child, objName);
                if (null != child)
                {
                    return child;
                }
            }
            return null;
        }
    }

    public virtual void SetInstance()
    {

    }


    public virtual void SetTexture(Texture texture)
    {

    }
}
