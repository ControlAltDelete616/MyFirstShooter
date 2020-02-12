using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    // Der Gegner den wir spawnen wollen
    public GameObject monsterPrefab;
    // Wie oft wir Gegner spawnen wollen
    public float interval = 1;
    // Die verschiedenen Spawnpoints
    public GameObject[] spawnPoints;


    // Start wird vor dem ersten Update Frame aufgerufen
    void Start()
    {
        // Soll alle "intervall" Sekunden die Methode "SpawnEnemy" aufrufen
        InvokeRepeating("SpawnEnemy", interval, interval);
    }

    // Methode um Gegner zu spawnen
    void SpawnEnemy()
    {
        // Zufallswert abhängig von Anzahl an Spawnpoints
        int rnd = Random.Range(0, spawnPoints.Length);
        // Erzeuge ein Monster an dem zufälligen Spawnpoint
        Instantiate(monsterPrefab, spawnPoints[rnd].transform.position, Quaternion.identity);
    }
}