using UnityEngine;
using UnityEngine.Tilemaps;


public class HeatableTile : GridTile
{
    [SerializeField]
    private uint ticksToHeatAgain;
    [SerializeField]
    private bool heated;
    private uint ticks;
    [SerializeField]
    private Sprite[] sprites;
    private SpriteRenderer sr;

    private void Awake()
    {
        sr = this.GetComponent<SpriteRenderer>();
        if (heated)
        {
            Level.instance.IncrementHotTileCount();
            sr.sprite = sprites[2];
        }
        else
            sr.sprite = sprites[0];
    }

    public void Update()
    {
        //If Tile is heated it heats surrounding Tiles
        if (heated)
        {
            for (int x = -1; x <= 1; x++)
                for (int y = -1; y <= 1; y++)
                {
                    Vector3Int position = new Vector3Int(Mathf.RoundToInt(transform.position.x) + x,
                        Mathf.RoundToInt(transform.position.y + y), 0);
                    HeatableTile temp = Level.instance.GetHeatableTile(position);
                    if (temp != null && !temp.heated && Level.instance.IgniteNeighbourTilePercent >= Random.value)
                        temp.HeatUp();
                }
        }
        else if(ticks > ticksToHeatAgain)
        {
            sr.sprite = sprites[0];
        }
        else
        {
            ticks++;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 9 && heated)
        {
            CoolDown();
        }
    }

    public void HeatUp()
    {
        if (ticks > ticksToHeatAgain)
        {
            Level.instance.IncrementHotTileCount();
            heated = true;
            sr.sprite = sprites[2];
        }
    }

    public void CoolDown()
    {
        Level.instance.DecrementHotTileCount();
        heated = false;
        ticks = 0;
        sr.sprite = sprites[1];
    }
}
