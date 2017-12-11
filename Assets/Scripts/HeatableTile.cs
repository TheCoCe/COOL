using UnityEngine;
using UnityEngine.Tilemaps;


public class HeatableTile : GridTile
{
    [SerializeField]
    private float inflammability;
    [SerializeField]
    private uint ticksToHeatAgain;

    private bool heated;
    private uint ticks;

    public float Inflammability
    {
        get
        {
            return inflammability;
        }
    }

    public void Update()
    {
        //If Tile is heated it heats surrounding Tiles
        if (heated)
        {
            for (int x = -1; x <= 1; x++)
                for (int y = -1; y <= 1; y++)
                {
                    Vector3Int position = new Vector3Int(location.x + x, location.y + y, location.z);
                    HeatableTile temp = GetHeatableTile(tilemap, location);
                    if (temp != null && !temp.heated && temp.Inflammability >= Random.value)
                        temp.HeatUp(position, tilemap);
                }
        }
        else
        {
            ticks++;
        }
    }

    public void HeatUp()
    {
        if (ticks > ticksToHeatAgain)
        {
            heated = true;
        }
    }
}
