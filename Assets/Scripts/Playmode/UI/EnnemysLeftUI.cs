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

	// Use this for initialization
	void Awake () {

        textLabel = GetComponent<Text>();
        nbEnnemysLeft = Const.NB_ENNEMYS;
        textLabel.text = "Ennemy(s) left : " + nbEnnemysLeft;
        gameController = GameObject.FindWithTag(Tags.GameController).GetComponent<GameController>();

    }
    private void OnEnable()
    {
        gameController.OnDecrementNbEnnemys += UpdateUI;
    }
    private void OnDisable()
    {
        gameController.OnDecrementNbEnnemys -= UpdateUI;
    }

    // Update is called once per frame
    void UpdateUI () {
        nbEnnemysLeft--;
        textLabel.text = "Ennemy(s) left : " + nbEnnemysLeft;
    }
}
