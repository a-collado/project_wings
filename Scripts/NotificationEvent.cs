using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NotificationEvent : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI notificationTextUI;

    [Header("Message Customisation")] 
    [SerializeField] [TextArea] private string notificationMessage;

    [Header("Notificatoin Removal")] 
    [SerializeField] private bool removeAfterTime = true;
    [SerializeField] private float disableTimer = 1.0f;
    
    [SerializeField] private Animator notificationsAnim;

    private void Awake()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        var notificationPanel = player.GetComponentInChildren<CanvasGroup>();
        notificationsAnim = notificationPanel.GetComponent<Animator>();
        notificationTextUI = notificationPanel.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void showNotification()
    {
        StartCoroutine(EnableNotification());
    }

    IEnumerator EnableNotification()
    {
        notificationTextUI.text = notificationMessage;
        notificationsAnim.Play("NotificationFadeIn");

        if (removeAfterTime)
        {
            yield return new WaitForSeconds(disableTimer);
            DisableNotification();
        }
    }

    void DisableNotification()
    {
        notificationsAnim.Play("NotificationFadeOut");
    }

    public void setMessage(string message)
    {
        notificationMessage = message;
    }
}
