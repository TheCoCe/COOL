using UnityEngine;
using UnityEngine.Tilemaps;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class HeatableTile : Tile
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

    public override void RefreshTile(Vector3Int location, ITilemap tilemap)
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
                        temp.RefreshTile(position, tilemap);
                }
        }
        else if(ticks > ticksToHeatAgain)
        {
            heated = true;
        }
        else
        {
            ticks++;
        }
    }

    private HeatableTile GetHeatableTile(ITilemap tilemap, Vector3Int position)
    {
        TileBase temp = tilemap.GetTile(position);
        if (temp == this)
            return (HeatableTile)temp;
        else
            return null;
    }

#if UNITY_EDITOR
    // The following is a helper that adds a menu item to create a RoadTile Asset
    [MenuItem("Assets/Create/HeatableTile")]
    public static void CreateHeatableTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save Heatable Tile", "New Heatable Tile", "Asset", "Save Heatable Tile", "Assets");
        if (path == "")
            return;
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<HeatableTile>(), path);
    }
#endif
}
