using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refrigerator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayCloseSfx()
    {
        AudioManager.instance.PlaySoundEffectAtPoint("FridgeClose", transform.position);
    }

    public void PlayOpenSfx()
    {
        AudioManager.instance.PlaySoundEffectAtPoint("FridgeOpen", transform.position);
    }

    // TODO: Play idle humming
    // TODO: Play open idle portal
    // TODO: Play portal enter when portal trigger occurs (may need to trim beginning of clip)
}
