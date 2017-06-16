//+=========================================================================================================+\\
//|			Made by..																						|\\
//|		   ____   ____  _				 __	  		  _		   												|\\
//|		  |_  _| |_  _|(_)	      		[  |		/ |_		 											|\\
//| 		\ \   / /  __   .--.   .--.  | |  ,--. `| |-' .--.   _ .--.  									|\\
//| 		 \ \ / /  [  | ( (`\]/ .'`\ \| | `'_\ : | | / .'`\ \[ `/'`\] 									|\\
//| 		  \ ' /    | |  `'.'.| \__. || | // | |,| |,| \__. | | |     									|\\
//|    		   \_/    [___][\__) )'.__.'[___]\'-;__/\__/ '.__.' [___]    									|\\
//|								BL_ID: 20490 | BL_ID: 48980													|\\
//|				Forum Profile(48980): http://forum.blockland.us/index.php?action=profile;u=144888;			|\\
//|																											|\\
//+=========================================================================================================+\\

//This is stupid do this since ForceRequiredAddOn WILL disconnect you if you are hosting non-dedicated for a missing/disabled add-on if using a non-custom gamemode
//This is the most right way to exec required add-ons

//Please alert me if there is a better way to do this
$doReturn = 0;
if($GameModeArg $= "Add-Ons/GameMode_Custom/gamemode.txt" || $GameModeArg $= "")
{
	$error = ForceRequiredAddOn("Support_NewHealth");
	if($error == $Error::AddOn_NotFound)
	{
		warn("ERROR: GameMode_RandomizerDM - required add-on Support_NewHealth not found");
		return;
	}

	//--------------------------------------

	$error = ForceRequiredAddOn("Support_FindItemByName");
	if($error == $Error::AddOn_NotFound)
	{
		warn("ERROR: GameMode_RandomizerDM - required add-on Support_FindItemByName not found");
		return;
	}

	//--------------------------------------

	$error = ForceRequiredAddOn("Support_SpeedFactor");
	if($error == $Error::AddOn_NotFound)
	{
		warn("ERROR: GameMode_RandomizerDM - required add-on Support_SpeedFactor not found");
		return;
	}

	//--------------------------------------

	$error = ForceRequiredAddOn("Gamemode_Slayer");
	if($error == $Error::AddOn_NotFound)
	{
		warn("ERROR: GameMode_RandomizerDM - required add-on Gamemode_Slayer not found");
		return;
	}
}
else
{
	//I am hoping other add-ons that require stuff would use this kind of method
	//I looked through stuff and didn't see an easier way for this but oh well
	if($GameMode::LastAddOnCount $= "" || $GameMode::AddOnCount != $GameMode::LastAddOnCount)
	{
		$GameMode::LastAddOnCount = $GameMode::AddOnCount;
		deleteVariables("$GameMode::NameAddOn*");
		for($FC = 0; $FC < $GameMode::AddOnCount; $FC++)
		{
			$addonName = $GameMode::AddOn[$FC];
			$GameMode::NameAddOn[$addonName] = 1;
		}
	}

	if(!isFile("Add-Ons/Support_NewHealth/server.cs") || !$GameMode::NameAddOn["Support_NewHealth"])
	{
		$doReturn = 1;
		warn("ERROR: GameMode_RandomizerDM - required add-on Support_NewHealth not found");
	}

	if(!isFile("Add-Ons/Support_FindItemByName/server.cs") || !$GameMode::NameAddOn["Support_FindItemByName"])
	{
		$doReturn = 1;
		warn("ERROR: GameMode_RandomizerDM - required add-on Support_FindItemByName not found");
	}

	if(!isFile("Add-Ons/Support_SpeedFactor/server.cs") || !$GameMode::NameAddOn["Support_SpeedFactor"])
	{
		$doReturn = 1;
		warn("ERROR: GameMode_RandomizerDM - required add-on Support_SpeedFactor not found");
	}

	if(!isFile("Add-Ons/Gamemode_Slayer/server.cs") || !$GameMode::NameAddOn["Gamemode_Slayer"])
	{
		$doReturn = 1;
		warn("ERROR: GameMode_RandomizerDM - required add-on Gamemode_Slayer not found");
	}

	//I did this so people can see ALL the required add-ons if there are more than 1
	if($doReturn)
		return;
}

exec("add-ons/GameMode_RandomizerDM/Support/Support_MissingFunctions.cs");
exec("./Instagib/Script_Instagib.cs");
datablock StaticShapeData(RDM_Cube)
{
	shapeFile = "./Shapes/Cube.dts";
};

function RDM_Init()
{
	if(isObject(Slayer))
	{
		//Slayer.Gamemodes.addMode("Randomizer", "RDM", 0, 1);
		new ScriptGroup(Slayer_GameModeTemplateSG)
		{
			// Game mode settings
			className = "RDM";
			uiName = "Randomizer";
			useTeams = true;
			disable_teamCreation = false;

			// Team settings
			teams_minTeams = 0;
			teams_maxTeams = 4;

			// Default minigame settings
			default_title = "Randomizer";
			default_weaponDamage = true;
			default_lives = 1;
			default_time = 5;
			default_points = 0;
			default_fallingDamage = true;
			default_brickDamage = false;
			default_selfDamage = true;
			default_allowMoveWhileResetting = true;
			default_enableBuilding = false;
			default_enablePainting = false;
			default_enableWand = false;
			default_useAllPlayersBricks = true;
			default_useSpawnBricks = true;
			locked_startEquip0 = 0;
			locked_startEquip1 = 0;
			locked_startEquip2 = 0;
			locked_startEquip3 = 0;
			locked_startEquip4 = 0;

			// Locked minigame settings
			locked_weaponDamage = true;
			locked_selfDamage = true;
			locked_enableBuilding = false;
			locked_enablePainting = false;
			locked_enableWand = false;
			locked_useSpawnBricks = true;
			locked_useAllPlayersBricks = true;
		};

		new ScriptObject(Slayer_PrefSO)
		{
			guiTag = "Rules RDM Mode";
			category = "RDM";
			title = "Enable invisibility";
			defaultValue = true;
			permissionLevel = $Slayer::PermissionLevel["Any"];
			variable = "%mini.RDM_Invisibility";
			type = "bool";
			notifyPlayersOnChange = true;
			requiresMiniGameReset = false;
			requiresServerRestart = false;
			priority = 1000;
		};

		new ScriptObject(Slayer_PrefSO)
		{
			guiTag = "Rules RDM Mode";
			category = "RDM";
			title = "Disable reveal lights";
			defaultValue = false;
			permissionLevel = $Slayer::PermissionLevel["Any"];
			variable = "%mini.RDM_DisableLights";
			type = "bool";
			notifyPlayersOnChange = true;
			requiresMiniGameReset = false;
			requiresServerRestart = false;
			priority = 1000;
		};

		new ScriptObject(Slayer_PrefSO)
		{
			guiTag = "Rules RDM Mode";
			category = "RDM";
			title = "Reveal time";
			defaultValue = 120;
			permissionLevel = $Slayer::PermissionLevel["Any"];
			variable = "%mini.RDM_RevealTime";
			type = "int";
			int_minValue = 60;
			int_maxValue = 1440;
			notifyPlayersOnChange = true;
			requiresMiniGameReset = false;
			requiresServerRestart = false;
			priority = 1000;
		};
	}
}
schedule(0, 0, "RDM_Init");

$Server::RDM::Version = 2;
if(!isObject(RDMLightGroup)) new SimGroup(RDMLightGroup);
if(!isObject(RDMShapeGroup)) new SimGroup(RDMShapeGroup);

function getRDMLightGroup(){return nameToID("RDMLightGroup");}
function getRDMShapeGroup(){return nameToID("RDMShapeGroup");}
function getRDMGroup(){return nameToID("RDMGroup");}
function isRegisteredRDMItem(%RDM){return RDMGroup.findScript(%RDM);}

if(!isObject(RDMGroup))
{
	new ScriptGroup(RDMGroup)
	{
		class = RDMSO;
		fileLoc = "config/server/RDM/Items/";
	};
	RDMGroup.schedule(1000, Load);
}
else
{
	RDMGroup.schedule(1000, Load);
}

function RDMSO::findScript(%this, %RDMName)
{
	//Agh got to make everything complicated
	for(%i=0;%i<%this.getCount();%i++)
	{
		%obj = %this.getObject(%i);
		if(nameToID(%RDMName) == nameToID(%obj) || %obj.uiName $= %RDMName)
			return %obj;
	}

	for(%i=0;%i<%this.getCount();%i++)
	{
		%obj = %this.getObject(%i);
		if(striPos(%obj.uiName, %RDMName) == 0)
			return %obj;
	}

	return -1;
}

function RDMSO::Load(%this)
{
	findItemByName(); //Activate the database
	%this.deleteAll();
	commandToAll('RDM', "CLEAR_ITEM");
	%path = %this.fileLoc @ "*";

	if(getFileCount(%path) <= 0)
	{
		echo("ERROR - No items exist in path -> " @ %path);
		announce("ERROR - No items exist in path -> " @ %path);
		return;
	}

	%file = findFirstFile(%path);
	if(isFile(%file))
	{
		%fileExt = fileExt(%file);
		%name = fileBase(%file);
		if(%fileExt $= ".cs") //Just making sure
		{
			if(isObject(%obj = isRegisteredRDMItem(fileBase(%path))))
				%obj.delete();

			exec(%file);
		}
	}
	else
		return;

	while(%file !$= "")
	{
		%file = findNextFile(%path);
		%fileExt = fileExt(%file);
		%name = fileBase(%file);
		if(%fileExt $= ".cs") //Just making sure
		{
			if(isObject(%obj = isRegisteredRDMItem(fileBase(%path))))
				%obj.delete();

			exec(%file);
		}
	}
}

function RDMItem::onAdd(%this)
{
	if(%this.RDMDataitemData !$= "" && !isObject(%this.RDMDataitemData))
	{
		warn("RDMItem::onAdd() - " @ %this.uiName @ " does not exist!");
		%this.delete();
		return;
	}

	RDMGroup.add(%this);
	%this.parseCommand(%this.command);
	cancel($SendRDMDataSch);
	$SendRDMDataSch = schedule(1000, 0, RDM_SendDataToAllClients);
}

function RDMItem::parseCommand(%this, %com)
{
	if(%com $= "")
		return;

	for(%i=0;%i<getFieldCount(%com);%i++)
	{
		%field = getField(%com, %i);
		%name = getWord(%field, 0);
		%value = collapseEscape(getWords(%field, 1, getWordCount(%field)-1));
		
		%this.RDMData[getSafeVariableName(%name)] = %value;
	}

	%this.command = "";

	if(isObject(%item = %this.RDMData["itemData"]))
	{
		echo("[RDM] [" @ %this.uiName @ "] -> Object has an item, overwriting save name");
		%this.fileLoc = getRDMGroup().fileLoc @ getSafeVariableName(%this.uiName) @ " (" @ %item.getName() @ ").cs";
	}

	%this.save(%this.fileLoc);
}

function registerRDMItem(%name, %parm)
{
	%strName = getSafeVariableName(%name);
	%objName = "RDM_" @ %strName;
	for(%i=0;%i<getFieldCount(%parm);%i++)
	{
		%field = getField(%parm,%i);
		%var = getWord(%field,0);
		%value = getWords(%field, 1, getWordCount(%field)-1);
	}

	if(isObject(%objName))
	{
		warn("Warning: RDM data \"" @ %objName @ "\" already exists. Overwriting.");
		%objName.delete();
	}

	%obj = new ScriptObject(%objName)
	{
		class = "RDMItem";
		uiName = %name;
		command = collapseEscape(%parm);
		fileLoc = getRDMGroup().fileLoc @ getSafeVariableName(%name) @ ".cs";
	};

	return %obj;
}

function serverCmdRDMHelp(%this)
{
	%this.chatMessage("\c6RandomizerDM Help");
	%this.chatMessage("\c6/GUI \c7- \c6Brings up the GUI if you are allowed to use it.");
	if(%this.isAdmin)
	{
		%this.chatMessage("\c6/TestRDM \c7- \c6Test color chat for most RDM messages.");
		%this.chatMessage("\c6/SetNextSpecial \c3[special] [type] \c7- \c6Sets the next special round");
		%this.chatMessage("\c6/SetSpecial \c3[special] [type] \c7- \c6Sets the round into a special");
		%this.chatMessage("\c6/Reveal \c7- \c6Activates the reveal system");

		if(%this.isSuperAdmin)
		{
			%this.chatMessage("\c6/RDMAdd \c3Name/BL_ID \c7- \c6Allow someone to add/remove weapons into the Randomizer");
			%this.chatMessage("\c6/RDMRemove \c3Name/BL_ID \c7- \c6Disallow someone to add/remove weapons into the Randomizer");
		}
	}
}

function serverCmdTestRDM(%this)
{
	if(!%this.isAdmin)
		return;

	%this.chatMessage($Pref::Server::RDM_ChatColor @ "Testing color chat. \c7| " @ $Pref::Server::RDM_ObjectColor @ "Object color");
}

function RDM_SendDataToAllClients()
{
	cancel($SendRDMDataSch);
	//Fix the categories.
	announce($Pref::Server::RDM_ChatColor @ "(" @ $Pref::Server::RDM_ObjectColor @ "RDM" @ $Pref::Server::RDM_ChatColor @ ") Building randomizer lookup table.");

	%group = getRDMGroup();

	for(%i = 0; %i < $RDM::RarityCount; %i++)
	{
		%rarity = getField($RDM::Rarity[%i], 0);
		%group.itemCount[getSafeVariableName(%rarity)] = 0;
	}

	%fixedObjects = 0;
	for(%i = 0; %i < %group.getCount(); %i++)
	{
		%obj = %group.getObject(%i);
		if(%obj.fileLoc !$= "")
			if(!isObject(%obj.RDMData["itemData"]))
				if(isObject(%itemData = findItemByName(%obj.RDMData["itemData"])))
				{
					%obj.RDMData["itemData"] = %itemData.getName();
					%fixedObjects++;
					%obj.save(%obj.fileLoc);
				}

		%type_n = getSafeVariableName(%obj.RDMData["type"]);
		%rarity_n = getSafeVariableName(%obj.RDMData["rarity"]);
		%type = %obj.RDMData["type"];
		%rarity = %obj.RDMData["rarity"];

		%group.item[%type_n, %group.itemCount[%type_n]] = nameToID(%obj);
		%group.itemCount[%type_n]++;

		%group.item[%rarity_n, %group.itemCount[%rarity_n]] = nameToID(%obj);
		%group.itemCount[%rarity_n]++;

		%remove = 0;
		if(getFieldCount(%newTree) > 0)
			for(%a = 0; %a < getFieldCount(%newTree); %a++)
				if(%type $= getField(%newTree, %a))
					%remove = 1;

		if(!%remove)
			%newTree = %type TAB %newTree;
	}

	%newTree = trim(%newTree);

	if($RDM_Trees !$= %newTree)
	{
		%reset = 1;
		$RDM_Trees = %newTree;
		announce("   " @ $Pref::Server::RDM_ChatColor @ "+ New category count: " @ $Pref::Server::RDM_ObjectColor @ getFieldCount($RDM_Trees));
	}

	if(%fixedObjects > 0)
		announce("   " @ $Pref::Server::RDM_ChatColor @ "+ Fixed " @ $Pref::Server::RDM_ObjectColor @ %fixedObjects @ " object ID" @ (%fixedObjects == 1 ? "" : "s"));

	for(%i = 0; %i < ClientGroup.getCount(); %i++)
	{
		%client = ClientGroup.getObject(%i);
		%client.RDM_SendData(%reset);
	}
}

function GameConnection::RDM_SendData(%this, %reset)
{
	if(!%this.RDM_Client)
		return;

	if(!isObject(%group = getRDMGroup()))
		return;

	if(%reset)
	{
		commandToClient(%this, 'RDM', "CLEAR_ITEM");
		commandToClient(%this, 'RDM', "SET_CATEGORIES", $RDM_Trees);

		for(%i = 0; %i < $RDM::RarityCount; %i++)
			%categories = %categories TAB getField($RDM::Rarity[%i], 0);

		commandToClient(%this, 'RDM', "SET_RARITYCATEGORIES", trim(%categories));
	}

	if(%group.getCount() > 0)
		for(%i = 0; %i < %group.getCount(); %i++)
		{
			%obj = %group.getObject(%i);

			if(%obj.uiName !$= "")
			{
				%strName = getSafeVariableName(%obj.uiName);
				commandToClient(%this,'RDM', "ADD_ITEM_EDITOR", nameToID(%obj), %obj.uiName, %obj.RDMData["type"], %obj.RDMData["rarity"]);
			}
		}

	if(!%this.RDM_datablockData || %reset == 2)
	{
		commandToClient(%this, 'RDM', "CLEAR_DATABLOCK");
		%this.chatMessage("Sending " @ mFloor(ItemCache.itemCount) @ " item(s)");
		%this.RDM_datablockData = 1;

		for(%i = 0; %i < ItemCache.itemCount; %i++)
		{
			%item = ItemCache.item[%i];
			if(isObject(%item))
			{
				if(isObject(%proj = %item.image.projectile))
					%intDamage = %proj.directDamage SPC %proj.explosion.radiusDamage;

				%damage = mFloor(getWord(%intDamage, 0));
				%radiusDamage = mFloor(getWord(%intDamage, 1));
				%image = %item.iconName;
				%imageColor = %item.colorShiftColor;
				commandToClient(%this, 'RDM', "ADD_ITEM", nameToID(%item), %item.uiName, %damage, %radiusDamage, %image, %imageColor);
			}
		}
	}
}

//End

$RDM::Profiles = "config/server/RDMProfiles/";
function GameConnection::RDM_Save(%this)
{
	if(!isObject(%this))
		return;

	%path = $RDM::Profiles @ %this.getBLID() @ ".RDMProfile";

	%file = new FileObject();
	%file.openForWrite(%path);
	//See line 8 for the args
	%file.writeLine("TotalKills" TAB %this.RDMData["TotalKills"] * 1);
	%file.writeLine("PlayTime" TAB %this.getTotalPlayTime());
	%file.writeLine("PointsSpent" TAB %this.RDMData["PointsSpent"] * 1);
	%file.writeLine("Score" TAB %this.RDMData["Score"] * 1);
	echo("\'" @ %this.name @ "\' profile has been saved.");
	%file.close();
	%file.delete();
}

function GameConnection::RDM_Load(%this)
{
	if(!isObject(%this))
		return;
	%path = $RDM::Profiles @ %this.getBLID() @ ".RDMProfile";
	if(!isFile(%path))
		return;
	%file = new FileObject();
	%file.openForRead(%path);
	//See line 8 for the args
	echo("\'" @ %this.name @ "\' profile has been loaded.");
	while(!%file.isEOF())
	{
		%line = %file.readLine();
		%fLine = strReplace(getField(%line,0)," ","_");
		%this.RDMData[getWord(getField(%fLine, 0), 0)] = getField(%line, 1);
	}
	%file.close();
	%file.delete();
}

function serverCmdRDM(%this, %type, %cmd0, %cmd1, %cmd2)
{
	switch$(%type)
	{
		case "Handshake":
			cancel(%this.RDMData["HandFailSch"]);
			if(!%this.isSuperAdmin && !$Pref::Server::RDM_CanEdit[%this.getBLID()])
			{
				%this.RDM_Client = -1;
				%this.RDM_ClientError = "SUPER_ADMIN_ERROR";
				commandToClient(%this, 'RDM', "Handshake", "UNSUCCESS", "INCORRECT_CODE");
				echo("RandomizerClient Handshake (" @ %this.getPlayerName() @ " : " @ %this.getBLID() @ ") - Failed (Not a super admin)");
				%this.RDM_Main("HandshakeFail");
				
				return;
			}

			%code = getField(%cmd0, 0); //Get what we sent
			%version = getField(%cmd0, 1); //Their version

			if(%this.RDM_Password !$= %code)
			{
				%this.RDM_Client = -1;
				%this.RDM_ClientError = "INCORRECT_CODE";
				commandToClient(%this, 'RDM', "Handshake", "UNSUCCESS", "INCORRECT_CODE");
				echo("RandomizerClient Handshake (" @ %this.getPlayerName() @ " : " @ %this.getBLID() @ ") - Failed (Incorrect password)");
				%this.RDM_Main("HandshakeFail");

				return;
			}

			if(%version < $Server::RDM::Version)
			{
				%this.RDM_Client = -1;
				%this.RDM_ClientError = "VERSION_OLD";
				commandToClient(%this, 'RDM', "Handshake", "UNSUCCESS", "VERSION_OLD");
				echo("RandomizerClient Handshake (" @ %this.getPlayerName() @ " : " @ %this.getBLID() @ ") - Failed (Outdated client)");

				%this.RDM_Main("HandshakeFail");
				if(!strLen($Server::RDM::Link))
					%this.chatMessage("You have an outdated version of RandomizerDM client.");
				else
					%this.chatMessage("You have an outdated version of RandomizerDM client. Please download it <a:" @ $Server::RDM::Link @ ">here</a>.");

				return;
			}

			%this.RDM_Client = 1;
			commandToClient(%this, 'RDM', "Handshake", "SUCCESS", %this.isAdmin);
			%this.RDM_SendData(2);
			echo("RandomizerClient Handshake (" @ %this.getPlayerName() @ " : " @ %this.getBLID() @ ") - Success");

		case "ADD_ITEM":
			if(!%this.RDM_Client)
				return;

			if(!%this.isSuperAdmin && !$Pref::Server::RDM_CanEdit[%this.getBLID()])
				return;

			if(%cmd1 $= "" || %cmd2 $= "")
			{
				commandToClient(%this, 'MessageBoxOK', "Whoops!", "You left a category/rarity blank.");
				return;
			}

			if(isObject(%item = findItemByName(%cmd0)))
			{
				registerRDMItem(%item.uiName, 
					"itemData " @ %item.getName() TAB
					"type " @ %cmd1 TAB
					"rarity " @ %cmd2
					);

				%msg = "\c7[" @ $Pref::Server::RDM_ObjectColor @ %this.getPlayerName() @ "\c7] " @ $Pref::Server::RDM_ObjectColor @ %item.uiName SPC $Pref::Server::RDM_ChatColor @ "(" @ $Pref::Server::RDM_ObjectColor @ %item.getName() @ $Pref::Server::RDM_ChatColor @ ") has been added to the system. \c7[" @ $Pref::Server::RDM_ChatColor @ "Category: " @ $Pref::Server::RDM_ObjectColor @ %cmd1 @ " \c7| " @ $Pref::Server::RDM_ChatColor @ "Rarity: " @ $Pref::Server::RDM_ObjectColor @ %cmd2 @ "\c7]";
				announce(%msg);
				RDM_Log(%msg);
			}
			else
				%this.chatMessage("Invalid item.");

		case "EDIT_ITEM":
			if(!%this.RDM_Client)
				return;

			if(!%this.isSuperAdmin && !$Pref::Server::RDM_CanEdit[%this.getBLID()])
				return;

			if(isObject(%item = getRDMGroup().findScript(%cmd0)))
			{
				if(%cmd1 !$= %item.RDMData["type"])
				{
					%msg0 = "\c7[" @ $Pref::Server::RDM_ObjectColor @ %this.getPlayerName() @ "\c7] [" @ $Pref::Server::RDM_ObjectColor @ %item.uiName @ "\c7] " @ $Pref::Server::RDM_ChatColor @ "Category set to: " @ $Pref::Server::RDM_ObjectColor @ %cmd1;
					announce(%msg0);
					RDM_Log(%msg0);
					%item.RDMData["type"] = %cmd1;
				}

				if(%cmd2 !$= %item.RDMData["rarity"])
				{
					%msg1 = "\c7[" @ $Pref::Server::RDM_ObjectColor @ %this.getPlayerName() @ "\c7] [" @ $Pref::Server::RDM_ObjectColor @ %item.uiName @ "\c7] " @ $Pref::Server::RDM_ChatColor @ "Rarity set to: " @ $Pref::Server::RDM_ObjectColor @ %cmd2;
					announce(%msg1);
					RDM_Log(%msg1);
					%item.RDMData["rarity"] = %cmd2;
				}

				%item.save(%item.fileLoc);

				commandToAll('RDM', "ADD_ITEM_EDITOR", nameToID(%item), %item.uiName, %item.RDMData["type"], %item.RDMData["rarity"]);
			}

		case "REMOVE_ITEM":
			if(!%this.RDM_Client)
				return;

			if(!%this.isSuperAdmin && !$Pref::Server::RDM_CanEdit[%this.getBLID()])
				return;

			if(isObject(%item = getRDMGroup().findScript(%cmd0)))
			{
				%itemID = nameToID(%item);
				%itemUI = %item.uiName;
				fileDelete(%item.fileLoc);
				%item.delete();
				RDM_SendDataToAllClients();
				commandToAll('RDM', "REMOVE_ITEM", %itemID);
				%msg = "\c7[" @ $Pref::Server::RDM_ObjectColor @ %this.getPlayerName() @ "\c7] " @ $Pref::Server::RDM_ChatColor @ "Removing \c7[" @ $Pref::Server::RDM_ObjectColor @ %itemUI @ "\c7] " @ $Pref::Server::RDM_ChatColor @ "has been successful.";
				announce(%msg);
				RDM_Log(%msg);
			}
			else
				%this.chatMessage("" @ $Pref::Server::RDM_ChatColor @ "Removing \c7[" @ $Pref::Server::RDM_ObjectColor @ %cmd0 @ "\c7] " @ $Pref::Server::RDM_ChatColor @ "has been unsuccessful.");

		case "minigame":
			if(!%this.isSuperAdmin)
				return;

			if(!isObject(%minigame = %this.minigame))
			{
				%this.chatMessage($Pref::Server::RDM_ChatColor @ "No minigame!");
				return;
			}

			%minigame.RDM_Enabled = !%minigame.RDM_Enabled;
			%minigame.messageAll('', $Pref::Server::RDM_ObjectColor @ %this.getPlayerName() @ $Pref::Server::RDM_ChatColor @ " has toggled RDM mode: " @ $Pref::Server::RDM_ObjectColor @ (%minigame.RDM_Enabled ? "true" : "false"));

		default:
			%this.chatMessage("/RDM \c7- " @ $Pref::Server::RDM_ChatColor @ "Unknown command \"" @ %type @ "\"");
	}
}

function GameConnection::RDM_Main(%this, %type, %cmd0, %cmd1, %cmd2, %cmd3)
{
	switch$(%type)
	{
		case "Handshake":
			if(%this.RDM_Client) //They already have it.
				return;

			if(!%this.isSuperAdmin && !$Pref::Server::RDM_CanEdit[%this.getBLID()])
				return;

			echo("RandomizerClient Handshake (" @ %this.getPlayerName() @ " : " @ %this.getBLID() @ ") - Sending handshake information.");

			%this.RDM_Password = sha1(getRandom(0, 10000));
			commandToClient(%this, 'RDM', "Handshake", %this.RDM_Password);

			cancel(%this.RDMData["HandFailSch"]);
			%this.RDMData["HandFailSch"] = %this.schedule(5000, "RDM_Main", "HandshakeFail");

		case "HandshakeFail":
			cancel(%this.RDMData["HandFailSch"]);

			if(%this.RDM_Client == -1)
			{
				%this.chatMessage($Pref::Server::RDM_ChatColor @ "Randomizer DM client " @ $Pref::Server::RDM_ChatColor @ "handshake \c1failed" @ $Pref::Server::RDM_ChatColor @ ". Reason: " @ $Pref::Server::RDM_ObjectColor @ %this.RDM_ClientError);
			}
			else if(%this.RDM_Client == 0 && $Server::RDM::Link !$= "")
				%this.chatMessage($Pref::Server::RDM_ChatColor @ "Randomizer DM client " @ $Pref::Server::RDM_ChatColor @ "has a " @ $Pref::Server::RDM_ObjectColor @ "client mod" @ $Pref::Server::RDM_ChatColor @ ", download it <a:" @ $Server::RDM::Link @ ">here</a>" @ $Pref::Server::RDM_ChatColor @ ". It isn't required, but it is recommended.");


		default:
			%this.chatMessage("GameConnection::RDM_Main() \c7- " @ $Pref::Server::RDM_ChatColor @ "Unknown command \"" @ %type @ "\"");
	}
}

function serverCmdRDMRemove(%this, %thing)
{
	if(!%this.isSuperAdmin)
		return;

	%bl_id = mFloor(%thing);
	if(isObject(%target = findClientByName(%thing)))
	{
		%name = %target.getPlayerName();
		%bl_id = %target.getBLID();
	}

	if(!$Pref::Server::RDM_CanEdit[%bl_id])
	{
		%this.chatMessage("That ID cannot edit the GUI.");
		return;
	}

	$Pref::Server::RDM_CanEdit[%bl_id] = 0;

	if(%name !$= "")
		%nameMsg = "" @ $Pref::Server::RDM_ChatColor @ "(" @ $Pref::Server::RDM_ObjectColor @ %name @ $Pref::Server::RDM_ChatColor @ ") ";

	messageAll('MsgAdminForce', "" @ $Pref::Server::RDM_ObjectColor @ %this.getPlayerName() SPC $Pref::Server::RDM_ChatColor @ "has disallowed BL_ID " @ $Pref::Server::RDM_ObjectColor @ %bl_id SPC $Pref::Server::RDM_ChatColor @ "to be able to use the " @ $Pref::Server::RDM_ObjectColor @ "Randomizer Editor Client" @ $Pref::Server::RDM_ChatColor @ ".");
	if(isObject(%target))
	{
		if(%target.RDM_Client)
		{
			%target.chatMessage("" @ $Pref::Server::RDM_ChatColor @ "Sorry, you can no longer use the GUI.");
			%target.RDM_Client = 0;
		}
	}
}

if(!strLen($Pref::Server::RDM_ObjectColor))
	$Pref::Server::RDM_ObjectColor = "\c4";

if(!strLen($Pref::Server::RDM_ChatColor))
	$Pref::Server::RDM_ChatColor = "\c6";

function serverCmdRDMAdd(%this, %thing)
{
	if(!%this.isSuperAdmin)
		return;

	%bl_id = mFloor(%thing);
	if(isObject(%target = findClientByName(%thing)))
	{
		%name = %target.getPlayerName();
		%bl_id = %target.getBLID();
	}

	if($Pref::Server::RDM_CanEdit[%bl_id])
	{
		%this.chatMessage("That ID can already edit the GUI.");
		return;
	}

	$Pref::Server::RDM_CanEdit[%bl_id] = 1;

	if(%name !$= "")
		%nameMsg = "" @ $Pref::Server::RDM_ChatColor @ "(" @ $Pref::Server::RDM_ObjectColor @ %name @ $Pref::Server::RDM_ChatColor @ ") ";

	messageAll('MsgAdminForce', "" @ $Pref::Server::RDM_ObjectColor @ %this.getPlayerName() SPC $Pref::Server::RDM_ChatColor @ "has allowed BL_ID " @ $Pref::Server::RDM_ObjectColor @ %bl_id @ %nameMsg SPC $Pref::Server::RDM_ChatColor @ "to be able to use the " @ $Pref::Server::RDM_ObjectColor @ "Randomizer Editor Client" @ $Pref::Server::RDM_ChatColor @ ".");
	if(isObject(%target))
	{
		if(%target.RDM_Client)
			%target.chatMessage("" @ $Pref::Server::RDM_ChatColor @ "Welcome to the Randomizer editor; looks like you already have it.");
		else
		{
			if($Server::RDM::Link !$= "")
				%target.chatMessage("" @ $Pref::Server::RDM_ChatColor @ "Welcome to the Randomizer editor, here's the link: <a:" @ $Server::RDM::Link @ ">Client_Randomizer</a>");
			else
				%target.chatMessage("" @ $Pref::Server::RDM_ChatColor @ "Welcome to the Randomizer editor, looks like there isn't a link yet, please try again later.");
		}
	}
}

function Player::SetKeyAward(%this, %award, %typeNum)
{
	if(!isObject(%client = %this.client))
		return;

	%max = $Pref::Server::RDM_MaxKeyAwards;

	if(%this.keyAwards >= %max)
	{
		%client.chatMessage("\c6Sorry, you already got enough key awards!");
		%this.removeItem("Key Blue");
		%this.removeItem("Key Green");
		%this.removeItem("Key Yellow");
		%this.removeItem("Key Red");
		return;
	}

	%this.keyAwards++;

	%typeNum = mFloor(%typeNum);
	if(%typeNum == 4)
		%typeNum = getRandom(0, 3);

	switch$(%award)
	{
		case "BlueKey" or 0:
			%this.removeItem("Key Blue");

			if(%this.keyAward["Blue"])
			{
				%client.chatMessage("\c6You already got this key award!");
				return;
			}

			switch(%typeNum)
			{
				case 0:
					%msg = "1.5x more speed!";
					%this.schedule(1000, "setSpeedFactor", %this.getSpeedFactor() * 1.5);

				case 1:
					%msg = "1.7x more speed!";
					%this.schedule(1000, "setSpeedFactor", %this.getSpeedFactor() * 1.7);

				case 2:
					%msg = "2.5x more speed!";
					%this.schedule(1000, "setSpeedFactor", %this.getSpeedFactor() * 2.5);

				case 3:
					%msg = "2x more speed!";
					%this.schedule(1000, "setSpeedFactor", %this.getSpeedFactor() * 2);

				default:
					%client.chatMessage("" @ $Pref::Server::RDM_ChatColor @ "Invalid award type.");
					return;
			}

			%this.keyAward["Blue"] = true;

		case "GreenKey" or 1:
			%this.removeItem("Key Green");

			if(%this.keyAward["Green"])
			{
				%client.chatMessage("\c6You already got this key award!");
				return;
			}

			switch(%typeNum)
			{
				case 0:
					%float = mFloatLength(getRandomF(1.2, 3.0), 1);
					%msg = "Projectile scale set to " @ %float @ "x!";
					%this.RDMData["ProjectileScale"] = %float;

				case 1:
					%float = mFloatLength(getRandomF(1.2, 3.0), 1);
					%msg = "Projectile scale set to " @ %float @ "x!";
					%this.RDMData["ProjectileScale"] = %float;

				case 2:
					%float = mFloatLength(getRandomF(1.2, 3.0), 1);
					%msg = "Projectile scale set to " @ %float @ "x!";
					%this.RDMData["ProjectileScale"] = %float;

				case 3:
					%float = mFloatLength(getRandomF(1.2, 3.0), 1);
					%msg = "Projectile scale set to " @ %float @ "x!";
					%this.RDMData["ProjectileScale"] = %float;

				default:
					%client.chatMessage("" @ $Pref::Server::RDM_ChatColor @ "Invalid award type.");
					return;
			}

			%this.keyAward["Green"] = true;

		case "RedKey" or 2:
			%this.removeItem("Key Red");

			if(%this.keyAward["Red"])
			{
				%client.chatMessage("\c6You already got this key award!");
				return;
			}

			switch(%typeNum)
			{
				case 0:
					%msg = "New LEGENDARY set!";
					%this.RDM_RarityPick = "Legendary";
					%this.schedule(1000, "RDM_Set", 3, 0, 0);

				case 1:
					%msg = "New LEGENDARY set!";
					%this.RDM_RarityPick = "Impossible";
					%this.schedule(1000, "RDM_Set", 5, 0, 0);

				case 2:
					%msg = "New IMPOSSIBLE set!";
					%this.RDM_RarityPick = "Legendary";
					%this.schedule(1000, "RDM_Set", 3, 0, 0);

				case 3:
					%msg = "New IMPOSSIBLE set!";
					%this.RDM_RarityPick = "Legendary";
					%this.schedule(1000, "RDM_Set", 5, 0, 0);

				default:
					%client.chatMessage("" @ $Pref::Server::RDM_ChatColor @ "Invalid award type.");
					return;
			}

			%this.keyAward["Red"] = true;

		case "YellowKey" or 3:
			%this.removeItem("Key Yellow");

			if(%this.keyAward["Yellow"])
			{
				%client.chatMessage("\c6You already got this key award!");
				return;
			}

			switch(%typeNum)
			{
				case 0:
					%msg = "Max health set to 200!";
					%this.setMaxHealth(200);

				case 1:
					%msg = "Max health set to 300!";
					%this.setMaxHealth(300);

				case 2:
					%msg = "Player scale set to 2x!";
					%this.setPlayerScale(2);

				case 3:
					%msg = "Protective shield! It's not effective against everything..";
					%this.doShield(0);

				default:
					%client.chatMessage("" @ $Pref::Server::RDM_ChatColor @ "Invalid award type.");
					return;
			}

			%this.keyAward["Yellow"] = true;

		default:
			%client.chatMessage("" @ $Pref::Server::RDM_ChatColor @ "Invalid award.");
			return;
	}

	%client.chatMessage("<font:impact:20>" @ $Pref::Server::RDM_ChatColor @ %msg);
	%client.centerPrint("<font:impact:20>" @ $Pref::Server::RDM_ChatColor @ %msg, 5);
	%this.setInvulnerbilityTime(1);
}
registerOutputEvent("Player", "SetKeyAward", "list Blue 0 Green 1 Red 2 Yellow 3" TAB "list Low 0 Medium 1 High 2 Insane 3 Random 4");

function GameConnection::addTempRDMKill(%this, %amt, %damageType)
{
	%damageTypeName = $DamageType_Array[%damageType];
	%this.TempRDMData["Kills"] += %amt;
	%total = %this.TempRDMData["Kills"];

	//if(%total >= 15)
	//	%this.unlockAchievement("Psycho killer");
}

function GameConnection::addRDMKill(%this, %amt, %damageType)
{
	%damageTypeName = $DamageType_Array[%damageType];
	%this.RDMKills += %amt;
	//if(%this.RDMKills >= 3)
	//	%this.unlockAchievement("Bloodthirsty");

	if(%damageTypeName !$= %this.RDMLastDamageType)
		%this.RDMDamageTypeKills = 0;

	%this.RDMDamageTypeKills++;
	%this.RDMLastDamageTypeName = %damageTypeName;
	%this.RDMLastDamageType = %damageType;

	//if(%this.RDMLastDamageTypeName $= "redBaseballBat")
	//	%this.unlockAchievement("Homerun!");

	cancel(%this.resetDRKillSch);
	%this.resetDRKillSch = %this.schedule(2000, "resetRDMKill");
}

function GameConnection::addRDMScore(%this, %amt)
{
	%this.RDMData["Score"] += %amt;
	//if(%this.RDMData["Score"] >= 20000)
	//	%this.unlockAchievement("I am rich!");
}

function GameConnection::resetRDMKill(%this)
{
	if(!isObject(%this))
		return;

	%this.RDMKills = 0;
}

function GameConnection::getTotalPlayTime(%this)
{
	return %this.DeathRaceData["PlayTime"] + ($Sim::Time - %this.TotalPlayTime);
}

function serverCmdDamageType(%this)
{
	if(!%this.isSuperAdmin)
		return;

	if(!isObject(%player = %this.player))
		return;

	if(!isObject(%image = %player.getMountedImage(0)))
	{
		%this.chatMessage("You must be holding an image to get the damage type info.");
		return;
	}

	%projectile = %image.projectile;

	%damageTypeName = $DamageType_Array[%projectile.directDamageType];
	%damageTypeRadiusName = $DamageType_Array[%projectile.radiusDamageType];
	%this.chatMessage("Weapon: \c3" @ %image.getName());
	%this.chatMessage("Direct damage: \c3" @ %projectile.directDamage);
	//%this.chatMessage("Explosive damage (range: " @ %projectile.explosion @ "): \c3" @ %projectile.directDamage);
	%this.chatMessage("Direct DamageType name: \c3" @ %damageTypeName);
	%this.chatMessage("Radius DamageType name: \c3" @ %damageTypeRadiusName);
}

//Forum link since you can't always rely on these kind of links
$Server::RDM::Link = "https://forum.blockland.us/index.php?topic=308152.0";
if(isPackage("Server_RDM"))
	deactivatePackage("Server_RDM");

package Server_RDM
{
	function Player::pickup(%this, %item)
	{
		if(isObject(%client = %this.client) && isObject(%mini = %client.minigame))
		{
			%data = %item.getDatablock();
			if(isObject(%image = %data.image) && (isObject(%proj = %image.projectile) || %data = nameToID("redKeyItem")) && %mini.isSpecialRound && isObject(%mini.TempRDMData["OnlyWeapon"]))
			{
				%client.centerPrint("\c5Item pickup is disabled during a special round!", 3);
				return;
			}
		}

		return Parent::pickup(%this, %item);
	}

	function serverCmdMessageSent(%this, %msg)
	{
		if(isObject(%player = %this.player))
		{
			if(isObject(%tool = %player.item[%player.currTool]))
				%msg = strReplace(%msg, "%mywep", "Current weapon: " @ %tool.uiName);

			for(%i = 0; %i < %player.getDatablock().maxTools; %i++)
			{
				if(isObject(%tool = %player.tool[%i]))
				{
					if(%weaponMsg $= "")
						%weaponMsg = firstWord(%tool.uiName);
					else
						%weaponMsg = %weaponMsg @ ", " @ firstWord(%tool.uiName);

					%toolCount++;
				}
			}

			if(%weaponMsg !$= "")
				%msg = strReplace(%msg, "%myset", "Weapon set: " @ %weaponMsg);
		}

		//if(striPos(%msg, "rotalosiV") >= 0 && $Server::AchievementsComplete[%this.getBLID(), "Illuminati"])
		//{
		//	%this.chatMessage("You already have this achievement!");
		//	return;
		//}

		Parent::serverCmdMessageSent(%this, %msg);
		//if(!%this.isSpamming)
		//{
		//	%this.RDMLastChat = %msg;
		//	%this.unlockAchievement("Illuminati");
		//}
	}
	function GameConnection::onClientLeaveGame(%this)
	{
		%this.RDM_Save();
		return Parent::onClientLeaveGame(%this);
	}

	function GameConnection::onClientEnterGame(%this)
	{
		Parent::onClientEnterGame(%this);
		%this.TotalPlayTime = $Sim::Time;
		%this.RDM_Main("Handshake");
	}

	function serverCmdSit(%this)
	{
		if(isObject(%minigame = %this.minigame))
			if(%minigame.isRandomizer())
				return;

		Parent::serverCmdSit(%this);
	}

	function GameConnection::AutoAdminCheck(%this)
	{
		%this.RDM_Load();
		return Parent::AutoAdminCheck(%this);
	}

	function GameConnection::CreatePlayer(%this, %transform)
	{
		cancel(%this.RDM_Schedule);
		%this.RDM_Schedule = %this.schedule(250, "RDM_Init");

		return Parent::CreatePlayer(%this, %transform);
	}

	function Armor::onEnterLiquid(%data, %obj, %coverage, %type)
	{
		Parent::onEnterLiquid(%data, %obj, %coverage, %type);
		if(!isObject(%obj.client.minigame))
			if(%obj.getClassName() $= "Player")
				return;

		if(%obj.isInvincible && isObject(%client = %obj.client))
		{
			if($Sim::Time - %obj.lastTeleTime < 0.1)
				return;

			%obj.setVelocity("0 0 0");
			%obj.setTransform(%client.getSpawnPoint());
			%obj.RDM_ApplyTime = $Sim::Time;
			%obj.addMaxHealth(-10);
			%obj.lastTeleTime = $Sim::Time;
			%client.chatMessage("\c6-10 max health penalty! Don't fall in water during preparation!");
		}
		else
		{
			%obj.setInvulnerbility(0);
			if(%obj.RDM_ApplyTime > 0 && $Sim::Time - %obj.RDM_ApplyTime < $Pref::Server::RDM_SpawnTimeout)
				%obj.RDM_ApplyTime -= $Pref::Server::RDM_SpawnTimeout * 2;

			%obj.hasShotOnce = true;
			%obj.invulnerable = false;
			%obj.isInvincible = false;
			%obj.schedule(0, damage, %obj, %obj.getPosition(), %obj.getDatablock().maxDamage * getWord(%obj.getScale(), 2) * 100, $DamageType::Lava);
		}
	}

	function Armor::onLeaveLiquid(%data, %obj, %coverage, %type)
	{
		Parent::onLeaveLiquid(%data, %obj, %coverage, %type);
		if(!isObject(%obj.client.minigame))
			if(%obj.getClassName() $= "Player")
				return;

		if(%obj.isInvincible && isObject(%client = %obj.client))
		{
			if($Sim::Time - %obj.lastTeleTime < 0.1)
				return;

			%obj.setVelocity("0 0 0");
			%obj.setTransform(%client.getSpawnPoint());
			%obj.RDM_ApplyTime = $Sim::Time;
			%obj.addMaxHealth(-10);
			%obj.lastTeleTime = $Sim::Time;
			%client.chatMessage("\c6-10 max health penalty! Don't fall in water during preparation!");
		}
		else
		{
			%obj.setInvulnerbility(0);
			if(%obj.RDM_ApplyTime > 0 && $Sim::Time - %obj.RDM_ApplyTime < $Pref::Server::RDM_SpawnTimeout)
				%obj.RDM_ApplyTime -= $Pref::Server::RDM_SpawnTimeout * 2;

			%obj.hasShotOnce = true;
			%obj.invulnerable = false;
			%obj.isInvincible = false;
			%obj.schedule(0, damage, %obj, %obj.getPosition(), %obj.getDatablock().maxDamage * getWord(%obj.getScale(), 2) * 100, $DamageType::Lava);
		}
	}

	function VehicleData::onEnterLiquid(%data, %obj, %coverage, %type)
	{
		Parent::onEnterLiquid(%data, %obj, %coverage, %type);
		if(isObject(%driver = %obj.getMountNodeObject(0)))
			if(%driver.getClassName() !$= "AIPlayer")
				if(!isObject(%driver.client.minigame))
					return;

		%obj.RDM_ApplyTime = $Sim::Time - $Pref::Server::RDM_SpawnTimeout - 1;
		%obj.damage(%obj, %obj.getPosition(), %obj.getDatablock().maxDamage * getWord(%obj.getScale(), 2) * 100, $DamageType::Lava);
		%obj.finalExplosion();
	}

	function MinigameSO::onReset(%this, %client)
	{
		Parent::onReset(%this, %client);
		if(%this.isSlayerMinigame)
			return;

		cancel($TimescaleSch);
		cancel(%this.randomRaritySch);
		cancel(%this.RDM_SetInitSch);
		%this.hasNotified = 0;

		%this.TempRDMData["SpeedFactor"] = $Pref::Server::RDM_SpeedFactor;
		%this.TempRDMData["MaxWeapons"] = $Pref::Server::RDM_MaxWeapons;
		%this.TempRDMData["Datablock"] = "";
		%this.TempRDMData["Size"] = 1;
		%this.TempRDMData["Item"] = "";
		%this.TempRDMData["Velocity"] = 0;
		%this.TempRDMData["OnlyWeapon"] = 0;
		%this.RDMData["ProjectileScale"] = 1;
		%this.RDMData["Instakill"] = 0;
		%this.RDMData["Shields"] = 0;
		%this.RDMData["Crits"] = 0;
		%this.RDM_GameApply(); //Reset values and apply

		%data = %this.playerDatablock;
		if(%data.oldMinImpactSpeed $= "")
			%data.oldMinImpactSpeed = %data.minImpactSpeed;

		if(striPos($Server::MapChanger::CurrentMap, "Quake") >= 0)
			%data.minImpactSpeed = 60;
		else
			%data.minImpactSpeed = %data.oldMinImpactSpeed;

		//%this.randomRaritySch = %this.schedule(5000, "RDM_DisplayRarityAvg");
		%this.RDM_StartTime = $Sim::Time;
		%this.RDM_SetInitSch = %this.schedule(1000 * ($Pref::Server::RDM_SpawnTimeout + 4), "RDM_ApplyInit");
		%this.lastPlayerCountTime = $Sim::Time;
		RDM_LightLoop(%this);

		if((%rarity = %this.TempRDMData["RarityPick"]) !$= "")
		{
			%this.messageAll('', $Pref::Server::RDM_ChatColor @ "Applying a rarity: " @ $Pref::Server::RDM_ObjectColor @ %rarity);
			if(%this.numMembers > 0)
			{
				for(%i = 0; %i < %this.numMembers; %i++)
				{
					if(isObject(%member = %this.member[%i]) && isObject(%player = %member.player))
						%player.RDM_RarityPick = %rarity;
				}
			}
			%this.TempRDMData["RarityPick"] = "";
		}
	}

	function SimObject::onCameraEnterOrbit(%obj, %camera)
	{
		%obj.observers++;
		if(isObject(%client = %obj.getControllingClient()))
			if(isObject(%camClient = %camera.getControllingClient())) //Activate status
			{
				%camClient.spyObj = %obj;
				%camClient.schedule(0, RDM_DeadPrint);
			}
	}

	function SimObject::onCameraLeaveOrbit(%obj, %camera)
	{
		%obj.observers--;
		if(isObject(%camClient = %camera.getControllingClient())) //Deactivate status
		{
			%camClient.spyObj = 0;
			%camClient.schedule(0, RDM_DeadPrint);
		}
	}

	function GameConnection::onDeath(%this, %killerObj, %killerClient, %damageType, %position)
	{
		if(isObject(%minigame = %this.minigame))
		{
			if(%minigame.isSlayerMinigame && !isEventPending($TimescaleSch))
				if(%minigame.getLiving() <= 2)
				{
					for(%i = 0; %i < %minigame.numMembers; %i++)
					{
						%cl = %minigame.member[%i];
						if(isObject(%cl) && %cl.getClassName() $= "GameConnection")
							if(!isObject(%pl = %cl.player) && isObject(%cam = %cl.camera))
								%cam.setMode("Corpse", %this.player);
							else if(isObject(%pl) && %pl.getState() !$= "dead")
								%winner = %pl;
					}

					%oldTimescale = getTimescale();
					setTimescale(0.2);
					%timescale = 1;
				}

			if(isObject(%killerClient) && %killerClient.getClassName() $= "GameConnection" && isObject(%killMini = %killerClient.minigame) && %killMini.isSlayerMinigame && %killerClient.getLives() == 2 && %killerClient != %this)
				%killerClient.setLives(1);
		}

		if(isObject(%killerClient) && %killerClient.getClassName() $= "GameConnection" && %killerClient != %this)
		{
			%killerClient.RDMData["totalKills"]++;
			//%killerClient.unlockAchievement("Unstoppable");
			//%killerClient.unlockAchievement("On the Kill");

			%killerClient.addRDMKill(1, %damageType);
			%killerClient.addTempRDMKill(1, %damageType);
			%killerClient.addRDMScore(1);
		}

		Parent::onDeath(%this, %killerObj, %killerClient, %damageType, %position);

		if(%timescale)
		{
			cancel($TimescaleSch);
			$TimescaleSch = schedule(1500 * getTimescale(), 0, "RDM_setTimescale", %minigame, %oldTimescale, %winner);
		}

		if(!isObject(%winner) && isObject(%cam = %this.camera) && isObject(%killerPlayer = %killerClient.player))
			%cam.schedule(500, setMode, "Corpse", %killerPlayer);
	}

	function Armor::onDisabled(%this, %obj)
	{
		if(%obj.getClassName() $= "Player")
		{
			%client = %obj.client;

			if(isObject(%client) && isObject(%mini = %client.minigame) && !%mini.isSpecialRound)
			{		
				%dataCount = 0;
				for(%i = 0; %i < %obj.getDatablock().maxTools; %i++)
				{
					if(isObject(%nItem = %obj.tool[%i]))
					{
						%data[%dataCount] = %nItem;
						%dataCount++;
					}
				}

				%data = %data[getRandom(0, %dataCount-1)];
				if(isObject(%data))
				{
					%pos = %obj.getPosition();
					%item = new Item()
					{
						dataBlock = %data;
						position = vectorAdd(%pos, "0 0 1");
						rotate = true;
					};
					%itemVec = vectorAdd(%vec, getRandom(-8, 8) SPC getRandom(-8, 8) SPC getRandom(14, 18));
					%item.BL_ID = %client.BL_ID;
					%item.minigame = %mini;
					%item.spawnBrick = -1;
					%item.setShapeName(%data.uiName);
					%item.setShapeNameDistance(30);

					%item.setVelocity(%itemVec);
					%item.schedulePop();

					if(!isObject(RDM_ItemGroup))
						new SimSet(RDM_ItemGroup);

					RDM_ItemGroup.add(%item);
				}
			}
		}

		Parent::onDisabled(%this, %obj);
	}

	function serverCmdLight(%this)
	{
		if(!isObject(%player = %this.player))
			return Parent::serverCmdLight(%this);

		if(isObject(%light = %player.RMD_Light))
		{
			%this.centerPrint($Pref::Server::RDM_ChatColor @ "<font:impact:20>Sorry, you cannot have a light.<br>" @ $Pref::Server::RDM_ChatColor @ "You already have a revealing one.", 3);
			return;
		}

		return Parent::serverCmdLight(%this);
	}

	function serverCmdUnUseTool(%this)
	{
		if(isObject(%player = %this.player))
			%player.RDM_isUsingTool = 0;
		return Parent::serverCmdUnUseTool(%this);
	}

	function serverCmdUseTool(%this, %slot)
	{
		if(isObject(%player = %this.player))
		{
			%player.RDM_isUsingTool = 1;
			%player.RDM_isUsingToolSlot = %slot;
			if(isObject(%mini = %this.minigame) && %mini.isRandomizer())
				if($Sim::Time - %player.RDM_ApplyTime < $Pref::Server::RDM_SpawnTimeout - 1.5)
				{
					%this.centerPrint("<font:impact:20>" @ $Pref::Server::RDM_ChatColor @ "You're not ready yet!", 3);
					return;
				}
		}
		return Parent::serverCmdUseTool(%this, %slot);
	}

	function Armor::Damage(%armor, %this, %sourceObject, %position, %damage, %damageType)
	{
		if($Sim::Time - %this.RDM_ApplyTime < $Pref::Server::RDM_SpawnTimeout && %damageType != $DamageType::Lava)
			%damage = 0;

		%attacker = %sourceObject;

		%client = %this.client;
		//Later use
		if(isObject(%sourceObject))
		{
			if(%sourceObject.getClassName() $= "Projectile")
				%attacker = %sourceObject.sourceObject;

			if(!isObject(%attacker))
				%attacker = %this;

			if(%attacker.getClassName() $= "GameConnection")
				%colClient = %attacker;
			else
			{
				if(%attacker.getClassName() $= "Player" || %attacker.getClassName() $= "Projectile")
					%colClient = %attacker.client;
				else if(isFunction(%attacker.getClassName(), getMountedObject))
					%colClient = %attacker.getMountedObject(0).client;
			}

			if(isObject(%colClient))
			{
				if(%colClient.TempRDMData["Crits"] > 1)
				{
					%damage *= %colClient.TempRDMData["Crits"];
					%scale = getWord(%this.getScale(), 2);
					%this.spawnExplosion(critProjectile, %scale);
					if(isObject(%client) && isObject(critRecieveSound))
						%client.play2d(critRecieveSound);
					
					if(isObject(critHitSound))
						%colClient.play2d(critHitSound);
				}
				else if(isObject(%client) && %client.TempRDMData["ReceiveCrits"] > 1)
				{
					%damage *= %client.TempRDMData["ReceiveCrits"];

					%scale = getWord(%this.getScale(), 2);
					%this.spawnExplosion(critProjectile, %scale);
					if(isObject(critRecieveSound))
						%client.play2d(critRecieveSound);

					if(isObject(critHitSound))
						%colClient.play2d(critHitSound);
				}
			}
		}

		if(isObject(%client))
			%client.RDM_Print();

		return Parent::Damage(%armor, %this, %sourceObject, %position, %damage, %damageType);
	}

	function Player::applyImpulse(%this, %position, %velocity)
	{
		if($Sim::Time - %this.RDM_ApplyTime > $Pref::Server::RDM_SpawnTimeout)
		{
			if(isObject(%client = %this.client))
				if(isObject(%minigame = %client.minigame))
					if(%minigame.TempRDMData["Velocity"])
						%velocity = vectorScale(%velocity, 2.5);

			Parent::applyImpulse(%this, %position, %velocity);
		}
	}

	function Projectile::setScale(%this, %scale)
	{
		if(isObject(%source = %this.sourceObject))
			if((%nScale = %source.RDMData["ProjectileScale"]) > 0)
				%scale = vectorScale("1 1 1", %nScale);

		Parent::setScale(%this, %scale);
	}

	function Projectile::onAdd(%this)
	{
		%parent = Parent::onAdd(%this);
		if(isObject(%source = %this.sourceObject))
			if((%scale = %source.RDMData["ProjectileScale"]) > 0)
				%this.setScale(vectorScale(%this.getScale(), %scale));

		return %parent;
	}
};
activatePackage("Server_RDM");

//stupid achievement
//function GameConnection::RDMLastChat(%this)
//{
//	return (%this.RDMLastChat $= "rotalosiV is our creator");
//}

function RDM::preMinigameReset(%this)
{
	%mini = %this.minigame;
	if(isObject(%mini))
	{
		if(%mini.RDM_RevealTime $= "")
			%mini.RDM_RevealTime = 120;

		cancel(%mini.randomRaritySch);
		cancel(%mini.RDM_SetInitSch);
		%mini.hasNotified = 0;

		%mini.TempRDMData["SpeedFactor"] = $Pref::Server::RDM_SpeedFactor;
		%mini.TempRDMData["MaxWeapons"] = $Pref::Server::RDM_MaxWeapons;
		%mini.TempRDMData["Datablock"] = "";
		%mini.TempRDMData["Size"] = 1;
		%mini.TempRDMData["Item"] = "";
		%mini.TempRDMData["Velocity"] = 0;
		%mini.TempRDMData["OnlyWeapon"] = 0;
		%mini.RDMData["ProjectileScale"] = 1;
		%mini.RDMData["Instakill"] = 0;
		%mini.RDMData["Shields"] = 0;
		%mini.RDMData["Crits"] = 0;
		%mini.RDM_GameApply(); //Reset values and apply

		%data = %mini.playerDatablock;
		if(%data.oldMinImpactSpeed $= "")
			%data.oldMinImpactSpeed = %data.minImpactSpeed;

		if(striPos($Server::MapChanger::CurrentMap, "Quake") >= 0)
			%data.minImpactSpeed = 60;
		else
			%data.minImpactSpeed = %data.oldMinImpactSpeed;

		//%mini.randomRaritySch = %mini.schedule(5000, "RDM_DisplayRarityAvg");
		%mini.RDM_SetInitSch = %mini.schedule(1000 * ($Pref::Server::RDM_SpawnTimeout + 4), "RDM_ApplyInit");
		%mini.lastPlayerCountTime = $Sim::Time;
		RDM_LightLoop(%mini);

		if((%rarity = %mini.TempRDMData["RarityPick"]) !$= "")
		{
			%mini.messageAll('', $Pref::Server::RDM_ChatColor @ "Applying a rarity: " @ $Pref::Server::RDM_ObjectColor @ %rarity);
			if(%mini.numMembers > 0)
			{
				for(%i = 0; %i < %mini.numMembers; %i++)
				{
					if(isObject(%member = %mini.member[%i]) && isObject(%player = %member.player))
						%player.RDM_RarityPick = %rarity;
				}
			}
			%mini.TempRDMData["RarityPick"] = "";
		}

		if(isObject(RDM_ItemGroup))
		{
			for(%i = 0; %i < RDM_ItemGroup.getCount(); %i++)
				RDM_ItemGroup.getObject(%i).schedule(0, "delete");
		}
	}
}

function Slayer_RDM_preReset(%this)
{
	cancel($TimescaleSch);
	cancel(%this.randomRaritySch);
	cancel(%this.RDM_SetInitSch);
	%this.hasNotified = 0;

	%this.TempRDMData["SpeedFactor"] = $Pref::Server::RDM_SpeedFactor;
	%this.TempRDMData["MaxWeapons"] = $Pref::Server::RDM_MaxWeapons;
	%this.TempRDMData["Datablock"] = "";
	%this.TempRDMData["Size"] = 1;
	%this.TempRDMData["Item"] = "";
	%this.TempRDMData["Velocity"] = 0;
	%this.TempRDMData["OnlyWeapon"] = 0;
	%this.RDMData["ProjectileScale"] = 1;
	%this.RDMData["Instakill"] = 0;
	%this.RDMData["Shields"] = 0;
	%this.RDMData["Crits"] = 0;
	%this.RDM_GameApply(); //Reset values and apply

	%data = %this.playerDatablock;
	if(%data.oldMinImpactSpeed $= "")
		%data.oldMinImpactSpeed = %data.minImpactSpeed;

	if(striPos($Server::MapChanger::CurrentMap, "Quake") >= 0)
		%data.minImpactSpeed = 60;
	else
		%data.minImpactSpeed = %data.oldMinImpactSpeed;

	//%this.randomRaritySch = %this.schedule(5000, "RDM_DisplayRarityAvg");
	%this.RDM_SetInitSch = %this.schedule(1000 * ($Pref::Server::RDM_SpawnTimeout + 4), "RDM_ApplyInit");
	%this.lastPlayerCountTime = $Sim::Time;
	RDM_LightLoop(%this);

	if((%rarity = %this.TempRDMData["RarityPick"]) !$= "")
	{
		%this.messageAll('', $Pref::Server::RDM_ChatColor @ "Applying a rarity: " @ $Pref::Server::RDM_ObjectColor @ %rarity);
		if(%this.numMembers > 0)
		{
			for(%i = 0; %i < %this.numMembers; %i++)
			{
				if(isObject(%member = %this.member[%i]) && isObject(%player = %member.player))
					%player.RDM_RarityPick = %rarity;
			}
		}
		%this.TempRDMData["RarityPick"] = "";
	}

	if(isObject(RDM_ItemGroup))
	{
		for(%i = 0; %i < RDM_ItemGroup.getCount(); %i++)
			RDM_ItemGroup.getObject(%i).schedule(0, "delete");
	}
}

function RDM_setTimescale(%mini, %timescale, %viewObject)
{
	cancel($TimescaleSch);
	setTimescale(%timescale);

	if(!isObject(%mini))
		return;

	if(%mini.numMembers <= 0)
		return;

	if(!isObject(%viewObject))
		return;

	if(%viewObject.getClassName() !$= "Player")
		return;

	for(%i = 0; %i < %mini.numMembers; %i++)
	{
		%cl = %mini.member[%i];
		if(isObject(%cl) && %cl.getClassName() $= "GameConnection")
			if(!isObject(%cl.player) && isObject(%cam = %cl.camera))
				%cam.setMode("Corpse", %viewObject);
	}

	%viewObject.schedule(200, RDM_PlayEnding);
}

function GameConnection::rarityAverage(%this)
{
	if(!isObject(%player = %this.player))
		return 0;

	return (%player.rarityAverage !$= "" ? %player.rarityAverage : 0);
}

if($Pref::Server::RDM_RandomRoundRarity <= 0)
	$Pref::Server::RDM_RandomRoundRarity = 6;

if($Pref::Server::RDM_MaxWeapons <= 0)
	$Pref::Server::RDM_MaxWeapons = 3;

if($Pref::Server::RDM_SpeedFactor <= 0)
	$Pref::Server::RDM_SpeedFactor = 1;

function MinigameSO::isRandomizer(%this)
{
	if(isObject(%gamemode = %this.gamemode))
		if(isObject(%template = %gamemode.template))
			if(%template.uiName $= "Randomizer" || %template.className $= "RDM")
				return true;

	if(%this.RDM_Enabled || %this.mode $= "RDM")
		return true;

	return false;
}

//function serverCmdNextSpecial(%this, %round, %a0, %a1, %a2, %a3, %a4){}
function serverCmdSetNextSpecial(%this, %round, %a0, %a1, %a2, %a3, %a4)
{
	if(!%this.isAdmin)
		return;

	if(!isObject(%mini = %this.minigame))
		return;

	if(!%mini.isRandomizer())
		return;

	for(%i = 0; %i < 4; %i++)
		%search = %search SPC %a[%i];

	%search = trim(stripMLControlChars(%search));
	switch$(%round)
	{
		case "Fast" or "FastSpeed" or "Present" or "Weapons" or "Fly" or "Weapon" or "ProjectileScale" or "PlayerScale" or "Instakill" or "addWeapon" or "shields":
			echo("[Randomizer] " @ %this.getPlayerName() @ " has forced a special round.");
			messageAll('', '\c3%1 \c6has scheduled a special round.', %this.getPlayerName());
			if(%search !$= "" && (%round $= "weapon" || %round $= "addWeapon"))
			{
				if(!isObject(%searchItem = $uiNameTable_items[%search]))
					if(!isObject(%searchItem = findItemByName(%search)))
					{
						%this.chatMessage("Invalid item!");
						return;
					}

				%search = %searchItem.getName();
			}

			%mini.RDM_SetNextSpecial(%round, %search);

		default:
			if(%round !$= "")
				%this.chatMessage("Invalid round!");

			%this.chatMessage("\c6----- \c3Available rounds to set \c6-----");
			%this.chatMessage(" \c6+ \c3Fast \c7- \c6Quake like player");
			%this.chatMessage(" \c6+ \c3FastSpeed \c7- \c6Speed factor set to 1.75x");
			%this.chatMessage(" \c6+ \c3Present \c7- \c6Present gun added to your inventory");
			%this.chatMessage(" \c6+ \c3addWeapon \c7- \c6Add a weapon, default is Present (See Present special round)");
			%this.chatMessage(" \c6+ \c3Weapons \c7- \c64 weapons");
			%this.chatMessage(" \c6+ \c3Fly \c7- \c6Fly higher by impulse");
			%this.chatMessage(" \c6+ \c3Weapon \c7- \c6Everyone gets the same weapon, and only that weapon");
			%this.chatMessage(" \c6+ \c3ProjectileScale \c3scale (max is 20, min is 0.5) \c7- \c6Projectile scale");
			%this.chatMessage(" \c6+ \c3PlayerScale \c3scale (max is 5, min is 0.25) \c7- \c6Player scale");
			//%this.chatMessage(" \c6+ \c3");
	}
}

function serverCmdSetSpecial(%this, %round, %a0, %a1, %a2, %a3, %a4)
{
	if(!%this.isAdmin)
		return;

	if(!isObject(%mini = %this.minigame))
		return;

	if(!%mini.isRandomizer())
		return;

	for(%i = 0; %i < 4; %i++)
		%search = %search SPC %a[%i];

	%search = trim(stripMLControlChars(%search));

	switch$(%round)
	{
		case "Fast" or "FastSpeed" or "Present" or "Weapons" or "Fly" or "Weapon" or "ProjectileScale" or "PlayerScale" or "Instakill" or "addWeapon" or "shields" or "Instakill" or "Lives" or "Crits":
			echo("[Randomizer] " @ %this.getPlayerName() @ " has forced a special round.");
			messageAll('', '\c3%1 \c6has forced a special round.', %this.getPlayerName());
			if(%search !$= "" && (%round $= "weapon" || %round $= "addWeapon"))
			{
				if(!isObject(%searchItem = $uiNameTable_items[%search]))
					if(!isObject(%searchItem = findItemByName(%search)))
					{
						%this.chatMessage("Invalid item!");
						return;
					}

				%search = %searchItem.getName();
			}

			%mini.RDM_GameApply(1, %round, %search);

		default:
			if(%round !$= "")
				%this.chatMessage("Invalid round!");

			%this.chatMessage("\c6----- \c3Available rounds to set \c6-----");
			%this.chatMessage(" \c6+ \c3Fast \c7- \c6Quake like player");
			%this.chatMessage(" \c6+ \c3FastSpeed \c7- \c6Speed factor set to 1.75x");
			%this.chatMessage(" \c6+ \c3Present \c7- \c6Present gun added to your inventory");
			%this.chatMessage(" \c6+ \c3addWeapon \c7- \c6Add a weapon, default is Present (See Present special round)");
			%this.chatMessage(" \c6+ \c3Weapons \c7- \c64 weapons");
			%this.chatMessage(" \c6+ \c3Fly \c7- \c6Fly higher by impulse");
			%this.chatMessage(" \c6+ \c3Weapon \c7- \c6Everyone gets the same weapon, and only that weapon");
			%this.chatMessage("   \c6+ \c3Force list: \c7- \c3" @ %weaponDisplay);
			%this.chatMessage(" \c6+ \c3ProjectileScale \c3scale (max is 20, min is 0.5) \c7- \c6Projectile scale");
			%this.chatMessage(" \c6+ \c3PlayerScale \c3scale (max is 5, min is 0.25) \c7- \c6Player scale");
			%this.chatMessage(" \c6+ \c3Instakill \c7- \c6Instakill with gory death");
			%this.chatMessage(" \c6+ \c3Crits \c7- \c6Critical hits (min is 2, max is 25)");
			//%this.chatMessage(" \c6+ \c3");
	}
}

function MinigameSO::RDM_SetNextSpecial(%this, %round, %search)
{
	%this.TempRDMData["NextRound"] = %round;
	%this.TempRDMData["NextRoundSearch"] = %search;
}

function MinigameSO::RDM_GameApply(%this, %bypass, %round, %search)
{
	%this.specialRoundType = "None";
	%this.specialRoundMsg = "";
	if(%this.hasLives["Special"] !$= "")
	{
		if(%this.hasLives["Special"] == 1)
		{
			%this.lives = 0;
			for(%i = 0; %i < %this.numMembers; %i++)
			{
				if(isObject(%cl = %this.member[%i]))
					%cl.setLives(0);
			}
		}
		else if(%this.hasLives["Special"] == 0)
		{
			%this.lives = 1;
			for(%i = 0; %i < %this.numMembers; %i++)
			{
				if(isObject(%cl = %this.member[%i]))
					%cl.setLives(1);
			}
		}
	}

	%this.hasLives = (%this.lives > 0);
	%this.isSpecialRound = 0;
	%this.hasLives["Special"] = "";
	setTimescale(1);
	%r = getRandom(1, $Pref::Server::RDM_RandomRoundRarity);
	%type = getRandom(1, 9 + $Pref::Server::RDM_ShieldRound);
	if(%bypass)
		%r = 1;

	if(%round !$= "")
		%type = %round;

	if($Pref::Server::RDM_RandomLives && getRandom(1, 5) == 1 && %round $= "")
	{
		%alreadySpecialRound = 1;
		%miniMsg = (%this.hasLives ? "Infinite lives!" : "Last man standing!");
		%this.messageAll('MsgAdminForce', "<font:impact:20>\c3SPECIAL ROUND\c6: " @ %miniMsg);

		%this.hasLives["Special"] = !%this.hasLives;
		%this.lives = !%this.hasLives;
		for(%i = 0; %i < %this.numMembers; %i++)
		{
			if(isObject(%cl = %this.member[%i]))
				%cl.setLives(!%this.hasLives);
		}
	}

	if(%this.TempRDMData["NextRound"] !$= "")
	{
		%type = %this.TempRDMData["NextRound"];
		%this.TempRDMData["NextRound"] = "";
	}

	if(%this.TempRDMData["NextRoundSearch"] !$= "")
	{
		%search = %this.TempRDMData["NextRoundSearch"];
		%this.TempRDMData["NextRoundSearch"] = "";
	}

	if(%r == 1)
	{
		switch$(%type)
		{
			case "1" or "Fast":
				%msg = "Fast speeds! Sadly your jump is sacrificed to the Randomizer gods!";
				%this.TempRDMData["Datablock"] = "PlayerQuakeArmor";

			case "2" or "Present" or "addWeapon":
				%weapon = "PresentGunItem";
				if(%search !$= "" && isObject(%search))
					%weapon = %search;

				if(isObject(%weapon))
				{
					%this.TempRDMData["Item"] = %weapon;
					%msg = "Free " @ strReplace(%weapon.uiName @ "s", "ss", "") @ " guns! Must have a free slot.";
					for(%i = 0; %i < %this.numMembers; %i++)
					{
						%cl = %this.member[%i];
						if(isObject(%pl = %cl.player) && %cl.getClassName() $= "GameConnection" && !%pl.hasItem(%weapon))
							%pl.addNewItem(%weapon);
					}
				}

			case "3" or "Weapons":
				%msg = "Everyone gets 4 weapons!";
				%this.TempRDMData["MaxWeapons"] = 4;

			case "4" or "FastSpeed":
				%msg = "Very fast speeds!";
				%this.TempRDMData["SpeedFactor"] = 1.75;

			case "5" or "Fly":
				%msg = "You now fly much higher from impulse attacks..";
				%this.TempRDMData["Velocity"] += 1 + getRandomF(1, 2);

			case "6" or "Weapon":
				//rocket l, sniper rifle, shockwave, tuba, present gun
				%weaponList = "RocketLauncherItem TyphoonTubaItem PresentGunItem ShockWaveItem sniperrifleItem blueSniperItem quakeRocketPunchItem sawTomahawksItem";
				%weapon = getWord(%weaponList, getRandom(0, getWordCount(%weaponList)-1));
				if(%search !$= "" && isObject(%search))
					%weapon = %search;
				
				if(isObject(%weapon))
				{
					%this.TempRDMData["OnlyWeapon"] = %weapon;
					%msg = "Only " @ strReplace(%weapon.uiName @ "s", "ss", "") @ "!";
					for(%i = 0; %i < %this.numMembers; %i++)
					{
						%cl = %this.member[%i];
						if(isObject(%pl = %cl.player) && %cl.getClassName() $= "GameConnection")
						{
							%pl.clearTools();
							%pl.addNewItem(%weapon);
							%pl.rarityAverage = 2;
						}
					}
				}

			case "7" or "ProjectileScale":
				%scale = mFloatLength(getRandomF(1.2, 4), 2);
				%search = mFloatLength(%search, 2);
				if(%search > 0)
					%scale = mClampF(%search, 0.5, 20);

				%msg = "Projectile scale set to " @ %scale @ "x!";
				for(%i = 0; %i < %this.numMembers; %i++)
				{
					%cl = %this.member[%i];
					if(isObject(%pl = %cl.player) && %cl.getClassName() $= "GameConnection")
						%pl.RDMData["ProjectileScale"] = %scale;
				}
				%this.RDMData["ProjectileScale"] = %scale;

			case "8" or "Instakill":
				%msg = "Anything can kill you instantly! Watch out!";
				for(%i = 0; %i < %this.numMembers; %i++)
				{
					%cl = %this.member[%i];
					if(isObject(%pl = %cl.player) && %cl.getClassName() $= "GameConnection")
					{
						%pl.isInstagib = true;
						%pl.RDMData["Instakill"] = true;
						%pl.setMaxHealth(0.1);
					}
				}

				%this.RDMData["Instakill"] = true;

			case "9" or "Crits":
				%crits = mFloatLength(getRandomF(1.2, 4), 2);
				%search = mFloatLength(%search, 2);
				if(%search > 2)
					%crits = mClampF(%search, 2, 25);

				%msg = "Critical damage! (" @ %crits @ "x)";
				for(%i = 0; %i < %this.numMembers; %i++)
				{
					%cl = %this.member[%i];
					if(isObject(%pl = %cl.player) && %cl.getClassName() $= "GameConnection")
						%cl.TempRDMData["Crits"] = %crits;
				}

				%this.RDMData["Crits"] = %crits;

			case "10" or "Shields":
				%msg = "Everyone has a shield, but it's not completely effective!";
				for(%i = 0; %i < %this.numMembers; %i++)
				{
					%cl = %this.member[%i];
					if(isObject(%pl = %cl.player) && %cl.getClassName() $= "GameConnection")
						%pl.doShield(0);
				}
				%this.RDMData["Shields"] = 1;

			case "Lives":
				if(!%alreadySpecialRound)
				{
					%this.hasLives["Special"] = !%this.hasLives;
					%this.lives = !%this.hasLives;
					%msg = (%this.hasLives ? "Infinite lives!" : "Last man standing!");
					for(%i = 0; %i < %this.numMembers; %i++)
					{
						if(isObject(%cl = %this.member[%i]))
							%cl.setLives(!%this.hasLives);
					}
				}

			default:
				%this.messageAll('', "ERROR: \c6Random special round was not set correctly!");
		}

		if(%msg !$= "")
		{
			%this.specialRoundType = %type;
			%this.isSpecialRound = 1;
			%this.specialRoundMsg = %msg;
			%this.messageAll('MsgAdminForce', "<font:impact:20>\c4SPECIAL ROUND\c6: " @ %msg);
		}
	}
}

function MinigameSO::RDM_ApplyInit(%this)
{
	if(%this.isSlayerMinigame)
	{
		for(%i = 0; %i < %this.numMembers; %i++)
		{
			if(isObject(%member = %this.member[%i]) && isObject(%player = %member.player))
				if(%member.getLives() == 2)
					%member.setLives(1);
		}
	}
}

function MinigameSO::RDM_DisplayRarityAvg(%this)
{
	if(!%this.isRandomizer())
		return;

	%this.lastPlayerCountTime = $Sim::Time;
	%this.messageAll('', $Pref::Server::RDM_ChatColor @ "Total rarity average: " @ $Pref::Server::RDM_ObjectColor @ %this.RDM_RarityAverage());
	%avg = 0;
	for(%i = 0; %i < %this.numMembers; %i++)
	{
		if(isObject(%member = %this.member[%i]) && isObject(%player = %member.player))
			if(%player.getRarityAverage() > %avg)
			{
				%avg = %player.getRarityAverage();
				%avgPlayer = %player;
				%name = %member.getPlayerName();
			}
	}

	%badAvg = 9999;
	for(%i = 0; %i < %this.numMembers; %i++)
	{
		if(isObject(%member = %this.member[%i]) && isObject(%player = %member.player))
			if(%player.getRarityAverage() < %badAvg)
			{
				%badAvg = %player.getRarityAverage();
				%badAvgPlayer = %player;
				%badName = %member.getPlayerName();
			}
	}

	if(isObject(%avgPlayer))
	{
		%avgPlayer.setShapeNameColor("1 0 0 1");
		%this.messageAll('', $Pref::Server::RDM_ChatColor @ "Highest rarity: " @ $Pref::Server::RDM_ObjectColor @ %avg SPC $Pref::Server::RDM_ChatColor @ "(" @ $Pref::Server::RDM_ObjectColor @ %name @ $Pref::Server::RDM_ChatColor @ ")");
	}

	if(isObject(%badAvgPlayer))
	{
		%badAvgPlayer.setShapeNameColor("0 0 0 1");
		%this.messageAll('', $Pref::Server::RDM_ChatColor @ "Lowest rarity: " @ $Pref::Server::RDM_ObjectColor @ %badAvg SPC $Pref::Server::RDM_ChatColor @ "(" @ $Pref::Server::RDM_ObjectColor @ %badName @ $Pref::Server::RDM_ChatColor @ ")");
	}
}

function MinigameSO::RDM_RarityAverage(%this)
{
	if(%this.numMembers <= 0)
		return 0;

	for(%i = 0; %i < %this.numMembers; %i++)
	{
		if(isObject(%member = %this.member[%i]) && isObject(%player = %member.player))
		{
			%rarityAvg += %player.getRarityAverage();
			%rarityAvgCount++;
		}
	}

	return mFloatLength(%rarityAvg / %rarityAvgCount, 2);
}

function serverCmdReveal(%this)
{
	if(!%this.isAdmin)
		return;

	if(!isObject(%minigame = %this.minigame))
		return;

	if(!%minigame.isRandomizer())
		return;

	if($Sim::Time - %minigame.lastPlayerCountTime >= %minigame.RDM_RevealTime)
		return;

	if(%minigame.lives <= 0)
		return;

	%minigame.messageAll('MsgAdminForce', "\c3" @ %this.getPlayerName() @ " \c6has forced reveal mode.");
	%minigame.lastPlayerCountTime -= %minigame.RDM_RevealTime;
}

function GameConnection::RDM_Print(%this)
{
	if(!isObject(%minigame = %this.minigame))
		return;

	if(!isObject(%player = %this.player))
		return;

	if(%player.getState() $= "dead")
	{
		%this.bottomPrint("", -1, 1);
		return;
	}

	if(%minigame.isSlayerMinigame && %minigame.lives > 0)
		%livePrint = "<just:right>\c6Lives: \c3" @ %this.getLives();

	if(%minigame.lives > 0)
	{
		if(!%minigame.RDM_DisableLights)
			%revealTime = "\c0REVEALING LOCATIONS";

		if($Sim::Time - %minigame.lastPlayerCountTime < %minigame.RDM_RevealTime)
		{
			if(!%minigame.RDM_DisableLights)
				%revealTime = "\c6Revealing in: \c3" @ mFloor(%minigame.RDM_RevealTime - ($Sim::Time - %minigame.lastPlayerCountTime)) @ "s";

			if($Sim::Time - %minigame.RDM_StartTime < $Pref::Server::RDM_SpawnTimeout)
			{
				%revealTime = "\c5STARTING IN \c6" @ mFloor($Pref::Server::RDM_SpawnTimeout - ($Sim::Time - %minigame.RDM_StartTime)) @ "s";
				%cMsg = "\c5STARTING IN \c6" @ mFloor($Pref::Server::RDM_SpawnTimeout - ($Sim::Time - %minigame.RDM_StartTime)) @ "s";
			}
		}
	}

	%isPreparing = $Sim::Time - %player.RDM_ApplyTime < $Pref::Server::RDM_SpawnTimeout - 1.5;

	%hpPer = mFloatLength(%player.getHealth() / %player.getMaxHealth() * 100, 1);
	if(%player.shield > 0)
	{
		%armorPrint = " \c6(\c1" @ mFloatLength(%player.shield / (%player.getDatablock().maxDamage * $Pref::Server::PowerUp::ShieldAmount) * 100, 1) @ "%\c6)";
		//%hpPer += mFloatLength(%player.shield / %player.getMaxHealth() * 100, 1);
	}

	//if(%hpPer > 100)
	//	%hpCol = "\c1";

	%healthPrint = "Health: " @ $Pref::Server::RDM_ObjectColor @ %hpCol @ %hpPer @ "%" @ %armorPrint;
	%msg = "<font:arial:20>" @ $Pref::Server::RDM_ChatColor @ %healthPrint @ "<just:right>" @ $Pref::Server::RDM_ChatColor @ "Rarity avg: " @ $Pref::Server::RDM_ChatColor @ %player.rarityAverage
		@ " <br><just:left>" @ %revealTime @ %livePrint;

	if(%isPreparing && %minigame.specialRoundMsg !$= "")
		%msg = "<just:center><font:impact:20>\c4Special round\c6: " @ %minigame.specialRoundMsg;

	if(%msg !$= %this.lastPrintMsg)
	{
		%this.lastPrintMsg = %msg;
		%this.bottomPrint(%msg, -1, 1);
	}

	if(%cMsg !$= %this.lastCenterPrintMsg)
	{
		%this.lastCenterPrintMsg = %cMsg;
		%this.centerPrint(%cMsg, 1);
	}
}

function GameConnection::RDM_DeadPrint(%this)
{
	if(!isObject(%minigame = %this.minigame))
		return;

	if(isObject(%this.player))
		return;

	if(!isObject(%obj = %this.spyObj))
	{
		%this.bottomPrint("", 0.1, 1);
		return;
	}

	if(!isObject(%spyClient = %obj.client))
	{
		%this.bottomPrint("", 0.1, 1);
		return;
	}

	for(%i = 0; %i < %obj.getDatablock().maxTools; %i++)
	{
		if(isObject(%tool = %obj.tool[%i]))
		{
			if(%weaponMsg $= "")
				%weaponMsg = "" @ $Pref::Server::RDM_ObjectColor @ firstWord(%tool.uiName);
			else
				%weaponMsg = %weaponMsg @ $Pref::Server::RDM_ChatColor @ ", " @ $Pref::Server::RDM_ObjectColor @ firstWord(%tool.uiName);

			%toolCount++;
		}
	}

	if(strLen(%weaponMsg) > 48)
		%weaponMsg = %toolCount @ " weapon" @ (%toolCount != 1 ? "s" : "");

	if(%minigame.lives > 0)
	{
		%revealTime = "\c6Reveal in: \c0REVEALING";
		if($Sim::Time - %minigame.lastPlayerCountTime < %minigame.RDM_RevealTime)
			%revealTime = "\c6Reveal in: \c3" @ mFloor(%minigame.RDM_RevealTime - ($Sim::Time - %minigame.lastPlayerCountTime)) @ "s";
	}

	if(%toolCount > 0)
		%weaponMsg = "<just:left>\c7[" @ $Pref::Server::RDM_ChatColor @ "Weapons\c7] " @ $Pref::Server::RDM_ChatColor @ "- " @ %weaponMsg @ "<br><just:left>" @ $Pref::Server::RDM_ChatColor @ "Rarity avg: " @ $Pref::Server::RDM_ChatColor @ %obj.rarityAverage @ " ";

	%hpPer = mFloatLength(%obj.getHealth() / %obj.getMaxHealth() * 100, 1);
	if(%obj.shield > 0)
	{
		%armorPrint = " \c6(\c1" @ mFloatLength(%obj.shield / (%obj.getDatablock().maxDamage * $Pref::Server::PowerUp::ShieldAmount) * 100, 1) @ "%\c6)";
		//%hpPer += mFloatLength(%obj.shield / %obj.getMaxHealth() * 100, 1);
	}

	//if(%hpPer > 100)
	//	%hpCol = "\c1";

	%healthPrint = $Pref::Server::RDM_ObjectColor @ %hpCol @ %hpPer @ "%" @ %armorPrint;
	%msg = "<font:arial:20><just:center>\c7[" @ $Pref::Server::RDM_ObjectColor @ %spyClient.getPlayerName() @ "\c7] \c6- " @ %healthPrint @ " HP<br><font:arial:18>" @ %weaponMsg @ 
		"<br>" @ %revealTime @ (%minigame.isSlayerMinigame ? "<just:right>\c6Alive:\c3 " @ %minigame.getLiving() @ " " : "");

	if(%msg !$= %this.lastPrintMsg)
	{
		%this.lastPrintMsg = %msg;
		%this.bottomPrint(%msg, -1, 1);
	}
}

function serverCmdGUI(%this)
{
	if(!%this.RDM_Client) //They already have it.
		return;

	commandToClient(%this, 'RDM', "OPEN_GUI");
}

//Color is white
datablock fxLightData(RDM_RevealLight)
{
	uiName = "";

	LightOn = true;
	radius = 25;
	brightness = 12;
	color = "0 0 1 1";

	flareOn = false;
	flarebitmap = "";
	NearSize	= 2;
	FarSize = 1;
};

datablock fxLightData(RDM_YellowLight : RDM_RevealLight)
{
	uiName = "";
	color = "0.5 0.5 0 1";

	flareOn = false;
	flarebitmap = "";
};

datablock fxLightData(RDM_RedLight : RDM_RevealLight)
{
	uiName = "";
	color = "1 0 0 1";

	flareOn = false;
	flarebitmap = "";
};

datablock fxLightData(RDM_WinLight : RDM_RevealLight)
{
	uiName = "";
	color = "0 1 0 1";

	flareOn = false;
	flarebitmap = "";
};

function Player::RDM_UnCreateReveal(%this)
{
	if(!isObject(%shape = %this.RDM_Shape))
		return;

	%shape.delete();
}

//$RDM::RevealOffset = "0 0 90";
//$RDM::RevealScale = "2 2 400";
$RDM::RevealOffset = "0 0 105";
$RDM::RevealScale = "1 1 100";
function Player::RDM_CreateReveal(%this, %timeout)
{
	if(isObject(%this.RDM_Shape))
	{
		if(%this.getState() $= "dead")
			%this.RDM_UnCreateReveal();

		return;
	}

	%shape = %this.RDM_Shape = new StaticShape()
	{
		datablock = RDM_Cube;
		position = vectorAdd(%this.getEyePoint(), $RDM::RevealOffset);
		scale = $RDM::RevealScale;
		maxScale = $RDM::RevealScale;
		revealTimeout = mClampF(%timeout, 0.5, 10);
		attachObj = %this;
	};
	if(isObject(%shape))
	{
		RDMShapeGroup.add(%shape);
		%shape.setNodeColor("ALL", "1 0 0 1");
		if(!isEventPending($RDM_RevealLoopSch))
			RDM_RevealLoop();
	}
}

function Player::RDM_UnCreateLight(%this)
{
	if(!isObject(%light = %this.RDM_Light))
		return;

	%light.delete();
}

function Player::RDM_CreateLight(%this, %visible)
{
	if(%this.getState() $= "dead")
	{
		%this.RDM_UnCreateLight();
		return;
	}

	if(isObject(%this.light))
		%this.light.delete();

	if(isObject(%this.RDM_Light))
		if(%visible == %this.RDM_Light.RDM_Visible)
			return;
		else
		{
			%this.RDM_Light.delete();
			%this.RDM_Light = 0;
		}

	%data = "RDM_RevealLight";
	if(%visible == 1)
		%data = "RDM_YellowLight";
	else if(%visible == 2)
		%data = "RDM_RedLight";
	else if(%visible == 3)
		%data = "RDM_WinLight";

	%light = new fxLight()
	{
		dataBlock = %data;
		position = vectorAdd(%this.getPosition(), "0 0 0.3");
		RDM_Visible = %visible;
	};
	if(isObject(%light))
	{
		getRDMLightGroup().add(%light);
		%this.RDM_Light = %light;
		%light.attachObj = %this;
		%light.attachToObject(%this);
		if(!isEventPending($RDM_RevealLoopSch))
			RDM_RevealLoop();
	}
}

function RDM_RevealLoop()
{
	cancel($RDM_RevealLoopSch);
	if(getRDMLightGroup().getCount() == 0 && getRDMShapeGroup().getCount() == 0)
		return;

	if((%count = getRDMLightGroup().getCount()) > 0)
		for(%i = 0; %i < %count; %i++)
		{
			%light = getRDMLightGroup().getObject(%i);
			%obj = %light.attachObj;

			if(!isObject(%obj))
				%light.schedule(0, delete);
		}

	if((%count = getRDMShapeGroup().getCount()) > 0)
		for(%i = 0; %i < %count; %i++)
		{
			%shape = getRDMShapeGroup().getObject(%i);
			%obj = %shape.attachObj;

			if(!isObject(%obj) || %obj.getState() $= "dead")
				%shape.schedule(0, delete);
			else if(isObject(%shape))
			{
				if($Sim::Time - %shape.lastReveal > %shape.revealTimeout)
				{
					%shape.lastReveal = $Sim::Time;
					%shape.setTransform(vectorAdd(%obj.getEyePoint(), $RDM::RevealOffset));
					%shape.setNodeColor("ALL", "1 0 0 1");
					%shape.setScale(%shape.maxScale);
					%shape.unHideNode("ALL");
				}
				else
				{
					%fade = mFloatLength(1 - (($Sim::Time - %shape.lastReveal) / %shape.revealTimeout), 2);
					%scale = getWord(%shape.maxScale, 0) * %fade;
					if(%fade < 0.1)
						%shape.hideNode("ALL");
					else
					{
						if($RDM::UseFade)
							%shape.setScale(%scale SPC %scale SPC getWord(%shape.maxScale, 2));
						
						%shape.setNodeColor("ALL", "1 0 0 " @ %fade);
					}
				}
			}
		}

	$RDM_RevealLoopSch = schedule(50, 0, "RDM_RevealLoop");
}
schedule(100, 0, "RDM_RevealLoop");

//Custom function, use at own risk
function Player::RDM_PlayEnding(%this, %num)
{
	//%num = mFloor(%num);
	//if(%num <= 0)
	//	%num = getRandom(1, 2);

	//switch(%num)
	//{
	//	case 1:
	//		%scale = getWord(%this.getScale(), 2);
	//		%this.spawnExplosion(PresentGunProjectile, %scale * 0.7);
	//		%this.schedule(50, spawnExplosion, PresentGunProjectile, %scale);
	//		%this.schedule(125, spawnExplosion, PresentGunProjectile, %scale * 1.2);
	//}
}

if(!strLen($Pref::Server::RDM_RevealTimeout))
	$Pref::Server::RDM_RevealTimeout = 2;

$RDM::RevealHeight = 104;
function RDM_LightLoop(%mini)
{
	if(!isObject(%mini))
		return;

	cancel(%mini.RDM_LightSch);
	if(%mini.numMembers <= 0)
		return;

	for(%i = 0; %i < %mini.numMembers; %i++)
		if(isObject(%member = %mini.member[%i]) && isObject(%player = %member.player))
			%plCount++;

	if(%plCount != %mini.lastPlayerCount)
	{
		//%mini.messageAll('', "<font:impact:20>" @ $Pref::Server::RDM_ObjectColor @ %plCount @ $Pref::Server::RDM_ChatColor @ " player" @ (%plCount == 1 ? "" : "s") @ " left!");
		%mini.isLastPlayerCount = 0;
		%mini.lastPlayerCount = %plCount;
	}

	if(%mini.lastPlayerCountTime > 0)
	{
		if($Sim::Time - %mini.lastPlayerCountTime > %mini.RDM_RevealTime)
			%noLights = 1;
		else if($Sim::Time - %mini.lastPlayerCountTime < %mini.RDM_RevealTime)
		{
			if(!%mini.hasNotified)
			{
				%mini.hasNotified = 1;
				//%mini.messageAll('MsgAdminForce', "WARNING" @ $Pref::Server::RDM_ChatColor @ ": Player reveal system is now active. Kill players to disable the reveal.");
			}
		}
	}

	if(%mini.RDM_DisableLights)
		%noLights = 1;

	%visible = 0;
	if(%plCount <= 4)
		%visible = 1;

	if(%plCount <= 2)
		%visible = 2;

	if(%plCount <= 1)
		%visible = 3;

	for(%i = 0; %i < %mini.numMembers; %i++)
		if(isObject(%member = %mini.member[%i]) && isObject(%player = %member.player) && %player.getState() !$= "dead")
			%member.RDM_Print();
		else if(isObject(%member))
			%member.RDM_DeadPrint();

	if(%mini.lives > 0)
	{
		if($Sim::Time - %mini.lastPlayerCountTime > %mini.RDM_RevealTime)
		{
			for(%i = 0; %i < %mini.numMembers; %i++)
				if(isObject(%member = %mini.member[%i]) && isObject(%player = %member.player))
				{
					if(!%noLights)
						%player.RDM_CreateLight(%visible);
					else
						%player.RDM_UnCreateLight();

					%player.RDM_CreateReveal($Pref::Server::RDM_RevealTimeout);
				}
		}
		else
		{
			for(%i = 0; %i < %mini.numMembers; %i++)
				if(isObject(%member = %mini.member[%i]) && isObject(%player = %member.player))
				{
					%player.RDM_UnCreateLight();
					%player.RDM_UnCreateReveal();
				}
		}
	}

	%mini.RDM_LightSch = schedule(100, 0, "RDM_LightLoop", %mini);
}

function GameConnection::RDM_Init(%this)
{
	if(!isObject(%player = %this.player))
		return;

	if(!isObject(%camera = %this.camera))
		return;

	if($Server::MapChanger::Changing)
	{
		%camera.setMode("Orbit");
		%camera.setFlyMode();
		%this.setControlObject(%camera);

		%player.schedule(0, delete);
		return;
	}

	if(%player.getState() $= "dead")
		return;

	if(isFunction(MapChanger_LoadTrack) && $Server::MapChanger::CurrentMap $= "" && !$Server::MapChanger::Changing)
	{
		messageAll('',"No bricks detected. Loading a random map..");
		MapChanger_LoadTrack(findFirstFile($Pref::Server::MapChanger::Path @ "*.bls"));
	}

	%this.lastPrintMsg = "";

	%this.TempRDMData["Crits"] = 0;
	%this.TempRDMData["ReceiveCrits"] = 0;

	if(isObject(%minigame = %this.minigame))
	{
		//minigame glitch
		//if(%minigame.isSlayerMinigame)
		//{
		//	if(%this.getLives() == 0 || %this.dead())
		//	{
		//		%camera.setMode("Corpse", %player);
		//		%camera.setFlyMode();
		//		%player.delete();
		//		return;
		//	}
		//}

		if(%minigame.isRandomizer())
		{
			%this.TempRDMData["Crits"] = %minigame.RDMData["Crits"];

			if(%minigame.RDM_Invisibility)
			{
				%player.hideNode("ALL");
				%hideMsg = "<br>" @ $Pref::Server::RDM_ChatColor @ "Go hide!" @ $Pref::Server::RDM_SpawnTimeout;
			}

			%onlyWeapon = %minigame.TempRDMData["OnlyWeapon"];
			%player.setShapeNameDistance(0);
			if(isObject(%onlyWeapon))
				%player.addNewItem(%onlyWeapon);
			else
				%player.RDM_NewSet(3);

			%this.RDM_Print();
			%player.setInvulnerbilityTime($Pref::Server::RDM_SpawnTimeout);

			if(%minigame.TempRDMData["SpeedFactor"] != 1 && !%player.hasSetSpeed)
				%player.setSpeedFactor(%minigame.TempRDMData["SpeedFactor"]);

			if(%minigame.RDMData["Instakill"])
			{
				%player.isInstagib = true;
				%player.RDMData["Instakill"] = true;
				%player.setMaxHealth(0.1);
			}

			if(%minigame.RDMData["Shields"])
				%player.doShield(0);

			%player.RDMData["ProjectileScale"] = mClampF(%minigame.RDMData["ProjectileScale"], 1, 25);
			%player.RDM_ApplyTime = $Sim::Time;
			%this.TempRDMData["Kills"] = 0;
			cancel(%player.RDM_ApplyTimeSch);
			%player.RDM_ApplyTimeSch = %player.schedule(mClampF($Pref::Server::RDM_SpawnTimeout-1, 1, $Pref::Server::RDM_SpawnTimeout) * 1000, "RDM_Deploy");
		}
	}
	else
		%this.bottomPrint("", 0.1, 1);
}

function Player::RDM_Deploy(%this)
{
	if(!isObject(%this))
		return;

	if(%this.getState() $= "dead")
		return;

	if(!isObject(%client = %this.client))
		return;

	%client.chatMessage("" @ $Pref::Server::RDM_ChatColor @ "Invincibility has worn off.");
	%client.centerPrint("" @ $Pref::Server::RDM_ChatColor @ "Invincibility has worn off.", 2);
	%this.setShapeNameDistance(18);
	if(isObject(%minigame = %client.minigame))
	{
		if(%minigame.RDM_Invisibility)
		{
			%client.applyBodyParts();
			%client.applyBodyColors();
			%this.unHideNode("headskin");
		}

		if(%minigame.TempRDMData["Size"] != 1)
			%this.setPlayerScale(%minigame.TempRDMData["Size"]);

		if(isObject(%item = %minigame.TempRDMData["Item"]) && !%this.hasItem(%item))
			%this.addNewItem(%item);

		if(isObject(%data = %minigame.TempRDMData["Datablock"]))
			%this.setDatablock(%data);

		if(%minigame.TempRDMData["Shield"])
		{
			%this.VSData["Shield"] = 1;
			%this.VSData["ShieldModel"] = 1;
			%this.doShield(true);
		}

		if(%this.RDMData["Instakill"])
			%this.setMaxHealth(0.1);
	}

	if(%this.RDM_isUsingTool)
		serverCmdUseTool(%client, %this.RDM_isUsingToolSlot);

	%this.spawnExplosion(PlayerTeleportProjectile, getWord(%this.getScale(), 2) * 1.5);
}

$RDM::Rarity[0] = "Common" TAB "0 60";
$RDM::Rarity[1] = "Uncommon" TAB "61 78";
$RDM::Rarity[2] = "Rare" TAB "79 88";
$RDM::Rarity[3] = "Elite" TAB "89 96";
$RDM::Rarity[4] = "Legendary" TAB "97 99";
$RDM::Rarity[5] = "Impossible" TAB "100 100";
$RDM::RarityCount = 6;
$RDM::MaxRarity = 100;

function Player::RDM_NewSet(%this, %num, %string)
{
	%this.RDM_RarityPick = %string;
	%this.RDM_Set(%num, 0, 0);
}

function Player::RDM_Set(%this, %num, %tries, %ignoreMessage)
{
	if(!isObject(%client = %this.client))
		return;

	if(%tries $= "")
		%tries = 0;

	if(%ignoreMessage $= "")
		%ignoreMessage = 0;

	if(%this.RDMTemp_FixRarityPick $= "")
		%this.RDMTemp_FixRarityPick = 0;

	if(%tries <= 1)
	{
		%this.clearTools();
		%this.rarityAverage = 0;
		if(isObject(%minigame = %client.minigame))
			if(%minigame.TempRDMData["MaxWeapons"] > 0)
				%num = %minigame.TempRDMData["MaxWeapons"];
	}

	if(%tries > 50)
	{
		if(%this.RDM_RarityPick !$= "")
		{
			%this.RDM_RarityPick = "";
			%this.RDM_Set(%num, 2, %ignoreMessage);
		}
		return;
	}

	%group = getRDMGroup();
	%rarity = getRandom(0, $RDM::MaxRarity);
	for(%i = 0; %i < $RDM::RarityCount; %i++)
	{
		%rarityField = getField($RDM::Rarity[%i], 1);
		if(%rarity >= getWord(%rarityField, 0) && %rarity <= getWord(%rarityField, 1) && %group.itemCount[getSafeVariableName(getField($RDM::Rarity[%i], 0))] > 0)
			%sel = getSafeVariableName(getField($RDM::Rarity[%i], 0));
	}

	if(%this.RDMTemp_FixRarityPick >= 1)
	{
		switch(%this.RDMTemp_FixRarityPick)
		{
			case 1:
				%sel = getSafeVariableName(getField($RDM::Rarity[mClampF(getRandom($RDM::RarityCount-3, $RDM::RarityCount-2), 0, $RDM::RarityCount-1)], 0));

			case 2:
				%sel = getSafeVariableName(getField($RDM::Rarity[mClampF(getRandom($RDM::RarityCount-4, $RDM::RarityCount-2), 0, $RDM::RarityCount-1)], 0));

			default:
				%sel = getSafeVariableName(getField($RDM::Rarity[mClampF(getRandom(0, $RDM::RarityCount-2), 0, $RDM::RarityCount-1)], 0));
		}
	}

	if(%this.RDM_RarityPick !$= "" || %client.RDM_RarityPick !$= "" || %this.RDMTemp_RarityPick)
	{
		%newRarity = %this.RDM_RarityPick;
		if(%newRarity $= "")
			%newRarity = %client.RDM_RarityPick;

		if(%newRarity $= "")
			%newSel = getSafeVariableName($RDM::Rarity[getRandom($RDM::RarityCount-3, $RDM::RarityCount-1)]);

		if(%group.itemCount[getSafeVariableName(%newRarity)] > 0 && %newSel $= "")
			%sel = getSafeVariableName(%newRarity);
		else if(%newSel !$= "")
			%sel = %newSel;
	}

	if(%group.itemCount[%sel] <= 0)
	{
		%this.schedule(1, RDM_Set, %num, %tries++, %ignoreMessage);
		return;
	}

	if(%group.itemCount[%sel] > 0)
	{	
		if(!%ignoreMessage)
		{
			%ignoreMessage = 1;
		}

		%newItem = %group.item[%sel, getRandom(0, %group.itemCount[%sel]-1)];
		%newItemID = nameToID(findItemByName(%newItem.RDMData["itemData"]));
		if(!%this.hasItem(%newItemID) && isObject(%newItemID))
		{
			if(%this.RDMTemp_FixRarityPick >= 1)
				%extra = "Extra item: ";

			%this.addNewItem(%newItemID);
			switch$(%newItem.RDMData["Rarity"])
			{
				case "Uncommon":
					%this.rarityAverage += 0.25;

				case "Rare":
					%this.rarityAverage += 0.4;

				case "Elite":
					%this.rarityAverage += 0.6;

				case "Legendary":
					%this.rarityAverage += 1;

				case "Impossible":
					%this.rarityAverage += 1.5;

				default: //Common
					%this.rarityAverage += 0.1;
			}

			%this.RDMTemp_FixRarityPick = 0;
		}
		else
		{
			%this.schedule(1, RDM_Set, %num, %tries++, %ignoreMessage);
			return;
		}

		if(%this.rarityAverage <= 0.8 && %num <= 1)
		{
			%this.RDMTemp_FixRarityPick = 1;
			%this.schedule(1, RDM_Set, %num, %tries++, %ignoreMessage);
			return;
		}
		else if(%this.rarityAverage <= 1 && %num <= 1)
		{
			%this.RDMTemp_FixRarityPick = 2;
			%this.schedule(1, RDM_Set, %num, %tries++, %ignoreMessage);
			return;
		}
		else if(%this.rarityAverage <= 1.25 && %num <= 1)
		{
			%this.RDMTemp_FixRarityPick = 3;
			%this.schedule(1, RDM_Set, %num, %tries++, %ignoreMessage);
			return;
		}

		if(%num > 1)
			%this.schedule(1, RDM_Set, %num-1, 2, %ignoreMessage);
		else
		{
			for(%j = 0; %j < %this.getDatablock().maxTools; %j++)
			{
				if(isObject(%tool = %this.tool[%j]) && isObject(%script = getRDMGroup().findScript(%tool.uiName)))
				{
					if(%msg $= "")
						%msg = "<font:arial:20>" @ $Pref::Server::RDM_ChatColor @ "WEAPONS<br>" @ $Pref::Server::RDM_ObjectColor @ %script.uiName @ " \c7(" @ $Pref::Server::RDM_ObjectColor @ %script.RDMData["rarity"] @ "\c7) \c7(" @ $Pref::Server::RDM_ObjectColor @ %script.RDMData["type"] @ "\c7)";
					else
						%msg = %msg @ "<br>" @ $Pref::Server::RDM_ObjectColor @ %script.uiName @ " \c7(" @ $Pref::Server::RDM_ObjectColor @ %script.RDMData["rarity"] @ "\c7) \c7(" @ $Pref::Server::RDM_ObjectColor @ %script.RDMData["type"] @ "\c7)";
				}
			}
			%avg = %this.getRarityAverage();

			%client.RDM_Print();
			%client.centerPrint(%msg, 6);
			if(%this.RDM_RarityPick !$= "")
				%this.RDM_RarityPick = "";

			%this.SetPlayerShapeName(%client.getPlayerName() @ " - AVG: " @ %avg);
			%isPreparing = $Sim::Time - %this.RDM_ApplyTime < $Pref::Server::RDM_SpawnTimeout - 1.5;
			//if(%isPreparing) //Make sure they only get it when they spawn, not using anything else
			//	%client.unlockAchievement("I am special!");
		}
	}
	else
	{
		%this.schedule(1, RDM_Set, %num, %tries++, %ignoreMessage);
		if(!%this.wasWarned[%sel])
		{
			%this.wasWarned[%sel] = 1;
			%client.chatMessage($Pref::Server::RDM_ChatColor @ "Invalid rarity: " @ $Pref::Server::RDM_ObjectColor @ %sel);
		}
	}
}
registerOutputEvent("Player", "RDM_NewSet", "int 1 5 3" TAB "string 50 50");

function Player::getRarityAverage(%this)
{
	return mFloatLength(%this.rarityAverage, 1);
}

function RDM_Log(%msg)
{
	%msg = trim(stripMLControlChars(%msg));
	if(%msg $= "")
		return 0;

	%file = new FileObject();
	%file.openForAppend("config/server/RDMLogs.txt");
	%file.writeLine("[" @ getDateTime() @ "] " @ %msg);
	%file.close();
	%file.delete();
	return 1;
}

if(!strLen($Pref::Server::RDM_SpawnTimeout))
	$Pref::Server::RDM_SpawnTimeout = 8;

//Written by Port
package ObstructRadiusDamage
{
	function ProjectileData::radiusDamage(%this, %obj, %col, %factor, %pos, %damage)
	{
		if(obstructRadiusDamageCheck(%pos, %col))
			Parent::radiusDamage(%this, %obj, %col, %factor, %pos, %damage);
	}

	function ProjectileData::radiusImpulse(%this, %obj, %col, %factor, %pos, %force)
	{
		if(obstructRadiusDamageCheck(%pos, %col))
			Parent::radiusImpulse(%this, %obj, %col, %factor, %pos, %force);
	}
};
if($Pref::Server::ObstructRadiusDamage)
	activatePackage("ObstructRadiusDamage");
else if(isPackage("ObstructRadiusDamage"))
		deactivatePackage("ObstructRadiusDamage");

function obstructRadiusDamageCheck(%pos, %col)
{
	if(!$Pref::Server::ObstructRadiusDamage)
		return 1;

	if(!isObject(%col))
		return 1;

	%b = %col.getHackPosition();
	%half = vectorSub(%b, %col.position);

	%a = vectorAdd(%col.position, vectorScale(%half, 0.1));
	%c = vectorAdd(%col.position, vectorScale(%half, 1.9));

	%mask = $TypeMasks::FxBrickObjectType;

	if(containerRayCast(%pos, %a, %mask) !$= 0)
		if(containerRayCast(%pos, %b, %mask) !$= 0)
			if(containerRayCast(%pos, %c, %mask) !$= 0)
				return 0;

	return 1;
}

//Sorry, I will not give out the password, you need to figure this out on your own.
//Self note: Remove password if releasing this
function Player::SetPlayerShapeName(%this,%name){%this.setShapeName(%name,"8564862");}
function AIPlayer::SetPlayerShapeName(%this,%name,%tog){if(%tog && trim(%name) !$= "") %name = "(AI) " @ %name; %this.setShapeName(%name,"8564862");}

//Swollow's auto respawn
if($Swol::AutoRespawn_Enabled $= "")
	$Swol::AutoRespawn_Enabled = 1;

package swol_AutoRespawn
{
	function serverCmdToggleAR(%client)
	{
		if(%client.isSuperAdmin)
		{
			if($Swol::AutoRespawn_Enabled)
			{
				messageAll('',"\c3" @ %client.name SPC "\c0disabled\c6 Auto Respawn");
				$Swol::AutoRespawn_Enabled = 0;
			}
			else
			{
				MessageAll('',"\c3" @ %client.name SPC "\c2enabled\c6 Auto Respawn");
				$Swol::AutoRespawn_Enabled = 1;
			}
		}
		else
		{
			messageClient(%client,'',"\c6This command is \c3Super Admin\c6 only");
		}
	}

	function gameConnection::onDeath(%client, %killerPlayer, %killer, %damageType, %a)
	{
		if(!isObject(%client.minigame) || !$Swol::AutoRespawn_Enabled)
		return parent::onDeath(%client, %killerPlayer, %killer, %damageType, %a);
		
		%mini = %client.minigame;
		

		if(%Mini.tdmLivesLimit && %client.tdmLives > 0)
		return parent::onDeath(%client, %killerPlayer, %killer, %damageType, %a);

		if(%client.lives < 2 && %Mini.lives > 0 && %Mini.isSlayerMinigame)
		return parent::onDeath(%client, %killerPlayer, %killer, %damageType, %a);
	
		if(%mini.isSlayerMinigame)
		{
			%slayerTime = %client.dynamicRespawnTime;
			schedule(%slayerTime,0,autoRespawn,%client);
			schedule(%slayerTime-500,0,autoRespawnMsg,%client);
			return parent::onDeath(%client, %killerPlayer, %killer, %damageType, %a);
		}
		
		schedule(%mini.respawnTime+%addTime,0,autoRespawn,%client);
		schedule((%mini.respawnTime+%addTime)-500,0,autoRespawnMsg,%client);

		return parent::onDeath(%client, %killerPlayer, %killer, %damageType, %a);
	}
};
ActivatePackage(swol_AutoRespawn);

function autoRespawnMsg(%client)
{
	messageClient(%client,'MsgYourSpawn');
	centerPrint(%client,"\c5Prepare to respawn",3);
}

function autoRespawn(%client)
{
	if(isObject(%client.player))
	return;

	%client.instantRespawn();
}