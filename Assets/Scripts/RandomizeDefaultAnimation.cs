using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeDefaultAnimation : MonoBehaviour
{
    private void Start()
    {
        Animator myAnimator = gameObject.GetComponent<Animator>();
        if (myAnimator)
        {
            myAnimator.Play(0, -1, Random.Range(0f, 4f) * 0.25f);
        }
        else
        {
            Debug.Log("This gameObject is missing an Animator component!");
        }
    }

}
