using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EndScreen : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI finalScoreText;
    ScoreKeeper scoreKeeper;

    private void Awake() {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    public void ShowFinalScore(){
        finalScoreText.text = $"Congratulations!\n You got a score of {scoreKeeper.CalculateScorePercentage()}%";
    }
}
