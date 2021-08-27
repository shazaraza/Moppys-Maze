using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// this script is still acting a bit funny
public class PathFinderThree : MonoBehaviour
{
    public Tilemap tilemap;
    public GameObject enemy;
    public GameObject firstEnemy;
    public GameObject dog;
    private float nextAction = 0.0f;
    private float period = 0.5f;

    void Start()
    {
        Debug.Log(tilemap.cellBounds.max);
        Debug.Log(tilemap.cellBounds.min);

    }
    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextAction)
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
        if (direction == "left")
        {
            Movement(Vector2.left * 2);
        }
        if (direction == "right")
        {
            Movement(Vector2.right * 2);
        }
        if (direction == "up") ;
        {
            Movement(Vector2.up * 2);
        }
        Movement(Vector2.up * 2);
    }
    void Movement(Vector2 offset)
    {
        // the offset is what we add to moppy's position
        Vector2 currentpos = enemy.transform.position;
        Vector2 sprite = dog.transform.position;
        Vector2 firstEnemyPos = new Vector2(firstEnemy.transform.position.x, firstEnemy.transform.position.y);
        Vector2 twoTilesAhead = sprite + offset; //two tiles ahead of pacman in his direction
        Vector2 spriteTwoAhead = (twoTilesAhead - firstEnemyPos); // this is the distance between the first enemy and 2 tiles ahead of pacman
        Vector2 endpos = spriteTwoAhead;
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
        if ((currentpos + Vector2.down).y > (tilemap.cellBounds.min.y + 1))
        {
            potentialCells.Add(currentpos + Vector2.down);
        }
        if ((currentpos + Vector2.right).x < (tilemap.cellBounds.max.x - 1))
        {
            potentialCells.Add(currentpos + Vector2.right);
        }
        if ((currentpos + Vector2.left).x < (tilemap.cellBounds.min.x + 1))
        {
            potentialCells.Add(currentpos + Vector2.left);
        }


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
