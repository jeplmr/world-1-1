using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KBPlayerController : MonoBehaviour
{

    public static bool isSuper = false;
    public static bool _facingRight = true;

    public float moveSpeed = 5f; 
    public float jumpSpeed = 2f;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    public float walkSpeedLimit = 10f;
    public float runSpeedLimit = 20f;
    public float groundedDistance = 0f;

    public Material normalMario;
    public Material normalMarioHat;
    public Material fireMario;
    public Material fireMarioHat;

    public Renderer bodyRenderer;
    public Renderer hatRenderer;

    public float hurtCooldown = 1f;

    public AudioClip smallMarioJumpSound;
    public AudioClip bigMarioJumpSound;
    public AudioClip marioHurtSound;
    public AudioClip fireballSound; 

    public GameObject fireball;
    public float fireballCooldown = 1f;

    public GameManager gm; 

    private float lastThrown = 0f; 

    private CameraFollow _cf; 

    private Rigidbody _rigidbody;
    private float _horizMovement;
    private bool _jumpRequest = false;
    private bool _isRunning = false;
    private bool _isGrounded = true;
    private Animator _animator;
    private AudioSource _jumpSound; 

    private Vector3 _originalPostion;

    private bool isEnabled;

    private Vector3 normalSize = new Vector3(0.75f, 1.5f, 0.75f);
    private Vector3 superSize = new Vector3(1, 2, 1);

    private int marioState = 0; //0 = tiny; 1 = super; 2 = fireflower


    private float lastHurt = 0f;
    private bool _isDead = false;






    // Start is called before the first frame update
    void Start()
    {
        _originalPostion = transform.position; 
        _cf = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
        _jumpSound = GetComponent<AudioSource>();
        //_rend = GetComponentInChildren<Renderer>();
        bodyRenderer.material = normalMario;
        hatRenderer.material = normalMarioHat;

        isSuper = false;
        _facingRight = true;
        marioState = 0; 
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.isPaused == true)
        {
            isEnabled = false; 
        } else
        {
            isEnabled = true; 
        }

        if (isEnabled)
        {
            //check for jump input
            if (Input.GetButtonDown("Jump") && !_jumpRequest)
            {
                _jumpRequest = true;
            }

            _isGrounded = CheckIfGrounded();
            _animator.SetBool("isGrounded", _isGrounded);
            _animator.SetFloat("Speed", Mathf.Abs(_rigidbody.velocity.x));

            _horizMovement = Input.GetAxisRaw("Horizontal") * moveSpeed;

            if (Input.GetButton("Fire3"))
            {
                _isRunning = true;
                _horizMovement = (Input.GetAxisRaw("Horizontal") * moveSpeed) * 2;
            }

            if (Input.GetButtonUp("Fire3"))
            {
                _isRunning = false;
            }

            if (Input.GetAxisRaw("Horizontal") > 0f)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                _facingRight = true;
            }
            else if (Input.GetAxisRaw("Horizontal") < 0f)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y, transform.localScale.z);
                _facingRight = false;
            }

            //ResetPlayerPosition();
            PlayerHasFallen(); 
            ThrowFireball(); 
            checkIfDead();
        }

 
    }

    void FixedUpdate()
    {
        ApplySpeedLimit();

        _rigidbody.AddForce(new Vector3(_horizMovement, 0, 0), ForceMode.Acceleration);

        Jump();

        if (_rigidbody.velocity.y < 0)
        {
            _rigidbody.AddForce(Vector3.up * Physics.gravity.y * fallMultiplier * Time.deltaTime); 
        }
        else if (_rigidbody.velocity.y > 0 && Input.GetButton("Jump") == false)
        {
            _rigidbody.AddForce(Vector3.up * Physics.gravity.y * lowJumpMultiplier * Time.deltaTime);
        }
    }


    private void Jump()
    {
        if (_jumpRequest && _isGrounded)
        {
            _rigidbody.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse); 
            _jumpRequest = false;
            if (marioState > 0)
            {
                _jumpSound.PlayOneShot(bigMarioJumpSound); 
            } else
            {
                _jumpSound.PlayOneShot(smallMarioJumpSound);
            }
        }
    }

    private void ApplySpeedLimit()
    {
        if (_rigidbody.velocity.x > walkSpeedLimit && _isRunning == false)
        {
            _rigidbody.velocity = new Vector3(walkSpeedLimit, _rigidbody.velocity.y, 0);
        }

        if (_rigidbody.velocity.x < walkSpeedLimit * -1 && _isRunning == false)
        {
            _rigidbody.velocity = new Vector3(walkSpeedLimit * -1, _rigidbody.velocity.y, 0);
        }

        if (_rigidbody.velocity.x > runSpeedLimit && _isRunning == true)
        {
            _rigidbody.velocity = new Vector3(runSpeedLimit, _rigidbody.velocity.y, 0);
        }

        if (_rigidbody.velocity.x < runSpeedLimit * -1 && _isRunning == true)
        {
            _rigidbody.velocity = new Vector3(runSpeedLimit * -1, _rigidbody.velocity.y, 0);
        }
    }

    private bool CheckIfGrounded()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, groundedDistance))
        {
            //Debug.Log(hit); 
            return true; 
        } else
        {
            //Debug.Log(hit);
            return false; 
        }
    }

    private void ResetPlayerPosition()
    {
        if (transform.position.y <= -5f)
        {
            _rigidbody.velocity = Vector3.zero;
            transform.position = _originalPostion;
            _cf.ResetCameraPosition();
        }
    }

    private void PlayerHasFallen()
    {
        if (transform.position.y <= -5f)
        {
            gameObject.SetActive(false);
            marioState = -1; 
        }
    }


    public void UpgradeMario(int i)
    {
        if (marioState >= 0 && marioState <= 2)
        {
            if (marioState < i)
            {
                marioState = i;
                transform.localScale = superSize;
            }
        }

        if (marioState == 2)
        {
            //FIREFLOWER
            hatRenderer.material = fireMarioHat;
            bodyRenderer.material = fireMario;
            transform.localScale = superSize;
        }
        
        if (marioState >= 1)
        {
            isSuper = true;
        } else
        {
            isSuper = false; 
        }
        //Debug.Log(marioState); 
    }

    public void HurtMario()
    {
        if (Time.time - lastHurt > hurtCooldown)
        {
            if (marioState >= 0 && marioState <= 2)
            {
                marioState--;
                bodyRenderer.material = normalMario;
                hatRenderer.material = normalMarioHat;
            }
            if (marioState == 1)
            {
                transform.localScale = superSize;
            }
            if (marioState == 0)
            {
                transform.localScale = normalSize;
                isSuper = false; 
            }
        }
        lastHurt = Time.time;
        _jumpSound.PlayOneShot(marioHurtSound); 
        Debug.Log(marioState); 
    }

    private void checkIfDead()
    {
        if (marioState < 0)
        {
            Die(); 
        }
    }

    private void ThrowFireball()
    {
        if (marioState >= 2 && Input.GetButtonDown("Fire3") && Time.time - lastThrown > fireballCooldown)
        {
            lastThrown = Time.time;
            
            _jumpSound.PlayOneShot(fireballSound); 
            if (_facingRight)
            {
                Instantiate(fireball, transform.position + new Vector3(1, 0, 0), Quaternion.identity);
            }
            else
            {
                Instantiate(fireball, transform.position + new Vector3(-1, 0, 0), Quaternion.identity);
            }
        }

    }

    private void Die()
    {
        _isDead = true;
        gameObject.SetActive(false);
        gm.SetPlayerStatus(_isDead); 
        //_animator.SetBool("isDead", true); 
    }

}
