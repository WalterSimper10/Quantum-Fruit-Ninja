using System.Runtime.CompilerServices;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private AudioClip bombSound;
    [SerializeField] private AudioClip lastBombSound;
    
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player")){
            GameManager gm = FindObjectOfType<GameManager>();
            AudioClip bombClip = (gm.currentLives <= 1) ? lastBombSound : bombSound;
            if (SfxManager.instance != null && bombClip != null)
            {
                SfxManager.instance.PlaySoundFXClip(bombClip, transform, 1f);
            }

            gm.LoseLife();
            Destroy(gameObject);
        }

    }

    
}
