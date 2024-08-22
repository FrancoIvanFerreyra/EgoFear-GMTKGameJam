using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileBreak : MonoBehaviour
{
    public Tilemap tilemap;
    public Ego ego;
    AudioSource source;
    public AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 hitPosition = Vector3.zero;
        if (tilemap != null)
        {
            if (collision.gameObject.name == "Breakables")
            {
                //If ego's inner collider touches breakable tiles...
                foreach (ContactPoint2D hit in collision.contacts)
                {
                    //Get the touch position
                    hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
                    hitPosition.y = hit.point.y - 0.01f * hit.normal.y;
                    
                    //Get the tile
                    TileBase tileBase = tilemap.GetTile(tilemap.WorldToCell(hitPosition));
                    TileData data = new();
                    if(tileBase != null)
                    {
                        //Get tile data
                        tileBase.GetTileData(tilemap.WorldToCell(hitPosition), tilemap, ref data);
                        //If is a breakable one...
                        if (data.sprite.name.Contains("breakme"))
                        {
                            //Break the tile
                            tilemap.SetTile(tilemap.WorldToCell(hitPosition), null);
                            //Breaking sound
                            BreakCrate();
                        }
                    }
                }
            }

        }
    }

    void BreakCrate()
    {
        source.clip = clip;
        source.Play();
    }
}
