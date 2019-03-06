#include "pch.h"
#include "pch.h"
#include "LevelEnd.h"
#include "Player.h"

void LevelEnd::Start()
{
	this->IsTrigger(true);
	EndOfLevel = false;
}


void LevelEnd::Collided(GameObject* _GameObject)
{
	if(_GameObject->GetTag().compare(L"Player") == 0)
	{		
		EndOfLevel = true;
		this->Destroy();
	}
}


void LevelEnd::Update(unsigned long frameNumber)
{

}

std::wstring LevelEnd::GetType()
{
	return L"LevelEnd";
}

