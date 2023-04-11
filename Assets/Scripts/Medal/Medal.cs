using UnityEngine;
using UnityEngine.UI;

public class Medal : MonoBehaviour
{
    public Sprite bronzeMedal;
    public Sprite silverMedal;
    public Sprite goldMedal;
    public Sprite platiniumMedal;
    public GameObject sparkle;
    private Image img;
    public float radius = 5;

    void Start()
    {
        img = GetComponent<Image>();
        int gameScore = GameManager.score;
        sparkle.SetActive(true);

        if (gameScore < 10)
        {
            img.enabled = false;
            sparkle.SetActive(false);
        }
        else if (gameScore < 20)
        {
            img.sprite = bronzeMedal;
        }
        else if (gameScore < 30)
        {
            img.sprite = silverMedal;
        }
        else if (gameScore < 40)
        {
            img.sprite = goldMedal;
        }
        else if (gameScore >= 40)
        {
            img.sprite = platiniumMedal;
        }
    }
}
