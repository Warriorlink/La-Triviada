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

    public Button nextButton;
    public Button previousButton;

    public Color normalColor = Color.white;
    public Color selectedColor = Color.yellow;
    public Color correctColor = Color.green;
    public Color wrongColor = Color.red;

    private void Start()
    {
        ShowQuestion();
    }

    public Question GetCurrentQuestion()
    {
        return questions[currentQuestionIndex];
    }

    public Button[] GetAnswerButtons()
    {
        return answerButtons;
    }

    public bool IsWaitingResult()
    {
        return waitingResult;
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
        UpdateNavigationButtons();
        RestoreQuestionState();
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
        Question q = questions[currentQuestionIndex];

        selectedAnswer = index;
        q.selectedAnswer = index;

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

        AudioManager.Instance.PauseMenuMusic();
        AudioManager.Instance.PlayTension();
        yield return new WaitForSeconds(
            AudioManager.Instance.tensionSound.length
        );

        Question q = questions[currentQuestionIndex];

        if (selectedAnswer == q.correctAnswer)
        {
            AudioManager.Instance.PlayCorrect();
            SetButtonColor(
                answerButtons[selectedAnswer],
                correctColor);
            yield return new WaitForSeconds(
            AudioManager.Instance.correctSound.length
        );
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
            yield return new WaitForSeconds(
            AudioManager.Instance.wrongSound.length
        );
        }
        waitingResult = false;
        q.revealed = true;
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
            q.isCorrect = true;
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

    public void NextQuestion()
    {
        if (waitingResult)
            return;

        if (currentQuestionIndex >= questions.Length - 1)
            return;

        currentQuestionIndex++;

        AudioManager.Instance.ResumeMenuMusic();

        ResetButtons();
        ShowQuestion();
    }

    public void PreviousQuestion()
    {
        if (waitingResult)
            return;

        if (currentQuestionIndex <= 0)
            return;

        currentQuestionIndex--;

        AudioManager.Instance.ResumeMenuMusic();

        ResetButtons();
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

    void RestoreQuestionState()
    {
        Question q = questions[currentQuestionIndex];

        ResetButtons();

        for (int i = 0; i < q.discardedAnswers.Length; i++)
        {
            if (q.discardedAnswers[i])
            {
                answerButtons[i].interactable = false;
            }
        }

        if (q.selectedAnswer == -1)
            return;

        selectedAnswer = q.selectedAnswer;

        if (!q.revealed)
        {
            answerButtons[q.selectedAnswer]
                .GetComponent<Image>().color = selectedColor;

            return;
        }

        foreach (Button button in answerButtons)
        {
            button.interactable = false;
        }

        if (q.selectedAnswer == q.correctAnswer)
        {
            answerButtons[q.selectedAnswer]
                .GetComponent<Image>().color = correctColor;
        }
        else
        {
            answerButtons[q.selectedAnswer]
                .GetComponent<Image>().color = wrongColor;

            answerButtons[q.correctAnswer]
                .GetComponent<Image>().color = correctColor;
        }
    }

    void UpdateNavigationButtons()
    {
        previousButton.gameObject.SetActive(
            currentQuestionIndex > 0);

        nextButton.gameObject.SetActive(
            currentQuestionIndex < questions.Length - 1);
    }
}
