using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField]
    TextMeshProUGUI questionText;

    [SerializeField]
    QuestionSO currentQuestion;

    [SerializeField]
    List<QuestionSO> questions;

    [Header("Answer")]
    [SerializeField]
    GameObject[] answerButtons;
    int correctAnswerIndex;
    bool hasAnsweredEarly;

    [Header("Button Colors")]
    [SerializeField]
    Sprite defaultAnswerSprite;

    [SerializeField]
    Sprite correctAnswerSprite;

    [Header("Timer")]
    [SerializeField]
    Image timerImage;
    Timer timer;
    bool timeUp;

    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    void Start()
    {
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    void Update()
    {
        timerImage.fillAmount = timer.GetFillFraction();
        if (timer.loadNextQuestion)
        {
            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }
        else if (!hasAnsweredEarly && !timer.GetIsAnsweringQuestion())
        {
            DisplayAnswer(-1); // Only works due to further checks
            toggleButtonsInteractableState(false);
            timeUp = true;
        }
    }

    public void onAnswerSelected(int index)
    {
        hasAnsweredEarly = true;
        DisplayAnswer(index);
        toggleButtonsInteractableState(false);
        timer.cancelTimer();
        scoreText.text = $"Score: {scoreKeeper.CalculateScorePercentage()}%";
    }

    void DisplayAnswer(int index)
    {
        Image buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
        if (index == correctAnswerIndex)
        {
            questionText.text = "Correct!";
            scoreKeeper.increaseCorrectAnswers();
        }
        else
        {
            if (timeUp)
            {
                questionText.text =
                    $"Time's up! right answer is {currentQuestion.GetAnswer(correctAnswerIndex)}";
            }
            else
            {
                questionText.text =
                    $"Incorrect! right answer is {currentQuestion.GetAnswer(correctAnswerIndex)}";
            }
        }
        buttonImage.sprite = correctAnswerSprite;
    }

    void setupQuiz()
    {
        correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
        questionText.text = currentQuestion.GetQuestion();
        setupAnswerButtons();
    }

    void setupAnswerButtons()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            string answer = currentQuestion.GetAnswer(i);
            buttonText.text = answer;
        }
    }

    void toggleButtonsInteractableState(bool state)
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    void toggleButtonsImagesToDefault()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Image buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }

    void GetNextQuestion()
    {
        if (questions.Count > 0)
        {
            scoreKeeper.increaseQuestionsSeen();
            toggleButtonsInteractableState(true);
            toggleButtonsImagesToDefault();
            GetRandomQuestion();
            setupQuiz();
        }
    }

    void GetRandomQuestion()
    {
        int index = Random.Range(0, questions.Count);
        currentQuestion = questions[index];
        if (questions.Contains(currentQuestion))
        {
            questions.Remove(currentQuestion);
        }
    }
}
