using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public Button[] answerButtons;
    public Question[] questions;
    private int currentQuestionIndex = 0;
    private int selectedAnswer = -1;
    private bool waitingResult = false;
    private bool answerRevealed = false;

    public Color normalColor = Color.white;
    public Color selectedColor = Color.yellow;
    public Color correctColor = Color.green;
    public Color wrongColor = Color.red;

    private void Start()
    {
        ShowQuestion();
    }

    void ShowQuestion()
    {
        Question q = questions[currentQuestionIndex];
        questionText.text = q.question;
        for (int i = 0; i < answerButtons.Length; i++)
        {
            int index = i;
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = q.answers[i];

            answerButtons[i].onClick.RemoveAllListeners();
            answerButtons[i].onClick.AddListener(() => OnAnswerClicked(index));
        }
    }

    public void OnAnswerClicked(int index)
    {
        if (waitingResult)
            return;

        // Primer clic o cambio de selección
        if (selectedAnswer != index)
        {
            SelectAnswer(index);
            return;
        }

        // Segundo clic en la misma respuesta
        StartCoroutine(RevealAnswer());
    }

    void SelectAnswer(int index)
    {
        selectedAnswer = index;

        for (int i = 0; i < answerButtons.Length; i++)
        {
            Image image = answerButtons[i].GetComponent<Image>();

            image.color =
                (i == index) ? selectedColor : normalColor;
        }
    }

    IEnumerator RevealAnswer()
    {
        waitingResult = true;

        foreach (Button button in answerButtons)
            button.interactable = false;

        AudioManager.Instance.PlayTension();
        //yield return new WaitForSeconds(2f);
        yield return new WaitForSeconds(
            AudioManager.Instance.tensionSound.length
        );

        Debug.Log("Audio terminado");

        Question q = questions[currentQuestionIndex];

        if (selectedAnswer == q.correctAnswer)
        {
            SetButtonColor(
                answerButtons[selectedAnswer],
                correctColor);
        }
        else
        {
            SetButtonColor(
                answerButtons[selectedAnswer],
                wrongColor);

            SetButtonColor(
                answerButtons[q.correctAnswer],
                correctColor);
        }
        answerRevealed = true;
    }

    void SetButtonColor(Button button, Color color)
    {
        button.GetComponent<Image>().color = color;
    }

    void CheckAnswer(int index)
    {
        Question q = questions[currentQuestionIndex];

        if (selectedAnswer == q.correctAnswer)
        {
            AudioManager.Instance.PlayCorrect();

            SetButtonColor(
                answerButtons[selectedAnswer],
                correctColor);
        }
        else
        {
            AudioManager.Instance.PlayWrong();

            SetButtonColor(
                answerButtons[selectedAnswer],
                wrongColor);

            SetButtonColor(
                answerButtons[q.correctAnswer],
                correctColor);
        }
    }

    void NextQuestion()
    {
        currentQuestionIndex++;

        if (currentQuestionIndex >= questions.Length)
        {
            Debug.Log("Fin del juego");
            return;
        }

        ShowQuestion();
    }

    void ResetButtons()
    {
        foreach (Button button in answerButtons)
        {
            button.interactable = true;
            button.GetComponent<Image>().color = normalColor;
        }

        selectedAnswer = -1;
        waitingResult = false;
    }
}
