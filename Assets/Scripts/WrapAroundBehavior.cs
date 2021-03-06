﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrapAroundBehavior : MonoBehaviour {

    public bool doGhostsDropMines = true;
    float screenWidth;
	float screenHeight;
	Transform[] ghosts = new Transform[4];
    private bool doGhostsExist;


    void Start () 
	{
		var cam = Camera.main;
 
		var screenBottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, transform.position.z));
		var screenTopRight = cam.ViewportToWorldPoint(new Vector3(1, 1, transform.position.z));
		screenWidth = screenTopRight.x - screenBottomLeft.x;
		screenHeight = screenTopRight.y - screenBottomLeft.y;
   	}
    
	void Update()
	{
        if (doGhostsExist)
        {
            SwapShips();
        }
		
	}

	public void CreateGhostShips()
	{
	    for(int i = 0; i < 4; i++)
	    {
	        ghosts[i] = Instantiate(transform, Vector3.zero, Quaternion.identity, GetComponent <PlayerController>().gameController.gameStageParent) as Transform;
            ghosts[i].GetComponent<PolygonCollider2D>().enabled = false;
	        ghosts[i].GetComponent<PlayerController>().dropsMines = doGhostsDropMines;
            ghosts[i].GetComponent<PlayerController>().playerImage.SetActive(true);
            Destroy(ghosts[i].GetComponent<WrapAroundBehavior>());
	    }
        doGhostsExist = true;
        PositionGhostShips();
    }

    void PositionGhostShips()
	{
	    // All ghost positions will be relative to the ships (this) transform,
	    // so let's star with that.
	    var ghostPosition = transform.position;
	 
	    // We're positioning the ghosts clockwise behind the edges of the screen.
	    // Let's start with the far right.
	    ghostPosition.x = transform.position.x + screenWidth;
	    ghostPosition.y = transform.position.y;
	    ghosts[0].position = ghostPosition;
	 
	    // Bottom
	    ghostPosition.x = transform.position.x;
	    ghostPosition.y = transform.position.y - screenHeight;
	    ghosts[1].position = ghostPosition;
	 
	    // Left
	    ghostPosition.x = transform.position.x - screenWidth;
	    ghostPosition.y = transform.position.y;
	    ghosts[2].position = ghostPosition;

	    // Top
	    ghostPosition.x = transform.position.x;
	    ghostPosition.y = transform.position.y + screenHeight;
	    ghosts[3].position = ghostPosition;
	 
	    // All ghost ships should have the same rotation as the main ship
	    for(int i = 0; i < 4; i++)
	    {
	        ghosts[i].rotation = transform.rotation;
	    }
	}

	void SwapShips()
	{
        for (int i = 0; i < ghosts.Length; i ++)
        {
            Transform ghost = ghosts[i];
            if (ghost.position.x < screenWidth/2 && ghost.position.x > -screenWidth/2 & ghost.position.y < screenHeight/2 && ghost.position.y > -screenHeight/2)
            {
                transform.position = ghost.position;
                PositionGhostShips();
                break;
            }
        }

	}

    public void DestroyAllGhosts()
    {
        foreach(Transform ghost in ghosts)
        {
            Destroy(ghost.gameObject);
        }
        doGhostsExist = false;
    }
}
