using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateFX : MonoBehaviour
{
    // вызывается каждый раз, когда gameobject активируется
    void OnEnable ()
    {
        Invoke("DeactivateGameObject", 2.0f);
    }

    // Update is called once per frame
    void DeactivateGameObject ()
    {
        gameObject.SetActive(false);
    }
}
