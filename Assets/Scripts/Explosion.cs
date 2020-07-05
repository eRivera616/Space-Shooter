using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    private AudioClip _explosionClip;
    private AudioSource _soundSource;
    void Start()
    {
        _soundSource = GetComponent<AudioSource>();
        if (_soundSource == null)
        {
            Debug.LogError("Explosion Audio Source is NULL");
        }
        _soundSource.PlayOneShot(_explosionClip);
        Destroy(this.gameObject, 3f);
    }
}
