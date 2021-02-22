using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Temporary script for demo purposes (until we know how we intend on triggering music)
public class MusicPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.PlayMusic("Kitchen");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
