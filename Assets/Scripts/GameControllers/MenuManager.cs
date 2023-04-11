using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Animator blackFadeAnim;
    public void StartGame()
    {
        blackFadeAnim.SetTrigger("Start");
        SceneManager.LoadScene(1);
    }

}
