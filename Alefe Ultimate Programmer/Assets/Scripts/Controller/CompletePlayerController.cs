using UnityEngine;
using System.Collections;

//Adding this allows us to access members of the UI namespace including Text.
using UnityEngine.UI;
using System.Threading;
using Assets.Scripts.Other;

public class CompletePlayerController : MonoBehaviour
{
    public GameObject player2;
    public GameObject life, chakra, lifePlayer2, chakraPlayer2;

    public float speed;             //Floating point variable to store the player's movement speed.
    public Text playerName;
    public Text ninjustuTextPlayer1;
    public Image ninjutsuHolder;

    private Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.
    private int count;				//Integer to store the number of pickups collected so far.

    SpriteHelper spriteHelper;
    public static SpriteModes spriteMode = SpriteModes.Normal;
    bool moveRight = false;

    string ninjutsu, ultimate;
    Character character;

    // Use this for initialization
    void Start()
    {
        DatabaseHelper dbHelper = new DatabaseHelper();
        character = dbHelper.GetPlayer(1);

        playerName.text = character.Name;
        ninjutsu = character.Ninjutsu;
        ultimate = character.Ultimate;

        spriteHelper = new SpriteHelper(character.Sprites);

        InvokeRepeating("ChangeSprite", 0, 0.25f);

        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D>();

        //Initialize count to zero.
        count = 0;
    }

    void ChangeSprite()
    {
        spriteHelper.LoadMode(gameObject, spriteMode, transform.position.x, player2.transform.position.x, 1);
    }

    void Update()
    {
        InputHelper input = new InputHelper(1);

        if (transform.position.x > player2.transform.position.x)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        if (Input.GetAxis("L_XAxis_1") < 0 || input.AnalogLeft.Left == ControlAction.Down)
        {
            //if (!moveRight)
            //{
            //    rb2d.velocity = Vector3.zero;
            //    rb2d.angularVelocity = 0;
            //}

            //moveRight = false;
        }

        if (Input.GetAxis("L_XAxis_1") > 0 || input.AnalogLeft.Right == ControlAction.Down)
        {
            if (!moveRight)
            {
                rb2d.velocity = Vector3.zero;
                rb2d.angularVelocity = 0;
            }

            moveRight = true;
        }

        if (Input.GetAxis("L_XAxis_1") != 0 || input.AnalogLeft.Left == ControlAction.Pressing || input.AnalogLeft.Right == ControlAction.Pressing)
        {
            if (spriteHelper.lastMode == SpriteModes.Normal)
            {
                spriteMode = SpriteModes.Walk;
            }
        }

        if(Input.GetAxis("L_XAxis_1") == 0 || input.AnalogLeft.Left == ControlAction.Up || input.AnalogLeft.Right == ControlAction.Up)
        {
            if (spriteHelper.lastMode == SpriteModes.Walk)
            {
                spriteMode = SpriteModes.Normal;
            }

            moveRight = false;
        }

        if (Input.GetButtonDown("A_1") || input.AnalogLeft.Up == ControlAction.Down)
        {
            if (transform.position.y < 5)
            {
                Vector2 movement = new Vector2(0, 20);
                rb2d.AddForce(movement * speed);
            }
        }

        if (Input.GetAxis("L_YAxis_1") > 0 || input.AnalogLeft.Down == ControlAction.Pressing)
        {
            Vector2 movement = new Vector2(0, -50);
            rb2d.AddForce(movement * speed);
        }

        if (Input.GetButtonDown("Y_1") || input.Y == ControlAction.Down)
        {
            spriteMode = SpriteModes.Chakra;
        }

        if (Input.GetButtonUp("Y_1") || input.Y == ControlAction.Up)
        {
            spriteMode = SpriteModes.Normal;
        }

        if (Input.GetButton("Y_1") || input.Y == ControlAction.Pressing)
        {
            if (chakra.GetComponent<RectTransform>().sizeDelta.x < 200)
            {
                chakra.GetComponent<RectTransform>().localPosition += new Vector3(0.5f, 0, 0);
                chakra.GetComponent<RectTransform>().sizeDelta += new Vector2(1, 0);
            }
        }

        if (Input.GetButtonDown("B_1") || input.B == ControlAction.Down)
        {
            spriteMode = SpriteModes.Combo1;
        }

        if (Vector3.Distance(transform.position, player2.transform.position) <= 5)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("B_1"))
            {
                lifePlayer2.GetComponent<RectTransform>().localPosition += new Vector3(2.5F, 0, 0);
                lifePlayer2.GetComponent<RectTransform>().sizeDelta -= new Vector2(5, 0);
            }
        }

        if (Input.GetKeyDown(KeyCode.Z) || Input.GetButtonDown("RB_1"))
        {
            if (chakra.GetComponent<RectTransform>().sizeDelta.x >= 20)
            {
                chakra.GetComponent<RectTransform>().localPosition -= new Vector3(10, 0, 0);
                chakra.GetComponent<RectTransform>().sizeDelta -= new Vector2(20, 0);
                ninjustuTextPlayer1.text = character.Ninjutsu;
                ninjutsuHolder.GetComponent<RectTransform>().sizeDelta = new Vector2(character.Ninjutsu.Length * 25, 50);
                spriteMode = SpriteModes.Ninjutsu;

                if (Vector3.Distance(transform.position, player2.transform.position) <= 5)
                {
                    lifePlayer2.GetComponent<RectTransform>().localPosition += new Vector3(10F, 0, 0);
                    lifePlayer2.GetComponent<RectTransform>().sizeDelta -= new Vector2(20, 0);
                }
            }
        }
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        //Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Input.GetAxis("L_XAxis_1");

        //Store the current vertical input in the float moveVertical.
        float moveVertical = Input.GetAxis("L_YAxis_1");

        //Use the two store floats to create a new Vector2 variable movement.
        Vector2 movement = new Vector2(moveHorizontal, 0);

        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        rb2d.AddForce(movement * speed);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (moveRight)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
