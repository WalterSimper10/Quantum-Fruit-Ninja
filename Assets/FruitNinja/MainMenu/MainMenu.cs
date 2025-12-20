using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("QuantumNinjaGame");
    }
    public void ReturnMenu()
    {
        Application.Quit();
        SceneManager.LoadScene("Menu");
    }
    public void ReadTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
    public void ViewOptions()
    {
        SceneManager.LoadScene("Options");
    }
}
