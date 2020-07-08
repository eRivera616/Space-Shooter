using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShield : MonoBehaviour
{
    [SerializeField]
    private int shieldDamageID;
    private SpriteRenderer _shieldSprite;
    void Start()
    {
        _shieldSprite = GetComponent<SpriteRenderer>();
        if (_shieldSprite == null)
        {
            Debug.LogError("Shield SpriteRenderer is NULL");
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Enemy Laser Left" || other.name == "Enemy Laser Right")
        {
            shieldDamageID += 1;
            Destroy(other.gameObject);
        }
        if (other.tag == "Enemy")
        {
            shieldDamageID += 1;
        }
        if (shieldDamageID > 2)
        {
            shieldDamageID = 0;
            gameObject.SetActive(false);
        }
        if (gameObject.activeSelf == true)
        {
            switch (shieldDamageID)
            {
                case 0:
                    _shieldSprite.color = new Color (1f,1f,1f,1f); //neutral
                 break;
                case 1:
                    _shieldSprite.color = new Color (0f,1f,0f,1f); //green
                break;
                case 2:
                    _shieldSprite.color = new Color (1f,0f,0f,1f); //red
                break;
            }
        }
    }
}