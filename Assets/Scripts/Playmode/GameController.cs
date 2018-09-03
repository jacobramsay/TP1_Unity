using Assets.Scripts.Playmode.Util.Values;
using Playmode.Application;
using Playmode.Util.Values;
using UnityEngine;

public delegate void GameControllerEventHandler();

public class GameController : MonoBehaviour {

    EnnemyDiedEventChannel ennemyDiedEventChannel;
    private MainController mainController;

    public event GameControllerEventHandler OnDecrementNbEnnemys;
    public event GameControllerEventHandler OnGameOverChanged;
    public event GameControllerEventHandler OnGameStartedChanged;

    private bool isGameStarted;
    private bool isGameOver;
    private string startKey;

    public int NbPlayersLeft { get; private set;}

    public bool IsGameStarted
    {
        get { return isGameStarted; }
        set
        {
            if (isGameStarted != value)
            {
                isGameStarted = value;
                NotifyGameStartedChanged();
            }
        }
    }

    public bool IsGameOver
    {
        get { return isGameOver; }
        set
        {
            if (isGameOver != value)
            {
                isGameOver = value;
                NotifyGameOverChanged();
            }
        }
    }
    // Use this for initialization
    void Awake () {
        ennemyDiedEventChannel = GetComponent<EnnemyDiedEventChannel>();
        NbPlayersLeft = Const.NB_ENNEMYS;
        mainController = GameObject.FindWithTag(Tags.MainController).GetComponent<MainController>();
        startKey = "space";
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
    void Update ()
    {
        if (!IsGameStarted)
        {
            if (Input.GetKeyDown(startKey))
            {
                StartGame();
            }
        }
        else if (IsGameOver)
        {
            if (Input.GetKeyDown(startKey))
            {
                RestartGame();
            }
        }
    }
    private void StartGame()
    {
        IsGameStarted = true;
    }

    private void StopGame()
    {
        IsGameOver = true;
    }
    private void RestartGame()
    {
        mainController.RestartGame();
    }

    private void DecrementPlayersLeft()
    {
        NbPlayersLeft--;
        NotifiyDecrementNbEnnemys();

        if(NbPlayersLeft <= 1)
        {
            StopGame();
        }
    }

    private void NotifiyDecrementNbEnnemys()
    {
        if (OnDecrementNbEnnemys != null) OnDecrementNbEnnemys();
    }
    private void NotifyGameStartedChanged()
    {
        if (OnGameStartedChanged != null) OnGameStartedChanged();
    }

    private void NotifyGameOverChanged()
    {
        if (OnGameOverChanged != null) OnGameOverChanged();
    }
}
