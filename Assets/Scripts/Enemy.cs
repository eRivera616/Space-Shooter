using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _speed = 2f;
    private float _fireRate = 3f;
    private float _canFire = -1f;
    private Player _player;
    [SerializeField]
    private GameObject _deathAnim;
    [SerializeField]
    private GameObject _enemyLaser;
    [SerializeField]
    private AudioClip _laserSoundClip;
    private AudioSource _soundSource;
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _soundSource = GetComponent<AudioSource>();
        if (_soundSource == null)
        {
            Debug.LogError("AudioSource is NULL");
        }
        if (_player == null)
        {
            Debug.LogError("Player is NULL");
        }
        if (_deathAnim == null)
        {
            Debug.LogError("Animator is NULL");
        }
        transform.position = new Vector3 (Random.Range(-8f, 8f), 7, 0);
    }
    void Update()
    {
        calculateMovement();
        if (_player == null)
        {
            Destroy(this.gameObject);
        }
        else if (_player != null)
        {
            EnemyFireRoutine();
        }
    }
    void calculateMovement()
    {
        float spawnPointX = Random.Range(-8f, 8f);
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
        
        if (transform.position.y < -5.4f)
        {
            transform.position = new Vector3 (spawnPointX, 7, 0);
        }
    }
    void EnemyFireRoutine()
    {
        if(Time.time > _canFire)
        {
            _fireRate = Random.Range(2f, 5f);
            _canFire = Time.time + _fireRate;
            Instantiate(_enemyLaser, this.transform.position, Quaternion.identity);
            _soundSource.PlayOneShot(_laserSoundClip);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (_player != null)
            {
                _player.Damage();
            }
            _speed = 0;
            Instantiate(_deathAnim, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        if (other.name == "Laser(Clone)")
        {
            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.AddScore(10);
            }
            _speed = 0;
            Instantiate(_deathAnim, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        
        if (other.tag == "Player Shield")
        {
            if (_player != null)
            {
                _player.AddScore(10);
            }
            _speed = 0;
            Instantiate(_deathAnim, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
