using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks.Unity.Math;
using JetBrains.Annotations;
using UnityEngine;

public class Terrainrealtime : MonoBehaviour
{
    public Transform diggingArea;
    public Terrain lv2Terrain;
    public int size = 5;
    public float newHeight = 0.5f;
    int xResolution;
    float[,] originalHeights;
    void Start()
    {
        originalHeights = lv2Terrain.terrainData.GetHeights(0, 0, lv2Terrain.terrainData.heightmapResolution,lv2Terrain.terrainData.heightmapResolution);
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Dig();
            Debug.Log(lv2Terrain.terrainData.size.x);
        }
    }   
    void Dig()
    {
        xResolution = lv2Terrain.terrainData.heightmapResolution;
        
        // 將世界坐標轉換為 Terrain 地形上的坐標
        int terX = (int)(((diggingArea.position.x) / lv2Terrain.terrainData.size.x) * xResolution - size/2);
        int terZ = (int)(((diggingArea.position.z)/ lv2Terrain.terrainData.size.z) * xResolution - size/2);
        
        // 創建一個數組來存儲新的高度值
        float[,] heightMap = new float[size, size];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                heightMap[i, j] = newHeight;
            }
        }
        //使用 SetHeights 函數設置 Terrain 高度
        lv2Terrain.terrainData.SetHeights(terX, terZ, heightMap);

    }
        void OnApplicationQuit()
    {
        // 結束時還原 Terrain
        lv2Terrain.terrainData.SetHeights(0, 0, originalHeights);
    }
}
