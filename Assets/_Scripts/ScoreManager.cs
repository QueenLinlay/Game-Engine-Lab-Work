using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour

{
    public static ScoreManager instance;
    public GameObject ChangingText;
    int score = 0;
    // Start is called before the first frame update
    void Awake()
    {
        if(!instance)
        {
            instance = this;
        }
    }

    public void ChangeScore(int coinValue)
    {
        score += coinValue;
        Debug.Log(score);
    }

    public void Update()
    {
        ChangingText.GetComponent<Text>().text = score.ToString();
    }
}
