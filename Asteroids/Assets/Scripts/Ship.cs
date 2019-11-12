using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField]
    GameObject prefabBullet;

    [SerializeField]
    GameObject objectHUD;

    #region Fields
    private Rigidbody2D component;
    private Vector2 thrustDirection;


    #endregion
    const float maxSpeed = 15;
    const float ThrustForce = 5;
    const float RotateDegreesPerSecond = 90;

    // Start is called before the first frame update
    void Start()
    {
        component = GetComponent<Rigidbody2D>();
        thrustDirection.x = 1;
        thrustDirection.y = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        float rotationInput = Input.GetAxis("Rotate");
        float rotationAmount = RotateDegreesPerSecond * Time.deltaTime;

        if (rotationInput < 0)
        {
            rotationAmount *= -1;

            float angle = transform.eulerAngles.z;

            thrustDirection.x = Mathf.Cos(Mathf.Deg2Rad * angle);
            thrustDirection.y = Mathf.Sin(Mathf.Deg2Rad * angle);

            transform.Rotate(Vector3.forward, rotationAmount);

        } else if (rotationInput > 0)
        {
            float angle = transform.eulerAngles.z;

            thrustDirection.x = Mathf.Cos(Mathf.Deg2Rad * angle);
            thrustDirection.y = Mathf.Sin(Mathf.Deg2Rad * angle);

            transform.Rotate(Vector3.forward, rotationAmount);

            
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.LeftShift))
        {
            AudioManager.Play(AudioClipName.PlayerShot);

            Vector3 position = gameObject.transform.position;

            GameObject bullet = Instantiate(prefabBullet) as GameObject;

            float angle = transform.eulerAngles.z;

            Vector3 bulletPosition = transform.position;

            bulletPosition.x += Mathf.Cos(Mathf.Deg2Rad * angle) * 2;
            bulletPosition.y += Mathf.Sin(Mathf.Deg2Rad * angle) * 2;


            bullet.transform.position = bulletPosition;
            bullet.transform.eulerAngles = transform.eulerAngles;

            Bullet component = bullet.GetComponent<Bullet>();
            component.ApplyForce(thrustDirection);
        }

    }

    private void FixedUpdate()
    {

        float thrustInput = Input.GetAxis("Thrust");

        if (thrustInput != 0)
        {
            component.AddForce(thrustDirection * ThrustForce * thrustInput);
        }

        

        if( thrustInput == 0)
        {
            component.velocity = Vector3.zero;
        }

        if (component.velocity.magnitude > maxSpeed)
        {
            component.velocity = component.velocity.normalized * maxSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            AudioManager.Play(AudioClipName.PlayerDeath);

            HUD hud = objectHUD.GetComponent<HUD>();
            hud.StopGameTimer();
            
            Destroy(gameObject);
        }
    }
}
