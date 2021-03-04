using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stinger : MonoBehaviour
{
    [SerializeField] float movementSpeed = 5.0f;

    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (direction != null)
        {
            float step = movementSpeed * Time.deltaTime;

            // @TODO Face the target
            transform.Translate(direction * step);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // @TODO Destroy the gameobject
        // @TODO Trigger the animation
        Destroy(gameObject);
    }

    public void FireAt(Vector3 target)
    {
        direction = (target - transform.position).normalized;
    }
}
