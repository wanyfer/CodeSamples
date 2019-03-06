#include "pch.h"
#include "pch.h"
#include "Collectables.h"

void Collectables::Start()
{
	this->IsTrigger(true);
}


void Collectables::Collided(GameObject* _GameObject)
{
	if(_GameObject->GetTag().compare(L"Player") == 0)
	{		
		this->Destroy();
	}
}


void Collectables::Update(unsigned long frameNumber)
{

}

std::wstring Collectables::GetType()
{
	return L"Collectables";
}

