using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DeloyInit : MonoBehaviour
{
    private Tilemap tilemap; // 타일맵
    public GameObject spritePrefab; // 배치할 스프라이트 프리팹
    public GameObject deloy_obj; // 클론이 생성 될 빈 개체
    public List<GameObject> highlight = new List<GameObject>();

    private void OnEnable()
    {
        tilemap = GetComponent<Tilemap>();
        PlaceSpritesInTilemap();
    }

    private void PlaceSpritesInTilemap()
    {
        // 타일맵의 셀 크기 가져오기
        Vector3 cellSize = tilemap.cellSize;

        // 타일맵의 모든 셀 반복
        BoundsInt bounds = tilemap.cellBounds;
        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int cellPos = new Vector3Int(x, y, 0);

                // 현재 셀에 타일이 있는지 확인
                if (tilemap.HasTile(cellPos))
                {
                    // 현재 셀의 월드 좌표 계산
                    Vector3 cellWorldPos = tilemap.GetCellCenterWorld(cellPos);

                    // 스프라이트를 셀의 중심에 배치
                    GameObject sprite = Instantiate(spritePrefab, cellWorldPos, Quaternion.identity);

                    // 배치된 스프라이트의 크기를 셀의 크기에 맞게 조정
                    sprite.transform.localScale = new Vector3(cellSize.x, cellSize.y, 1f);

                    sprite.transform.SetParent(deloy_obj.transform);
                    highlight.Add(sprite);
                }
            }
        }
    }
}
