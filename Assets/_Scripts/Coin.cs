using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float rotateSpeed;
    private void OnCollisionEnter(Collision other)
    {
      if(other.collider.tag == "Player")
        {
            ScoreManager.instance.ChangeScore(1);
            Destroy(gameObject);

        }
    }

   void Update()
    {
        transform.Rotate(0, rotateSpeed, 0, Space.World);
    }
}
