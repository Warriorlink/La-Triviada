using UnityEngine;

[System.Serializable]
public class Question
{

    public string question;
    public string[] answers;
    public int correctAnswer;

    public int selectedAnswer = -1;
    public bool revealed = false;
    public bool isCorrect = false;
    public bool[] discardedAnswers = new bool[4];
}
