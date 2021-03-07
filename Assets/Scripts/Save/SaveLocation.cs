using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLocation : MonoBehaviour
{
    #region Serialized Variables
    [SerializeField] Transform savePosition;
    [SerializeField] string saveLocationName;
    [SerializeField] float saveDelay = 10.0f;
    #endregion

    #region Save Variables
    private float currentDelay;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        // Allow the player to use the save location immediately
        currentDelay = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        currentDelay -= Time.deltaTime;
        currentDelay = Mathf.Max(0, currentDelay);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (currentDelay > 0.0f)
        {
            return;
        }

        currentDelay = saveDelay;
        SaveSystem.SavePlayer(new PlayerSaveData(FindObjectOfType<Player>(), saveLocationName));
    }

    public string GetSaveLocationName()
    {
        return saveLocationName;
    }

    public void MovePlayerTo(Player player)
    {
        player.transform.position = savePosition.position;
    }
}
