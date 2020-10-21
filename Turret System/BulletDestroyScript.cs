using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroyScript : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("Destroy", 2f);
    }

    void Destroy()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

}
