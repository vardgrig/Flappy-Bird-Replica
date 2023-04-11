using UnityEngine;

public class BackgroundRoller : MonoBehaviour
{
    public float speed;
    void Update()
    {
        Roller();
    }
    private void Roller()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
    }
}
