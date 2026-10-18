using UnityEngine;

public class CarController : MonoBehaviour
{
    public float moveSpeed = 8f;
    public float maxX = 2.2f;

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        Vector3 moveDir = new Vector3(moveInput, 0, 0);
        transform.Translate(moveDir * moveSpeed * Time.deltaTime);

        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -maxX, maxX);
        transform.position = clampedPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            // GameManager orqali o'yinni tugatish va Game Over panelini chiqarish
            FindObjectOfType<GameManager>().GameOver();
        }
    }
}