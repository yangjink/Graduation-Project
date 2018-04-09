using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameDefine;
public class UIButtonEvent : MonoBehaviour
{
    public delegate void OnClickEvent(GameObject btn, object sender = null);
    public delegate void OnPressEvent(GameObject btn, bool isPressed, object sender = null);

    public OnClickEvent Callback { get; set; }
    public OnPressEvent PressCallback { get; set; }
    public object SenderParam { get; set; }

    [System.NonSerialized]
    public bool IsNeedSound = true;

    private void OnDestroy()
    {
        SenderParam = null;
        Callback = null;
        PressCallback = null;
    }

    void OnClick()
    {
            Callback(this.gameObject, SenderParam);
    }
    void OnPress(bool isPressed)
    {
        if (null != PressCallback)
        {
            PressCallback(this.gameObject, isPressed, SenderParam);
        }

        if (isPressed)
        {
            Play();
        }
    }

    public void setEnable(bool bEnable)
    {
        BoxCollider box = this.gameObject.GetComponent<BoxCollider>();
        if (box != null)
        {
            box.enabled = bEnable;
        }
    }


    public int SoundID = -1;

    bool canPlay
    {
        get
        {
            if (!enabled) return false;
            UIButton btn = GetComponent<UIButton>();
            return (btn == null || btn.isEnabled);
        }
    }

    public void Play()
    {

    }
}
