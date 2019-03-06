#pragma once
#include "engine\gameobject.h"

class Enemy :public GameObject
{
	
	void Collided(GameObject*);
	void Start();
	void Update(unsigned long frameNumber);
	void PatrolRoute(int Direction, int Speed);
	void MoveRight();
	void MoveLeft();
	std::wstring GetType();
	public:

		// Set weather player is grounded 
		bool isGrounded;
		//End of Level
		bool LevelFinished;
		// Set the coin count
		bool _Playerhit;
		//int boxcount;
		bool ToRight;
		float _EnemySpeed;
		int _AnimationSpeed;

		Enemy() : GameObject(){}
		Enemy(
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


