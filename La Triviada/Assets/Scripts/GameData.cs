using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance;

    public int finalScore;
    public int totalQuestions;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
