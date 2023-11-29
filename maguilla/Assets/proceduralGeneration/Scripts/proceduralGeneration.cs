using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private int _width, _height;
    //[SerializeField] private GameObject _dirt, _grass;
    [SerializeField] private int _repeatNum;
    [SerializeField] private int _posX;
    [SerializeField] private int _posY;
    [SerializeField] private Tilemap _dirtTileMap, _grassTileMap;
    [SerializeField] Tile _dirt, _grass;
    [Range(0, 100)] [SerializeField] private float _heightValue, _smoothness;
    [SerializeField] private float _seed;

    private void Awake()
    {
        Generation();
    }

    private void Generation()
    {
        int repeatValue = 0;
        for (int x = 0; x < _width; x++)
        {
            if (repeatValue == 0)
            {
                _seed = Random.Range(-1000000, 1000000);
                _height = Mathf.RoundToInt(_heightValue * Mathf.PerlinNoise(x/_smoothness, _seed));
                GenerateFlatPlatform(x);
                repeatValue = _repeatNum;
            }
            else
            {
                GenerateFlatPlatform(x);
                repeatValue--;
            }


        }
    }

    private void GenerateFlatPlatform(int x)
    {
        for (int y = 0; y < _height; y++)
        {
            //spawnObj(_dirt, x, y);
            _dirtTileMap.SetTile(new Vector3Int(x + _posX, y + _posY), _dirt);
        }
        //spawnObj(_grass, x, _height);
        _grassTileMap.SetTile(new Vector3Int(x + _posX, _height + _posY), _grass);
    }
}
