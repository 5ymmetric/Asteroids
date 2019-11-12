using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject prefabAsteroid;

    [SerializeField]
    Sprite asteroidSprite0;

    [SerializeField]
    Sprite asteroidSprite1;

    [SerializeField]
    Sprite asteroidSprite2;

    float radius;
    Vector3 rightEnd;
    Vector3 leftEnd;
    Vector3 topEnd;
    Vector3 bottomEnd;

    // spawn control
    const float MinSpawnDelay = 3;
    const float MaxSpawnDelay = 10;
    Timer spawnTimer;

    // Start is called before the first frame update
    void Start()
    {
        radius = prefabAsteroid.GetComponent<CircleCollider2D>().radius;

        rightEnd = new Vector3(ScreenUtils.ScreenRight + radius, 0f);
        leftEnd = new Vector3(ScreenUtils.ScreenLeft - radius, 0f);
        topEnd = new Vector3(0f, ScreenUtils.ScreenTop + radius);
        bottomEnd = new Vector3(0f, ScreenUtils.ScreenBottom - radius);

        // create and start timer
        spawnTimer = gameObject.AddComponent<Timer>();
        spawnTimer.Duration = Random.Range(MinSpawnDelay, MaxSpawnDelay);
        spawnTimer.Run();

    }

    void Update()
    {
        if (spawnTimer.Finished)
        {
            SpawnAsteroid();

            spawnTimer.Duration = Random.Range(MinSpawnDelay, MinSpawnDelay);
            spawnTimer.Run();
        }

        void SpawnAsteroid()
        {
            GameObject asteroid = Instantiate(prefabAsteroid) as GameObject;
            Asteroid component = asteroid.GetComponent<Asteroid>();

            int rand = Random.Range(0,4);

            SpriteRenderer spriteRenderer = asteroid.GetComponent<SpriteRenderer>();
            int spriteNumber = Random.Range(0, 3);

            if (spriteNumber == 0)
            {
                spriteRenderer.sprite = asteroidSprite0;
            } else if (spriteNumber == 1)
            {
                spriteRenderer.sprite = asteroidSprite1;
            } else if (spriteNumber == 2)
            {
                spriteRenderer.sprite = asteroidSprite2;
            }

            if (rand == 0)
            {
                component.Initialize(Direction.Left, rightEnd);

            } else if (rand == 1)
            {
                component.Initialize(Direction.Right, leftEnd);

            } else if (rand == 2)
            {
                component.Initialize(Direction.Up, bottomEnd);

            } else if (rand == 3)
            {
                component.Initialize(Direction.Down, topEnd);

            }
        }
    }

}
