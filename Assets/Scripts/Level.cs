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
    private HeatableTile[,] array;
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

    private void Awake()
    {
        instance = this;
        currentHotTileCount = 0;
    }

    void Start () {
        array = new HeatableTile[sizeX, sizeY];
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
                HeatableTile temp = child.GetComponent<HeatableTile>();
                if (temp != null)
                    array[childX, childY] = temp;
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
            if (array[position.x, position.y] != null)
            {
                return array[position.x, position.y];
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
