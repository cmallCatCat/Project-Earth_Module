using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Core.Root.Utilities
{
    public class VoronoiMapGenerator : MonoBehaviour
    {
        public Tilemap  tilemap;
        public TileBase grassTile;
        public TileBase desertTile;
        public TileBase forestTile;
    
        public int numberOfFeaturePoints = 10;
        public int mapWidth              = 50;
        public int mapHeight             = 50;

        private List<Vector2Int>                 featurePoints;
        private Dictionary<Vector2Int, TileBase> featurePointTiles;

        void Start()
        {
            GenerateVoronoiMap();
        }

        void GenerateVoronoiMap()
        {
            // Step 1: Generate random feature points
            GenerateFeaturePoints();

            // Step 2: Assign tiles based on the nearest feature point
            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    Vector3Int tilePosition        = new Vector3Int(x, y, 0);
                    Vector2Int nearestFeaturePoint = GetNearestFeaturePoint(new Vector2Int(x, y));
                    TileBase   tileToSet           = featurePointTiles[nearestFeaturePoint];
                    tilemap.SetTile(tilePosition, tileToSet);
                }
            }
        }

        void GenerateFeaturePoints()
        {
            featurePoints     = new List<Vector2Int>();
            featurePointTiles = new Dictionary<Vector2Int, TileBase>();

            for (int i = 0; i < numberOfFeaturePoints; i++)
            {
                Vector2Int randomPoint = new Vector2Int(Random.Range(0, mapWidth), Random.Range(0, mapHeight));
                featurePoints.Add(randomPoint);

                // Assign a random tile type to each feature point
                int tileType = Random.Range(0, 3);
                if (tileType == 0)
                    featurePointTiles[randomPoint] = grassTile;
                else if (tileType == 1)
                    featurePointTiles[randomPoint] = desertTile;
                else
                    featurePointTiles[randomPoint] = forestTile;
            }
        }

        Vector2Int GetNearestFeaturePoint(Vector2Int position)
        {
            float      minDistance  = float.MaxValue;
            Vector2Int nearestPoint = new Vector2Int(-1, -1);

            foreach (Vector2Int point in featurePoints)
            {
                float distance = Vector2.Distance(position, point);
                if (distance < minDistance)
                {
                    minDistance  = distance;
                    nearestPoint = point;
                }
            }

            return nearestPoint;
        }
    }
    
    
}