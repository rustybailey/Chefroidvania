using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stinger : MonoBehaviour
{
    #region Serialized Variables
    [SerializeField] float movementSpeed = 5.0f;
    #endregion

    #region Movement Variables
    private Vector3 direction;
    #endregion

    #region Component Variables
    private Animator animator;
    #endregion

    #region State Variables
    private bool destroyed;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        destroyed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (direction != null && !destroyed)
        {
            float step = movementSpeed * Time.deltaTime;

            transform.Translate(direction * step);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        destroyed = true;
        animator.SetBool("destroy", true);
    }

    public void FireAt(Vector3 target)
    {
        direction = (target - transform.position).normalized;

        // @see https://answers.unity.com/questions/1023987/lookat-only-on-z-axis.html
        Vector3 difference = target - transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        // Subtract 180 degrees so the projectile faces the target on its left
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ - 180);
    }

    public void Destroyed()
    {
        Destroy(gameObject);
    }
}
