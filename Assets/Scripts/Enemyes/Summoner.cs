using OpenCover.Framework.Model;
using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Summoner : MonoBehaviour
{
    // External parameters
    private static Transform entityInfoManagersBasicTransform;
    private static Transform pivot;

    [SerializeField]
    private Transform pivotStatCandidat;

    // Internal parameters

    [SerializeField]
    private EntityInfoManager[] entityInfoManagers; 



    private void Start()
    {
        entityInfoManagersBasicTransform = transform;
        pivot = pivotStatCandidat;
        foreach (var entityInfoManager in entityInfoManagers)
        {
            entityInfoManager.InstantiateAll();
        }
    }

    private void Update()
    {
        /*
        if (Wall.ResponsCount > maxObjectsCount)
        {
            if (Wall.WallNearPlayer)
            {
                Wall.ResetNearPlayerChecker();
            }
            else
            {
                Wall.ResetNearPlayerChecker();
            }
        }*/
    }

    IEnumerator CreateInstance()
    {
        /*
        if (instancesCount < maxObjectsCount)
        {
            PerformInstantiate();
        }
        else
        {
            PerformReuse();
        }
        */
        yield return new WaitForSeconds(1f);

        StartCoroutine(CreateInstance());
    }

    void PerformInstantiate()
    {
        /*
        Vector3 position = transform.position;
        position.y = UnityEngine.Random.value * verticalHeightBoundsSize * 2 - verticalHeightBoundsSize;
        GameObject instance = Instantiate(instanceExample, position, Quaternion.identity, gameObject.transform);
        InstanceSpeaker instanceSpeaker = instance.GetComponent<InstanceSpeaker>();

        instanceSpeaker.sendReusePoolRequest += PerformReuse;
        instancesCount++;*/
    }

    void PerformReuse(GameObject instance)
    {
        /*
        if (reusePool.Count > 0)
        {
            Vector3 position = transform.position;
            position.y = UnityEngine.Random.value * verticalHeightBoundsSize * 2 - verticalHeightBoundsSize;
            instance.SetActive(true);
            instance.transform.position = position;
        }
        */
    }

    [Serializable]
    private class EntityInfoManager
    {
        [SerializeField]
        private EntityInfo entityInfo;

        private Transform transform = null;
        private Transform pivot = null;

        [SerializeField]
        public bool xAutoIncreasement = false;
        [SerializeField]
        public bool yAutoIncreasement = false;
        [SerializeField]
        public bool zAutoIncreasement = false;

        // Buffers

        private Vector3 bufferedPosition;

        public Vector3 GetNextPosition(bool recursive = false)
        {
            CheckAllImportantTransforms();
            Vector3 position = transform.position;
            if (recursive)
                position = bufferedPosition;

            position = entityInfo.EntityCoordinats.ProcessVector3(position);
            
            Vector3 newTransformPosition = transform.position;

            if (xAutoIncreasement)
                newTransformPosition.x = position.x;

            if (yAutoIncreasement)
                newTransformPosition.y = position.y;

            if (zAutoIncreasement)
                newTransformPosition.z = position.z;
            

            if (entityInfo.ValidatePosition(position, pivot.position))
            {
                transform.position = newTransformPosition;
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
            if (transform == null)
                transform = entityInfoManagersBasicTransform;
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
            }
        }

        private void Reuse(GameObject gameObject)
        {
            gameObject.transform.position = GetNextPosition();
        }

    }
}

