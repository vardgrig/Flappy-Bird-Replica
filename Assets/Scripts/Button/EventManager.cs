using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{
    private GameManager gm;
    private RectTransform rt;
    void Start()
    {
        gm = GameManager.instance;
        rt = GetComponent<RectTransform>();
    }
    public void ShowBoard()
    {
        gm.ShowScoreBoard();
    }
    public void ShowButtons()
    {
        StartCoroutine(ButtonsSec());
    }
    private IEnumerator ButtonsSec()
    {
        yield return new WaitForSeconds(1.0f);
        gm.ShowButtons();
    }
    public void DrawScore()
    {
        gm.StartCoroutine(nameof(DrawScore));
    }
    public void StartPanelOff()
    {
        gm.DisableStartPage();
    }
    public void StartLevel()
    {
        SceneManager.LoadScene(1);
    }
    public void Sparkle()
    {
        GetComponent<RectTransform>().localPosition = new Vector3(Random.Range(-15, 15), Random.Range(-15, 15));
    }
}
