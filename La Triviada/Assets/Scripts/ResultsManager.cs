using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ResultsManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    private void Start()
    {
        scoreText.text =
        GameData.Instance.finalScore + " / " + GameData.Instance.totalQuestions;
        AudioManager.Instance.PlayResultsMusic();
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void Secret()
    {
        SceneManager.LoadScene("SecretScene");
    }
}