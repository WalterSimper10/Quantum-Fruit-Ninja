using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Image[] lifeImages;
    public int maxLives = 3;
    public int currentLives;
    public Text scoreText;
    public Image FadeImage;

    private Blade blade;
    private Spawner spawner;

    private int score;

    private void Awake()
    {
        blade = FindObjectOfType<Blade>();
        spawner = FindObjectOfType<Spawner>();
    }

    private void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        blade.enabled = true;
        spawner.enabled = true;
        score = 0;
        scoreText.text = score.ToString();
        Time.timeScale =1f;

        currentLives = maxLives;
        for (int i=0; i<lifeImages.Length; i++)
        {
            lifeImages[i].enabled = true;
        }

        ClearScene();
    }
    private void ClearScene()
    {
        Fruit[] fruits = FindObjectsOfType<Fruit>();
        foreach (Fruit fruit in fruits)
        {
            Destroy(fruit.gameObject);
        }
        Bomb[] bombs = FindObjectsOfType<Bomb>();
        foreach (Bomb bomb in bombs)
        {
            Destroy(bomb.gameObject);
        }
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        scoreText.text = score.ToString();
    }

    public void Explode()
    {
        blade.enabled = false;
        spawner.enabled = true;

        StartCoroutine(ExplodeSequence());
    }

    public void LoseLife()
    {
        currentLives--;
        if (currentLives >= 0 && currentLives < lifeImages.Length)
        {
            lifeImages[currentLives].enabled = false;
        }

        if (currentLives <= 0)
        {
            Explode();
        }
    }
    private IEnumerator ExplodeSequence()
    {
        float elapsed = 0f;
        float duration = 0.5f;

        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed/duration);
            FadeImage.color = Color.Lerp(Color.clear, Color.white, t);

            Time.timeScale = 1f-t;

            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        yield return new WaitForSecondsRealtime(1f);

        NewGame();
        elapsed = 0f;
        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed/duration);
            FadeImage.color = Color.Lerp(Color.white, Color.clear, t);

            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }
    }
}
