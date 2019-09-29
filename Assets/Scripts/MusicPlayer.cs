using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float lowVolume = 0.1f;
    [SerializeField] float highVolume = 0.7f;

    AudioSource audioSource;
    void Awake()
    {
        SetUpSingleton();
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        SetLowVolume();
    }

    private void SetUpSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SetLowVolume()
    {
        audioSource.volume = lowVolume;
    }

    public void SetHighVolume()
    {
        audioSource.volume = highVolume;
    }
}
