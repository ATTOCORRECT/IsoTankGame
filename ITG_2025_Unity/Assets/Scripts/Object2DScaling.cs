using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object2DScaling : MonoBehaviour
{
    public bool grounded;

    void Start()
    {
        Sprite Sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        float textureHeight = Sprite.texture.height;
        float pixelsPerUnit = Sprite.pixelsPerUnit;
        if (grounded){
            transform.position = new Vector3(transform.position.x, textureHeight / 2 / pixelsPerUnit * (1 / Mathf.Cos((30 - transform.eulerAngles.x) * Mathf.Deg2Rad)) * (Mathf.Cos(transform.eulerAngles.x * Mathf.Deg2Rad)), transform.position.z);
        }
        transform.localScale = new Vector3(1, 1 / Mathf.Cos((30 - transform.eulerAngles.x) * Mathf.Deg2Rad) , 1); 
    }
}
