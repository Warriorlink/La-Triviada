using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class SecretManager : MonoBehaviour
{
    public TextMeshProUGUI secretText;
    public Image secretImage;
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
            1.5f
        );
        Debug.Log("Imagen");
        secretImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(
            0.5f
        );
        Debug.Log("Troll");
        Application.Quit();
    }
}