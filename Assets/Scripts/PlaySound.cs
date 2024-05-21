using UnityEngine;

public class PlaySound : MonoBehaviour
{
    private float timer = 0f;
    public float interval = 2f;
    public float playCount= 0f;


    private AudioManager _audioManager;
    private bool soundPlaying = false;
    private float soundDuration = 0f;

    void Start()
    {
        _audioManager = AudioManager.Instance;
    }

    void Update()
    {
        Debug.Log(playCount);
        timer += Time.deltaTime;

        if (timer >= interval)
        {
            int randomIndex = Random.Range(0, 4);

            _audioManager.PlaySFX(randomIndex);

            timer = 0f;
            playCount++;
        }

        if (playCount > 20)
        {
            _audioManager.StopSFX();    
        }
    }
}
