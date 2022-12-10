using UnityEngine;

[DefaultExecutionOrder(0)]
public class GameAudio : MonoBehaviour
{
    [SerializeField] private AudioSource _sounds;
    [SerializeField] private AudioSource _music;
    public static GameAudio Instance { get; private set; }

    private void Awake()
    {
        if (Instance)
        {
            return;
        }

        Instance = this;
    }

    public void PlayAudio(AudioClip clip)
    {
        _sounds.PlayOneShot(clip);
    }

    public void StopMusic()
    {
        _music.Stop();
    }
}
