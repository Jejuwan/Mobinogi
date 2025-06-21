using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance;
    private AudioSource audioSource;

    void Awake()
    {
        if(Instance != null)
        {
            Destroy(transform.root.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(transform.root.gameObject);

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);  // 한번 재생
    }
}
