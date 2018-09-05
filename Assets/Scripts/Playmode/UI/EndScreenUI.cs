using Playmode.Util.Values;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreenUI : MonoBehaviour
{
    private GameController gameController;

    void Awake()
    {
        gameController = GameObject.FindWithTag(Tags.GameController)
                           .GetComponent<GameController>();
        UpdateUI();
    }

    private void OnEnable()
    {
        gameController.OnGameOverChanged += UpdateUI;
    }

    private void OnDestroy()
    {
        gameController.OnGameOverChanged -= UpdateUI;
    }

    void UpdateUI()
    {
        gameObject.SetActive(gameController.IsGameOver);
    }
}
