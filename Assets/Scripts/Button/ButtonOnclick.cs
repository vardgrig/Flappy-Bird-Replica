using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ButtonOnclick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    Animator animator;
    public Animator blackFadeAnim;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        animator.SetTrigger("Pressed");
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        animator.SetTrigger("Disabled");
        AudioManager.instance.Play("Swoosh");
        blackFadeAnim.SetTrigger("Start");
        //StartCoroutine(StartGame());
    }
    //private IEnumerator StartGame()
    //{
    //}
}
