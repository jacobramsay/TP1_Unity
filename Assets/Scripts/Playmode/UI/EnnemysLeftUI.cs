using Assets.Scripts.Playmode.Util.Values;
using Playmode.Ennemy;
using Playmode.Entity.Status;
using Playmode.Util.Values;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnnemysLeftUI : MonoBehaviour {

    public Text textLabel;

    private GameController gameController;
    private int nbEnnemysLeft;

	void Awake ()
    {
        textLabel = GetComponent<Text>();
        nbEnnemysLeft = Const.NB_ENNEMYS;
        gameController = GameObject.FindWithTag(Tags.GameController).GetComponent<GameController>();
        UpdateText();
    }
    private void OnEnable()
    {
        gameController.OnDecrementNbEnnemys += UpdateUI;
    }
    private void OnDisable()
    {
        gameController.OnDecrementNbEnnemys -= UpdateUI;
    }

    void UpdateUI ()
    {
        nbEnnemysLeft--;
        UpdateText();
    }

    private void UpdateText()
    {
        textLabel.text = "Ennemy(s) left : " + nbEnnemysLeft;
    }
}
