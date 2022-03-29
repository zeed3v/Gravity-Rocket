using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionUpdate : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip success;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void OnCollisionEnter(Collision other) 
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This thing is friendly");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;    
        }
    }
    void StartSuccessSequence()
    {
        GetComponent<Movement>().enabled = false;
        audioSource.PlayOneShot(success);
        Invoke ("LoadNextLevel", levelLoadDelay);
    }

    void StartCrashSequence()
    {
        //to do add SFX upon crush
        //to do add particle upon crush
        GetComponent<Movement>().enabled = false;
        audioSource.PlayOneShot(crash);
        Invoke ("ReloadLevel", levelLoadDelay);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);
    }


    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}