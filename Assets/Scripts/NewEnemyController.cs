using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEnemyController : MonoBehaviour
{
    public float enemySpeed;
    public float changeTime = 3.0f;
    public bool vertical;
    Animator animator;

    Rigidbody2D rigidbody2d;
    float timer;
    int direction = 1;

    bool aggressive = true;

    AudioSource audioSource;

    public ParticleSystem smokeEffect;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    
    }

    private void FixedUpdate()
    {
        if (!aggressive)
        {
            return;
        }
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
        Vector2 position = rigidbody2d.position;
        if (vertical) {
            position.y = position.y + enemySpeed * direction * Time.deltaTime;
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        } else
        {
            position.x = position.x + enemySpeed * direction * Time.deltaTime;
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }

        rigidbody2d.MovePosition(position);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if (player != null)
        { 
            player.ChangeHealth(-1);
        }
    }

    public void Fix()
    {
        aggressive = false;
        rigidbody2d.simulated = false;
        animator.SetTrigger("Fixed");
        audioSource.Stop();
        smokeEffect.Stop();

    }
}
