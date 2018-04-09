using UnityEngine;
using System.Collections;

//封装按住事件回调 按住后，DelayTime后回调
public class UIPressEvent : MonoBehaviour
{
    public delegate void OnPressCallback(GameObject btn, object sender = null);
    public float DelayTime = 0.5f;
    private bool m_bPress = false;
    private float m_bContinuedTime = 0.0f;
    private bool m_bNotity = false;

    public OnPressCallback m_PressCallback;
    public object m_ObjCallback;
    public void OnMyPress(GameObject obj, bool isPress, object sender)
    {
        m_bPress = isPress;
        m_bContinuedTime = 0.0f;
        m_bNotity = false;
    }

    void Update()
    {
        if (m_bPress && !m_bNotity)
        {
            m_bContinuedTime += Time.deltaTime;
            if (m_bContinuedTime > DelayTime)
            {
                m_bNotity = true;
                if (m_PressCallback != null)
                {
                    m_PressCallback(this.gameObject, m_ObjCallback);
                }
            }
        }
    }
}
