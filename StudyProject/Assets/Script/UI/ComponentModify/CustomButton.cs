using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;
using UnityEngine.EventSystems;
[AddComponentMenu("UI/CustomButton", 31)]
public class CustomButton : Button
{
    [FormerlySerializedAs("onPress")]
    [SerializeField]
    private ButtonClickedEvent m_OnPress = new ButtonClickedEvent();

    [FormerlySerializedAs("onRelease")]
    [SerializeField]
    private ButtonClickedEvent m_OnRelease = new ButtonClickedEvent();
    public ButtonClickedEvent onPress
    {
        get { return m_OnPress; }
        set { m_OnPress = value; }
    }
   

    public ButtonClickedEvent onRelease
    {
        get { return m_OnRelease; }
        set { m_OnRelease = value; }
    }

    private void ButtonPress()
    {
        if (!IsActive() || !IsInteractable())
            return;

        UISystemProfilerApi.AddMarker("Button.onPress", this);
        m_OnPress.Invoke();
    }

    private void ButtonRelease()
    {
        if (!IsActive() || !IsInteractable())
            return;

        UISystemProfilerApi.AddMarker("Button.onRelease", this);
        m_OnRelease.Invoke();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        if (eventData.button != PointerEventData.InputButton.Left)
            return;
        ButtonPress();
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        if (eventData.button != PointerEventData.InputButton.Left)
            return;
        ButtonRelease();
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        if (eventData.button != PointerEventData.InputButton.Left)
            return;
        ButtonRelease();
    }
}
