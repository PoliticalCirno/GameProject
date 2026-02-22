using System.Collections;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private AudioClip jumpSFX;
    [SerializeField] private AudioClip deathSFX;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    private IEnumerator Start()
    {
        Debug.Log("AudioController.Start");
        yield return new WaitForSeconds(4f);
        Debug.Log("AudioController.PlayMusic After 4 sec");
        PlayBgMusic();
    }

    public void PlayJumpSFX()
    {
        sfxSource.clip = jumpSFX;
        sfxSource.Play();
    }
    public void PlayDeathSFX()
    {
        sfxSource.clip = deathSFX;
        sfxSource.Play();
    }
    public void PlayBgMusic()
    {
        musicSource.clip = backgroundMusic;
        musicSource.Play();
    }
}
