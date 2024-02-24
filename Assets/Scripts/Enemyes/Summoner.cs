using System;
using System.Collections;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UIElements;

public class Summoner : MonoBehaviour
{
    private static Transform pivot;

    [SerializeField]
    private Transform pivotStatCandidat;

    [SerializeField]
    private LevelManager levelManager;

    // Internal parameters

    [SerializeField]
    private EntityInfoManager[] entityInfoManagers;



    private void Start()
    {
        levelManager.onChangeGameState += OnChangeGameState;
        pivot = pivotStatCandidat;
        foreach (var entityInfoManager in entityInfoManagers)
        {
            entityInfoManager.InstantiateAll();
        }
    }

    private void OnChangeGameState(GameState gameState)
    {
        if (gameState == GameState.Game)
        {
            foreach (var entityInfoManager in entityInfoManagers)
            {
                entityInfoManager.Restart();
            }
        }
        
    }

    [Serializable]
    private class EntityInfoManager
    {
        public event Action<Func<Vector3, bool>> restartSummonFlow = null;

        [SerializeField]
        private EntityInfo entityInfo;

        private Vector3 currentPosition;
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
            if (currentPosition == null)
                currentPosition = Vector3.zero;
            if (pivot == null)
                pivot = Summoner.pivot;
        }

        public void InstantiateAll()
        {
            Quaternion rotation = Quaternion.identity;
            for (int i = 0; i < entityInfo.InstancesCount; i++)
            {
                Instance instance = Instantiate(entityInfo.entity, GetNextPosition(), rotation).GetComponent<Instance>();
                instance.pivotPoint = pivot;
                instance.retireDistance = entityInfo.RetireDistance;
                instance.sendReuseRequest = Reuse;
                restartSummonFlow += instance.OnRestartGame;
            }
        }

        private void Reuse(GameObject gameObject)
        {
            gameObject.transform.position = GetNextPosition();
        }

        public void Restart()
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
}

