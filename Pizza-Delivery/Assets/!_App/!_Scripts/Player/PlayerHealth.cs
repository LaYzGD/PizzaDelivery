using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _health = 1;
    [SerializeField] private GameObject _graphicsComponent;
    [SerializeField] private ParticleSystem _crushParticle;
    [SerializeField] private AudioClip _crushSound;

    private bool _isLost;

    private GameAudio _audio;

    public Action OnLose;

    private void Start()
    {
        _audio = GameAudio.Instance;
    }

    public void TakeDamage(int damage)
    {
        if (_isLost)
        {
            return;
        }

        _health -= damage;
        _crushParticle.Play();
        _audio.PlayAudio(_crushSound);

        if (_health <= 0)
        {
            _isLost = true;
            OnLose?.Invoke();
            _audio.StopMusic();
            _graphicsComponent.SetActive(false);
        }
    }
}
