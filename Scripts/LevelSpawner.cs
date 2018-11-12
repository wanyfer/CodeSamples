using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelSpawner : MonoBehaviour 
{
    public ObjectPool objectPoolRef;
	public SaveLoad saveLoadRef;
	public SaveClass saveRef;
    public GameManager gmRef;
    public static LevelSpawner lSpawner;
    public List<Vector3> laneVectorsStraight, laneVectorsSide;
    public GameObject pathStraight, pathSide, coin, pickup;
    public float forwardSpeed, forwardSpeedSnapshot, spawnGap, path1Pos, laneAngle, startPosZ, spawnerDistance;
    public Vector3 spawnPosStraight, spawnPosSide, firstSpawnPos, lastSpawnPos;
    public int pathQuantity, startSteps, laneQuantity, lanes;
    public bool spawnCoin, spawnPcikup, increaseSpeed;
    public int spawnCoinCount, cointoSpawn, coinToSpawnMin, coinToSpawnMax, pathToSpawn, pickupPathToSpawn;
    public float coinSpawnTimer, coinSpawnTime, coinSpawnTimeMin, coinSpawnTimeMax;
    public PlayerHandler playerRef;
   // public List<int> paths;

    public struct Paths
    {
        public int pathNumber;
        public int pathDirection;
        public int laneIn;
        public bool spawnCoins;
    }


    public struct LaneInfo
    {
       public bool hasPath;
    }

    public Paths[] paths;
    public LaneInfo[] currentBlock, previousBlock;

    void Awake()
    {
        lSpawner = this;
    }


	// Use this for initialization
	void Start () 
	{
		saveLoadRef = SaveLoad.saveLoad;
		saveRef = saveLoadRef.savedData;

        startPosZ = transform.position.z;
        gmRef = GameManager.gameManager;
        playerRef = gmRef.playerRef;
        cointoSpawn = Random.Range(coinToSpawnMin, coinToSpawnMax);
        coinSpawnTimer = Random.Range(coinToSpawnMin, coinToSpawnMax);

        forwardSpeed = GameObject.FindObjectOfType<PlayerHandler>().forwardSpeed + 0.2f;
        currentBlock = new LaneInfo[laneQuantity];
        previousBlock = new LaneInfo[laneQuantity];


        lanes = laneQuantity - 1;
        laneAngle = (float) 360 / laneQuantity;
        int pathcount = 0;
        paths = new Paths[10];
        firstSpawnPos = transform.position;
        lastSpawnPos = transform.position;

        for (int i = 1; i<10; i++)
        {
            paths[i].pathNumber = i;
            pathcount++;
            paths[i].laneIn = i;
        }

        for (int i = 0; i < laneQuantity; i++)
        {
            currentBlock[i].hasPath = false;
            previousBlock[i].hasPath = false;
        }

        gmRef.CheckForCoinSpawn();
        //BuildStart();

        

    }
	
	// Update is called once per frame
	void Update () 
	{
        //the forward movement of the spawner
        transform.Translate(-Vector3.forward * forwardSpeed * Time.deltaTime);



        spawnerDistance = startPosZ - transform.position.z;


        //building the path
        if(transform.position.z <= lastSpawnPos.z - spawnGap)
        {
            transform.position = new Vector3(lastSpawnPos.x, lastSpawnPos.y, lastSpawnPos.z - spawnGap);
			if (saveRef.tutorialDone) 
			{
				BuildPath ();
			} else
				BuildTutorialPath ();
			
            lastSpawnPos = transform.position;
        }
        path1Pos = paths[0].laneIn;

        if(playerRef.transform.position.z - transform.position.z < 300 && increaseSpeed == false)
        {
            increaseSpeed = true;
            forwardSpeedSnapshot = forwardSpeed;
        }
        if(increaseSpeed == true)
        {
            forwardSpeed++;
        }
                
        if(playerRef.transform.position.z - transform.position.z > 320 && increaseSpeed == true)
        {
            increaseSpeed = false;
            forwardSpeed = forwardSpeedSnapshot;
        }
        
    }



    void BuildPath()
    {        
        Vector3 currentSpawnPos = transform.position;
         
        
        for (int i = 0; i < laneQuantity; i++)
        {
            previousBlock[i].hasPath = currentBlock[i].hasPath;
            currentBlock[i].hasPath = false;
        }
        int loopBreaker = 0;
        GameObject tempObj;
        bool canMove = false;
        for (int i = 0; i < pathQuantity; i++)
        {
            canMove = false;
            paths[i].pathDirection = GetRandomDirection();
            if (paths[i].pathDirection == 2)
            {

                if (canMove)
                {
                    loopBreaker = 0;
                    tempObj = objectPoolRef.CreateStraightPath(currentSpawnPos);
                    //tempObj = (GameObject)Instantiate(pathStraight, currentSpawnPos, Quaternion.identity);
                    tempObj.transform.Rotate(new Vector3(0, 0, paths[i].laneIn * laneAngle));
                    tempObj.GetComponent<PlateHandler>().gMRef = GameManager.gameManager;
                    tempObj.GetComponent<PlateHandler>().objectPoolRef = ObjectPool.objectPool;
                    //tempObj.tag = "Path" + i;
                    if (spawnCoin && pathToSpawn == i)
                    {
                        tempObj.GetComponent<PlateHandler>().SpawnCoin();
                        spawnCoinCount++;
                    }
                    if(gmRef.spawnPickup && pickupPathToSpawn == i && pathToSpawn != pickupPathToSpawn)
                    {
                        tempObj.GetComponent<PlateHandler>().SpawnPickUp(gmRef.nextPickupToSpawn);
                        gmRef.spawnPickup = false;
                    }
                }
                
            }
            if (paths[i].pathDirection == 3)
            {
                bool pathTobuild = CheckBuildLocation(ref canMove, paths[i].laneIn - 1);

                if (canMove)
                {                    
                    loopBreaker = 0;
                    paths[i].laneIn--;
                    if (paths[i].laneIn < 0)
                    {
                        paths[i].laneIn = lanes;
                    }

                    if (pathTobuild)
                    {
                        tempObj = objectPoolRef.CreateStraightPath(currentSpawnPos);
                        //tempObj = (GameObject)Instantiate(pathStraight, currentSpawnPos, Quaternion.identity);
                    }
                    else
                    {
                        tempObj = objectPoolRef.CreateSidePath(currentSpawnPos);
                        //tempObj = (GameObject)Instantiate(pathSide, currentSpawnPos, Quaternion.identity);
                    }
                    tempObj.GetComponent<PlateHandler>().gMRef = GameManager.gameManager;
                    tempObj.GetComponent<PlateHandler>().objectPoolRef = ObjectPool.objectPool;
                    tempObj.transform.Rotate(new Vector3(0, 0, paths[i].laneIn * laneAngle));
                    //tempObj.tag = "Path" + i;
                    if (spawnCoin && pathToSpawn == i)
                    {
                        tempObj.GetComponent<PlateHandler>().SpawnCoin();
                        spawnCoinCount++;
                    }
                    if (gmRef.spawnPickup && pickupPathToSpawn == i && pathToSpawn != pickupPathToSpawn)
                    {
                        tempObj.GetComponent<PlateHandler>().SpawnPickUp(gmRef.nextPickupToSpawn);
                        gmRef.spawnPickup = false;
                    }
                }


            }
            if (paths[i].pathDirection == 1)
            {
                bool pathTobuild = CheckBuildLocation(ref canMove, paths[i].laneIn + 1);

                if (canMove)
                {
                    loopBreaker = 0;
                    paths[i].laneIn++;
                    if (paths[i].laneIn > lanes)
                    {
                        paths[i].laneIn = 0;
                    }

                    if (pathTobuild)
                    {
                        tempObj = objectPoolRef.CreateStraightPath(currentSpawnPos);
                        //tempObj = (GameObject)Instantiate(pathStraight, currentSpawnPos, Quaternion.identity);
                    }
                    else
                    {
                        tempObj = objectPoolRef.CreateSidePath(currentSpawnPos);
                        //tempObj = (GameObject)Instantiate(pathSide, currentSpawnPos, Quaternion.identity);
                    }
                    tempObj.GetComponent<PlateHandler>().gMRef = GameManager.gameManager;
                    tempObj.GetComponent<PlateHandler>().objectPoolRef = ObjectPool.objectPool;
                    tempObj.transform.Rotate(new Vector3(0, 0, paths[i].laneIn * laneAngle));
                    //tempObj.tag = "Path" + i;
                    if (spawnCoin && pathToSpawn == i)
                    {
                        tempObj.GetComponent<PlateHandler>().SpawnCoin();
                        spawnCoinCount++;
                    }
                    if (gmRef.spawnPickup == true && pickupPathToSpawn == i && pathToSpawn != pickupPathToSpawn)
                    {
                        tempObj.GetComponent<PlateHandler>().SpawnPickUp(gmRef.nextPickupToSpawn);
                        gmRef.spawnPickup = false;
                    }
                }

            }


            if(canMove == false)
            {
                i--;
                loopBreaker++;
            }
            if (loopBreaker > 10)
            {
                i++;
            }
        }
        


        
    }

    int GetRandomDirection()
    {
        return (int)Random.Range(1, 4);
    }


    void BuildStart()
    {
        int coinCount = 0, randomCoin = Random.Range(2,5);

        BuildPath();
        for(int i = 0; i<startSteps; i++)
        {
            coinCount++;
            gmRef.CheckForCoinMultiplier();

            transform.Translate(-Vector3.forward * spawnGap);
            BuildPath();

            if(coinCount >= randomCoin && spawnCoin == false)
            {
                spawnCoin = true;
            }


            if (spawnCoinCount >= cointoSpawn)
            {
                spawnCoin = false;
                pathToSpawn = Random.Range(0, pathQuantity);
                spawnCoinCount = 0;
                coinCount = 0;
            }
        }
        lastSpawnPos = transform.position;
    }

    bool CheckBuildLocation(ref bool canMove, int laneCheck)
    {
        bool returnValue = false;
        if(laneCheck >= laneQuantity)
        {
            laneCheck = 0;
        }
        if (laneCheck < 0)
        {
            laneCheck = laneQuantity - 1;
        }

        if (pathQuantity > 1)
        {              
            if(currentBlock[laneCheck].hasPath == false)
            {
                canMove = true;
                currentBlock[laneCheck].hasPath = true;
                if (previousBlock[laneCheck].hasPath)
                {
                    returnValue = true;                    
                }
            }
               
        }
        else canMove = true;

        return returnValue;
    }

    public void CheckPickupSpawn()
    {

    }


	public void BuildTutorialPath()
	{
		GameObject tempObj;
		Vector3 currentSpawnPos = transform.position;

		for (int i = 0; i < 10; i++) 
		{
			
			paths [i].pathDirection = GetRandomDirection ();



			tempObj = (GameObject)Instantiate (pathStraight, currentSpawnPos, Quaternion.identity);
			tempObj.transform.Rotate (new Vector3 (0, 0, paths [i].laneIn * laneAngle));
			tempObj.GetComponent<PlateHandler> ().gMRef = GameManager.gameManager;
            tempObj.GetComponent<PlateHandler>().objectPoolRef = ObjectPool.objectPool;
            tempObj.tag = "Path" + i;
			if (spawnCoin && pathToSpawn == i) 
			{
				tempObj.GetComponent<PlateHandler> ().SpawnCoin ();
				spawnCoinCount++;
			}
			if (gmRef.spawnPickup && pickupPathToSpawn == i && pathToSpawn != pickupPathToSpawn) 
			{
				tempObj.GetComponent<PlateHandler> ().SpawnPickUp (gmRef.nextPickupToSpawn);
				gmRef.spawnPickup = false;
			}



		}
	}

}
