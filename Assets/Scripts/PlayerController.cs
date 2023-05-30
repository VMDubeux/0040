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
    [SerializeField] private int _jumpForce = 10;
    [SerializeField] private float _gravityModifier;
    [SerializeField] private bool _isGrounded = true;

    //Not Serialized Private Variables:
    private Rigidbody _rbPlayer;
    private Animator _playerAnim;
    private AudioSource _playerAudio;
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
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded && !_theGameIsOver)
        {
            _rbPlayer.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            _isGrounded = false;
            _playerAnim.SetTrigger("Jump_trig");
            DirtyParticles.Stop();
            _playerAudio.PlayOneShot(JumpSound, 1.0f);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
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
