using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    void Start()
    {
        GameObject background = gameObject;
        SpriteRenderer sr = background.GetComponent<SpriteRenderer>();
        float height = sr.bounds.size.y;
        float width = sr.bounds.size.x;
        float scaleHeight = Camera.main.orthographicSize * 2f;
        float scaleWidth = scaleHeight * Screen.width / Screen.height;
        background.transform.localScale = new Vector3(scaleWidth / width, scaleHeight / height, 0);
    }
}
