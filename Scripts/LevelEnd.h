#pragma once
#include "engine\gameobject.h"

class LevelEnd :public GameObject
{
	void Collided(GameObject*);
	void Start();
	void Update(unsigned long frameNumber);
	std::wstring GetType();
	public:
		bool EndOfLevel;
		bool isGrounded;
		LevelEnd() : GameObject(){}
		LevelEnd(
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



