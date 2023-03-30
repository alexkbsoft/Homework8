using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform[] wayPoints = new Transform[0];
    public Transform[] runners = new Transform[0];
    public Runner Runner;
    public bool Forward = true;

    public enum RaceType
    {
        Points,
        Race
    }

    public RaceType CurrentType {
        get => _type;
    }

    private RaceType _type;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartRace()
    {
        if (Runner == null)
        {
            return;
        }

        _type = RaceType.Race;

        Runner.StartWay();
    }

    public void Run()
    {
        if (Runner == null)
        {
            return;
        }
        _type = RaceType.Points;

        Runner.StartWay();
    }
}
