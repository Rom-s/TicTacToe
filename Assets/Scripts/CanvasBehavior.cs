using System;
using UnityEngine;
using UnityEngine.UI;

public class CanvasBehavior : MonoBehaviour
{
    [SerializeField] private Text centralText;
    [SerializeField] private GameObject quitButton;
    [SerializeField] private GameObject subCanvas;

    private void Start()
    {
        quitButton.SetActive(true);
        subCanvas.SetActive(false);
    }

    public void SetText(String text)
    {
        centralText.text = text;
    }

    public void RestartButton()
    {
        GameManager.GetInstance().Restart();
        centralText.text = "";
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void ChangeState()
    {
        quitButton.SetActive(!quitButton.activeSelf);
        subCanvas.SetActive(!subCanvas.activeSelf);
    }
}
