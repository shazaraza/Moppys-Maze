using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
// problem with this script is that when enemy moves up
// it moves up by like 2 sqaures
public class PathFinderTwo : MonoBehaviour
{
    public Tilemap tilemap;
    public GameObject enemy;
    public GameObject dog;
    private float nextAction = 5.0f;
    private float period = 2.0f;
    private float timeDelay = 0;
    void Update()
    {
            if (Time.timeSinceLevelLoad > nextAction)
            {
                nextAction += period;
                direction();
            }



    }
    void direction()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            getVector("up");
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            getVector("down");
        }
        if (dog.transform.localScale.x == 1.5f)
        {
            getVector("right");
        }
        if (dog.transform.localScale.x == -1.5f)
        {
            getVector("left");
        }
    }
    void getVector(string direction)
    {
        if  (direction == "left")
        {
            Movement(Vector2.left * 3);
        }
        if(direction == "right")
        {
            Movement(Vector2.right * 3);
        }
        if (direction == "up") ;
        {
            Movement(Vector2.up * 3);
        }
        Movement(Vector2.up * 3);
    }
    void Movement(Vector2 offset)
    {
        // the offset is what we add to moppy's position
        Vector2 currentpos = enemy.transform.position;
        Vector2 sprite = dog.transform.position;
        Vector2 endpos = sprite+ offset;
        List<int> gCost = new List<int>();
        List<int> hCost = new List<int>();
        List<int> fCost = new List<int>();
        List<Vector2> potentialCells = new List<Vector2>();

        // we need to first check if the current pos is out of bounds
        // if it is, then we don't add it to the potential cell list
        if ((currentpos + Vector2.up).y < (tilemap.cellBounds.max.y - 1))
        {
            potentialCells.Add(currentpos + Vector2.up);
        }
        if ((currentpos+Vector2.down).y > (tilemap.cellBounds.min.y+1))
        {
            potentialCells.Add(currentpos + Vector2.down);
        }
        potentialCells.Add(currentpos + Vector2.left);
        potentialCells.Add(currentpos + Vector2.right);
        // Calculating all of the related costs for the node
        for (int i = 0; i < potentialCells.Count; i++)
        {
            gCost.Add(CalculateGCost(currentpos, potentialCells[i]));
            hCost.Add(CalculateHCost(potentialCells[i], endpos));
            fCost.Add(CalculateFCost(gCost[i], hCost[i]));
        }

        // This goes through potential node fCount and returns smallest
        int node = fCost.IndexOf(fCost.Min());
        enemy.transform.position = potentialCells[node];

    }
    int CalculateGCost(Vector2 enemyPos, Vector2 potentialPos)
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
