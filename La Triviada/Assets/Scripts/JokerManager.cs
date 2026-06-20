using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class JokerManager : MonoBehaviour
{
    public GameManager gameManager;
    public Button callButton;
    public Button fiftyButton;
    public Button rouletteButton;
    public Button confirmInputButton;
    public Button confirmRecoverButton;
    public TMP_InputField rouletteInputField;
    public TMP_InputField recoverInputField;

    void Start()
    {
        rouletteInputField.text = "0";
        rouletteInputField.gameObject.SetActive(false);
        confirmInputButton.gameObject.SetActive(false);
    }

    public void UseCallJoker()
    {
        if (gameManager.IsWaitingResult())
            return;
        callButton.interactable = false;
    }

    public void UseFiftyJoker()
    {
        if (gameManager.IsWaitingResult())
            return;

        RemoveWrongAnswers(2);

        fiftyButton.interactable = false;
    }

    public void ShowRouletteInput()
    {
        if (gameManager.IsWaitingResult())
            return;
        rouletteInputField.gameObject.SetActive(true);
        confirmInputButton.gameObject.SetActive(true);
        rouletteButton.interactable = false;

        rouletteInputField.text = "";

        rouletteInputField.ActivateInputField();
    }

    public void UseRoulette()
    {
        if (gameManager.IsWaitingResult())
            return;

        int amount;

        if (!int.TryParse(rouletteInputField.text, out amount))
        {
            return;
        }

        amount = Mathf.Clamp(amount, 0, 3);

        RemoveWrongAnswers(amount);
        rouletteInputField.gameObject.SetActive(false);
        confirmInputButton.gameObject.SetActive(false);
    }

    void RemoveWrongAnswers(int amount)
    {
        Question q = gameManager.GetCurrentQuestion();

        List<int> wrongAnswers = new List<int>();

        for (int i = 0; i < 4; i++)
        {
            if (i != q.correctAnswer &&
                !q.discardedAnswers[i])
            {
                wrongAnswers.Add(i);
            }
        }

        amount = Mathf.Min(amount, wrongAnswers.Count);

        int removed = 0;

        while (removed < amount)
        {
            int randomIndex =
                Random.Range(0, wrongAnswers.Count);

            int answerToRemove =
                wrongAnswers[randomIndex];

            q.discardedAnswers[answerToRemove] = true;

            gameManager.GetAnswerButtons()[answerToRemove]
                .interactable = false;

            wrongAnswers.RemoveAt(randomIndex);

            removed++;
        }
    }

    public void UseRecover()
    {
        if (gameManager.IsWaitingResult())
            return;

        int amount;

        if (!int.TryParse(recoverInputField.text, out amount))
        {
            return;
        }

        amount = Mathf.Clamp(amount, 0, 2);

        switch (amount)
        {
            case 0:
                callButton.interactable = true;
                break;
            case 1:
                rouletteButton.interactable = true;
                break;
            case 2:
                fiftyButton.interactable = true;
                break;
            default:
                return;
        }
    }
}
