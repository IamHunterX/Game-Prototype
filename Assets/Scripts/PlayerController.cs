using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;
    public ParticleSystem explosion;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    private AudioSource playerAudio;
    public float physicsModifier;
    public float jumpForce = 10.0f;
    public bool isOnGround = true;
    public bool gameOver = false;
    private int jumpCounter = 0;
    public int score = 0;
    private MoveLeft check;
    private Vector3 startPos = new Vector3(-5, 0, 0);
    private Vector3 endPos = new Vector3(0, 0, 0);
    private float timeToLerp = 1f;
    private float lerpSpeed = 0;
    public float speedToMove = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        check = GetComponent<MoveLeft>();
        playerAnim = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= physicsModifier;
        StartCoroutine(PlayIntro());

        //transform.Translate(Vector3.back * 5.0f);
    }

    // Update is called once per frame
    void Update()
    { 
        playerAnim.SetFloat("Speed_f", 0.51f);
        //if (check.flashMode == true)
        //{
        //    playerAnim.SetFloat("Speed_f", 1.0f);
        //}
        playerAnim.SetBool("Static_b", false);
        if (Input.GetKeyDown(KeyCode.Space) && jumpCounter < 2 && !gameOver){
            playerAnim.SetTrigger("Jump_trig");
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            dirtParticle.Pause();
            playerAudio.PlayOneShot(jumpSound);
            jumpCounter++;
        }
        if(Input.GetKey(KeyCode.A) && !gameOver)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speedToMove);
        }
        if (Input.GetKey(KeyCode.D) && !gameOver)
        {
            transform.Translate(Vector3.right * Time.deltaTime * speedToMove);
        }
        if (!gameOver)
        {
            score++;
            Debug.Log(score);
        }
        Debug.Log(Time.time);
        

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticle.Play();
            jumpCounter = 0;
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over!");
            explosion.Play();
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            gameOver = true;
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound);
        }
    }
    IEnumerator PlayIntro()
    {
        gameOver = true;

        dirtParticle.Stop();

        // Lerping the player between two positions frame by frame
        while (lerpSpeed < timeToLerp)
        {
            transform.position = Vector3.Lerp(startPos, endPos, lerpSpeed / timeToLerp);

            lerpSpeed += Time.deltaTime;

            yield return null;
        }

        // Snapping the player to the end position when close enough
        transform.position = endPos;

        dirtParticle.Play();

        gameOver = false;

        // Change from Walking animation to Running animation
        playerAnim.SetFloat("Speed_f", 1f);
    }
}
