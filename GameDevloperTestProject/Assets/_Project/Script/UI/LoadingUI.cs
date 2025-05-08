using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadingUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI loadingText;

    public float interval = 0.5f;

    private float timer;
    private int dotCount = 1;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= interval)
        {
            dotCount++;
            if (dotCount > 3)
                dotCount = 1;

            loadingText.text = "Resting" + new string('.', dotCount);
            timer = 0f;
        }
    }
}
