using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallInstance : Instance
{
    // External parameters

    List<GameObject> colectables = new List<GameObject>();

    public int betweenInstancesOffset = 0;

    // Internal parameters

    private void Update()
    {
        if (transform.position.x < pivotPoint.position.x - retireDistance)
        {
            sendReuseRequest?.Invoke(gameObject);
        }
    }

    public override void OnRestartGame(Func<Vector3, bool> validatePosition)
    {
        if (!validatePosition(transform.position))
        {
            ClearColectablesColection();
            sendReuseRequest?.Invoke(gameObject);
        }
    }

    public override void OnForceRestart()
    {
        ClearColectablesColection();
        base.OnForceRestart();
    }

    public void AddColectable(GameObject colectable)
    {
        colectables.Add(colectable);
    }

    public void ClearColectablesColection()
    {
        if (colectables.Count > 0)
        {
            foreach (var colectable in colectables)
                Destroy(colectable);
            colectables = new List<GameObject>();
        }
    }
}
