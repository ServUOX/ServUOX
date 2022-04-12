using System;
using Server.Items;
using System.Collections.Generic;
using Server.Engines.ShameRevamped;

namespace Server.Mobiles
{
    [CorpseName("a mud pie corpse")]
    public class MudPie : BaseCreature
    {
        public static Dictionary<Mobile, Timer> Table { get; private set; }

        [Constructible]
        public MudPie()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.4, 0.2)
        {
            Name = "a mud pie";
            Body = 779;
            BaseSoundID = 422;

            Hue = 2012;

            SetStr(140, 210);
            SetDex(70, 100);
            SetInt(90, 110);

            SetHits(280, 340);

            SetDamage(9, 12);

            SetDamageType(ResistType.Phys, 80);
            SetDamageType(ResistType.Pois, 20);

            SetResist(ResistType.Phys, 30, 45);
            SetResist(ResistType.Fire, 35, 40);
            SetResist(ResistType.Cold, 30, 40);
            SetResist(ResistType.Pois, 35, 45);
            SetResist(ResistType.Engy, 40);

            SetSkill(SkillName.MagicResist, 65, 85);
            SetSkill(SkillName.Tactics, 65, 85);
            SetSkill(SkillName.Wrestling, 65, 85);

            Fame = 500;
            Karma = -500;

            PackItem(Loot.PackReg(1, 2));

            PackItem(Loot.PackGem(1, 2));

            PackItem(new ExecutionersCap());

            if (0.33 > Utility.RandomDouble())
                PackItem(new ExecutionersCap());

            SetSpecialAbility(SpecialAbility.StickySkin);
        }

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);

            if (0.05 > Utility.RandomDouble() && Region.Find(c.Location, c.Map).IsPartOf("Shame"))
                c.DropItem(new ShameCrystal());
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich, 1);
        }

        public MudPie(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadInt();
        }
    }

    [CorpseName("a stone elemental corpse")]
    public class StoneElemental : EarthElemental
    {
        [Constructible]
        public StoneElemental()
        {
            Name = "a stone elemental";
            Hue = 2401;

            SetStr(140, 210);
            SetDex(80, 110);
            SetInt(90, 120);

            SetHits(900, 1000);

            SetDamage(15, 17);

            SetDamageType(ResistType.Phys, 100);

            SetResist(ResistType.Phys, 60, 65);
            SetResist(ResistType.Fire, 50, 60);
            SetResist(ResistType.Cold, 45, 55);
            SetResist(ResistType.Pois, 55, 60);
            SetResist(ResistType.Engy, 45, 55);

            SetSkill(SkillName.MagicResist, 100.0);
            SetSkill(SkillName.Tactics, 80.0, 96.0);
            SetSkill(SkillName.Wrestling, 80.0, 97.0);

            Fame = 4000;
            Karma = -4000;

            PackItem(Loot.PackReg(1, 2));

            PackItem(Loot.PackGem(1, 2));

            PackItem(new Granite());
            PackItem(new Sand());

            SetSpecialAbility(SpecialAbility.ColossalRage);
        }

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);

            if (0.15 > Utility.RandomDouble() && Region.Find(c.Location, c.Map).IsPartOf("Shame"))
                c.DropItem(new ShameCrystal());
        }

        public override int TreasureMapLevel => 2;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich, 2);
        }

        public StoneElemental(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadInt();
        }
    }

    [CorpseName("a cave troll corpse")]
    public class CaveTroll : Troll
    {
        [CommandProperty(AccessLevel.GameMaster)]
        public ShameWall Wall { get; set; }

        [Constructible]
        public CaveTroll() : this(null)
        {
        }

        [Constructible]
        public CaveTroll(ShameWall wall)
        {
            Name = "a cave troll";
            BodyValue = 0x1;
            FightMode = FightMode.Aggressor;

            if (wall != null)
                Title = "the wall guardian";

            Hue = 638;
            Wall = wall;

            SetStr(180, 210);
            SetDex(107, 205);
            SetInt(40, 70);

            SetHits(638, 978);

            SetDamage(15, 17);

            SetDamageType(ResistType.Phys, 100);

            SetResist(ResistType.Phys, 55, 65);
            SetResist(ResistType.Fire, 45, 55);
            SetResist(ResistType.Cold, 45, 55);
            SetResist(ResistType.Pois, 35, 45);
            SetResist(ResistType.Engy, 35, 45);

            SetSkill(SkillName.MagicResist, 70, 90);
            SetSkill(SkillName.Tactics, 80, 110);
            SetSkill(SkillName.Wrestling, 80, 110);
            SetSkill(SkillName.DetectHidden, 100.0);

            Fame = 3500;
            Karma = -3500;
            PackItem(Loot.PackGem());

            PackItem(new Saltpeter(Utility.RandomMinMax(1, 5)));
            PackItem(new Potash(Utility.RandomMinMax(1, 5)));
            PackItem(new Charcoal(Utility.RandomMinMax(1, 5)));
            PackItem(new BlackPowder(Utility.RandomMinMax(1, 5)));

            SetWeaponAbility(WeaponAbility.ArmorIgnore);
        }

        public override MeatType MeatType => MeatType.Ribs;
        public override int Meat => 2;

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);

            if (Wall != null)
                Wall.OnTrollKilled();

            if (0.10 > Utility.RandomDouble() && Region.Find(c.Location, c.Map).IsPartOf("Shame"))
                c.DropItem(new ShameCrystal());
        }

        public override int TreasureMapLevel => 1;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich, 2);
        }

        public CaveTroll(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadInt();
        }
    }

    [CorpseName("a clay golem corpse")]
    public class ClayGolem : Golem
    {
        [Constructible]
        public ClayGolem()
        {
            Name = "a clay golem";
            Hue = 654;

            SetStr(450, 600);
            SetDex(100, 150);
            SetInt(100, 150);

            SetHits(700, 900);

            SetDamage(13, 24);

            SetDamageType(ResistType.Phys, 100);

            SetResist(ResistType.Phys, 45, 55);
            SetResist(ResistType.Fire, 50, 60);
            SetResist(ResistType.Cold, 45, 55);
            SetResist(ResistType.Pois, 99);
            SetResist(ResistType.Engy, 35, 45);

            SetSkill(SkillName.MagicResist, 150, 200);
            SetSkill(SkillName.Tactics, 80, 120);
            SetSkill(SkillName.Wrestling, 80, 110);
            SetSkill(SkillName.Parry, 70, 80);
            SetSkill(SkillName.DetectHidden, 70.0, 80.0);

            Fame = 4500;
            Karma = -4500;

            PackItem(new ExecutionersCap());
        }

        public override void SpawnPackItems()
        {
        }

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);

            if (0.2 > Utility.RandomDouble() && Region.Find(c.Location, c.Map).IsPartOf("Shame"))
                c.DropItem(new ShameCrystal());
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich, 2);
        }

        public ClayGolem(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadInt();
        }
    }

    [CorpseName("a greater earth elemental corpse")]
    public class GreaterEarthElemental : EarthElemental
    {
        [Constructible]
        public GreaterEarthElemental()
        {
            Name = "a greater earth elemental";
            Hue = 1143;

            SetHits(500, 600);

            SetDamageType(ResistType.Phys, 100);

            SetResist(ResistType.Phys, 50, 65);
            SetResist(ResistType.Fire, 35, 45);
            SetResist(ResistType.Cold, 35, 45);
            SetResist(ResistType.Pois, 45, 55);
            SetResist(ResistType.Engy, 25, 35);

            SetSkill(SkillName.MagicResist, 40, 70);
            SetSkill(SkillName.Tactics, 70, 90);
            SetSkill(SkillName.Wrestling, 80, 95);

            Fame = 2500;
            Karma = -2500;

            SetSpecialAbility(SpecialAbility.ColossalRage);
        }

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);

            if (0.08 > Utility.RandomDouble() && Region.Find(c.Location, c.Map).IsPartOf("Shame"))
                c.DropItem(new ShameCrystal());
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich, 1);
        }

        public GreaterEarthElemental(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadInt();
        }
    }

    [CorpseName("a mud elemental corpse")]
    public class MudElemental : EarthElemental
    {
        [Constructible]
        public MudElemental()
        {
            Name = "a mud elemental";
            Hue = 542;

            SetStr(400, 550);
            SetHits(650, 850);
            SetDamage(17, 19);

            SetDamageType(ResistType.Phys, 50);
            SetDamageType(ResistType.Fire, 50);

            SetResist(ResistType.Phys, 50, 65);
            SetResist(ResistType.Fire, 55, 65);
            SetResist(ResistType.Cold, 45, 50);
            SetResist(ResistType.Pois, 55, 65);
            SetResist(ResistType.Engy, 50, 60);

            SetSkill(SkillName.MagicResist, 100);
            SetSkill(SkillName.Tactics, 100);
            SetSkill(SkillName.Wrestling, 120);
            SetSkill(SkillName.Parry, 120);

            Fame = 3500;
            Karma = -3500;

            PackItem(new FertileDirt());
            PackItem(new ExecutionersCap());

            SetSpecialAbility(SpecialAbility.ColossalRage);
        }

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);

            if (0.08 > Utility.RandomDouble() && Region.Find(c.Location, c.Map).IsPartOf("Shame"))
                c.DropItem(new ShameCrystal());
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich, 2);
        }

        public MudElemental(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadInt();
        }
    }

    public class GreaterAirElemental : AirElemental
    {
        [Constructible]
        public GreaterAirElemental()
        {
            SetStr(250, 315);
            SetHits(800, 900);
            SetDamage(15, 17);

            SetDamageType(ResistType.Phys, 20);
            SetDamageType(ResistType.Cold, 40);
            SetDamageType(ResistType.Engy, 40);

            SetResist(ResistType.Phys, 75, 85);
            SetResist(ResistType.Fire, 55, 65);
            SetResist(ResistType.Cold, 55, 65);
            SetResist(ResistType.Pois, 55, 65);
            SetResist(ResistType.Engy, 45, 55);

            SetSkill(SkillName.MagicResist, 100, 120);
            SetSkill(SkillName.Tactics, 100, 120);
            SetSkill(SkillName.Wrestling, 100, 120);
            SetSkill(SkillName.Magery, 100, 120);
            SetSkill(SkillName.EvalInt, 100, 120);

            Fame = 4500;
            Karma = -4500;
        }

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);

            if (0.10 > Utility.RandomDouble() && Region.Find(c.Location, c.Map).IsPartOf("Shame"))
                c.DropItem(new ShameCrystal());
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich, 2);
        }

        public GreaterAirElemental(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadInt();
        }
    }

    [CorpseName("a molten earth elemental corpse")]
    public class MoltenEarthElemental : GreaterEarthElemental
    {
        [Constructible]
        public MoltenEarthElemental()
        {
            Hue = 442;
            Name = "a molten earth elemental";

            SetStr(400, 550);
            SetHits(1200, 1400);
            SetDamage(17, 19);

            SetDamageType(ResistType.Phys, 50);
            SetDamageType(ResistType.Fire, 50);

            SetResist(ResistType.Phys, 50, 70);
            SetResist(ResistType.Fire, 50, 60);
            SetResist(ResistType.Cold, 40, 50);
            SetResist(ResistType.Pois, 55, 65);
            SetResist(ResistType.Engy, 50, 60);

            SetSkill(SkillName.MagicResist, 100);
            SetSkill(SkillName.Tactics, 100);
            SetSkill(SkillName.Wrestling, 120);
            SetSkill(SkillName.Parry, 120);

            Fame = 5000;
            Karma = -5000;

            SetSpecialAbility(SpecialAbility.SearingWounds);
            SetSpecialAbility(SpecialAbility.DragonBreath);
        }

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);

            if (0.10 > Utility.RandomDouble() && Region.Find(c.Location, c.Map).IsPartOf("Shame"))
                c.DropItem(new ShameCrystal());
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich, 2);
        }

        public MoltenEarthElemental(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadInt();
        }
    }

    [CorpseName("a flame elemental corpse")]
    public class LesserFlameElemental : BaseCreature, IAuraCreature
    {
        [Constructible]
        public LesserFlameElemental()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.4, 0.2)
        {
            Name = "a flame elemental";
            Body = 15;
            BaseSoundID = 838;
            Hue = 1161;

            SetStr(420, 460);
            SetDex(160, 210);
            SetInt(120, 190);

            SetHits(700, 800);
            SetMana(1000, 1200);

            SetDamage(13, 15);

            SetDamageType(ResistType.Phys, 25);
            SetDamageType(ResistType.Fire, 75);

            SetResist(ResistType.Phys, 40, 60);
            SetResist(ResistType.Fire, 100);
            SetResist(ResistType.Cold, 30, 40);
            SetResist(ResistType.Pois, 60, 70);
            SetResist(ResistType.Engy, 60, 70);

            SetSkill(SkillName.MagicResist, 90, 140);
            SetSkill(SkillName.Tactics, 90, 130.0);
            SetSkill(SkillName.Wrestling, 90, 120);
            SetSkill(SkillName.Magery, 100, 145);
            SetSkill(SkillName.EvalInt, 90, 140);
            SetSkill(SkillName.Meditation, 80, 120);
            SetSkill(SkillName.Parry, 100, 120);

            Fame = 3500;
            Karma = -3500;

            PackItem(new SulfurousAsh(5));
            SetSpecialAbility(SpecialAbility.DragonBreath);
            SetAreaEffect(AreaEffect.AuraDamage);
        }

        public void AuraEffect(Mobile m)
        {
            m.SendLocalizedMessage(1008112); // The intense heat is damaging you!
        }

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);

            if (0.10 > Utility.RandomDouble() && Region.Find(c.Location, c.Map).IsPartOf("Shame"))
                c.DropItem(new ShameCrystal());
        }

        public override int TreasureMapLevel => 2;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich, 2);
        }

        public LesserFlameElemental(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadInt();
        }
    }

    [CorpseName("a wind elemental corpse")]
    public class LesserWindElemental : BaseCreature
    {
        [Constructible]
        public LesserWindElemental()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.4, 0.2)
        {
            Name = "a wind elemental";
            Body = 13;
            BaseSoundID = 655;
            Hue = 33765;

            SetStr(370, 460);
            SetDex(160, 250);
            SetInt(150, 220);

            SetHits(2500, 2600);
            SetMana(1000, 1300);

            SetDamage(15, 17);

            SetDamageType(ResistType.Phys, 20);
            SetDamageType(ResistType.Cold, 40);
            SetDamageType(ResistType.Engy, 40);

            SetResist(ResistType.Phys, 65, 75);
            SetResist(ResistType.Fire, 55, 65);
            SetResist(ResistType.Cold, 55, 65);
            SetResist(ResistType.Pois, 100);
            SetResist(ResistType.Engy, 60, 75);

            SetSkill(SkillName.MagicResist, 60, 80);
            SetSkill(SkillName.Tactics, 60, 80.0);
            SetSkill(SkillName.Wrestling, 60, 80);
            SetSkill(SkillName.Magery, 60, 80);
            SetSkill(SkillName.EvalInt, 60, 80);

            Fame = 3500;
            Karma = -3500;
        }

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);

            if (0.10 > Utility.RandomDouble() && Region.Find(c.Location, c.Map).IsPartOf("Shame"))
                c.DropItem(new ShameCrystal());
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich, 2);
        }

        public LesserWindElemental(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadInt();
        }
    }

    [CorpseName("an eternal gazer corpse")]
    public class EternalGazer : ElderGazer
    {
        [Constructible]
        public EternalGazer()
        {
            Name = "an eternal gazer";
            SetStr(450, 600);
            SetDex(125, 165);
            SetInt(350, 550);

            SetHits(7250, 7600);
            SetMana(2500, 2900);
            SetDamage(18, 21);

            SetDamageType(ResistType.Phys, 50);
            SetDamageType(ResistType.Engy, 50);

            SetResist(ResistType.Phys, 65, 75);
            SetResist(ResistType.Fire, 60, 70);
            SetResist(ResistType.Cold, 70, 75);
            SetResist(ResistType.Pois, 65, 75);
            SetResist(ResistType.Engy, 65, 75);

            SetSkill(SkillName.MagicResist, 125, 140);
            SetSkill(SkillName.Tactics, 115, 130);
            SetSkill(SkillName.Wrestling, 110, 130);
            SetSkill(SkillName.Anatomy, 75, 90);
            SetSkill(SkillName.Magery, 120, 130);
            SetSkill(SkillName.EvalInt, 120, 130);
        }

        public override MeatType MeatType => MeatType.Ribs;
        public override int Meat => 1;

        public override void AlterSpellDamageFrom(Mobile from, ref int damage)
        {
            if (from is BaseCreature && (((BaseCreature)from).Summoned || ((BaseCreature)from).Controlled))
                damage /= 2;

            base.AlterSpellDamageFrom(from, ref damage);
        }

        public override void AlterMeleeDamageFrom(Mobile from, ref int damage)
        {
            if (from is BaseCreature && (((BaseCreature)from).Summoned || ((BaseCreature)from).Controlled))
                damage /= 2;

            base.AlterMeleeDamageFrom(from, ref damage);
        }

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);

            if (0.15 > Utility.RandomDouble() && Region.Find(c.Location, c.Map).IsPartOf("Shame"))
                c.DropItem(new ShameCrystal());
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich, 3);
        }

        public EternalGazer(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadInt();
        }
    }

    [CorpseName("a burning mage corpse")]
    public class BurningMage : BaseCreature
    {
        public static Dictionary<Mobile, Timer> Table { get; private set; }

        [Constructible]
        public BurningMage() : base(AIType.AI_Mage, FightMode.Weakest, 10, 1, 0.4, 0.2)
        {
            Name = NameList.RandomName("male");
            Title = "the burning";
            SetStr(100, 125);

            BodyValue = 0x190;
            Hue = 1281;

            SetHits(3000);
            SetMana(600, 800);
            SetDamage(10, 15);

            SetDamageType(ResistType.Phys, 50);
            SetDamageType(ResistType.Fire, 50);

            SetResist(ResistType.Phys, 50, 60);
            SetResist(ResistType.Fire, 50, 60);
            SetResist(ResistType.Cold, 50, 60);
            SetResist(ResistType.Pois, 50, 60);
            SetResist(ResistType.Engy, 50, 60);

            SetSkill(SkillName.MagicResist, 125, 140);
            SetSkill(SkillName.Tactics, 100, 120);
            SetSkill(SkillName.Wrestling, 110, 130);
            SetSkill(SkillName.Magery, 120, 130);
            SetSkill(SkillName.EvalInt, 120, 130);

            AddItem(new Robe(1156));
            AddItem(new Sandals());

            PackItem(Loot.PackReg(31));

            Utility.AssignRandomHair(this);

            Fame = 22000;
            Karma = -22000;

            SetSpecialAbility(SpecialAbility.DragonBreath);
        }

        public override bool CanRummageCorpses => true;
        public override bool AlwaysMurderer => true;

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);

            if (0.33 > Utility.RandomDouble() && Region.Find(c.Location, c.Map).IsPartOf("Shame"))
                c.DropItem(new ShameCrystal(4));
        }

        public override void OnDamagedBySpell(Mobile from)
        {
            base.OnDamagedBySpell(from);

            if (!IsUnderEffects(from) && 0.50 > Utility.RandomDouble())
            {
                DoEffects(from);
            }
        }

        public static bool IsUnderEffects(Mobile from)
        {
            return from != null && Table != null && Table.ContainsKey(from);
        }

        public void DoEffects(Mobile from)
        {
            if (Table == null)
                Table = new Dictionary<Mobile, Timer>();

            if (!Table.ContainsKey(from))
            {
                Table[from] = Timer.DelayCall(TimeSpan.FromSeconds(1.5), TimeSpan.FromSeconds(1.5), new TimerStateCallback(SapMana), new object[] { from, this });
                Table[from].Start();

                from.SendLocalizedMessage(1151482); // Your mana has been tainted!
                from.SendLocalizedMessage(1151485); // Your mana is being diverted.
            }
        }

        private static void SapMana(object o)
        {
            object[] objs = o as object[];
            Mobile from = objs[0] as Mobile;
            Mobile mob = objs[1] as Mobile;

            if (IsUnderEffects(from))
            {
                if (mob.Alive && from.Alive)
                {
                    from.SendLocalizedMessage(1151484); // You feel extra mana being drawn from you.
                    from.SendLocalizedMessage(1151481); // Channeling the corrupted mana has damaged you!

                    int toSap = Math.Min(from.Mana, Utility.RandomMinMax(30, 40));
                    from.Mana -= toSap;

                    AOS.Damage(from, mob, Math.Max(1, toSap / 10), false, 0, 0, 0, 0, 0, 0, 100, false, false, false);

                    if (0.5 > Utility.RandomDouble())
                        EndEffects(from);
                }
                else
                    EndEffects(from);
            }
        }

        public static void EndEffects(Mobile from)
        {
            if (IsUnderEffects(from))
            {
                Table[from].Stop();
                Table.Remove(from);

                from.SendLocalizedMessage(1151486); // Your mana is no longer being diverted.
                from.SendLocalizedMessage(1151483); // Your mana is no longer corrupted.
            }
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.UltraRich, 2);
            AddLoot(LootPack.HighScrolls, Utility.RandomMinMax(5, 20));
        }

        public BurningMage(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadInt();
        }
    }

    [CorpseName("a crazed corpse")]
    public class CrazedMage : BaseCreature
    {
        public static Dictionary<Mobile, Timer> Table { get; private set; }

        [Constructible]
        public CrazedMage() : base(AIType.AI_Mystic, FightMode.Weakest, 10, 1, 0.4, 0.2)
        {
            Name = NameList.RandomName("male");
            Title = "the crazed";

            BodyValue = 0x190;
            SetStr(225, 400);

            SetHits(3500);
            SetMana(600, 800);
            SetDamage(15, 21);

            SetDamageType(ResistType.Phys, 100);

            SetResist(ResistType.Phys, 60, 80);
            SetResist(ResistType.Fire, 50, 60);
            SetResist(ResistType.Cold, 50, 60);
            SetResist(ResistType.Pois, 50, 60);
            SetResist(ResistType.Engy, 50, 60);

            SetSkill(SkillName.MagicResist, 125, 140);
            SetSkill(SkillName.Tactics, 100, 120);
            SetSkill(SkillName.Wrestling, 140);
            SetSkill(SkillName.Anatomy, 100, 120);
            SetSkill(SkillName.Magery, 100, 110);
            SetSkill(SkillName.EvalInt, 100, 110);

            AddItem(new Robe(1157));
            AddItem(new Sandals());

            Utility.AssignRandomHair(this);
            Hue = Race.RandomSkinHue();

            Fame = 15000;
            Karma = -15000;
        }

        public override bool CanRummageCorpses => true;
        public override bool AlwaysMurderer => true;

        public override void OnDamagedBySpell(Mobile from)
        {
            base.OnDamagedBySpell(from);

            if (!IsUnderEffects(from) && 0.50 > Utility.RandomDouble())
            {
                DoEffects(from);
            }
        }

        public static bool IsUnderEffects(Mobile from)
        {
            return from != null && Table != null && Table.ContainsKey(from);
        }

        public void DoEffects(Mobile from)
        {
            if (Table == null)
                Table = new Dictionary<Mobile, Timer>();

            if (!Table.ContainsKey(from))
            {
                Table[from] = Timer.DelayCall(TimeSpan.FromSeconds(1.5), TimeSpan.FromSeconds(1.5), new TimerStateCallback(SapMana), new object[] { from, this });
                Table[from].Start();

                from.SendLocalizedMessage(1151482); // Your mana has been tainted!
                from.SendLocalizedMessage(1151485); // Your mana is being diverted.
            }
        }

        private static void SapMana(object o)
        {
            object[] objs = o as object[];
            Mobile from = objs[0] as Mobile;
            Mobile mob = objs[1] as Mobile;

            if (IsUnderEffects(from))
            {
                if (mob.Alive && from.Alive)
                {
                    from.SendLocalizedMessage(1151484); // You feel extra mana being drawn from you.
                    from.SendLocalizedMessage(1151481); // Channeling the corrupted mana has damaged you!

                    int toSap = Math.Min(from.Mana, Utility.RandomMinMax(30, 40));
                    from.Mana -= toSap;

                    AOS.Damage(from, mob, Math.Max(1, toSap / 10), false, 0, 0, 0, 0, 0, 0, 100, false, false, false);

                    if (0.5 > Utility.RandomDouble())
                        EndEffects(from);
                }
                else
                    EndEffects(from);
            }
        }

        public static void EndEffects(Mobile from)
        {
            if (IsUnderEffects(from))
            {
                Table[from].Stop();
                Table.Remove(from);

                from.SendLocalizedMessage(1151486); // Your mana is no longer being diverted.
                from.SendLocalizedMessage(1151483); // Your mana is no longer corrupted.
            }
        }

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);

            if (0.33 > Utility.RandomDouble() && Region.Find(c.Location, c.Map).IsPartOf("Shame"))
                c.DropItem(new ShameCrystal(5));
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich, 2);
        }

        public CrazedMage(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadInt();
        }
    }

    [CorpseName("a corrupted mage corpse")]
    public class CorruptedMage : EvilMage
    {
        public static Dictionary<Mobile, Timer> Table { get; private set; }

        [Constructible]
        public CorruptedMage()
        {
            Title = "the corrupted mage";

            SetStr(150, 170);
            SetInt(100, 120);
            SetDex(110, 120);

            SetHits(1200, 1250);
            SetMana(800, 900);
            SetDamage(14, 17);

            SetDamageType(ResistType.Phys, 100);

            SetResist(ResistType.Phys, 55, 70);
            SetResist(ResistType.Fire, 70, 80);
            SetResist(ResistType.Cold, 60, 70);
            SetResist(ResistType.Pois, 60, 70);
            SetResist(ResistType.Engy, 65, 75);

            SetSkill(SkillName.MagicResist, 115, 120);
            SetSkill(SkillName.Tactics, 110, 120);
            SetSkill(SkillName.Wrestling, 100, 110);
            SetSkill(SkillName.Magery, 120, 130);
            SetSkill(SkillName.EvalInt, 120, 130);
            SetSkill(SkillName.Meditation, 100, 110);
        }

        public override bool CanRummageCorpses => true;
        public override bool AlwaysMurderer => true;

        public override void OnDamagedBySpell(Mobile from)
        {
            base.OnDamagedBySpell(from);

            if (!IsUnderEffects(from) && 0.10 > Utility.RandomDouble())
            {
                DoEffects(from);
            }
        }

        public static bool IsUnderEffects(Mobile from)
        {
            return from != null && Table != null && Table.ContainsKey(from);
        }

        public void DoEffects(Mobile from)
        {
            if (Table == null)
                Table = new Dictionary<Mobile, Timer>();

            if (!Table.ContainsKey(from))
            {
                Table[from] = Timer.DelayCall(TimeSpan.FromSeconds(1.5), TimeSpan.FromSeconds(1.5), new TimerStateCallback(SapMana), new object[] { from, this });
                Table[from].Start();

                from.SendLocalizedMessage(1151482); // Your mana has been tainted!
                from.SendLocalizedMessage(1151485); // Your mana is being diverted.
            }
        }

        private static void SapMana(object o)
        {
            object[] objs = o as object[];
            Mobile from = objs[0] as Mobile;
            Mobile mob = objs[1] as Mobile;

            if (IsUnderEffects(from))
            {
                if (mob.Alive && from.Alive)
                {
                    from.SendLocalizedMessage(1151484); // You feel extra mana being drawn from you.
                    from.SendLocalizedMessage(1151481); // Channeling the corrupted mana has damaged you!

                    int toSap = Math.Min(from.Mana, Utility.RandomMinMax(30, 40));
                    from.Mana -= toSap;

                    AOS.Damage(from, mob, Math.Max(1, toSap / 10), false, 0, 0, 0, 0, 0, 0, 100, false, false, false);

                    if (0.5 > Utility.RandomDouble())
                        EndEffects(from);
                }
                else
                    EndEffects(from);
            }
        }

        public static void EndEffects(Mobile from)
        {
            if (IsUnderEffects(from))
            {
                Table[from].Stop();
                Table.Remove(from);

                from.SendLocalizedMessage(1151486); // Your mana is no longer being diverted.
                from.SendLocalizedMessage(1151483); // Your mana is no longer corrupted.
            }
        }

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);

            if (0.33 > Utility.RandomDouble() && Region.Find(c.Location, c.Map).IsPartOf("Shame"))
                c.DropItem(new ShameCrystal(3));
        }

        public override int TreasureMapLevel => 2;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich, 2);
        }

        public CorruptedMage(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadInt();
        }
    }

    [CorpseName("a vile mage corpse")]
    public class VileMage : CorruptedMage
    {
        [Constructible]
        public VileMage()
        {
            Title = "the vile mage";

            SetStr(150, 170);
            SetInt(150, 170);
            SetDex(100, 110);

            SetHits(500, 900);
            SetMana(550, 600);
            SetDamage(11, 13);

            SetDamageType(ResistType.Phys, 100);

            SetResist(ResistType.Phys, 55, 70);
            SetResist(ResistType.Fire, 55, 65);
            SetResist(ResistType.Cold, 60, 70);
            SetResist(ResistType.Pois, 55, 65);
            SetResist(ResistType.Engy, 65, 75);

            SetSkill(SkillName.MagicResist, 110, 115);
            SetSkill(SkillName.Tactics, 110, 115);
            SetSkill(SkillName.Wrestling, 100, 110);
            SetSkill(SkillName.Magery, 110, 115);
            SetSkill(SkillName.EvalInt, 115, 125);
        }

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);

            if (0.33 > Utility.RandomDouble() && Region.Find(c.Location, c.Map).IsPartOf("Shame"))
                c.DropItem(new ShameCrystal(3));
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich, 2);
        }

        public VileMage(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadInt();
        }
    }

    [CorpseName("a chaos vortex corpse")]
    public class ChaosVortex : BaseCreature
    {
        [Constructible]
        public ChaosVortex()
            : base(AIType.AI_Melee, FightMode.Weakest, 10, 1, 0.4, 0.2)
        {
            Name = "a chaos vortex";
            Body = 164;
            Hue = 34212;

            SetStr(450);
            SetDex(200);
            SetInt(100);

            SetHits(27000);
            SetMana(0);

            SetDamage(21, 23);

            SetDamageType(ResistType.Phys, 20);
            SetDamageType(ResistType.Fire, 20);
            SetDamageType(ResistType.Cold, 20);
            SetDamageType(ResistType.Pois, 20);
            SetDamageType(ResistType.Engy, 20);

            SetResist(ResistType.Phys, 65, 75);
            SetResist(ResistType.Fire, 65, 75);
            SetResist(ResistType.Cold, 65, 75);
            SetResist(ResistType.Pois, 65, 75);
            SetResist(ResistType.Engy, 65, 75);

            SetSkill(SkillName.MagicResist, 100, 110);
            SetSkill(SkillName.Tactics, 110, 130);
            SetSkill(SkillName.Wrestling, 124, 140);

            Fame = 22500;
            Karma = -22500;
        }

        public override int GetAngerSound()
        {
            return 0x15;
        }

        public override int GetAttackSound()
        {
            return 0x28;
        }

        public override bool AlwaysMurderer => true;
        public override bool BleedImmunity => true;
        public override Poison PoisonImmunity => Poison.Lethal;

        private DateTime NextTeleport { get; set; }

        public override void AlterSpellDamageFrom(Mobile from, ref int damage)
        {
            if (from is BaseCreature && (((BaseCreature)from).Summoned || ((BaseCreature)from).Controlled))
                damage /= 2;

            if (NextTeleport < DateTime.UtcNow)
                DoTeleport(from);

            base.AlterSpellDamageFrom(from, ref damage);
        }

        public override void AlterMeleeDamageFrom(Mobile from, ref int damage)
        {
            if (from is BaseCreature && (((BaseCreature)from).Summoned || ((BaseCreature)from).Controlled))
                damage /= 2;

            if (NextTeleport < DateTime.UtcNow)
                DoTeleport(from);

            base.AlterMeleeDamageFrom(from, ref damage);
        }

        public void DoTeleport(Mobile m)
        {
            if (!InRange(m.Location, 1))
            {
                int x, y, z = 0;
                Point3D p = Point3D.Zero;

                for (int i = 0; i < 25; i++)
                {
                    x = Utility.RandomMinMax(X - 1, X + 1);
                    y = Utility.RandomMinMax(Y - 1, Y + 1);
                    z = Map.GetAverageZ(x, y);

                    if (Map.CanSpawnMobile(x, y, z) && (x != X || y != Y))
                    {
                        p = new Point3D(x, y, z);
                        break;
                    }
                }

                if (p == Point3D.Zero)
                    p = Location;

                Point3D from = m.Location;

                Effects.SendLocationParticles(EffectItem.Create(from, m.Map, EffectItem.DefaultDuration), 0x3728, 10, 10, 2023);
                Effects.SendLocationParticles(EffectItem.Create(p, m.Map, EffectItem.DefaultDuration), 0x3728, 10, 10, 5023);

                m.MoveToWorld(p, Map);

                m.PlaySound(0x1FE);
            }

            NextTeleport = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(30, 60));
        }

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);

            if (0.33 > Utility.RandomDouble() && Region.Find(c.Location, c.Map).IsPartOf("Shame"))
                c.DropItem(new ShameCrystal(5));
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich, 2);
        }

        public ChaosVortex(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadInt();
        }
    }

    [CorpseName("an unbound energy vortex corpse")]
    public class UnboundEnergyVortex : BaseCreature
    {
        [Constructible]
        public UnboundEnergyVortex() : base(AIType.AI_Melee, FightMode.Weakest, 10, 1, 0.4, 0.2)
        {
            Name = "an unbound energy vortex";
            Body = 13;

            SetStr(450);
            SetDex(200);
            SetInt(100);

            SetHits(20000);
            SetMana(0);

            SetDamage(21, 23);

            SetDamageType(ResistType.Phys, 0);
            SetDamageType(ResistType.Engy, 100);

            SetResist(ResistType.Phys, 65, 75);
            SetResist(ResistType.Fire, 65, 75);
            SetResist(ResistType.Cold, 65, 75);
            SetResist(ResistType.Pois, 65, 75);
            SetResist(ResistType.Engy, 100);

            SetSkill(SkillName.MagicResist, 100, 110);
            SetSkill(SkillName.Tactics, 110, 130);
            SetSkill(SkillName.Wrestling, 124, 140);

            Fame = 22500;
            Karma = -22500;
        }

        public override bool AlwaysMurderer => true;
        public override bool BleedImmunity => true;
        public override Poison PoisonImmunity => Poison.Lethal;

        public override int GetAngerSound()
        {
            return 0x15;
        }

        public override int GetAttackSound()
        {
            return 0x28;
        }

        private DateTime NextTeleport { get; set; }

        public override void AlterSpellDamageFrom(Mobile from, ref int damage)
        {
            if (from is BaseCreature && (((BaseCreature)from).Summoned || ((BaseCreature)from).Controlled))
                damage /= 2;

            if (NextTeleport < DateTime.UtcNow)
                DoTeleport(from);

            base.AlterSpellDamageFrom(from, ref damage);
        }

        public override void AlterMeleeDamageFrom(Mobile from, ref int damage)
        {
            if (from is BaseCreature && (((BaseCreature)from).Summoned || ((BaseCreature)from).Controlled))
                damage /= 2;

            if (NextTeleport < DateTime.UtcNow)
                DoTeleport(from);

            base.AlterMeleeDamageFrom(from, ref damage);
        }

        public void DoTeleport(Mobile m)
        {
            if (!InRange(m.Location, 1))
            {
                int x, y, z = 0;
                Point3D p = Point3D.Zero;

                for (int i = 0; i < 25; i++)
                {
                    x = Utility.RandomMinMax(X - 1, X + 1);
                    y = Utility.RandomMinMax(Y - 1, Y + 1);
                    z = Map.GetAverageZ(x, y);

                    if (Map.CanSpawnMobile(x, y, z) && (x != X || y != Y))
                    {
                        p = new Point3D(x, y, z);
                        break;
                    }
                }

                if (p == Point3D.Zero)
                    p = Location;

                Point3D from = m.Location;

                Effects.SendLocationParticles(EffectItem.Create(from, m.Map, EffectItem.DefaultDuration), 0x3728, 10, 10, 2023);
                Effects.SendLocationParticles(EffectItem.Create(p, m.Map, EffectItem.DefaultDuration), 0x3728, 10, 10, 5023);

                m.MoveToWorld(p, Map);

                m.PlaySound(0x1FE);
            }

            NextTeleport = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(30, 60));
        }

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);

            if (0.33 > Utility.RandomDouble() && Region.Find(c.Location, c.Map).IsPartOf("Shame"))
                c.DropItem(new ShameCrystal(5));

            if (0.2 > Utility.RandomDouble())
                c.DropItem(new VoidCore());
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.UltraRich, 2);
        }

        public UnboundEnergyVortex(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadInt();
        }
    }

    [CorpseName("a diseased blood elemental")]
    public class DiseasedBloodElemental : BloodElemental
    {
        [Constructible]
        public DiseasedBloodElemental()
        {
            Name = "a diseased blood elemental";
            Body = 0x9F;
            Hue = 1779;

            SetStr(650, 750);
            SetDex(70, 80);
            SetInt(300, 400);

            SetHits(2500, 2700);
            SetMana(1400, 1600);

            SetDamage(19, 27);

            SetDamageType(ResistType.Pois, 50);
            SetDamageType(ResistType.Engy, 50);

            SetResist(ResistType.Phys, 65, 75);
            SetResist(ResistType.Fire, 55, 65);
            SetResist(ResistType.Cold, 50, 60);
            SetResist(ResistType.Pois, 60, 70);
            SetResist(ResistType.Engy, 50, 60);

            SetSkill(SkillName.MagicResist, 110, 125);
            SetSkill(SkillName.Tactics, 130, 140);
            SetSkill(SkillName.Wrestling, 120, 140);
            SetSkill(SkillName.Poisoning, 100);
            SetSkill(SkillName.Magery, 110, 120);
            SetSkill(SkillName.EvalInt, 115, 130);
            SetSkill(SkillName.Meditation, 130, 155);
            SetSkill(SkillName.DetectHidden, 80.0);
            SetSkill(SkillName.Parry, 90.0, 100.0);

            PackItem(Loot.PackReg(7, 11));

            // int scrolls = Utility.RandomMinMax(4, 6);

            Fame = 8500;
            Karma = -8500;

            SetWeaponAbility(WeaponAbility.BleedAttack);
            SetSpecialAbility(SpecialAbility.LifeLeech);
        }

        public DiseasedBloodElemental(Serial serial)
            : base(serial)
        {
        }

        public override bool AutoDispel => true;
        public override double AutoDispelChance => 1.0;
        public override int TreasureMapLevel => 5;
        public override double TreasureMapChance => 1.0;
        public override Poison HitPoison => Poison.Lethal;
        public override Poison PoisonImmunity => Poison.Parasitic;

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);

            if (0.33 > Utility.RandomDouble() && Region.Find(c.Location, c.Map).IsPartOf("Shame"))
                c.DropItem(new ShameCrystal(5));
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich, 2);
            AddLoot(LootPack.HighScrolls, Utility.RandomMinMax(1, 8));
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadInt();
        }
    }

    public class GreaterWaterElemental : WaterElemental
    {
        [Constructible]
        public GreaterWaterElemental()
        {
            SetStr(400, 500);
            SetDex(150, 160);
            SetInt(120, 140);

            SetHits(500, 600);
            SetMana(600, 700);

            SetDamage(14, 16);

            SetDamageType(ResistType.Phys, 50);
            SetDamageType(ResistType.Cold, 50);

            SetResist(ResistType.Phys, 60, 70);
            SetResist(ResistType.Fire, 40, 50);
            SetResist(ResistType.Cold, 50, 60);
            SetResist(ResistType.Pois, 70, 80);
            SetResist(ResistType.Engy, 40, 50);

            SetSkill(SkillName.MagicResist, 100, 110);
            SetSkill(SkillName.Tactics, 90, 110);
            SetSkill(SkillName.Wrestling, 90, 110);
            SetSkill(SkillName.Magery, 90, 110);
            SetSkill(SkillName.EvalInt, 90, 100);

            Fame = 3500;
            Karma = -3500;
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich, 1);
        }

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);

            if (0.10 > Utility.RandomDouble() && Region.Find(c.Location, c.Map).IsPartOf("Shame"))
                c.DropItem(new ShameCrystal());
        }

        public GreaterWaterElemental(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadInt();
        }
    }

    public class ShameGreaterPoisonElemental : PoisonElemental
    {
        [Constructible]
        public ShameGreaterPoisonElemental()
        {
            Hue = 32854;

            SetStr(400, 500);
            SetDex(170, 175);
            SetInt(400, 450);

            SetHits(950, 1050);

            SetDamage(16, 19);

            SetDamageType(ResistType.Phys, 10);
            SetDamageType(ResistType.Pois, 90);

            SetResist(ResistType.Phys, 60, 70);
            SetResist(ResistType.Fire, 50, 60);
            SetResist(ResistType.Cold, 50, 60);
            SetResist(ResistType.Pois, 100);
            SetResist(ResistType.Engy, 40, 50);

            SetSkill(SkillName.MagicResist, 110, 120);
            SetSkill(SkillName.Tactics, 90, 120);
            SetSkill(SkillName.Wrestling, 100, 115);
            SetSkill(SkillName.Magery, 90, 110);
            SetSkill(SkillName.EvalInt, 90, 100);
            SetSkill(SkillName.Meditation, 100, 120);
            SetSkill(SkillName.DetectHidden, 85.1);
            SetSkill(SkillName.Parry, 80, 100);
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.UltraRich, 1);
            AddLoot(LootPack.FilthyRich, 1);
        }

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);

            if (0.10 > Utility.RandomDouble() && Region.Find(c.Location, c.Map).IsPartOf("Shame"))
                c.DropItem(new ShameCrystal(5));
        }

        public ShameGreaterPoisonElemental(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadInt();
        }
    }

    public class GreaterBloodElemental : BloodElemental
    {
        [Constructible]
        public GreaterBloodElemental()
        {
            SetStr(500, 600);
            SetDex(60, 90);
            SetInt(230, 350);

            SetHits(1350, 1500);
            SetHits(900, 1000);

            SetDamage(17, 27);

            SetDamageType(ResistType.Pois, 50);
            SetDamageType(ResistType.Engy, 50);

            SetResist(ResistType.Phys, 55, 65);
            SetResist(ResistType.Fire, 40, 50);
            SetResist(ResistType.Cold, 40, 50);
            SetResist(ResistType.Pois, 50, 60);
            SetResist(ResistType.Engy, 40, 50);

            SetSkill(SkillName.MagicResist, 115, 120);
            SetSkill(SkillName.Tactics, 100, 120);
            SetSkill(SkillName.Wrestling, 110, 120);
            SetSkill(SkillName.Magery, 80, 100);
            SetSkill(SkillName.EvalInt, 110, 120);
            SetSkill(SkillName.Meditation, 120, 140);
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.UltraRich, 1);
            AddLoot(LootPack.FilthyRich, 1);
        }

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);

            if (0.10 > Utility.RandomDouble() && Region.Find(c.Location, c.Map).IsPartOf("Shame"))
                c.DropItem(new ShameCrystal(5));
        }

        public GreaterBloodElemental(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadInt();
        }
    }

    public class ShameEarthElemental : EarthElemental
    {
        [Constructible]
        public ShameEarthElemental()
        {
            SetHits(300, 400);
            SetDamage(11, 13);

            SetDamageType(ResistType.Phys, 100);

            SetResist(ResistType.Phys, 30, 35);
            SetResist(ResistType.Fire, 25, 30);
            SetResist(ResistType.Cold, 25, 30);
            SetResist(ResistType.Pois, 25, 30);
            SetResist(ResistType.Engy, 20, 25);

            SetSkill(SkillName.MagicResist, 65, 85);
            SetSkill(SkillName.Tactics, 65, 90);
            SetSkill(SkillName.Wrestling, 80, 85);

            Fame = 3500;
            Karma = -3500;
        }

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);

            if (0.08 > Utility.RandomDouble() && Region.Find(c.Location, c.Map).IsPartOf("Shame"))
                c.DropItem(new ShameCrystal());
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich, 1);
        }

        public ShameEarthElemental(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadInt();
        }
    }
}
