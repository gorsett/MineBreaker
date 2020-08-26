using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // config params
    [SerializeField] AudioClip blockBreakSound;
    [SerializeField] GameObject blockDestroyVFX;
    [SerializeField] int maxHits;
   
    // cached reference
    Level level;

    // state variables
    [SerializeField] int timesHit; //TODO only serialized for debugging



    private void Start()
    {
        CountBreakableBlocks();
    }

    private void CountBreakableBlocks()
    {
        level = FindObjectOfType<Level>();
        if (CompareTag("Breakable"))
        {
            level.CountBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(CompareTag("Breakable"))
        {
            HandleHit();
        }

    }

    private void HandleHit()
    {
        timesHit++;
        if (timesHit == maxHits)
        {
            DestroyBlock();
        }
    }

    private void DestroyBlock()
    {
        PlayBlockDestroySFX();
        Destroy(gameObject);
        level.BlockDestroyed();
        TriggerParticlesVFX();
        
    }

    private void PlayBlockDestroySFX()
    {
        FindObjectOfType<GameSession>().AddToScore();
        AudioSource.PlayClipAtPoint(blockBreakSound, Camera.main.transform.position);
    }

    private void TriggerParticlesVFX()
    {
        GameObject particles = Instantiate(blockDestroyVFX, transform.position, transform.rotation);
        Destroy(particles, 2);
    }
}
