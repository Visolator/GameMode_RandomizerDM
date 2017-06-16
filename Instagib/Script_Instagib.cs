//instagib's goremod: people who genuinely know what the fuck they're doing can turn it into a server-wide goremod, too. which is nice.

datablock AudioProfile(bloodBlastSound)
{
	filename    = "./kablooey.wav";
	description = AudioClose3d;
	preload = true;
};

//particle fx
//////////////////////////////////////////
datablock ParticleData(bloodStreakParticle)
{
	dragCoefficient      = 1;
	gravityCoefficient   = 0;
	inheritedVelFactor   = 0.2;
	constantAcceleration = 0.0;
	lifetimeMS           = 400;
	lifetimeVarianceMS   = 100;
	textureName          = "base/data/particles/cloud";
	spinSpeed			= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	colors[0]			= "0.3 0.00 0.00 0.08";
	colors[1]			= "0.3 0.00 0.00 0.2";
	colors[2]			= "0.3 0.00 0.00 0.01";
	colors[3]			= "0.3 0.00 0.00 0";
	sizes[0]			= 0.85;
	sizes[1]			= 0.95;
	sizes[2]			= 0.65;
	sizes[3]			= 0.0;

	useInvAlpha = true;
};

datablock ParticleEmitterData(bloodStreakEmitter)
{
	lifeTimeMS			= 1300;

	ejectionPeriodMS	= 12;
	periodVarianceMS	= 0;
	ejectionVelocity	= 0;
	velocityVariance	= 0.0;
	ejectionOffset		= 0.1;
	thetaMin			= 0;
	thetaMax			= 180;
	phiReferenceVel		= 0;
	phiVariance			= 360;
	overrideAdvance		= false;
	particles			= "bloodStreakParticle";
};

datablock DebrisData(gib1Debris)
{
   emitters = "bloodStreakEmitter";

	shapeFile = "./GIBLET1.dts";
	lifetime = 6.0;
	spinSpeed			= 1200.0;
	minSpinSpeed = -3600.0;
	maxSpinSpeed = 3600.0;
	elasticity = 0.5;
	friction = 0.2;
	numBounces = 4;
	staticOnMaxBounce = true;
	snapOnMaxBounce = false;
	fade = true;

	gravModifier = 2;
};

datablock DebrisData(gib2Debris)
{
   emitters = "bloodStreakEmitter";

	shapeFile = "./GIBLET2.dts";
	lifetime = 6.8;
	spinSpeed			= 1200.0;
	minSpinSpeed = -3600.0;
	maxSpinSpeed = 3600.0;
	elasticity = 0.5;
	friction = 0.2;
	numBounces = 3;
	staticOnMaxBounce = true;
	snapOnMaxBounce = false;
	fade = true;

	gravModifier = 4;
};

datablock DebrisData(gib3Debris)
{
   emitters = "bloodStreakEmitter";

	shapeFile = "./NEWGIB5.dts";
	lifetime = 6.0;
	spinSpeed			= 200.0;
	minSpinSpeed = -300.0;
	maxSpinSpeed = 300.0;
	elasticity = 0.5;
	friction = 0.2;
	numBounces = 3;
	staticOnMaxBounce = true;
	snapOnMaxBounce = false;
	fade = true;

	gravModifier = 3;
};

datablock DebrisData(gib4Debris)
{
   emitters = "bloodStreakEmitter";

	shapeFile = "./NEWGIB3.dts";
	lifetime = 6.0;
	spinSpeed			= 200.0;
	minSpinSpeed = -300.0;
	maxSpinSpeed = 300.0;
	elasticity = 0.5;
	friction = 0.2;
	numBounces = 4;
	staticOnMaxBounce = true;
	snapOnMaxBounce = false;
	fade = true;

	gravModifier = 2;
};

datablock DebrisData(gib5Debris)
{
	shapeFile = "./NEWGIB2.dts";
	lifetime = 6.9;
	spinSpeed			= 100.0;
	minSpinSpeed = -600.0;
	maxSpinSpeed = 600.0;
	elasticity = 0.2;
	friction = 0.1;
	numBounces = 3;
	staticOnMaxBounce = true;
	snapOnMaxBounce = false;
	fade = true;

	gravModifier = 3;
};

datablock DebrisData(gib6Debris)
{
   emitters = "bloodStreakEmitter";

	shapeFile = "./NEWGIB4.dts";
	lifetime = 6.0;
	spinSpeed			= 200.0;
	minSpinSpeed = -300.0;
	maxSpinSpeed = 300.0;
	elasticity = 0.5;
	friction = 0.2;
	numBounces = 3;
	staticOnMaxBounce = true;
	snapOnMaxBounce = false;
	fade = true;

	gravModifier = 3;
};

datablock ParticleData(bloodExplosionParticle)
{
	dragCoefficient      = 1;
	gravityCoefficient   = 0.4;
	inheritedVelFactor   = 0.2;
	constantAcceleration = 0.0;
	lifetimeMS           = 2700;
	lifetimeVarianceMS   = 100;
	textureName          = "base/data/particles/cloud";
	spinSpeed			= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	colors[0]			= "0.3 0.00 0.00 0.3";
	colors[1]			= "0.3 0.00 0.00 0.0";
	sizes[0]			= 6.25;
	sizes[1]			= 8.25;

	useInvAlpha = true;
};

datablock ParticleEmitterData(bloodExplosionEmitter)
{
	ejectionPeriodMS	= 1;
	periodVarianceMS	= 0;
	ejectionVelocity	= 4;
	velocityVariance	= 1.0;
	ejectionOffset  	= 0.0;
	thetaMin			= 89;
	thetaMax			= 90;
	phiReferenceVel		= 0;
	phiVariance			= 360;
	overrideAdvance		= false;
	particles			= "bloodExplosionParticle";
};

datablock ParticleData(bloodChunksParticle)
{
	dragCoefficient			= 0;
	gravityCoefficient		= 3;
	inheritedVelFactor		= 0.2;
	constantAcceleration	= 0.0;
	lifetimeMS				= 2900;
	lifetimeVarianceMS		= 100;
	textureName				= "base/data/particles/chunk";
	spinSpeed				= 190.0;
	spinRandomMin			= -290.0;
	spinRandomMax			= 290.0;
	colors[0]				= "0.15 0.1 0.1 0.9";
	colors[1]				= "0.15 0.1 0.1 0.8";
	sizes[0]				= 0.7;
	sizes[1]				= 0.6;

	useInvAlpha				= false;
};

datablock ParticleEmitterData(bloodChunksEmitter)
{
	lifeTimeMS			= 100;
	ejectionPeriodMS	= 1;
	periodVarianceMS	= 0;
	ejectionVelocity	= 17;
	velocityVariance	= 16.0;
	ejectionOffset		= 1.0;
	thetaMin			= 0;
	thetaMax			= 180;
	phiReferenceVel		= 0;
	phiVariance			= 360;
	overrideAdvance		= false;
	particles			= "bloodChunksParticle";
};

datablock ParticleData(bloodSprayParticle)
{
	dragCoefficient			= 2;
	gravityCoefficient		= 2;
	inheritedVelFactor		= 0.2;
	constantAcceleration	= 0.0;
	lifetimeMS				= 5840;
	lifetimeVarianceMS		= 200;
	textureName				= "base/data/particles/dot";
	spinSpeed				= 0.0;
	spinRandomMin			= 0.0;
	spinRandomMax			= 0.0;
	colors[0]				= "0.27 0.0 0.0 1";
	colors[1]				= "0.27 0.0 0.0 1";
	colors[2]				= "0.27 0.0 0.0 0";
	sizes[0]				= 0.2;
	sizes[1]				= 0.2;
	sizes[2]				= 0.2;
	useInvAlpha				= false;
};

datablock ParticleEmitterData(bloodSprayEmitter)
{
	lifeTimeMS			= 100;
	ejectionPeriodMS	= 1;
	periodVarianceMS	= 0;
	ejectionVelocity	= 18;
	velocityVariance	= 7.0;
	ejectionOffset		= 1.0;
	thetaMin			= 0;
	thetaMax			= 180;
	phiReferenceVel		= 0;
	phiVariance			= 360;
	overrideAdvance		= false;
	particles			= "bloodSprayParticle";
};

//explosion
//////////////////////////////////////////

datablock ExplosionData(bloodDebris1Explosion)
{
   particleEmitter = bloodChunksEmitter;
   particleDensity = 30;
   particleRadius = 0.2;

   debris = gib2Debris;
   debrisNum = 8;
   debrisNumVariance = 6;
   debrisPhiMin = 0;
   debrisPhiMax = 360;
   debrisThetaMin = 0;
   debrisThetaMax = 180;
   debrisVelocity = 8;
   debrisVelocityVariance = 6;
};

datablock ExplosionData(bloodDebris2Explosion)
{
   debris = gib3Debris;
   debrisNum = 6;
   debrisNumVariance = 6;
   debrisPhiMin = 0;
   debrisPhiMax = 360;
   debrisThetaMin = 0;
   debrisThetaMax = 180;
   debrisVelocity = 8;
   debrisVelocityVariance = 6;
};
datablock ExplosionData(bloodDebris3Explosion)
{
   debris = gib4Debris;
   debrisNum = 9;
   debrisNumVariance = 7;
   debrisPhiMin = 0;
   debrisPhiMax = 360;
   debrisThetaMin = 0;
   debrisThetaMax = 180;
   debrisVelocity = 8;
   debrisVelocityVariance = 6;
};

datablock ExplosionData(bloodDebris4Explosion)
{
   debris = gib5Debris;
   debrisNum = 1;
   debrisNumVariance = 0;
   debrisPhiMin = 0;
   debrisPhiMax = 360;
   debrisThetaMin = 0;
   debrisThetaMax = 180;
   debrisVelocity = 12;
   debrisVelocityVariance = 8;
};

datablock ExplosionData(bloodDebris5Explosion)
{
   debris = gib6Debris;
   debrisNum = 16;
   debrisNumVariance = 9;
   debrisPhiMin = 0;
   debrisPhiMax = 360;
   debrisThetaMin = 0;
   debrisThetaMax = 180;
   debrisVelocity = 8;
   debrisVelocityVariance = 6;
};

datablock ExplosionData(goremodExplosion)
{
	soundProfile		= bloodBlastSound;

   explosionShape = "";

   lifeTimeMS = 150;

   debris = gib1Debris;
   debrisNum = 16;
   debrisNumVariance = 16;
   debrisPhiMin = 0;
   debrisPhiMax = 360;
   debrisThetaMin = 0;
   debrisThetaMax = 180;
   debrisVelocity = 8;
   debrisVelocityVariance = 6;

   particleEmitter = bloodExplosionEmitter;
   particleDensity = 10;
   particleRadius = 0.2;

   emitter[0] = bloodSprayEmitter;

   subExplosion[0] = bloodDebris1Explosion;
   subExplosion[1] = bloodDebris2Explosion;
   subExplosion[2] = bloodDebris3Explosion;
   subExplosion[3] = bloodDebris4Explosion;
   subExplosion[4] = bloodDebris5Explosion;

   faceViewer     = true;
   explosionScale = "1 1 1";

   shakeCamera = true;
   camShakeFreq = "10.0 11.0 10.0";
   camShakeAmp = "3.0 10.0 3.0";
   camShakeDuration = 0.1;
   camShakeRadius = 20.0;
};

datablock ProjectileData(goreModProjectile)
{
	uiname							= "";

	lifetime						= 10;
	fadeDelay						= 10;
	explodeondeath						= true;
	explosion						= goremodExplosion;

};

package Randomizer_GoreDeath
{
	function Armor::onDisabled(%this, %obj, %state)
	{
		if($Pref::Server::RDM_RandomInstagib > 0)
			%randomize = getRandom(1, $Pref::Server::RDM_RandomInstagib);
		
		if(%obj.RDMData["Instagib"] || %obj.isInstagib || %randomize == 1)
		{
			%obj.spawnExplosion("goreModProjectile", "1 1 1"); //not sure of arguments :V
			%obj.schedule(50, "delete");
		}

		return Parent::onDisabled(%this, %obj, %state);
	}
};
activatePackage("Randomizer_GoreDeath");
