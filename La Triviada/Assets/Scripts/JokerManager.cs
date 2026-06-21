using UnityEngine;
using UnityEngine.UI;
using System.Collections;
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
    public Image noJokersSprite;
    public Image recoverJokersSprite;

    private int activeJokers = 3;
    private bool noJokersUsed = false;
    private bool recoverJokersUsed = false;

    void Start()
    {
        rouletteInputField.text = "0";
        rouletteInputField.gameObject.SetActive(false);
        confirmInputButton.gameObject.SetActive(false);
        activeJokers = 3;
        noJokersUsed = false;
        recoverJokersUsed = false;
    }

    public void UseCallJoker()
    {
        if (gameManager.IsWaitingResult())
            return;
        callButton.interactable = false;
        activeJokers -= 1;
        StartCoroutine(NoJokersImage());
    }

    public void UseFiftyJoker()
    {
        if (gameManager.IsWaitingResult())
            return;

        RemoveWrongAnswers(2);

        fiftyButton.interactable = false;
        activeJokers -= 1;
        StartCoroutine(NoJokersImage());
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
        activeJokers -= 1;
        StartCoroutine(NoJokersImage());
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

        StartCoroutine(RecoverJokersImage());

        switch (amount)
        {
            case 0:
                callButton.interactable = true;
                activeJokers += 1;
                break;
            case 1:
                rouletteButton.interactable = true;
                activeJokers += 1;
                break;
            case 2:
                fiftyButton.interactable = true;
                activeJokers += 1;
                break;
            default:
                return;
        }
    }

    IEnumerator NoJokersImage()
    {
        if (activeJokers <= 0 && noJokersUsed == false)
        {
            noJokersUsed = true;
            noJokersSprite.gameObject.SetActive(true);
            AudioManager.Instance.PauseMenuMusic();
            AudioManager.Instance.PlayNoJokers();
            yield return new WaitForSeconds(5.0f);
            AudioManager.Instance.StopNoJokers();
            AudioManager.Instance.ResumeMenuMusic();
            noJokersSprite.gameObject.SetActive(false);
        }
    }

    IEnumerator RecoverJokersImage()
    {
        if (recoverJokersUsed == false)
        {
            recoverJokersUsed = true;
            recoverJokersSprite.gameObject.SetActive(true);
            AudioManager.Instance.PauseMenuMusic();
            AudioManager.Instance.PlayRecoverJokers();
            yield return new WaitForSeconds(5.0f);
            AudioManager.Instance.StopRecoverJokers();
            AudioManager.Instance.ResumeMenuMusic();
            recoverJokersSprite.gameObject.SetActive(false);
        }
    }
}