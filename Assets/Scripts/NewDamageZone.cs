using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewDamageZone : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        PlayerController controller = other.GetComponent<PlayerController>();
        if (controller != null)
        {
            Debug.Log(controller.health);
            controller.ChangeHealth(-1);
        }
    }
}
