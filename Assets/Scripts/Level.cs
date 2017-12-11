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

    public HeatableTile GetHeatableTile(ITilemap tilemap, Vector3Int position)
    {
        TileBase temp = tilemap.GetTile(position);
        if (temp == this)
            return (HeatableTile)temp;
        else
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
