using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawableSprite : MonoBehaviour
{
    public IDrawable drawable; 

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(drawable.Sprite);
        if (GetComponent<SpriteRenderer>().sprite == null)
        {
            Debug.LogError("Attempt to Load: " + drawable.Sprite + " failed.");
        }
        gameObject.transform.rotation = Quaternion.Euler(Vector3.forward * drawable.Rotation);
        name = drawable.ToString();

        if (drawable is Player)
        {
            gameObject.transform.Translate(new Vector3(0, 0, -0.001f));
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (drawable is Beholder)
        {
            gameObject.transform.rotation = Quaternion.Euler(Vector3.forward * 0);
            gameObject.transform.position = new Vector3(drawable.X + 1.5f, -drawable.Y - 1.0f, gameObject.transform.position.z);
        }
        else
        {
            gameObject.transform.rotation = Quaternion.Euler(Vector3.forward * drawable.Rotation);
            gameObject.transform.position = new Vector3(drawable.X + 0.5f, -drawable.Y - 0.5f, gameObject.transform.position.z);
        }
    }
}
