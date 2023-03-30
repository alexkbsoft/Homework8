using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour
{
    #region [public]
    public float Speed = 1.0f;
    public bool Go = false;
    public float PassDistance = 0.1f;
    #endregion


    #region [private]
    private Transform _target;
    private GameManager _gm;
    #endregion
    void Start()
    {
        _gm = GameObject.FindObjectOfType<GameManager>();
    }

    void Update()
    {

        if (Go && _target != null)
        {
            transform.Rotate(new Vector3(0, 0, 1));


            transform.position = Vector3.MoveTowards(transform.position, _target.position, Time.deltaTime * Speed);

            if (Vector3.Distance(transform.position, _target.transform.position) < PassDistance)
            {
                SelectNextTarget();
            }
        }


    }

    public void StartWay()
    {
        _target = null;
        Go = true;

        SelectNextTarget();
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    public void SelectNextTarget()
    {
        if (_gm.CurrentType == GameManager.RaceType.Points)
        {
            SelectNextPoint();
        }
        else
        {
            SelectNextRunner();
        }
    }

    public void SelectNextRunner()
    {

        var currentIndex = Array.IndexOf(_gm.runners, transform);

        if (currentIndex >= 0)
        {
            var nextTarget = GetNext(currentIndex);

            if (_target == null) {
                ChangeTarget(nextTarget);
            } else {
                Go = false;
                var nextRunner = nextTarget.GetComponent<Runner>();
                nextRunner.StartWay();
            }
        }
    }

    public void SelectNextPoint()
    {
        if (_target != null)
        {
            var currentIndex = Array.IndexOf(_gm.wayPoints, _target);

            if (currentIndex >= 0)
            {
                ChangeTarget(GetNext(currentIndex));
            }
        }
        else if (_gm.wayPoints.Length != 0)
        {
            ChangeTarget(_gm.wayPoints[0].transform);
        }
    }

    private void ChangeTarget(Transform newTarget)
    {
        _target = newTarget;
        transform.LookAt(_target);
    }

    private Transform GetNext(int index)
    {
        var arr = _gm.CurrentType == GameManager.RaceType.Points ? _gm.wayPoints : _gm.runners;

        if (index == arr.Length - 1)
        {
            return arr[0];
        }
        else
        {
            return arr[index + 1];
        }
    }
}

