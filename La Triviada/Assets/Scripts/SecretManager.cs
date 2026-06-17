using UnityEngine;
using System.Collections;

public class SecretManager : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(PlayAndQuit());
    }

    IEnumerator PlayAndQuit()
    {
        AudioManager.Instance.PlaySecretMusic();

        yield return new WaitForSeconds(
            AudioManager.Instance.secretMusic.length
        );
        Debug.Log("Troll");
        Application.Quit();
    }
}