using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed = 7.0f;
    private PlayerController myPlayer;
    public bool flashMode = false;
    
    // Start is called before the first frame update
    void Start()
    {
        myPlayer = GameObject.Find("Player").GetComponent<PlayerController>();  
    }

    // Update is called once per frame
    void Update()
    {
        if (myPlayer.gameOver == false)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        if (transform.position.y < 0)
        {
            Destroy(gameObject);
        }
        if(Input.GetKeyDown(KeyCode.A) && myPlayer.gameOver == false)
        {
            //speed = speed * 2;
            flashMode = true;

        }
        if (Input.GetKey(KeyCode.A) && myPlayer.gameOver == false)
        {
            myPlayer.score++;
            myPlayer.score++;
            flashMode = true;
        }
        if (Input.GetKeyUp(KeyCode.A) && myPlayer.gameOver == false)
        {
            //speed = 15.0f;
            flashMode = false;
        }
    }
}
