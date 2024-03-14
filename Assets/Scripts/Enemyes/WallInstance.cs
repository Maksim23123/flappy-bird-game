using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallInstance : Instance
{
    // External parameters

    GameObject colectable;

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
            if (colectable != null)
            {
                Destroy(colectable);
            }
            sendReuseRequest?.Invoke(gameObject);
        }
    }

    public override void OnForceRestart()
    {
        if (colectable != null)
        {
            Destroy(colectable);
        }
        base.OnForceRestart();
    }

    public void SetColectable(GameObject colectable)
    {
        if (this.colectable != null)
        {
            Destroy(this.colectable);
        }
        this.colectable = colectable;
    }
}
