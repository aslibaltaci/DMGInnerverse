using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CutsceneStart : MonoBehaviour
{
    public PlayableDirector cutscene;
    public string nextSceneName; 
    void Start()
    {
        cutscene.Play();
        cutscene.stopped += OnCutsceneEnd;
    }

    void OnCutsceneEnd(PlayableDirector pd)
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
