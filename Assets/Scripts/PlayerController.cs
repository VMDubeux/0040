using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Complementars GameObjects 1:")]
    public ParticleSystem ExplosionParticles;
    public ParticleSystem DirtyParticles;

    [Header("Complementars GameObjects 2:")]
    public AudioClip CrashSound;
    public AudioClip JumpSound;

    [Header("Private Variables:")]
    [SerializeField] private int _jumpForce = 600;
    [SerializeField] private int _jumpExtraForce = 200;
    [SerializeField] private float _gravityModifier;
    [SerializeField] private bool _isGrounded = true;

    //Not Serialized Private Variables:
    private Rigidbody _rbPlayer;
    private Animator _playerAnim;
    private AudioSource _playerAudio;
    private bool _firstJump = false;
    internal bool _doubleSpeed = false;
    internal bool _theGameIsOver = false;

    void Start()
    {
        _rbPlayer = GetComponent<Rigidbody>();
        _playerAnim = GetComponent<Animator>();
        _playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= _gravityModifier;
        
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            _doubleSpeed = true;
            _playerAnim.SetFloat("Speed_Multiplier", 2.0f);
        }
        else if (_doubleSpeed)
        {
            _doubleSpeed = false;
            _playerAnim.SetFloat("Speed_Multiplier", 1.0f);
        }

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded && !_theGameIsOver && !_firstJump)
        {
            _rbPlayer.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            _isGrounded = false;
            _firstJump = true;
            _playerAnim.SetTrigger("Jump_trig");
            DirtyParticles.Stop();
            _playerAudio.PlayOneShot(JumpSound, 1.0f);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !_isGrounded && !_theGameIsOver && _firstJump)
        {
            _rbPlayer.AddForce(Vector3.up * _jumpExtraForce, ForceMode.Impulse);
            _firstJump = false;
            DirtyParticles.Stop();
            _playerAudio.PlayOneShot(JumpSound, 1.0f);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
            _firstJump = false;
            DirtyParticles.Play();
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over!");
            _theGameIsOver = true;
            _playerAnim.SetBool("Death_b", true);
            _playerAnim.SetInteger("DeathType_int", 1);
            ExplosionParticles.Play();
            DirtyParticles.Stop();
            _playerAudio.PlayOneShot(CrashSound, 1.0f);
            transform.position = new Vector3(transform.position.x - 3.0f, 0, 0);
        }
    }
}
