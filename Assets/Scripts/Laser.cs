using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private float _speed = 8.0f;
    private bool _enemyLaser = false;
    void Update()
    {
        MovementCheck();
        OffScreenCheck();
    }
    void MoveUp()
    {
        transform.Translate(Vector3.up * Time.deltaTime * _speed);
    }
    void MoveDown()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
    }
    void MovementCheck()
    {
        if (transform.parent == null || transform.parent.tag == "Triple Shot")
        {
            _enemyLaser = false;
            MoveUp();
        }
        else
        {
            _enemyLaser = true;
            MoveDown();
        }
    }
    void OffScreenCheck()
    {
        if(transform.position.y > 8f || transform.position.y < -6f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && _enemyLaser == true)
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }
            Destroy(this.gameObject);
        }
    }
}
