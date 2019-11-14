using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 3.0f;
    GameObject sword;
    void Start()
    {
        sword = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("up"))
        {

        }
        if (Input.GetKey("down"))
        {

        }
        if (Input.GetKey("right"))
        {

        }
        if (Input.GetKey("left"))
        {

        }

        Swipe();
    }

    void Swipe()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 target_pos = ray.GetPoint(5.0f);

            sword.transform.LookAt(target_pos);
        }
    }
}
