using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refrigerator : MonoBehaviour
{
    private GameObject humSfx;
    private GameObject portalSfx;

    // Start is called before the first frame update
    void Start()
    {
        humSfx = AudioManager.instance.PlayLoopingSoundEffectAtPoint("FridgeHum", transform.position);
    }

    public void PlayCloseSfx()
    {
        AudioManager.instance.PlaySoundEffectAtPoint("FridgeClose", transform.position);
    }

    public void PlayOpenSfx()
    {
        AudioManager.instance.PlaySoundEffectAtPoint("FridgeOpen", transform.position);
    }

    public void PlayPortalSfx()
    {
        portalSfx = AudioManager.instance.PlayLoopingSoundEffectAtPoint("FridgePortal", transform.position);
    }

    private void OnDestroy()
    {
        if (humSfx)
        {
            Destroy(humSfx);
        }

        if (portalSfx)
        {
            Destroy(portalSfx);
        }
    }

    // TODO: Play portal enter when portal trigger occurs (may need to trim beginning of clip)
}
