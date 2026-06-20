using UnityEngine;
using System.Collections;
using TMPro;

public class SecretManager : MonoBehaviour
{
    public TextMeshProUGUI secretText;
    private void Start()
    {
        StartCoroutine(PlayAndQuit());
    }

    IEnumerator PlayAndQuit()
    {
        AudioManager.Instance.PlaySecretMusic();

        yield return new WaitForSeconds(
            AudioManager.Instance.secretMusic.length - 2.0f
        );
        Debug.Log("Texto");
        secretText.gameObject.SetActive(true);
        yield return new WaitForSeconds(
            2.0f
        );
        Debug.Log("Troll");
        Application.Quit();
    }
}