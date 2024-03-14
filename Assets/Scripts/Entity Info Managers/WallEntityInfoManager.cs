using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallEntityInfoManager : EntityInfoManager
{
    public event Action<Func<Vector3, bool>> restartSummonFlow = null;
    public event Action forceRestart = null;

    [SerializeField]
    private LevelManager levelManager;
    [SerializeField]
    private EntityInfo entityInfo;
    [SerializeField]
    private DropManager dropManager;

    [SerializeField]
    private SerializableDict<RoundStats.Difficulty, Vector3> difficultySettings 
        = new SerializableDict<RoundStats.Difficulty, Vector3>();

    
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

    [SerializeField]
    public float coinProbability = 0.3f;

    // Buffers

    private Vector3 currentPosition;
    private Vector3 difficultyOffset;
    private Vector3 bufferedPosition;
    Queue<Transform> transformsQueue = new Queue<Transform>();

    private void Start()
    {
        OnDificultyChanged(RoundStats.CurrentDificulty);
        RoundStats.DifficultyChanged += OnDificultyChanged;
    }

    private void OnDificultyChanged(RoundStats.Difficulty difficulty)
    {
        if (difficultySettings.TryGetValue(difficulty, out Vector3 newDiffOffset))
        {
            difficultyOffset = newDiffOffset;
            forceRestart();
        }
    }

    public Vector3 GetNextPosition(bool recursive = false)
    {
        CheckAllImportantTransforms();
        Vector3 position = currentPosition;
        if (recursive)
            position = bufferedPosition;

        position = entityInfo.EntityCoordinats.ProcessVector3(position);
        position += difficultyOffset;

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
            Instance instance = Instantiate(entityInfo.entity, GetNextPosition(), rotation, transform).GetComponent<Instance>();
            if (instance != null)
            {
                instance.pivotPoint = pivot;
                instance.retireDistance = entityInfo.RetireDistance;
                instance.sendReuseRequest = Reuse;
                forceRestart += instance.OnForceRestart;
                restartSummonFlow += instance.OnRestartGame;
                if (instance.GetType() == typeof(WallInstance) && dropManager.GetRandomDrop(out GameObject drop))
                {
                    GameObject newColectable = Instantiate(drop, gameObject.transform);
                    Colectable colectableData = newColectable.GetComponent<Colectable>();
                    if (colectableData != null)
                    {
                        colectableData.PickUpColectable += OnPickUpColectable;
                    }
                    ((WallInstance)instance).SetColectable(newColectable);
                }
                transformsQueue.Enqueue(instance.gameObject.transform);
            }
        }
    }

    private void Reuse(GameObject gameObject)
    {
        gameObject.transform.position = GetNextPosition();
        Instance instance = gameObject.GetComponent<Instance>();
        if (instance.GetType() == typeof(WallInstance) && dropManager.GetRandomDrop(out GameObject drop))
        {
            GameObject newColectable = Instantiate(drop, gameObject.transform);
            Colectable colectableData = newColectable.GetComponent<Colectable>();
            if (colectableData != null)
            {
                colectableData.PickUpColectable += OnPickUpColectable;
            }
            ((WallInstance)instance).SetColectable(newColectable);
        }
        transformsQueue.Dequeue();
        transformsQueue.Enqueue(gameObject.transform);
        RoundStats.firstEnemyPosition = transformsQueue.Peek().position;
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

    private void OnPickUpColectable(object sender, PickUpColectableEventArgs e)
    {
        if (e.Type == ColectableType.Coin)
        {
            levelManager.GameData.CoinsCount += e.ColectableCount;
        }
        else if (e.Type == ColectableType.Gem)
        {
            levelManager.GameData.GemsCount += e.ColectableCount;
        }
        
    }
}
