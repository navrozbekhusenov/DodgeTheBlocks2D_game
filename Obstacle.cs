using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float fallSpeed = 5f; // Toshning tushish tezligi

    void Update()
    {
        // Toshni har doim pastga qarab harakatlantirish
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);

        // Ekrandan pastga chiqib ketsa, xotirani band qilmasligi uchun uni o'chirib tashlash
        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }
}