using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class MapManager : MonoBehaviour
{
    [Header("Point Settings")]
    [SerializeField] private GameObject pointPrefab;
    [SerializeField] private List<Transform> pointPositions;
    [SerializeField] private int minPoints = 3;
    [SerializeField] private int maxPoints = 5;

    [Header("References")]
    [SerializeField] private EnterPointButton enterPointButton;

    private List<MapPoint> spawnedPoints = new List<MapPoint>();
    private MapPoint selectedPoint;
    private GameDateSystem dateSystem;

    private void Start()
    {
        dateSystem = FindObjectOfType<GameDateSystem>();
        if (dateSystem != null)
        {
            dateSystem.OnDateChanged += OnDateChanged;
        }
        else
        {
            Debug.LogWarning("GameDateSystem not found!");
        }

        SpawnRandomPoints();
    }

    private void OnDestroy()
    {
        if (dateSystem != null)
        {
            dateSystem.OnDateChanged -= OnDateChanged;
        }
    }

    private void OnDateChanged(DateTime newDate)
    {
        RespawnPoints();
    }

    public void RespawnPoints()
    {
        foreach (var point in spawnedPoints)
        {
            if (point != null && point.gameObject != null)
            {
                Destroy(point.gameObject);
            }
        }
        spawnedPoints.Clear();

        SpawnRandomPoints();

        selectedPoint = null;
        if (enterPointButton != null)
        {
            enterPointButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
        }
    }

    private void SpawnRandomPoints()
    {
        if (pointPrefab == null || pointPositions == null || pointPositions.Count == 0)
        {
            Debug.LogError("Point prefab or positions not set!");
            return;
        }

        int pointCount = UnityEngine.Random.Range(minPoints, maxPoints + 1);
        pointCount = Mathf.Clamp(pointCount, 1, pointPositions.Count);

        List<int> availableIndices = GetShuffledIndices();

        for (int i = 0; i < pointCount && i < availableIndices.Count; i++)
        {
            int index = availableIndices[i];
            Transform position = pointPositions[index];

            GameObject pointObj = Instantiate(pointPrefab, position.position, Quaternion.identity, position);
            MapPoint mapPoint = pointObj.GetComponent<MapPoint>();

            if (mapPoint != null)
            {
                mapPoint.Initialize(this);
                spawnedPoints.Add(mapPoint);
            }
        }
    }

    private List<int> GetShuffledIndices()
    {
        List<int> indices = new List<int>();
        for (int i = 0; i < pointPositions.Count; i++)
        {
            indices.Add(i);
        }

        for (int i = indices.Count - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            int temp = indices[i];
            indices[i] = indices[j];
            indices[j] = temp;
        }

        return indices;
    }

    public void SelectPoint(MapPoint selected)
    {
        if (selected == null) return;

        foreach (var point in spawnedPoints)
        {
            if (point != null)
            {
                if (point == selected)
                {
                    point.SetActiveVisual();
                }
                else
                {
                    point.SetInactiveVisual();
                }
            }
        }

        selectedPoint = selected;

        if (enterPointButton)
        {
            enterPointButton.GetComponent<Button>().interactable = true;
        }
    }
}