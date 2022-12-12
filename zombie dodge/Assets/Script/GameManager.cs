using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
    public AudioSource zombieSrc;
    public AudioSource pointSrc;
    public AudioSource deathSrc;
    public float spawnTime = 2f;


    // Start is called before the first frame update
    void Start(){
        AudioSource zombieSrc = GetComponent<AudioSource>();
        AudioSource pointSrc = GetComponent<AudioSource>();
        Screen.SetResolution(768, 1024, false);
    }

    // Update is called once per frame
    void Update(){
        if((score%20)==0&&score!=0){
            spawnTime-=5f;
        }
    }

    public void GameStart() {
        score = 0;
        scoreTxt.text = "Score: " + score;
        stopTrigger = false;
        StartCoroutine(CreateZombieRoutine());
        panel.SetActive(false);
    }

    public void LoadGame(){
        SceneManager.LoadScene("playing");
    }

    public void GameOver() {
        stopTrigger = true;
        deathSrc.Play();
        StopCoroutine(CreateZombieRoutine());
        if(score >= PlayerPrefs.GetInt("BestScore", 0)){
            PlayerPrefs.SetInt("BestScore",score);
        }
        bestScore.text = PlayerPrefs.GetInt("BestScore",0).ToString();
        panel.SetActive(true);
    }

    public void Score() {
        score++;
        scoreTxt.text = "Score: " + score;
        pointSrc.Play(0);
    }

    IEnumerator CreateZombieRoutine(){
        while(!stopTrigger) {
            CreateZombie();
            zombieSrc.Play(0);
            yield return new WaitForSeconds(spawnTime);
        }
    }

    private void CreateZombie() {
        Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(UnityEngine.Random.Range(0.0f, 1.0f), 1.1f, 0));
        pos.z = 0.0f;
        GameObject obj = Instantiate(zombie,pos, Quaternion.identity);
        obj.transform.parent = objbox.transform;
    }
}
