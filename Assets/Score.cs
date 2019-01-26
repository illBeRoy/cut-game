using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static int score = 0;


    void updateScore() {
        gameObject.GetComponent<Text>().text = Score.score.ToString();
    }

    public void addScore(int score) {
        Score.score += score;
    }
    // Start is called before the first frame update
    void Start()
    {
        Score.score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        updateScore();
    }
}
