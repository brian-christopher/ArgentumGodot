namespace ArgentumOnline.Net;

public enum ServerPacketId 
{
	Logged,
	RemoveDialogs,
	RemoveCharDialog,
	NavigateToggle,
	Disconnect,
	CommerceEnd,
	BankEnd,
	CommerceInit,
	BankInit,
	UserCommerceInit,
	UserCommerceEnd,
	UserOfferConfirm,
	CommerceChat,
	ShowBlacksmithForm,
	ShowCarpenterForm,
	UpdateSta,
	UpdateMana,
	UpdateHP,
	UpdateGold,
	UpdateBankGold,
	UpdateExp,
	ChangeMap,
	PosUpdate,
	ChatOverHead,
	ConsoleMsg,
	GuildChat,
	ShowMessageBox,
	UserIndexInServer,
	UserCharIndexInServer,
	CharacterCreate,
	CharacterRemove,
	CharacterChangeNick,
	CharacterMove,
	ForceCharMove,
	CharacterChange,
	ObjectCreate,
	ObjectDelete,
	BlockPosition,
	PlayMIDI,
	PlayWave,
	GuildList,
	AreaChanged,
	PauseToggle,
	RainToggle,
	CreateFX,
	UpdateUserStats,
	WorkRequestTarget,
	ChangeInventorySlot,
	ChangeBankSlot,
	ChangeSpellSlot,
	Atributes,
	BlacksmithWeapons,
	BlacksmithArmors,
	CarpenterObjects,
	RestOK,
	ErrorMsg,
	Blind,
	Dumb,
	ShowSignal,
	ChangeNPCInventorySlot,
	UpdateHungerAndThirst,
	Fame,
	MiniStats,
	LevelUp,
	AddForumMsg,
	ShowForumForm,
	SetInvisible,
	DiceRoll,
	MeditateToggle,
	BlindNoMore,
	DumbNoMore,
	SendSkills,
	TrainerCreatureList,
	GuildNews,
	OfferDetails,
	AlianceProposalsList,
	PeaceProposalsList,
	CharacterInfo,
	GuildLeaderInfo,
	GuildMemberInfo,
	GuildDetails,
	ShowGuildFundationForm,
	ParalizeOK,
	ShowUserRequest,
	TradeOK,
	BankOK,
	ChangeUserTradeSlot,
	SendNight,
	Pong,
	UpdateTagAndStatus,
	
	//GM messages
	SpawnList,
	ShowSOSForm,
	ShowMOTDEditionForm,
	ShowGMPanelForm,
	UserNameList,
	ShowDenounces,
	RecordList,
	RecordDetails,

	ShowGuildAlign,
	ShowPartyForm,
	UpdateStrenghtAndDexterity,
	UpdateStrenght,
	UpdateDexterity,
	AddSlots,
	MultiMessage,
	StopWorking,
	CancelOfferItem
}

public enum ClientPacketId : byte {
	LoginExistingChar,
	ThrowDices,
	LoginNewChar,
	Talk,
	Yell,
	Whisper,
	Walk,
	RequestPositionUpdate,
	Attack,
	PickUp,
	SafeToggle,
	ResuscitationSafeToggle,
	RequestGuildLeaderInfo,
	RequestAtributes,
	RequestFame,
	RequestSkills,
	RequestMiniStats,
	CommerceEnd,
	UserCommerceEnd,
	UserCommerceConfirm,
	CommerceChat,
	BankEnd,
	UserCommerceOk,
	UserCommerceReject,
	Drop,
	CastSpell,
	LeftClick,
	DoubleClick,
	Work,
	UseSpellMacro,
	UseItem,
	CraftBlacksmith,
	CraftCarpenter,
	WorkLeftClick,
	CreateNewGuild,
	SpellInfo,
	EquipItem,
	ChangeHeading,
	ModifySkills,
	Train,
	CommerceBuy,
	BankExtractItem,
	CommerceSell,
	BankDeposit,
	ForumPost,
	MoveSpell,
	MoveBank,
	ClanCodexUpdate,
	UserCommerceOffer,
	GuildAcceptPeace,
	GuildRejectAlliance,
	GuildRejectPeace,
	GuildAcceptAlliance,
	GuildOfferPeace,
	GuildOfferAlliance,
	GuildAllianceDetails,
	GuildPeaceDetails,
	GuildRequestJoinerInfo,
	GuildAlliancePropList,
	GuildPeacePropList,
	GuildDeclareWar,
	GuildNewWebsite,
	GuildAcceptNewMember,
	GuildRejectNewMember,
	GuildKickMember,
	GuildUpdateNews,
	GuildMemberInfo,
	GuildOpenElections,
	GuildRequestMembership,
	GuildRequestDetails,
	Online,
	Quit,
	GuildLeave,
	RequestAccountState,
	PetStand,
	PetFollow,
	ReleasePet,
	TrainList,
	Rest,
	Meditate,
	Resucitate,
	Heal,
	Help,
	RequestStats,
	CommerceStart,
	BankStart,
	Enlist,
	Information,
	Reward,
	RequestMOTD,
	Uptime,
	PartyLeave,
	PartyCreate,
	PartyJoin,
	Inquiry,
	GuildMessage,
	PartyMessage,
	CentinelReport,
	GuildOnline,
	PartyOnline,
	CouncilMessage,
	RoleMasterRequest,
	GMRequest,
	BugReport,
	ChangeDescription,
	GuildVote,
	Punishments,
	ChangePassword,
	Gamble,
	InquiryVote,
	LeaveFaction,
	BankExtractGold,
	BankDepositGold,
	Denounce,
	GuildFundate,
	GuildFundation,
	PartyKick,
	PartySetLeader,
	PartyAcceptMember,
	Ping,
	RequestPartyForm,
	ItemUpgrade,
	GMCommands,
	InitCrafting,
	Home,
	ShowGuildNews,
	ShareNpc,
	StopSharingNpc,
	Consultation,
	MoveItem
}

enum EGMCommands {
	GM_MESSAGE = 1,            //GMSG
	SHOW_NAME,                 //SHOWNAME
	ONLINE_ROYAL_ARMY,         //ONLINEREAL
	ONLINE_CHAOS_LEGION,       //ONLINECAOS
	GO_NEARBY,                 //IRCERCA
	COMMENT,                   //REM
	SERVER_TIME,               //HORA
	WHERE,                     //DONDE
	CREATURES_IN_MAP,          //NENE
	WARP_ME_TO_TARGET,         //TELEPLOC
	WARP_CHAR,                 //TELEP
	SILENCE,                   //SILENCIAR
	SOS_SHOW_LIST,             //SHOW SOS
	SOS_REMOVE,                //SOSDONE
	GO_TO_CHAR,                //IRA
	INVISIBLE,                 //INVISIBLE
	GM_PANEL,                  //PANELGM
	REQUEST_USER_LIST,         //LISTUSU
	WORKING,                   //TRABAJANDO
	HIDING,                    //OCULTANDO
	JAIL,                      //CARCEL
	KILL_NPC,                  //RMATA
	WARN_USER,                 //ADVERTENCIA
	EDIT_CHAR,                 //MOD
	REQUEST_CHAR_INFO,         //INFO
	REQUEST_CHAR_STATS,        //STAT
	REQUEST_CHAR_GOLD,         //BAL
	REQUEST_CHAR_INVENTORY,    //INV
	REQUEST_CHAR_BANK,         //BOV
	REQUEST_CHAR_SKILLS,       //SKILLS
	REVIVE_CHAR,               //REVIVIR
	ONLINE_GM,                 //ONLINEGM
	ONLINE_MAP,                //ONLINEMAP
	FORGIVE,                   //PERDON
	KICK,                      //ECHAR
	EXECUTE,                   //EJECUTAR
	BAN_CHAR,                  //BAN
	UNBAN_CHAR,                //UNBAN
	NPC_FOLLOW,                //SEGUIR
	SUMMON_CHAR,               //SUM
	SPAWN_LIST_REQUEST,        //CC
	SPAWN_CREATURE,            //SPA
	RESET_NPC_INVENTORY,       //RESETINV
	CLEAN_WORLD,               //LIMPIAR
	SERVER_MESSAGE,            //RMSG
	NICK_TO_IP,                //NICK2IP
	IP_TO_NICK,                //IP2NICK
	GUILD_ONLINE_MEMBERS,      //ONCLAN
	TELEPORT_CREATE,           //CT
	TELEPORT_DESTROY,          //DT
	RAIN_TOGGLE,               //LLUVIA
	SET_CHAR_DESCRIPTION,      //SETDESC
	FORCE_MIDI_TO_MAP,         //FORCEMIDIMAP
	FORCE_WAVE_TO_MAP,         //FORCEWAVMAP
	ROYAL_ARMY_MESSAGE,        //REALMSG
	CHAOS_LEGION_MESSAGE,      //CAOSMSG
	CITIZEN_MESSAGE,           //CIUMSG
	CRIMINAL_MESSAGE,          //CRIMSG
	TALK_AS_NPC,               //TALKAS
	DESTROY_ALL_ITEMS_IN_AREA, //MASSDEST
	ACCEPT_ROYAL_COUNCIL_MEMBER, // /ACEPTCONSE
	ACCEPT_CHAOS_COUNCIL_MEMBER, // /ACEPTCONSECAOS
	ITEMS_IN_THE_FLOOR,        ///PISO
	MAKE_DUMB,                 ///ESTUPIDO
	MAKE_DUMB_NO_MORE,         ///NOESTUPIDO
	DUMP_IP_TABLES,            ///DUMPSECURITY
	COUNCIL_KICK,              ///KICKCONSE
	SET_TRIGGER,               ///TRIGGER
	ASK_TRIGGER,               ///TRIGGER with no args
	BANNED_IP_LIST,            ///BANIPLIST
	BANNED_IP_RELOAD,          ///BANIPRELOAD
	GUILD_MEMBER_LIST,         ///MIEMBROSCLAN
	GUILD_BAN,                 ///BANCLAN
	BAN_IP,                    ///BANIP
	UNBAN_IP,                  ///UNBANIP
	CREATE_ITEM,               ///CI
	DESTROY_ITEMS,             ///DEST
	CHAOS_LEGION_KICK,         ///NOCAOS
	ROYAL_ARMY_KICK,           ///NOREAL
	FORCE_MIDI_ALL,            ///FORCEMIDI
	FORCE_WAVE_ALL,            ///FORCEWAV
	REMOVE_PUNISHMENT,         ///BORRARPENA
	TILE_BLOCKED_TOGGLE,       ///BLOQ
	KILL_NPC_NO_RESPAWN,       ///MATA
	KILL_ALL_NEARBY_NPCS,      ///MASSKILL
	LAST_IP,                   ///LASTIP
	CHANGE_MOTD,               ///MOTDCAMBIA
	SET_MOTD,                  //ZMOTD
	SYSTEM_MESSAGE,            ///SMSG
	CREATE_NPC,                ///ACC
	CREATE_NPC_WITH_RESPAWN,   ///RACC
	IMPERIAL_ARMOUR,           ///AI1 - 4
	CHAOS_ARMOUR,              ///AC1 - 4
	NAVIGATE_TOGGLE,           ///NAVE
	SERVER_OPEN_TO_USERS_TOGGLE, // # /HABILITAR
	TURN_OFF_SERVER,           ///APAGAR
	TURN_CRIMINAL,             ///CONDEN
	RESET_FACTIONS,            ///RAJAR
	REMOVE_CHAR_FROM_GUILD,    ///RAJARCLAN
	REQUEST_CHAR_MAIL,         ///LASTEMAIL
	ALTER_PASSWORD,            ///APASS
	ALTER_MAIL,                ///AEMAIL
	ALTER_NAME,                ///ANAME
	TOGGLE_CENTINEL_ACTIVATED, ///CENTINELAACTIVADO
	DO_BACKUP,                 ///DOBACKUP
	SHOW_GUILD_MESSAGES,       ///SHOWCMSG
	SAVE_MAP,                  ///GUARDAMAPA
	CLEAN_SOS,                 ///BORRAR SOS
	SHOW_SERVER_FORM,          ///SHOW INT
	NIGHT,                     ///NOCHE
	KICK_ALL_CHARS,            ///ECHARTODOSPJS
	RELOAD_NPCS,               ///RELOADNPCS
	RELOAD_SERVER_INI,         ///RELOADSINI
	RELOAD_SPELLS,             ///RELOADHECHIZOS
	RELOAD_OBJECTS,            ///RELOADOBJ
	RESTART,                   ///REINICIAR
	RESET_AUTO_UPDATE,         ///AUTOUPDATE
	CHAT_COLOR,                ///CHATCOLOR
	IGNORED,                   ///IGNORADO
	CHECK_SLOT,                ///SLOT
	SET_INI_VAR,               ///SETINIVAR LLAVE CLAVE VALOR
	CREATE_PRETORIAN_CLAN,     ///CREARPRETORIANOS
	REMOVE_PRETORIAN_CLAN,     ///ELIMINARPRETORIANOS
	ENABLE_DENOUNCES,          ///DENUNCIAS
	SHOW_DENOUNCES_LIST,       ///SHOW DENUNCIAS
	MAP_MESSAGE,               ///MAPMSG
	SET_DIALOG,                ///SETDIALOG
	IMPERSONATE,               ///IMPERSONAR
	IMITATE,                   ///MIMETIZAR
	RECORD_ADD,
	RECORD_REMOVE,
	RECORD_ADD_OBS,
	RECORD_LIST_REQUEST,
	RECORD_DETAILS_REQUEST
}