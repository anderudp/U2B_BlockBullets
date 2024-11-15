using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    static int maxBounces = 6;
    int bounces = 0;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position);

        if(screenPos.y < 0f || bounces >= maxBounces) Destroy(gameObject);

        if(screenPos.y > Screen.height)
        {
            BindPosition(screenPos);
            bounces++;
            rb.velocity *= new Vector2(1, -1);
        }
        if(screenPos.x > Screen.width || screenPos.x < 0)
        {
            BindPosition(screenPos);
            bounces++;
            rb.velocity *= new Vector2(-1, 1);
        }
    }

    void BindPosition(Vector2 pos)
    {
        pos.x = Mathf.Clamp(pos.x, 10, Screen.width-10);
        pos.y = Mathf.Clamp(pos.y, 10, Screen.height-10);

        pos = Camera.main.ScreenToWorldPoint(pos);

        transform.position = new Vector3(pos.x, pos.y, 0);
    }
}
