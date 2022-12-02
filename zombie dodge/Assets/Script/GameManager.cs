using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class GameManager : MonoBehaviour {   
    private static GameManager _instance;
    public static GameManager Instance {
        get{
            if(_instance == null){
                _instance = FindObjectOfType<GameManager>();
            }
            return _instance;
        }
    }

    [SerializeField]
    private GameObject zombie;
    [SerializeField]
    public int score;
    [SerializeField]
    public TMP_Text scoreTxt;
    [SerializeField]
    public Transform objbox;
    [SerializeField]
    public Text bestScore;
    [SerializeField]
    public GameObject panel;

    public bool stopTrigger = false;
    public AudioSource audioSrc;

    // Start is called before the first frame update
    void Start(){
        AudioSource audioSrc = GetComponent<AudioSource>();
        Screen.SetResolution(768, 1024, false);
    }

    // Update is called once per frame
    void Update(){}

    public void GameStart() {
        score = 0;
        scoreTxt.text = "Score: " + score;
        stopTrigger = false;
        StartCoroutine(CreateZombieRoutine());
        panel.SetActive(false);
    }

    public void GameOver() {
        stopTrigger = true;
        StopCoroutine(CreateZombieRoutine());
        if(score >= PlayerPrefs.GetInt("BestScore", 0)){
            PlayerPrefs.SetInt("BestScore",score);
        }
        bestScore.text = PlayerPrefs.GetInt("BestScore",0).ToString();
        panel.SetActive(true);
    }

    public void Score() {
        score++;
        scoreTxt.text = "score : " + score;
    }

    IEnumerator CreateZombieRoutine(){
        while(!stopTrigger) {
            CreateZombie();
            audioSrc.Play(0);
            yield return new WaitForSeconds(0.6f);
        }
    }

    private void CreateZombie() {
        Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(UnityEngine.Random.Range(0.0f, 1.0f), 1.1f, 0));
        pos.z = 0.0f;
        GameObject obj = Instantiate(zombie,pos, Quaternion.identity);
        obj.transform.parent = objbox.transform;
    }
}
