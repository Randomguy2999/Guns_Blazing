using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BluePlayerHealth : MonoBehaviour
{
    [SerializeField] private int _MaxHealth = 10;
    [SerializeField] private int _CurrentHealth = 10;
    [SerializeField] private TMP_Text _blueHealthText;

    AudioSource audioData;

    private void Start()
    {
        audioData = GetComponent<AudioSource>();
    }

    private void Update()
    {
        _blueHealthText.text = $"Health: {_CurrentHealth }";
    }

    private void OnTriggerEnter(Collider other)
    {
        RedBlast red = other.gameObject.GetComponent<RedBlast>();

        HealthPack health = other.gameObject.GetComponent<HealthPack>();

        if (red != null)
        {
            _CurrentHealth -= 2;

            Debug.Log("Hit");

            if (_CurrentHealth <= 0)
            {
                Destroy(gameObject);
            }

            else if (health != null)
            {
                _CurrentHealth += 3;
                audioData.Play();
                Destroy(gameObject);

            }
        }
    }

    private void OnCollisionEnter(Collision col)
    {

        DeathZone deathZone = col.gameObject.GetComponent<DeathZone>();

        HazardDamage hazard = col.gameObject.GetComponent<HazardDamage>();


        if (deathZone != null)
        {
            _CurrentHealth = 0;

            if (_CurrentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
        else if (hazard != null)
        {
            _CurrentHealth--;

            if (_CurrentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }

    }
}