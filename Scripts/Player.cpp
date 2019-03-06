#include "pch.h"
#include "Player.h"
//#include "UserMain.h"

void Player::Start()
{
	//LevelFinished = false;
	DoorUpUsed = false;
	GhostOut = false;
	PlayerThrough1 = false;
	HitHole = false; 
	hitcount = 0;
	isGrounded = false;
	// Setting thr player tag
	this->SetTag(L"Player");
	
	// Scale down the collision
	this->SetCollisionScale(0.5f,0.85f);

	// Set the friction to 0
	SetFriction(0);
	// Stop rotaion with physics
	FixRotation(true);

	// set coin count to 0
	_CoinCount = 0;
}


void Player::Collided(GameObject* _GameObject)
{
	if(_GameObject->GetTag().compare(L"Collectables") == 0)
	{
		_CoinCount++;
	}
	if(_GameObject->GetTag().compare(L"Enemy") == 0)
	{
		hitcount++;
	}

	if(_GameObject->GetTag().compare(L"hide") == 0)
	{
		SetTag(L"BOB");
		SetName(L"BOB");
	}
	else
	{
		SetTag(L"Player");
		SetName(L"Player");
	}
	
}


void Player::Update(unsigned long frameNumber)
{

	if(this->RayCast(Vector2(this->position.x+0.5f , this->position.y ),180,1.1f,L"Ground")||this->RayCast(Vector2(this->position.x-0.5f , this->position.y ),180,1.1f,L"Ground"))
	{
		isGrounded = true;
		HitHole = false;
	}
	else
	{
		isGrounded = false;
	}
	if(this->RayCast(Vector2(this->position.x, this->position.y),180,1.1f,L"FallHole"))
	{
		HitHole = true;
		isGrounded = true;
		
	}
	if(this->RayCast(Vector2(this->position.x+1.5f, this->position.y ),270,1.1f,L"Hide") || this->RayCast(Vector2(this->position.x-1.5f, this->position.y ),90,1.1f,L"Hide"))
	{
		IsHidden = true;
	}
	else
	{
		IsHidden = false;
	}
	if(this->RayCast(Vector2(this->position.x, this->position.y),270,1.1f,L"Trigger") || this->RayCast(Vector2(this->position.x, this->position.y ),90,1.1f,L"Trigger"))
	{
		GhostOut = true;
	}
	if(this->RayCast(Vector2(this->position.x+1.5f, this->position.y),270,1.1f,L"DoorUp1") || this->RayCast(Vector2(this->position.x-1.5f, this->position.y ),90,1.1f,L"DoorUp1"))
	{
    	DoorUpUsed = true;
	}
	



}


std::wstring Player::GetType()
{
	return L"Player";
}
