using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

    #region Data
    public static Level instance;
    [SerializeField]
    private float winTemp, currentTemp, maxTemp;
    private int currentHotTileCount;
    [SerializeField]
    private float hotTileTemperature;
    [SerializeField]
    private float difficulty = 1f;
    [SerializeField]
    private int sizeX, sizeY;
    private GridTile[,] array;
    [SerializeField]
    private GameObject tileContainer;
    #endregion

    #region Properties
    public float IgniteNeighbourTilePercent
    {
        get
        {
            return InverseLerp(0, maxTemp, currentTemp);
        }
    }
    #endregion

    #region Events
    public delegate void WinCondition();
    public static event WinCondition OnGameWon;
    public static event WinCondition OnGameLost;

    void Start () {
        array = new GridTile[sizeX, sizeY];
        foreach(Transform child in tileContainer.transform)
        {
            int childX, childY;
            childX = Mathf.RoundToInt(child.position.x);
            childY = Mathf.RoundToInt(child.position.y);
            if (childX <= sizeX && childY <= sizeY)
            {
                if(array[childX, childY] != null)
                {
                    continue;
                }
                array[childX, childY] = child.GetComponent<GridTile>();
            }
        }
	}

    private void FixedUpdate()
    {
        if(currentTemp < winTemp)
        {
            if(OnGameWon != null)
                OnGameWon.Invoke();
        }
        if(currentTemp > maxTemp)
        {
            if (OnGameLost != null)
                OnGameLost.Invoke();
        }
    }
    #endregion

    #region Methods
    public void IncrementHotTileCount()
    {
        currentHotTileCount++;
        currentTemp = currentHotTileCount * hotTileTemperature * difficulty;
    }

    public void DecrementHotTileCount()
    {
        if(currentHotTileCount > 0)
        {
            currentHotTileCount--;
            currentTemp = currentHotTileCount * hotTileTemperature * difficulty;
        }
    }

    public HeatableTile GetHeatableTile(Vector3Int position)
    {
        if(position.x <= sizeX && position.y <= sizeY)
        {
            if (array[position.x, position.y] != null && array[position.x, position.y] is HeatableTile)
            {
                return array[position.x, position.y] as HeatableTile;
            }
        }
        return null;
    }

    private float InverseLerp(float min, float max, float value)
    {
        if(min != max)
        {
            return Mathf.Clamp01((value - min) / (max - min));
        }
        return 0f;
    }
    #endregion
}
