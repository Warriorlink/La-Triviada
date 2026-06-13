using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenButtonsHandler : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("QuizScene");
    }

    public void ExitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
}
