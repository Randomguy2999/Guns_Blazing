using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private float _lastFire = float.MinValue;
    [SerializeField] private Vector3 spawnOffSet = Vector3.zero;

    public Transform _blastPrefab;
    private MeshRenderer _object;

    private void Start()
    {
        Vector3 spawnLocation = Vector3.zero;
        _object = GetComponent<MeshRenderer>();
        spawnLocation.x = -spawnOffSet.x;
    }

    private void Update()
    {
        if (PauseManager.isPaused) return;

        Vector3 spawnLocation = Vector3.zero;
        Quaternion rotation;
        if (_object)
        {
            spawnLocation.x = -spawnOffSet.x;
            rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        else
        {
            spawnLocation.x = spawnOffSet.x;
            rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }

        spawnLocation.y = spawnOffSet.y;

        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(_blastPrefab, transform.position + (Vector3)spawnLocation, rotation);

            _lastFire = Time.time;
        }
    }
}  

