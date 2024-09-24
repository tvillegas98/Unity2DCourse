using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{

    [SerializeField] float timeToCompleteQuestion = 30f;
    [SerializeField]float timeToShowCorrectAnswer = 10f;

    public bool loadNextQuestion = true;
    bool isAnsweringQuestion;
    float fillFraction;
    float timerValue;

    // Update is called once per frame
    void Update()
    {
        UpdateTimer();
    }

    public void cancelTimer(){
        timerValue = 0;
    }
    void UpdateTimer(){
        timerValue -= Time.deltaTime;

        if(isAnsweringQuestion){
            if(timerValue > 0){
                fillFraction = timerValue / timeToCompleteQuestion;
            }
            else{
                isAnsweringQuestion = false;
                timerValue = timeToShowCorrectAnswer;
            }
        }else{
            if(timerValue > 0){
                fillFraction = timerValue / timeToShowCorrectAnswer;
            }
            else{
                isAnsweringQuestion = true;
                timerValue = timeToCompleteQuestion;
                loadNextQuestion = true;
            }
        }
    }

    public float GetFillFraction (){
        return fillFraction;
    }

    public bool GetIsAnsweringQuestion() {
        return isAnsweringQuestion;
    }
}
