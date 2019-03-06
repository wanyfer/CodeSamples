#include "pch.h"
#include "Player.h"
#include "Enemy.h"

using namespace std;
#include <sstream>

void Enemy::Start()
{
	//SetCollisionScale(0.1f,0.1f);
	_EnemySpeed = 0.03f;
	_AnimationSpeed = 20;
	AddTexture(L"Textures/sprite sheet small 6.png",true,8,4);
	AddTexture(L"Textures/Jump1.png",true,2,2);
	AddTexture(L"Textures/Jump.png",false,1,1);
	
	ToRight = false;
	_Playerhit = false;
	//LevelFinished = false;
	//boxcount = 
	isGrounded = false;
	// Setting thr player tag
	this->SetTag(L"Enemy");
	
	// Scale down the collision
	this->SetCollisionScale(0.5f,0.85f);

	// Set the friction to 0
	SetFriction(0);
	// Stop rotaion with physics
	FixRotation(true);
	SetTexture(L"Textures/sprite sheet small 6.png");

}


void Enemy::Collided(GameObject* _GameObject)
{
	if(_GameObject->GetTag().compare(L"Player") == 0)
	{
		_Playerhit = true;
	}

}


void Enemy::Update(unsigned long frameNumber)
{

	if(this->RayCast(Vector2(this->position.x+0.5f , this->position.y ),180,3.1f,L"Ground") || this->RayCast(Vector2(this->position.x-0.5f , this->position.y ),180,3.1f,L"Ground"))
	{
		isGrounded = true;
	}
	else
	{
		isGrounded = false;
	}
	if(this->isGrounded)
	{
		// Idle State or running state swap the images
		//if(this->GetTextureName().compare(L"Textures/Idle.png") != 0 && this->GetVelocity().x == 0)
		//{
		//	this->SetTexture(L"Textures/Idle.png");
			//DoOnce = 1;
		//}
		//else if(this->GetTextureName().compare(L"Textures/Run.png") != 0 && this->GetVelocity().x != 0)
		//{
		//	this->SetTexture(L"Textures/Run.png");
		//	//DoOnce = 1;
		//}
	}
	if(this->GetVelocity().x > 0.1f && this->isGrounded)
	 {		 		 
		 // Walking forward
		 //we swap the drawscale to swap the image direction around
		 this->SetDrawScale(1,1);
		 this->SetAnimationSpeed(_EnemySpeed);
	 }
	 else if(this->GetVelocity().x < -0.1f && this->isGrounded)
	 {			 
		 // Walking backward
		 //we swap the drawscale to swap the image direction around
		 this->SetDrawScale(-1,1);
		 this->SetAnimationSpeed(_EnemySpeed);
	 }
	if(ToRight == true)
	{
    	MoveRight();
	}
	else
	{
		MoveLeft();
	}

}


std::wstring Enemy::GetType()
{
	return L"Enemy";
}

void Enemy::PatrolRoute(int Direction, int Speed)
{
	int Xtemp = Direction;
	int Ytemp = Speed;

}

void Enemy::MoveRight()
{
	this->SetDrawScale(1,1);
	SetPosition(position.x+_EnemySpeed,position.y);
	if(this->RayCast(Vector2(this->position.x+0.5f , this->position.y ),180,3.1f,L"Ground"))
	{
		/*if(GetVelocity().x < _EnemySpeed)
		{
    		AddForce(5,0,Coordinate::Global);
		}
		if(GetVelocity().x > _EnemySpeed)
		{
			SetVelocity(_EnemySpeed,(GetVelocity().y));
		}
		if(GetVelocity().x < _EnemySpeed)
		{
			SetVelocity(_EnemySpeed,(GetVelocity().y));
		}*/
		//SetPosition(position.x+0.1f,position.y);
	}
	else
	{
		SetVelocity(0,(GetVelocity().y));
		ToRight = false;
	}
	if(this->RayCast(Vector2(this->position.x+0.5f , this->position.y+0.5f),90,0.1f,L"Block") || this->RayCast(Vector2(this->position.x+0.5f , this->position.y-0.5f),90,0.1f,L"Block2"))
	{
		SetVelocity(0,(GetVelocity().y));
		ToRight = false;
	}
	if(!this->RayCast(Vector2(this->position.x, this->position.y ),90,6,L"Block")/* || !this->RayCast(Vector2(this->position.x, this->position.y ),90,6,L"Block")*/)
	{
    	if(this->RayCast(Vector2(this->position.x, this->position.y ),90,5,L"Player"))
    	{
    		_Playerhit = true;
    	}
	}
}

void Enemy::MoveLeft()
{
	this->SetDrawScale(-1,1);
	SetPosition(position.x-_EnemySpeed,position.y);
	if(this->RayCast(Vector2(this->position.x-0.5f , this->position.y ),180,3.1f,L"Ground"))
	{
		/*if(GetVelocity().x > - _EnemySpeed)
		{
    		AddForce(-5,0,Coordinate::Global);
		}
		if(GetVelocity().x < - _EnemySpeed)
		{
			SetVelocity(- _EnemySpeed,(GetVelocity().y));
		}
		if(GetVelocity().x > -2)
		{
			SetVelocity(- _EnemySpeed,(GetVelocity().y));
		}*/
	}
	else
	{
		SetVelocity(0,(GetVelocity().y));
		ToRight = true;
	}
	if(this->RayCast(Vector2(this->position.x-0.5f , this->position.y+0.5f),270,0.1f,L"Block") || this->RayCast(Vector2(this->position.x-0.5f , this->position.y-1),270,0.1f,L"Block2"))
	{
		SetVelocity(0,(GetVelocity().y));
		ToRight = true;
	}
	if(!this->RayCast(Vector2(this->position.x+0.5f , this->position.y ),270,5,L"Block")/* || !this->RayCast(Vector2(this->position.x+0.5f , this->position.y ),270,5,L"Block")*/)
	{
    	if(this->RayCast(Vector2(this->position.x+0.5f , this->position.y ),270,5,L"Player"))
    	{
    		_Playerhit = true;
    	}
	}
}