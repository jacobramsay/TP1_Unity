using Playmode.Util.Values;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreenUI : MonoBehaviour
{
    private GameController gameController;

    // Use this for initialization
    void Awake()
    {
        gameController = GameObject.FindWithTag(Tags.GameController)
                           .GetComponent<GameController>();
        gameController.OnGameOverChanged += UpdateUI;

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

    // Update is called once per frame
    void UpdateUI()
    {
        gameObject.SetActive(gameController.IsGameOver);
    }
}
