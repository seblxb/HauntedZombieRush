using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;   

public class Player : MonoBehaviour {

    private Animator anim;
    private Rigidbody rigidBody;
    [SerializeField] private float jumpForce = 100f;
    [SerializeField] private AudioClip sfxJump;
    [SerializeField] private AudioClip sfxDeath;
    private AudioSource audioSource;
    private bool jump = false;


    private void Awake()
    {
        Assert.IsNotNull(sfxJump);
        Assert.IsNotNull(sfxDeath);
    }


    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator> ();
        rigidBody = GetComponent<Rigidbody> ();
        audioSource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {

        if (!GameManager.instance.GameOver && GameManager.instance.GameStarted) 
        {
			if (Input.GetMouseButtonDown(0))
			{
                GameManager.instance.PlayerStartedGame();
				anim.Play("jump");
				audioSource.PlayOneShot(sfxJump);
				jump = true;
				rigidBody.useGravity = true;

			}
        }

	}
	void FixedUpdate() {
        
        if (jump == true) {
            jump = false;
            rigidBody.velocity = new Vector2(0, 0);
            rigidBody.AddForce(new Vector2(0, jumpForce), ForceMode.Impulse);

        }
        //print(rigidBody.velocity.y);
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "obstacle")
        {
            rigidBody.detectCollisions = false;
            rigidBody.AddForce(new Vector2(-50f, 20), ForceMode.Impulse);
            audioSource.PlayOneShot(sfxDeath);
            GameManager.instance.PlayerCollided();
        }
    }
}
