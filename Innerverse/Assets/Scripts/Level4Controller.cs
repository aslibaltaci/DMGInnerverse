using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Level4Controller : MonoBehaviour
{
    public static Level4Controller instance;
    public Animator transition;
    public float transitionTime = 1f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }



    public void NextLevel()
    {
        transition.SetTrigger("Start");
        StartCoroutine(NextLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneCoroutine(sceneName));

    }

    IEnumerator NextLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadSceneAsync(levelIndex);
    }

    IEnumerator LoadSceneCoroutine(string sceneName)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadSceneAsync(sceneName);
    }
}
