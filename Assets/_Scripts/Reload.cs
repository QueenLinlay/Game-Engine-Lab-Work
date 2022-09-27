using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reload : MonoBehaviour
{
    public static Reload instance;

    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Player")
        {
            Reload.instance.LoadThing();

        }
    }
    void LoadThing()
    {
        SceneManager.LoadScene(0);
    }
}
