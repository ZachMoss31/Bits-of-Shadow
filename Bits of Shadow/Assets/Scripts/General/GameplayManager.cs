using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
    public TextMeshProUGUI timer;
    public GameObject player;
    public RawImage[] lives;
    public Transform[] respawnPoints;
    public Canvas objectiveComplete;
    public Canvas gameOver;
    public TextMeshProUGUI timerResult;
    public AudioSource mainAudio;
    public int keys;

    [Header("Objective Forcefield")]
    public GameObject dome;

    float _time = 0f;
    float _sec;
    float _min;
    string _curTime;

    PlayerControls _playerControls;
    Animator _playerAnimator;

    /// <summary>
    /// Section Dealing with Respawning and Player Deaths
    /// </summary>
    [SerializeField]
    int playerLives = 3;
    [SerializeField]
    int _curSpawn;
    public ParticleSystem deathEffect;
    public CinemachineVirtualCamera mainCam;

    FadeControl _fadeControl;

    void Awake()
    {
        _playerAnimator = player.GetComponentInChildren<Animator>();
        _curSpawn = 0;
        timer.text = "Time: 00:00";
        _playerControls = player.GetComponent<PlayerControls>();
        _fadeControl = FindObjectOfType<FadeControl>();
        objectiveComplete.gameObject.SetActive(false);
    }

    void Update()
    {
        UpdateTimer();
    }

    public void KillPlayer(GameObject curPlayer)
    {
        Debug.Log("Player killed at " + curPlayer.transform.position.ToString());
        DecrementLives();

        //Play effects at the location sent from Kill Volume
        deathEffect.transform.position = curPlayer.transform.position;
        deathEffect.Play();

        //Stop the player where they are to play Animations / effects there
        _playerControls.StopPlayer();

        //Animate the player to play the Death Animation
        _playerControls.SetAllAnimations();
        //_playerAnimator.SetBool("isDead", true);
        _playerAnimator.Play("PlayerDead");

        //Call a coroutine here to wait and then move player etc
        StartCoroutine(RespawnPlayer());

        //Fade in and Out for death of player...
        _fadeControl.FadeOutAndBack();
    }

    IEnumerator RespawnPlayer()
    {
        _playerControls.enabled = false;
        yield return new WaitForSeconds(2.5f);
        deathEffect.Stop();
        _playerAnimator.enabled = false;
        _playerAnimator.enabled = true;
        _playerControls.SetAllAnimations();
        _playerControls.RestorePlayerValues();
        Debug.Log("Respawning player...");
        //_playerAnimator.Play("PlayerRespawn");
        //_playerAnimator.SetBool("isRespawning", true);
        MovePlayer();
    }

    void MovePlayer()
    {
        Debug.Log("Moving Player from " + player.transform.position.ToString() + " to " + respawnPoints[_curSpawn].transform.position.ToString());
        player.transform.position = respawnPoints[_curSpawn].transform.position;
        player.transform.rotation = respawnPoints[_curSpawn].transform.rotation;
        player.transform.forward = respawnPoints[_curSpawn].transform.forward;
        StartCoroutine(EnableControls(player.transform));
    }

    public void UpdateSpawn(int spawnPoint)
    {
        _curSpawn = spawnPoint;
    }

    IEnumerator EnableControls(Transform playerMovedPos)
    {
        yield return new WaitForFixedUpdate();
        _playerAnimator.Play("PlayerRespawn");
        if(playerMovedPos.transform.position == respawnPoints[_curSpawn].transform.position)
        {
            _playerAnimator.SetBool("isRespawning", false);
            _playerControls.enabled = true;
        }
    }

    public void UpdateTimer()
    {
        _time += Time.deltaTime;
        _min = Mathf.FloorToInt(_time / 60);
        _sec = Mathf.FloorToInt(_time % 60);
        timer.text = "Time: " + string.Format("{0:00}:{1:00}", _min, _sec);
    }

    public void DecrementLives()
    {
        if(playerLives > 1)
        {
            --playerLives;
            lives[playerLives].gameObject.SetActive(false);
            Debug.Log("Player now has " + playerLives + " lives");
        }
        else
        {
            --playerLives;
            lives[playerLives].gameObject.SetActive(false);
            Debug.Log("Game Over Condition Met");
            mainAudio.Stop();
            mainAudio.clip = null;
            StartCoroutine(GameOver());
        }
    }

    IEnumerator GameOver()
    {
        _fadeControl.StartFade(1);
        yield return new WaitForSeconds(2f);
        gameOver.gameObject.SetActive(true);
        player.gameObject.SetActive(false);
        timerResult.text += timer.text;
        Cursor.lockState = CursorLockMode.None;
    }

    public void IncrementKeys()
    {
        ++keys;
        CheckKeys();
        Debug.Log("Gained a key!");
    }

    public void CheckKeys()
    {
        if(keys == 3)
        {
            dome.gameObject.SetActive(false);
            objectiveComplete.gameObject.SetActive(true);
        }
    }
}