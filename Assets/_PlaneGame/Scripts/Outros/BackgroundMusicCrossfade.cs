using UnityEngine;
using System.Collections;

public class BackgroundMusicCrossfade : MonoBehaviour
{
    public AudioSource sourceA;
    public AudioSource sourceB;

    public AudioClip[] musicTracks;
    public float crossfadeDuration = 2f; // Tempo do crossfade
    private int currentTrack = 0;

    private AudioSource activeSource;
    private AudioSource inactiveSource;

    void Start()
    {
        activeSource = sourceA;
        inactiveSource = sourceB;

        PlayTrack(activeSource, musicTracks[currentTrack]);
    }

    void Update()
    {
        // Se a música atual terminou, inicia o crossfade
        if (!activeSource.isPlaying)
        {
            StartCoroutine(CrossfadeToNextTrack());
        }
    }

    IEnumerator CrossfadeToNextTrack()
    {
        // Seleciona a próxima música
        currentTrack = (currentTrack + 1) % musicTracks.Length;

        // Prepara a música na inactiveSource
        PlayTrack(inactiveSource, musicTracks[currentTrack]);

        float elapsed = 0f;

        while (elapsed < crossfadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / crossfadeDuration;

            // Fade out da ativa, fade in da inativa
            activeSource.volume = Mathf.Lerp(1f, 0f, t);
            inactiveSource.volume = Mathf.Lerp(0f, 1f, t);

            yield return null;
        }

        // Garante volumes finais
        activeSource.volume = 0f;
        inactiveSource.volume = 1f;

        // Troca as fontes
        AudioSource temp = activeSource;
        activeSource = inactiveSource;
        inactiveSource = temp;
    }

    void PlayTrack(AudioSource source, AudioClip clip)
    {
        source.clip = clip;
        source.volume = (source == activeSource) ? 1f : 0f;
        source.Play();
    }
}
