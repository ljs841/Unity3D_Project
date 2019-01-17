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

    public ButtonClickedEvent onPress
    {
        get { return m_OnPress; }
        set { m_OnPress = value; }
    }

    private void ButtonPress()
    {
        if (!IsActive() || !IsInteractable())
            return;

        UISystemProfilerApi.AddMarker("Button.onClick", this);
        m_OnPress.Invoke();

        
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        if (eventData.button != PointerEventData.InputButton.Left)
            return;
        ButtonPress();
    }
}
