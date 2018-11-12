/*using UnityEngine;
using System.Collections;

public class BrickHandler : MonoBehaviour 
{
	public GameObject selectionBox, redBrickRef, blueBrickRef, yellowBrickRef, greenBrickRef, topSpawnerRef, element5Ref, element6Ref, explosion1Ref, explosion2Ref;
	public bool addAvailable, matchAvalable, moveSuper, selected, canBeSelected, needsDestroy, needsRevert, showVelocity, mouseLock, spawnedExplosion, spawndExplosion, isFirstTile, positionSent = false;
	public float rayDistance2 = 2.0f, rayDistance = 2.0f, destroyCD, destroyDelay, revertTime, revertCount, sPosChangeCount, sPosChangeTime;
	public string myTag, selectionKey, opositeElement, elementChoosen;
	GameHandler gameHandler;
	//Vector3 startPos1;
	public ExplosionHandler objExplosionRef;
	public int matchCount;
	//public Rigidbody objExplosionRef;
	
	
	// Use this for initialization
	void Start () 
	{
		gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();
		matchAvalable = false;
		addAvailable = true;
		
		spawndExplosion = true;
		myTag = tag;
		if(tag == "FireElement")
		{
			opositeElement = "WaterElement";
		}
		if(tag == "WaterElement")
		{
			opositeElement = "FireElement";
		}
		if(tag == "EarthElement")
		{
			opositeElement = "WindElement";
		}
		if(tag == "WindElement")
		{
			opositeElement = "EarthElement";
		}
		if(tag == "LightElement")
		{
			opositeElement = "DarknessElement";
		}
		if(tag == "DarknessElement")
		{
			opositeElement = "LightElement";
		}
		
		
		
		
		
	
		showVelocity = false;
		mouseLock = true;
		elementChoosen = gameHandler.elementChosen;
		isFirstTile = !gameHandler.tilePositionFilled;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//checking if there are any matches on the board
		CheckMatch();//this function does most of the work
		if(gameHandler.availableMatchesCount == 0)
		{
			addAvailable = true;
		}
		
		if(matchAvalable && addAvailable)
		{
			gameHandler.availableMatchesCount++;
			addAvailable = false;
		}
		if(!matchAvalable && !addAvailable)
		{
			gameHandler.availableMatchesCount--;
			addAvailable = true;
		}
		//////////////////////////////////////////////////SuperElement section//////////////////////////////////////////////////
		
		
		if(myTag == "SuperElement")
		{
			if(gameHandler.matchCount == 0)
			{
				//moveSuper = false;
			}
			if(gameHandler.matchCount != 0)
			{
				moveSuper = true;
			}
			if(moveSuper)
			{
				sPosChangeCount += Time.deltaTime;
			}
				
			if(sPosChangeCount >= sPosChangeTime)
			{
				Vector3 positionChosen = gameHandler.GetRandomPosition();
				Vector3 startPosition = transform.position;
				transform.position = positionChosen;
				Collider []swapObject = Physics.OverlapSphere(positionChosen, 0.5f);
				if(swapObject[0])
				{
			    	swapObject[0].transform.position = startPosition;
				}
				//Debug.Log (swapObject[0].tag);
				moveSuper = false;
				sPosChangeCount = 0;
			}
		}
		
		
		///////////////////////////////////////////////adding position to gamehandler///////////////////////////////////////
		if(isFirstTile && GetComponent<Rigidbody>().velocity.y == 0 && !positionSent)
		{
			gameHandler.AddTilePosition(transform.position);
			positionSent = true;
		}
		
		
		
		
		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		if(showVelocity)
		{
			//Debug.Log (GetComponent<Rigidbody>().velocity.y);
			Debug.Log(opositeElement);
		}
		
		////////////////////////////////////////selection ring section/////////////////////////////////////////////////////
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hitInto;
		
		if(gameHandler.mouseEnabled)
		{
			if(collider.Raycast(ray, out hitInto, Mathf.Infinity) && Input.GetButtonDown(selectionKey) && canBeSelected && gameHandler.selectionCount < 2)
			{
				selectionBox.SetActive(true);
				selected = true;			
				if(gameHandler.selectionCount == 0)
				{
			    	gameHandler.selected1Position = transform.position;
					gameHandler.firstSelection = gameObject;
				}
				if(gameHandler.selectionCount == 1)
				{
					gameHandler.selected2Position = transform.position;
					gameHandler.secondSelection = gameObject;
				}
				gameHandler.selectionCount ++;
				
				return;
			}
			if(collider.Raycast(ray, out hitInto, Mathf.Infinity) && Input.GetButtonDown(selectionKey) && selected)
			{
				selectionBox.SetActive(false);
				selected = false;
				gameHandler.selectionCount --;
				
				return;
				
			}
			
		}
		
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		
		///////////////////////////////////////////Spawning section//////////////////////////////////////////////////////
		
		
    	RaycastHit starterHit1, starterHit2, starterHit3, starterHit4;
		
		
		
		if(gameHandler.resetTimer)
		{
			if(Physics.Raycast(transform.position, Vector3.up, out starterHit1, rayDistance) && Physics.Raycast(transform.position, Vector3.down, out starterHit2, rayDistance))
			{
				Debug.DrawLine (transform.position, starterHit1.point);
				Debug.DrawLine (transform.position, starterHit2.point);
				if(starterHit1.collider.tag == tag && starterHit2.collider.tag == tag)
				{
					if(!needsDestroy)
					{
						gameHandler.matchCount++;
					}
					
					if(starterHit1.collider.GetComponent<BrickHandler>())
					{
						if(!starterHit1.collider.GetComponent<BrickHandler>().needsDestroy)
						{
							gameHandler.matchCount++;
						}
					}
					if(starterHit2.collider.GetComponent<BrickHandler>())
					{
						if(!starterHit2.collider.GetComponent<BrickHandler>().needsDestroy)
						{
							gameHandler.matchCount++;
						}
					}
					
					needsDestroy = true;
					needsRevert = false;
					revertCount = 0;
					//gameHandler.score ++;
					//SpawnBrickOnDeadth();
					gameHandler.lastBlockDeleted = tag;
					starterHit1.collider.GetComponent<BrickHandler>().needsDestroy = true;
					starterHit2.collider.GetComponent<BrickHandler>().needsDestroy = true;
					//Destroy(starterHit1.collider.gameObject);
					//Destroy(starterHit2.collider.gameObject);
				    //Destroy(gameObject);
					gameHandler.destroyEnable = false;	
					
						
					
				}
				
				
				
			}
			if(Physics.Raycast(transform.position, Vector3.left, out starterHit3, rayDistance) && Physics.Raycast(transform.position, Vector3.right, out starterHit4, rayDistance))
			{
				Debug.DrawLine (transform.position, starterHit3.point);
				Debug.DrawLine (transform.position, starterHit4.point);
				if(starterHit3.collider.tag == myTag && starterHit4.collider.tag == myTag)
				{
					
					
					needsRevert = false;
					
					//if(Physics.Raycast(testRay,out testHit ,0.7f))
					if(GetComponent<Rigidbody>().velocity.y >= 0)
					{
						if(starterHit3.rigidbody.velocity.y >= 0 && starterHit4.rigidbody.velocity.y >= 0)
						{
							if(!needsDestroy)
							{
								gameHandler.matchCount++;
							}
							if(!starterHit3.collider.GetComponent<BrickHandler>().needsDestroy)
							{
								gameHandler.matchCount++;
							}
							if(!starterHit4.collider.GetComponent<BrickHandler>().needsDestroy)
							{
								gameHandler.matchCount++;
							}
							
							needsDestroy = true;
							needsRevert = false;
							revertCount = 0;
					    	//Debug.DrawLine(transform.position, testHit.point);
							//SpawnBrickOnDeadth();
							starterHit3.collider.GetComponent<BrickHandler>().needsDestroy = true;
							starterHit4.collider.GetComponent<BrickHandler>().needsDestroy = true;
							gameHandler.lastBlockDeleted = tag;
							//Destroy(starterHit3.collider.gameObject);
				     		//Destroy(starterHit4.collider.gameObject);
					    	//Destroy(gameObject);
							//destroyCD = 0;
							gameHandler.destroyEnable = false;
						}
					}
					
				}
			}
		}
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		//////////////////////////////////////////selection section for being able to be selected///////////////////////////
		Ray rayLeft, rayRight, rayUp, rayDown;
		rayLeft = new Ray(transform.position, Vector3.left);
		rayRight = new Ray(transform.position, Vector3.right);
		rayUp = new Ray(transform.position, Vector3.up);
		rayDown = new Ray(transform.position, Vector3.down);
		RaycastHit hitLeft, hitRight, hitUp, hitDown;
		
		if(gameHandler.selectionCount == 1)
		{
			canBeSelected = false;
		}
		
		if(gameHandler.selectionCount == 0)
		{
			canBeSelected = true;
			selected = false;
		}
		float distanceOfRay = 2.0f;
		if(Physics.Raycast(rayLeft, out hitLeft, distanceOfRay) && selected)
		{
			hitLeft.collider.GetComponent<BrickHandler>().canBeSelected = true;
		}
		if(Physics.Raycast(rayRight, out hitRight, distanceOfRay) && selected)
		{
			hitRight.collider.GetComponent<BrickHandler>().canBeSelected = true;
		}
		if(Physics.Raycast(rayUp, out hitUp, distanceOfRay) && selected)
		{
			if(hitUp.collider.tag != "TopSpawner")
			{
		    	hitUp.collider.GetComponent<BrickHandler>().canBeSelected = true;
			}
		}
		if(Physics.Raycast(rayDown, out hitDown, distanceOfRay) && selected)
		{
			if(hitDown.collider.tag != "Ground")
			{
		    	hitDown.collider.GetComponent<BrickHandler>().canBeSelected = true;
			}
			
		}
		//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		////////////////////////////////////////swaping tiles///////////////////////////////////////////////////////////////
		if(gameHandler.selectionCount == 2)
		{
			//startPos1 = transform.position;
			gameHandler.firstSelection.transform.position = gameHandler.secondSelection.transform.position;
			gameHandler.secondSelection.transform.position = gameHandler.selected1Position;
			gameHandler.selectionCount = 0;
			needsRevert = true;
			gameHandler.mouseEnabled = false;
			
		}
		
		if(gameHandler.selectionCount == 0)
		{
			selectionBox.SetActive(false);
			//needsRevert = false;
		}
		//////////////////////////////reverting tile subsection///////////////////////////////////////////////////////////////
		if(needsRevert)
		{
			revertCount += Time.deltaTime;
			if(revertCount >= revertTime)
			{
				//gameHandler.destroyCount = 0;
				gameHandler.mouseEnabled = true;
				needsRevert = false;
				revertCount = 0;
				if(gameHandler.firstSelection != null && gameHandler.secondSelection != null)
				{
			    	gameHandler.firstSelection.transform.position = gameHandler.selected1Position;
					gameHandler.secondSelection.transform.position = gameHandler.selected2Position;
				}
	    		
				
			}
		}
		
		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		///////////////////////////////////////destroying self after a cooldown/////////////////////////////////////////////////
		if(needsDestroy)
		{
			
			//gameHandler.destroyCount += Time.deltaTime;
			destroyDelay += Time.deltaTime;
			if(destroyDelay > 0 && destroyDelay < 0.2)
			{
				matchCount = gameHandler.matchCount;
			}
			
			if(destroyDelay >= destroyCD)
			{
				gameHandler.score ++;
				gameHandler.mouseEnabled = true;
				//SpawnBrickOnDeadth ();
				gameHandler.needsSpawn = true;
				//GameObject clone;
				
				if(matchCount >= 4 && spawndExplosion)
				{
					ExplosionHandler clone = (ExplosionHandler) Instantiate(objExplosionRef, transform.position, transform.rotation);
					
					clone.tag = tag + "Exp";
				}
				
				
				if(elementChoosen == tag)
				{
					gameHandler.barFillLevel ++;
				}
				gameHandler.availableMatchesCount--;
				Destroy(gameObject);
			}
		}
		if(gameHandler.lastBlockDeleted == tag)
		{
			//needsDestroy = true;
		}
	}
	
	
	void OnMouseEnter()
	{
		//selectionBox.SetActive(true);
		
	}
	void OnMouseExit()
	{
		//selectionBox.SetActive(false);
	}
	
	public void SpawnBrickOnDeadth ()
	{
		////////////////////////////////////////////////randomly selecting a cube to spawn///////////////////////////////////////
		int chooseBrick;
		if(myTag == "FireElement")
		{
			chooseBrick = Random.Range(1,4);
			//Debug.Log(chooseBrick);
			if(chooseBrick == 1)
			{
				Instantiate(blueBrickRef, transform.position, transform.rotation);
			}
			if(chooseBrick == 2)
			{
				Instantiate(yellowBrickRef, transform.position, transform.rotation);
			}
			if(chooseBrick == 3)
			{
				Instantiate(greenBrickRef, transform.position, transform.rotation);
			}
		}
		if(myTag == "WaterElement")
		{
			chooseBrick = Random.Range(1,4);
			if(chooseBrick == 1)
			{
				Instantiate(redBrickRef, transform.position, transform.rotation);
			}
			if(chooseBrick == 2)
			{
				Instantiate(yellowBrickRef, transform.position, transform.rotation);
			}
			if(chooseBrick == 3)
			{
				Instantiate(greenBrickRef, transform.position, transform.rotation);
			}
		}
		if(myTag == "WindElement")
		{
			chooseBrick = Random.Range(1,4);
			if(chooseBrick == 1)
			{
				Instantiate(redBrickRef, transform.position, transform.rotation);
			}
			if(chooseBrick == 2)
			{
				Instantiate(blueBrickRef, transform.position, transform.rotation);
			}			
			if(chooseBrick == 3)
			{
				Instantiate(greenBrickRef, transform.position, transform.rotation);
			}
		}
		if(myTag == "EarthElement")
		{
			chooseBrick = Random.Range(1,4);
			if(chooseBrick == 1)
			{
				Instantiate(redBrickRef, transform.position, transform.rotation);
			}
			if(chooseBrick == 2)
			{
				Instantiate(blueBrickRef, transform.position, transform.rotation);
			}
			if(chooseBrick == 3)
			{
				Instantiate(yellowBrickRef, transform.position, transform.rotation);
			}
		}
	}
	
	void OnDestroy ()
	{
		
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == opositeElement + "Exp")
		{
	    	destroyDelay = destroyCD;
			needsDestroy = true;
			spawndExplosion = false;
			Debug.Log ("Destroyed");
		}
		//Debug.Log(other.tag + " " + opositeElement);
	}
	
	
	void CheckMatch()
	{
		float zstart = -1.5f;
		
		Ray rayLeft = new Ray(transform.position, Vector3.left);
		Ray rayRight = new Ray(transform.position, Vector3.right);
		Ray rayUp = new Ray(transform.position, Vector3.up);
		Ray rayDown = new Ray(transform.position, Vector3.down);
		
		
		Ray ray1 = new Ray(transform.position + new Vector3(0,0,zstart), new Vector3(1,-1,1));//lower right
		Ray ray2 = new Ray(transform.position + new Vector3(0,0,zstart), new Vector3(-1,-1,1));//lower left
		Ray ray3 = new Ray(transform.position + new Vector3(0,0,zstart), new Vector3(1,1,1));//upper right
		Ray ray4 = new Ray(transform.position + new Vector3(0,0,zstart), new Vector3(-1,1,1));//upper left
		Ray ray5 = new Ray(transform.position + new Vector3(1.3f,0,zstart), new Vector3(1,0,1));//right
		Ray ray6 = new Ray(transform.position + new Vector3(-1.3f,0,zstart), new Vector3(-1,0,1));//left
		Ray ray7 = new Ray(transform.position + new Vector3(0,1.3f,zstart), new Vector3(0,1,1));//up
		Ray ray8 = new Ray(transform.position + new Vector3(0,-1.3f,zstart), new Vector3(0,-1,1));//down
		
		
		RaycastHit hit1,hit2,hit3,hit4,hit5,hit6,hit7,hit8,hitLeft,hitRight,hitUp,hitDown;
		
		//////////////////////////////////////////////////corners up check/////////////////////////////////////////////////////////
		if(Physics.Raycast(ray3, out hit1, 2) && Physics.Raycast(ray4, out hit2, 2))
		{
			if(hit1.collider.tag == tag && hit2.collider.tag == tag)
			{
				matchAvalable = true;
				return;
				
			}
			else
			{
				matchAvalable = false;
				
			}
		}
		//////////////////////////////////////////////////corners right check/////////////////////////////////////////////////////////
		if(Physics.Raycast(ray1, out hit1, 2) && Physics.Raycast(ray3, out hit2, 2))
		{
			if(hit1.collider.tag == tag && hit2.collider.tag == tag)
			{
				matchAvalable = true;
				return;
				
			}
			else
			{
				matchAvalable = false;
				
			}
		}
		//////////////////////////////////////////////////corners down check/////////////////////////////////////////////////////////
		if(Physics.Raycast(ray1, out hit1, 2) && Physics.Raycast(ray2, out hit2, 2))
		{
			if(hit1.collider.tag == tag && hit2.collider.tag == tag)
			{
				matchAvalable = true;
				return;
				
			}
			else
			{
				matchAvalable = false;
				
			}
		}
		//////////////////////////////////////////////////corners left check/////////////////////////////////////////////////////////
		if(Physics.Raycast(ray2, out hit1, 2) && Physics.Raycast(ray4, out hit2, 2))
		{
			if(hit1.collider.tag == tag && hit2.collider.tag == tag)
			{
				matchAvalable = true;
				return;
				
			}
			else
			{
				matchAvalable = false;
				
			}
		}
		
		
		
	
		//////////////////////////////////////////////////////////left checks////////////////////////////////////////////////////////////////////
		if(Physics.Raycast(rayLeft, out hitLeft, 1))
		{
			if(hitLeft.collider.tag == tag)
			{
				if(Physics.Raycast(ray1, out hit1, 2))
				{
					//Debug.DrawLine(ray1.origin, hit1.point);
					if(hit1.collider.tag == tag)
					{					
						matchAvalable = true;
						return;
					}
					else
					{
						matchAvalable = false;
					}
				}
				if(Physics.Raycast(ray3, out hit2, 2))
				{
					//Debug.DrawLine(ray3.origin, hit2.point);
					if(hit2.collider.tag == tag)
					{
						matchAvalable = true;
						return;
					}
					else
					{
						matchAvalable = false;
					}
				}
				if(Physics.Raycast(ray5, out hit3, 2))
				{
					//Debug.DrawLine(ray5.origin, hit3.point);
					if(hit3.collider.tag == tag)
					{
						matchAvalable = true;
						return;
					}
					else
					{
						matchAvalable = false;
					}
				}
			}
			else
			{
				matchAvalable = false;
			}
		}
		
		
		///////////////////////////////////////////////////////////right checks/////////////////////////////////////////////////////////////
		if(Physics.Raycast(rayRight, out hitRight, 1))
		{
			if(hitRight.collider.tag == tag)
			{
				if(Physics.Raycast(ray2, out hit1, 2))
				{
					//Debug.DrawLine(ray2.origin, hit1.point);
					if(hit1.collider.tag == tag)
					{
						matchAvalable = true;
						return;
					}
					else
					{
						matchAvalable = false;
					}
				}
				if(Physics.Raycast(ray4, out hit2, 2))
				{
					//Debug.DrawLine(ray4.origin, hit2.point);
					if(hit2.collider.tag == tag)
					{
						matchAvalable = true;
						return;
					}
					else
					{
						matchAvalable = false;
					}
				}
				if(Physics.Raycast(ray6, out hit3, 2))
				{
					//Debug.DrawLine(ray6.origin, hit3.point);
					if(hit3.collider.tag == tag)
					{
						matchAvalable = true;
						return;
					}
					else
					{
						matchAvalable = false;
					}
				}
			}
			else
			{
				matchAvalable = false;
			}
		}
		
		/////////////////////////////////////////////////up checks////////////////////////////////////////////////////
		if(Physics.Raycast(rayUp, out hitUp, 1))
		{
			if(hitUp.collider.tag == tag)
			{
				if(Physics.Raycast(ray1, out hit1, 2))
				{
					Debug.DrawLine(ray1.origin, hit1.point);
					if(hit1.collider.tag == tag)
					{
						matchAvalable = true;
						return;
					}
					else
					{
						matchAvalable = false;
					}
				}
				if(Physics.Raycast(ray2, out hit2, 2))
				{
					Debug.DrawLine(ray2.origin, hit2.point);
					if(hit2.collider.tag == tag)
					{
						matchAvalable = true;
						return;
					}
					else
					{
						matchAvalable = false;
					}
				}
				if(Physics.Raycast(ray8, out hit3, 2))
				{
					Debug.DrawLine(ray8.origin, hit3.point);
					if(hit3.collider.tag == tag)
					{
						matchAvalable = true;
						return;
					}
					else
					{
						matchAvalable = false;
					}
				}
			}
			else
			{
				matchAvalable = false;
			}
		}
		
		//////////////////////////////////////////////////////down checks/////////////////////////////////////////////////////////
		if(Physics.Raycast(rayDown, out hitDown, 1))
		{
			if(hitDown.collider.tag == tag)
			{
				if(Physics.Raycast(ray3, out hit1, 2))
				{
					//Debug.DrawLine(ray3.origin, hit1.point);
					if(hit1.collider.tag == tag)
					{
						matchAvalable = true;
						return;
					}
					else
					{
						matchAvalable = false;
					}
				}
				if(Physics.Raycast(ray4, out hit2, 2))
				{
					//Debug.DrawLine(ray4.origin, hit2.point);
					if(hit2.collider.tag == tag)
					{
						matchAvalable = true;
						return;
					}
					else
					{
						matchAvalable = false;
					}
				}
				if(Physics.Raycast(ray7, out hit3, 2))
				{
					//Debug.DrawLine(ray7.origin, hit3.point);
					if(hit3.collider.tag == tag)
					{
						matchAvalable = true;
						return;
					}
					else
					{
						matchAvalable = false;
					}
				}
			}
			else
			{
				matchAvalable = false;
			}
		}
		
	}
	
	
}
 */