using Assets.Scripts.Playmode.Util.Values;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void GameControllerEventHandler();

public class GameController : MonoBehaviour {

    EnnemyDiedEventChannel ennemyDiedEventChannel;

    public event GameControllerEventHandler OnDecrementNbEnnemys;

    public int NbPlayersLeft { get; private set; }
    // Use this for initialization
    void Awake () {
        ennemyDiedEventChannel = GetComponent<EnnemyDiedEventChannel>();
        NbPlayersLeft = Const.NB_ENNEMYS;
	}
    private void OnEnable()
    {
        ennemyDiedEventChannel.OnEventPublished += DecrementPlayersLeft;
    }

    private void OnDisable()
    {
        ennemyDiedEventChannel.OnEventPublished -= DecrementPlayersLeft;
    }
    // Update is called once per frame
    void Update () {
		
	}

    private void DecrementPlayersLeft()
    {
        NbPlayersLeft--;
        NotifiyDecrementNbEnnemys();
    }

    private void NotifiyDecrementNbEnnemys()
    {
        if (OnDecrementNbEnnemys != null) OnDecrementNbEnnemys();
    }

}
