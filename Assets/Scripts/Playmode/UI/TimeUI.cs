using Playmode.Util.Values;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeUI : MonoBehaviour
{
    public Text timerLabel;

    private GameController gameController;
    private float time;

    private void Awake()
    {
        timerLabel = GetComponent<Text>();
        gameController = GameObject.FindWithTag(Tags.GameController).GetComponent<GameController>();
    }

    void Update()
    {
        if (gameController.IsGameStarted && !gameController.IsGameOver)
        {
            UpdateText();
        }
    }

    void UpdateText()
    {
        time += Time.deltaTime;

        var minutes = time / 60;
        var seconds = time % 60;
        var fraction = (time * 100) % 100;

        timerLabel.text = string.Format("{0:00} : {1:00} : {2:000}", minutes, seconds, fraction);
    }
}
