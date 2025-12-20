using UnityEngine;

public class GameMenu : MonoBehaviour
{
    [Header("OptionsPanel")]
    public GameObject optionsPanel; 
    public void OpenMenu()
    {
        optionsPanel.SetActive(true);
        Time.timeScale = 0f; 
    }

    public void CloseMenu()
    {
        optionsPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}