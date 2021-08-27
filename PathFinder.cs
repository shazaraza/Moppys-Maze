using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class PathFinder : MonoBehaviour
{
    public Tilemap tilemap;
    public GameObject enemy;
    public GameObject dog;
    private float nextAction = 0.0f;
    private float period = 0.7f;
    private float timeDelay = 0;
    // Update is called once per frame
    void OnLevelWasLoaded()
    {
        print("Loaded");
    }
    void FixedUpdate()
    {

            // this is to slow down how often this is called
            if (Time.timeSinceLevelLoad > nextAction)
            {
                nextAction += period;
                A_StarAlgo();
            }
    }

    void A_StarAlgo()
    {
        float maxX = tilemap.size.x;
        float maxY = tilemap.size.y;
        Vector2 currentpos = enemy.transform.position;
        Vector2 endpos = dog.transform.position;
        List<int> gCost = new List<int>();
        List<int> hCost = new List<int>();
        List<int> fCost = new List<int>();
        List<Vector2> potentialCells = new List<Vector2>();
        potentialCells.Add(currentpos + Vector2.up);
        potentialCells.Add(currentpos + Vector2.down);
        potentialCells.Add(currentpos + Vector2.left);
        potentialCells.Add(currentpos + Vector2.right);

        // Calculating all of the related costs for the node
        for(int i =0; i < potentialCells.Count; i++)
        {
            gCost.Add(CalculateGCost(currentpos, potentialCells[i]));
            hCost.Add(CalculateHCost(potentialCells[i], endpos));
            fCost.Add(CalculateFCost(gCost[i],hCost[i]));
        }

        // This goes through potential node fCount and returns largest
        int node = fCost.IndexOf(fCost.Min());
        enemy.transform.position = potentialCells[node];
        
    }

    int CalculateGCost(Vector2 enemyPos,Vector2 potentialPos)
    {
        float xDiff = Math.Abs(enemyPos.x - potentialPos.x);
        float yDiff = Math.Abs(enemyPos.y - potentialPos.y);
         
        if (xDiff > 0)
        {
            
            return (int)xDiff;
               
        }
        
        return (int)yDiff;

    }
    int CalculateHCost(Vector2 enemyPos, Vector2 moppyPos)
    {
        float yDiff = Math.Abs(enemyPos.y - moppyPos.y);
        float xDiff = Math.Abs(enemyPos.x - moppyPos.x);
        return (int)(xDiff + yDiff);
    }
    
    int CalculateFCost(int gCost, int hCost) { return gCost + hCost; }

}
