#pragma once
#include "engine\gameobject.h"

class Player :public GameObject
{
	
	void Collided(GameObject*);
	void Start();
	void Update(unsigned long frameNumber);
	std::wstring GetType();
	public:

		// Set weather player is grounded 
		bool isGrounded;
		//End of Level
		bool LevelFinished;
		// Set the coin count
		int _CoinCount;
		//int boxcount;
		int hitcount;
		bool HitHole;
		bool IsHidden;
		bool PlayerThrough1;
		bool GhostOut;
		bool DoorUpUsed;

		Player() : GameObject(){}
		Player(
			std::wstring objectName, 
			Main* _Main, 
			bool dynamic, 
			bool physicsBody, 
			Vector2 &_Position, 
			std::wstring _TextureName, 
			bool _IsSprite, 
			int _Rows, 
			int _Columns) : GameObject(objectName, _Main, dynamic, physicsBody, _Position, _TextureName, _IsSprite, _Rows, _Columns) {}

};

