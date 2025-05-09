using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Announcement : MonoBehaviour
{
    public static Announcement Instance { get; private set; }
    
    [SerializeField] private TextMeshProUGUI announcementText;
    [SerializeField] private float announcementDuration = 3f;
    private float announcementTimer;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Update()
    {
        if (announcementText.text != String.Empty)
        {
            announcementTimer += Time.deltaTime;
            if (announcementTimer >= announcementDuration)
            {
                announcementText.text = String.Empty;
                announcementTimer = 0f;
            }
        }
    }

    public void SetAnnouncementText(string text)
    {
        announcementText.text = text;
        announcementTimer = 0f;
    }
}
