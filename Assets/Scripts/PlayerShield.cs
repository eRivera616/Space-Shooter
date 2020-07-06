using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShield : MonoBehaviour
{
    private int shieldDamageID;
    private SpriteRenderer _shieldSprite;
    public bool _shieldActive;
    void Start()
    {
        _shieldSprite = GetComponent<SpriteRenderer>();
        if (_shieldSprite = null)
        {
            Debug.LogError("Shield SpriteRenderer is NULL");
        }
    }
    private void Update()
    {
        if (_shieldActive == true)
        {
            gameObject.SetActive(true);
        }    
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy Laser" || other.tag == "Enemy")
        {
            shieldDamageID += 1;
            return;
        }
        if (shieldDamageID > 2)
        {
            gameObject.SetActive(false);
            _shieldActive = false;
            shieldDamageID = 0;

        }
            switch (shieldDamageID)
            {
                case 0:
                 _shieldSprite.color = Color.clear;
                 break;
                case 1:
                    _shieldSprite.color = Color.green;
                break;
                case 2:
                    _shieldSprite.color = Color.red;
                break;
            }
    }
}
