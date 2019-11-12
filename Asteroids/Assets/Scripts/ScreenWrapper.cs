using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrapper : MonoBehaviour
{
    #region Fields
    private float radius;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        CircleCollider2D collider = GetComponent<CircleCollider2D>();
        radius = collider.radius;
    }

    private void OnBecameInvisible()
    {
        Vector2 position = transform.position;
        if (position.x + radius < ScreenUtils.ScreenLeft ||
            position.x - radius > ScreenUtils.ScreenRight)
        {
            position.x *= -1;
        }
        if (position.y - radius > ScreenUtils.ScreenTop ||
            position.y + radius < ScreenUtils.ScreenBottom)
        {
            position.y *= -1;
        }

        transform.position = position;
    }
}
