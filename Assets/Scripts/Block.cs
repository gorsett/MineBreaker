using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    [SerializeField] AudioClip blockBreakSound;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        AudioSource.PlayClipAtPoint(blockBreakSound, Camera.main.transform.position);
        Destroy(gameObject);
    }
}
