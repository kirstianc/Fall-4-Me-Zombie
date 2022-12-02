using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombie : MonoBehaviour
{   

    [SerializeField]
    private Animator animator;
    private Rigidbody2D rigidbody;
    public AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update(){}

   private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Ground" ) {
            GameManager.Instance.Score();
            animator.SetTrigger("splat");
            audioSrc.Play(0);
        }
         
        if(collision.gameObject.tag == "Player") {
            GameManager.Instance.GameOver();
            animator.SetTrigger("splat");
            audioSrc.Play(0);
        }
     }
}
