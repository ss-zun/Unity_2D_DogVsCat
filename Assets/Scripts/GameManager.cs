using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject normalCat;
    public GameObject retryBtn;

    public RectTransform levelFront;
    public Text levelTxt;

    int level = 0; // 현재 레벨
    int score = 0; // 점수 5점당 1레벨

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        Application.targetFrameRate = 60;
        Time.timeScale = 1f;
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("MakeCat", 0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MakeCat()
    {
        Instantiate(normalCat);
    }

    public void GameOver()
    {
        retryBtn.SetActive(true);
        Time.timeScale = 0f;
    }

    public void AddScore()
    {
        score++;
        level = score / 5; // 0~4는 0레벨, 5~9는 1레벨 이런식으로 몫을 구함
        levelTxt.text = level.ToString();
        levelFront.localScale = new Vector3((score - level * 5) / 5.0f, 1f, 1f); // ex. 점수가 12면 12 - 10 = 2가 됨
                                                                                 // 소수점값을 얻기 위해 5.0f(실수값)으로 나누어줌
    }
}
