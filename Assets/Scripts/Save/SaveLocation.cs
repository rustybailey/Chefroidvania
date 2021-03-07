using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLocation : MonoBehaviour
{
    #region Serialized Variables
    [SerializeField] Transform savePosition;
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // @TODO Don't save rapidly

        SaveSystem.SavePlayer(new PlayerSaveData(FindObjectOfType<Player>(), savePosition.position));
    }
}
