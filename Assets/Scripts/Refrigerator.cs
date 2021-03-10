using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refrigerator : MonoBehaviour
{
    private GameObject humSfx;
    private GameObject portalSfx;
    private SpriteRenderer bodySpriteRenderer;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        humSfx = AudioManager.instance.PlayLoopingSoundEffectAtPoint("FridgeHum", transform.position);
        bodySpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();
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

    public void FlickerIntoExistence()
    {
        StartCoroutine(InternalFlickerIntoExistence());
    }

    public IEnumerator InternalFlickerIntoExistence()
    {
        yield return new WaitForEndOfFrame();
        float startTime = Time.time;

        while (Time.time - startTime < 1f)
        {
            bodySpriteRenderer.enabled = !bodySpriteRenderer.enabled;
            yield return new WaitForSeconds(0.025f);
        }

        while (Time.time - startTime < 2.5f)
        {
            bodySpriteRenderer.enabled = !bodySpriteRenderer.enabled;
            yield return new WaitForSeconds(0.05f);
        }

        bodySpriteRenderer.enabled = true;
        animator.SetBool("shouldOpen", true);
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
}
