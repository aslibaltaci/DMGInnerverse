
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    [SerializeField] bool goNextLevel;
    [SerializeField] string levelName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //next level
            if(goNextLevel)
            {
                SceneController.instance.NextLevel();
            }
            else
            {
                SceneController.instance.LoadScene(levelName);
            }
        }
    }
}
