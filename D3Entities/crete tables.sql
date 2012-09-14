drop table [ItemAttributes]
go
drop table [Items]
go
drop table [Runes]
go
drop table [Skills]
go
drop table [Quests]
go
alter table [CarrerProfiles] drop constraint FK_LastHeroPlayed
drop table  [HeroProfiles]
go
drop table [CarrerProfiles]
go

create table CarrerProfiles(
	Id int primary key,
	BattleTag nvarchar(50) unique, -- implicid unique index
	LastHeroPlayed int, -- fk constraint added later
	LastUpdated datetime not null,
	KillsMonsters int not null,
	KillsElites int not null,
	KillsHardcoreMonsters int not null,
	TimePlayedBarbarian float not null,
	TimePlayedDemonHunter float not null,
	TimePlayedMonk float not null,
	TimePlayedWitchDoctor float not null,
	TimePlayedWizard float not null, 
)

create table HeroProfiles(
	[Id] int primary key nonclustered, -- 1213
	[CarrerProfileId] int foreign key references CarrerProfiles([Id]) on delete cascade,
	[Name] nvarchar(16) not null, -- "Yharr"
	[Class] varchar(12) not null, -- "demon-hunter"
	[Gender] int not null, -- 0
	[HeroLevel] int not null, -- 1/0
	[Hardcore] bit not null, -- 1/0
	[DamageIncrease] float not null,-- 10.069999694824219
    [DamageReduction] float not null, -- 0.5207669734954834
    [CritChance] float not null, -- 0.10999999940395355,
    [Life] int not null, -- 17285,
    [Strength] int not null, -- 1007,
    [Dexterity] int not null, -- 295,
    [Intelligence] int not null, -- 156,
    [Vitality] int not null, -- 570,
    [Armor] int not null, -- 2934,
    [ColdResist] int not null, -- 0,
    [FireResist] int not null, -- 0,
    [LightningResist] int not null, -- 0,
    [PoisonResist] int not null, -- 0,
    [ArcaneResist] int not null, -- 0,
    [Damage] float not null, -- 4763.17
    [KillsElites] int not null,
	[LastUpdated] datetime not null
)
go

create clustered index IX_IdCareerProfileId on [HeroProfiles] ([Id], [CarrerProfileId]) 
go

alter table CarrerProfiles add constraint FK_LastHeroPlayed foreign key ([LastHeroPlayed]) references [HeroProfiles]([Id])
go

create table Skills(
	[Id] int primary key nonclustered,
	[HeroProfileId] int foreign key references HeroProfiles([Id]) on delete cascade,
	[Type] nchar(7) not null, -- active / passive
	[Slug] nvarchar(50) not null, -- "frenzy"
	[Name] nvarchar(50) not null, -- "Frenzy"
	[Icon] nvarchar(50) not null, -- "barbarian_frenzy"
	[TooltipParams] nvarchar(100) not null, -- "skill/barbarian/frenzy"
	[Description] nvarchar(500), -- "Generate: 3 Fury per attack\r\n\r\nSwing for 110% weapon damage. Frenzy attack speed increases by 15% with each swing. This effect can stack up to 5 times for a total bonus of 75% attack speed."
	[SimpleDescription] nvarchar(500), -- "Generate: 3 Fury per attack\r\n\r\nEnter a frenzied state that increases your attack speed with each hit."
	[Flavor] nvarchar(500), -- "The clans each paint their warriors in their own unique way. Some celebrate the mountain, others honor the fire, but the Targos clan worships the purifying virtue of pain."
)
go

create clustered index IX_IdHeroProfileId on [Skills] ([Id], [HeroProfileId]) 
go

create table Runes(
	[Id] int primary key nonclustered,
	[SkillId] int foreign key references Skills([Id]) on delete cascade,
	[Slug] nvarchar(50) not null, -- "frenzy-d"
	[Type] nchar(1) not null, -- "d"
	[Name] nvarchar(50) not null, -- "Smite"
	[Description] nvarchar(500), -- "Add a 20% chance to call down a bolt of lightning from above, stunning your target for 1.5 seconds."
	[SimpleDescription] nvarchar(500), -- "Add a chance to call down a bolt of lightning that stuns your target."
	[TooltipParams] nvarchar(100) not null, -- "rune/frenzy/d"
	[OrderIndex] int not null -- 3
)
go

create clustered index IX_IdSkillId on [Runes] ([Id], [SkillId]) 
go


create table Quests(
	[Id] int primary key nonclustered,
	[HeroProfileId] int foreign key references HeroProfiles([Id]) on delete cascade,
	[Act] int not null,
	[Hardcore] bit not null,
	[Difficulty] smallint not null,	
	[Slug] nvarchar(50) not null,
	[Name] nvarchar(50) not null,
	[Completed] bit not null
)
go

create clustered index IX_IdHeroProfileId on [Quests] ([Id], [HeroProfileId]) 
go

create table Items(
	[Id] int primary key nonclustered,
	[HeroProfileId] int foreign key references HeroProfiles([Id]) on delete cascade,
	-- [ItemLocation] nvarchar(20) not null, -- TODO
	[Name] nvarchar(50) not null,
	[Icon] nvarchar(50) not null,
	[DisplayColor] nvarchar(10) not null,
	[TooltipParams] nvarchar(100) not null,
	[RequiredLevel] int not null,
	[ItemLevel] int not null,
	[BonuAffixes] int not null,
	[DpsMin] float,
	[DpsMax] float,
	[AttacksPerSecondMin] float,
	[AttacksPerSecondMax] float,
	--TODO
	
/*{
   "name":"Exsanguinating Chopsword of Assault",
   "icon":"mightyweapon1h_202",
   "displayColor":"blue",
   "tooltipParams":"item-data/COGHsoAIEgcIBBXIGEoRHYQRdRUdnWyzFB2qXu51MA04kwNAAFAKYJMD",
   "requiredLevel":60,
   "itemLevel":61,
   "bonusAffixes":0,
   "minDamage":{
      "min":112,
      "max":112
   },
   "maxDamage":{
      "min":206,
      "max":206
   },
   "attributes":[
      "+211 Strength",
      "+112 Vitality",
      "2.80% of Damage Dealt Is Converted to Life"
   ],
   "attributesRaw":{
      "Attacks_Per_Second_Item":{
         "min":1.2999999523162842,
         "max":1.2999999523162842
      },
      "Damage_Weapon_Min#Physical":{
         "min":112,
         "max":112
      },
      "Damage_Weapon_Delta#Physical":{
         "min":94,
         "max":94
      },
      "Strength_Item":{
         "min":211,
         "max":211
      },
      "Durability_Cur":{
         "min":403,
         "max":403
      },
      "Durability_Max":{
         "min":403,
         "max":403
      },
      "Steal_Health_Percent":{
         "min":0.028,
         "max":0.028
      },
      "Vitality_Item":{
         "min":112,
         "max":112
      }
   },
   "socketEffects":[

   ],
   "salvage":[
      {
         "chance":1,
         "item":{
            "name":"Exquisite Essence",
            "icon":"crafting_tier_04b",
            "displayColor":"blue",
            "tooltipParams":"item/exquisite-essence"
         },
         "quantity":1
      },
      {
         "chance":0.14984228,
         "item":{
            "name":"Iridescent Tear",
            "icon":"crafting_tier_04c",
            "displayColor":"yellow",
            "tooltipParams":"item/iridescent-tear"
         },
         "quantity":1
      },
      {
         "chance":0.0001577287,
         "item":{
            "name":"Fiery Brimstone",
            "icon":"crafting_tier_04d",
            "displayColor":"orange",
            "tooltipParams":"item/fiery-brimstone"
         },
         "quantity":1
      }
   ]
}	*/
)
go

create clustered index IX_IdHeroProfileId on [Items] ([Id], [HeroProfileId]) 
go

create table ItemAttributes(
	[ItemId] int foreign key references Items([Id]) on delete cascade,
	[AttributeValue] nvarchar(50) not null
	primary key ([ItemId])
)
go