using UnityEngine;

public class Jukebox : MonoBehaviour
{
    public int delay = 5;
    
    private AudioSource _audioSource;
    private bool _isPlaying = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.PlayDelayed(delay);
        _isPlaying = true;
    }

    void Play()
    {
        if (!_isPlaying)
        {
            _audioSource.Play();
            _isPlaying = true;            
        }
    }

    void Pause()
    {
        if (_isPlaying)
        {
            _audioSource.Pause();
            _isPlaying = false;
        }
    }
}
