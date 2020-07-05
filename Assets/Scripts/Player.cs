using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float _speed = 6f;
    private float _boostMultiplier = 2f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    private float _fireRate = 0.25f;
    private float _canFire = -1f;
    private int _lives = 3;
    private SpawnManager _spawnManager;
    private UIManager _uiManager;
    private bool _tripleShotActive = false;
    private bool _speedBoostActive = false;
    private bool _shieldActive = false;
    [SerializeField]
    private GameObject _shieldVis;
    [SerializeField]
    private GameObject _boostVis;
    [SerializeField]
    private GameObject _leftEngineFireVis;
    [SerializeField]
    private GameObject _rightEngineFireVis;
    [SerializeField]
    private GameObject _deathExplosionPrefab;
    [SerializeField]
    private GameObject _turnLeftAnim;
    [SerializeField]
    private GameObject _turnRightAnim;
    private SpriteRenderer _playerSprite;
    public int _score;
    private int damageID;
    [SerializeField]
    private AudioClip _laserSoundClip;
    [SerializeField]
    private AudioClip _powerupSoundClip;
    private AudioSource _soundSource;
    void Start()
    {
        _playerSprite = GetComponent<SpriteRenderer>();
        transform.position = new Vector3(0, -3.8f, 0);
        _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _soundSource = GetComponent<AudioSource>();
        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL");
        }
        if (_uiManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL");
        }
        if (_soundSource == null)
        {
            Debug.LogError("Audio Source on the player is NULL");
        }
        if (_playerSprite == null)
        {
            Debug.LogError("SpriteRenderer on the player is null");
        }
    }
    void Update()
    {
        calculateMovement();
        if(Input.GetKey(KeyCode.Space) && Time.time > _canFire)
        {
            fireLaser();
        }
    }
    void calculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);
        if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }
    void fireLaser()
    {
        _canFire = Time.time + _fireRate;
       if(_tripleShotActive == false)
       {
          Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
       }
       else
       {
           Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
       }
       _soundSource.PlayOneShot(_laserSoundClip);
    }
    public void Damage()
    {
        if (_shieldActive == true)
        {
            _shieldActive = false;
            _shieldVis.SetActive(false);
            return;
        }
        _lives --;
        _uiManager.UpdateLives(_lives);
        if(_lives < 1)
        {
            _spawnManager.onPlayerDeath();
            Instantiate(_deathExplosionPrefab, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        else if(_lives == 2)
        {
            damageID = Random.Range(0, 2);
        }
        else if(_lives == 1 && damageID == 0)
        {
            damageID = 1;
        }
        else if(_lives == 1 && damageID == 1)
        {
            damageID = 0;
        }
        switch(damageID)
        {
            case 0:
                _leftEngineFireVis.SetActive(true);
                break;
            case 1:
                _rightEngineFireVis.SetActive(true);
                break;
        }
    }
    public void TripleShotActive()
    {
        _tripleShotActive = true;
        _soundSource.PlayOneShot(_powerupSoundClip);
        StartCoroutine(TripleShotPowerDownRoutine());
    }
    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _tripleShotActive = false;
    }
    public void SpeedBoostActive()
    {
        _speedBoostActive = true;
        _speed *= _boostMultiplier;
        _boostVis.SetActive(true);
        _soundSource.PlayOneShot(_powerupSoundClip);
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }
    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _speedBoostActive = false;
        _boostVis.SetActive(false);
        _speed /= _boostMultiplier;
    }
    public void ShieldActive()
    {
        _shieldActive = true;
        _shieldVis.SetActive(true);
        _soundSource.PlayOneShot(_powerupSoundClip);
    }
    public void AddScore(int points)
    {

        _score += points;
        _uiManager.UpdateScore(_score);
    }
}