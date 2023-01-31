using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private AudioSource _audioSource = default;

    [SerializeField, Header("発射音")]
    private AudioClip _shotSound = default;

    [SerializeField, Header("デリート音")]
    private AudioClip _deleteSound = default;

    void Start()
    {

        _audioSource = this.gameObject.GetComponent<AudioSource>();

    }

    /// <summary>
    /// 弾を撃った時の音を鳴らす
    /// </summary>
    public void ShotSound()
    {
        _audioSource.PlayOneShot(_shotSound);
    }

    /// <summary>
    /// 倒された時の音を鳴らす
    /// </summary>
    public void DeleteSound()
    {
        _audioSource.PlayOneShot(_deleteSound);
    }
}
