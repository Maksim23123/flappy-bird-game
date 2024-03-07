using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultEntityInfoManager : EntityInfoManager
{
    public event Action<Func<Vector3, bool>> restartSummonFlow = null;

    [SerializeField]
    private EntityInfo entityInfo;

    private Vector3 currentPosition;
    private bool currentPositionIsAssigned = false;
    [SerializeField]
    private Transform pivot = null;

    [SerializeField]
    public bool xAutoIncreasement = false;
    [SerializeField]
    public bool yAutoIncreasement = false;
    [SerializeField]
    public bool zAutoIncreasement = false;

    [SerializeField]
    public bool restartable = false;

    // Buffers

    private Vector3 bufferedPosition;

    public Vector3 GetNextPosition(bool recursive = false)
    {
        CheckAllImportantTransforms();
        Vector3 position = currentPosition;
        if (recursive)
            position = bufferedPosition;

        position = entityInfo.EntityCoordinats.ProcessVector3(position);

        Vector3 newTransformPosition = currentPosition;

        if (xAutoIncreasement)
            newTransformPosition.x = position.x;

        if (yAutoIncreasement)
            newTransformPosition.y = position.y;

        if (zAutoIncreasement)
            newTransformPosition.z = position.z;


        if (ValidatePositionPivotAccording(position))
        {
            currentPosition = newTransformPosition;
            return position;
        }
        else
        {
            bufferedPosition = newTransformPosition;
            return GetNextPosition(true);
        }
    }

    private void CheckAllImportantTransforms()
    {
        if (!currentPositionIsAssigned)
        {
            currentPosition = new Vector3(entityInfo.EntityCoordinats.XStartPos
                , entityInfo.EntityCoordinats.YStartPos
                , entityInfo.EntityCoordinats.ZStartPos);
            currentPositionIsAssigned = true;
        }
    }

    public override void InstantiateAll()
    {
        Quaternion rotation = Quaternion.identity;
        for (int i = 0; i < entityInfo.InstancesCount; i++)
        {
            Instance instance = Instantiate(entityInfo.entity, GetNextPosition(), rotation).GetComponent<Instance>();
            if (instance != null)
            {
                instance.pivotPoint = pivot;
                instance.retireDistance = entityInfo.RetireDistance;
                instance.sendReuseRequest = Reuse;
                restartSummonFlow += instance.OnRestartGame;
            }
        }
    }

    private void Reuse(GameObject gameObject)
    {
        gameObject.transform.position = GetNextPosition();
    }

    public override void Restart()
    {
        if (restartable)
        {
            restartSummonFlow.Invoke(ValidatePositionPivotAccording);
        }
    }

    private bool ValidatePositionPivotAccording(Vector3 position)
    {
        return entityInfo.ValidatePosition(position, pivot.position);
    }
}
