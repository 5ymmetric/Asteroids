using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    const int lifeTime = 2;
    Timer deathTimer;


    public void ApplyForce(Vector2 direction)
    {
        const float magnitude = 2f;

        Rigidbody2D component = gameObject.GetComponent<Rigidbody2D>();

        component.AddForce(magnitude * direction, ForceMode2D.Impulse);
    }


    // Start is called before the first frame update
    void Start()
    {
        deathTimer = gameObject.AddComponent<Timer>();
        deathTimer.Duration = lifeTime;
        deathTimer.Run();

    }

    // Update is called once per frame
    void Update()
    {
        if (deathTimer.Finished)
        {
            Destroy(gameObject);
        }
    }
}
