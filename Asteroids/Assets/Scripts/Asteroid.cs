using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    HUD hud;
    const int points = 1;
    const int doublePoints = 2;

    private void Start()
    {
        hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUD>();
    }

    public void StartMoving(float angle)
    {
        // apply impulse force to get game object moving
        const float MinImpulseForce = 1f;
        const float MaxImpulseForce = 2f;

        Vector2 moveDirection = new Vector2(
            Mathf.Cos(angle), Mathf.Sin(angle));
        float magnitude = Random.Range(MinImpulseForce, MaxImpulseForce);
        GetComponent<Rigidbody2D>().AddForce(
            moveDirection * magnitude,
            ForceMode2D.Impulse);
    }


    public void Initialize(Direction direction, Vector3 location)
    {

        float angle = 0;

        if (direction == Direction.Up)
        {
            angle = Random.Range(0, 30 * Mathf.Deg2Rad);
            angle += 75 * Mathf.Deg2Rad;
        }
        else if (direction == Direction.Down)
        {
            angle = Random.Range(0, 30 * Mathf.Deg2Rad);
            angle += 255 * Mathf.Deg2Rad;
        }
        else if (direction == Direction.Right)
        {
            angle = Random.Range(0, 30 * Mathf.Deg2Rad);
            angle += 165 * Mathf.Deg2Rad;
        }
        else if (direction == Direction.Left)
        {
            angle = Random.Range(0, 30 * Mathf.Deg2Rad);
            angle += 345 * Mathf.Deg2Rad;
        }

        gameObject.transform.position = location;

        StartMoving(angle);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {

            AudioManager.Play(AudioClipName.AsteroidHit);

            // apply impulse force to get game object moving
            const float MinImpulseForce = 1f;
            const float MaxImpulseForce = 2f;

            hud.AddPoints(points);
            Destroy(collision.gameObject);

            if (gameObject.transform.localScale.x <= 0.75)
            {
                hud.AddPoints(doublePoints);
                Destroy(gameObject);
            }
            else
            {

                Vector3 scale = gameObject.transform.localScale;

                scale.x = scale.x * 0.75f;
                scale.y = scale.y * 0.75f;

                gameObject.transform.localScale = scale;

                float radius = gameObject.GetComponent<CircleCollider2D>().radius;
                radius = radius * 0.75f;
                gameObject.GetComponent<CircleCollider2D>().radius = radius;

                GameObject rock1 = Instantiate(gameObject, transform.position, transform.rotation);

                float angle = Random.Range(0, 2 * Mathf.PI);

                Vector2 moveDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

                float magnitude = Random.Range(MinImpulseForce, MaxImpulseForce);

                rock1.GetComponent<Rigidbody2D>().AddForce(magnitude * moveDirection, ForceMode2D.Impulse);

                GameObject rock2 = Instantiate(gameObject, transform.position, transform.rotation);

                float angle1 = Random.Range(0, 2 * Mathf.PI);

                Vector2 moveDirection1 = new Vector2(Mathf.Cos(angle1), Mathf.Sin(angle1));

                float magnitude1 = Random.Range(MinImpulseForce, MaxImpulseForce);

                rock2.GetComponent<Rigidbody2D>().AddForce(magnitude1 * moveDirection1, ForceMode2D.Impulse);

                Destroy(gameObject);
            }
        }
    }
}
