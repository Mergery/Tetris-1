using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    public Tilemap tilemaps;
    public Piece activePiece{get; private set;} 
   public TetrominoData[] tetraminoes;
   public Vector3Int spawnPosition;

   public Vector2Int boardSize= new Vector2Int( 10, 20);

   public RectInt Bounds
   {
    get
    {
        Vector2Int position = new Vector2Int(-this.boardSize.x/ 2, -this.boardSize.y/2 );
        return new RectInt(position, this.boardSize);

    }
   }

   private void  Awake()
   {
    this.tilemaps= GetComponentInChildren<Tilemap>();
    this.activePiece = GetComponentInChildren<Piece>();


    for(int i=0 ; i<this.tetraminoes.Length; i++)
    {
        this.tetraminoes[i].Initialize();
    }
   }

   private void Start() 
   {
    SpawnPiece();
   }

   public void SpawnPiece()
   {
    int random = Random.Range(0, this.tetraminoes.Length);
    TetrominoData data = this.tetraminoes[random];

    this.activePiece.Initialize(this, spawnPosition, data);
    Set(this.activePiece);
   }

   public void Set(Piece piece )
   {
    for (int i= 0; i< piece.cells.Length; i++)
    {
        Vector3Int tilePosition= piece.cells[i] + piece.position; 
        this.tilemaps.SetTile(tilePosition,piece.data.tile);

    }
   }
   public void Clear(Piece piece )
   {
    for (int i= 0; i< piece.cells.Length; i++)
    {
        Vector3Int tilePosition= piece.cells[i] + piece.position; 
        this.tilemaps.SetTile(tilePosition,null);

    }
   }

   public bool IsValidPosition( Piece piece , Vector3Int position)
   {
    
    RectInt bounds = this.Bounds;

    for ( int i =0; i < piece.cells.Length; i++)
    {
        Vector3Int tilePosition = piece.cells[i] + position;

        if(!bounds.Contains((Vector2Int)tilePosition))
        {
            return false ;
        }

        if(this.tilemaps.HasTile(tilePosition))
        {
            return false;
        }
    }
    return true;

   }

}