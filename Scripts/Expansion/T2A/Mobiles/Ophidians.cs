using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("an ophidian corpse")]
    [TypeAlias("Server.Mobiles.OphidianJusticar", "Server.Mobiles.OphidianZealot")]
    public class OphidianArchmage : BaseCreature
    {
        private static readonly string[] m_Names = new string[]
        {
            "an ophidian justicar",
            "an ophidian zealot"
        };

        [Constructible]
        public OphidianArchmage()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = m_Names[Utility.Random(m_Names.Length)];
            Body = 85;
            BaseSoundID = 639;

            SetStr(281, 305);
            SetDex(191, 215);
            SetInt(226, 250);

            SetHits(169, 183);
            SetStam(36, 45);

            SetDamage(5, 10);

            SetDamageType(ResistType.Phys, 100);

            SetResist(ResistType.Phys, 40, 45);
            SetResist(ResistType.Fire, 20, 30);
            SetResist(ResistType.Cold, 25, 35);
            SetResist(ResistType.Pois, 35, 40);
            SetResist(ResistType.Engy, 25, 35);

            SetSkill(SkillName.EvalInt, 95.1, 100.0);
            SetSkill(SkillName.Magery, 95.1, 100.0);
            SetSkill(SkillName.MagicResist, 75.0, 97.5);
            SetSkill(SkillName.Tactics, 65.0, 87.5);
            SetSkill(SkillName.Wrestling, 20.2, 60.0);

            Fame = 11500;
            Karma = -11500;

            VirtualArmor = 44;

            PackItem(Loot.PackReg(5, 15));
            PackItem(Loot.PackNecroReg(5, 15));
        }

        public OphidianArchmage(Serial serial)
            : base(serial)
        {
        }

        public override int Meat => 1;
        public override int TreasureMapLevel => 2;

        public override TribeType Tribe => TribeType.Ophidian;

        public override OppositionGroup OppositionGroup => OppositionGroup.TerathansAndOphidians;
        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);
            AddLoot(LootPack.MedScrolls, 2);
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

    [CorpseName("an ophidian corpse")]
    [TypeAlias("Server.Mobiles.OphidianAvenger")]
    public class OphidianKnight : BaseCreature
    {
        private static readonly string[] m_Names = new string[]
        {
            "an ophidian knight-errant",
            "an ophidian avenger"
        };

        [Constructible]
        public OphidianKnight()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = m_Names[Utility.Random(m_Names.Length)];
            Body = 86;
            BaseSoundID = 634;

            SetStr(417, 595);
            SetDex(166, 175);
            SetInt(46, 70);

            SetHits(266, 342);
            SetMana(0);

            SetDamage(16, 19);

            SetDamageType(ResistType.Phys, 100);

            SetResist(ResistType.Phys, 35, 40);
            SetResist(ResistType.Fire, 30, 40);
            SetResist(ResistType.Cold, 35, 45);
            SetResist(ResistType.Pois, 90, 100);
            SetResist(ResistType.Engy, 35, 45);

            SetSkill(SkillName.Poisoning, 60.1, 80.0);
            SetSkill(SkillName.MagicResist, 65.1, 80.0);
            SetSkill(SkillName.Tactics, 90.1, 100.0);
            SetSkill(SkillName.Wrestling, 90.1, 100.0);

            Fame = 10000;
            Karma = -10000;

            VirtualArmor = 40;

            PackItem(new LesserPoisonPotion());
        }

        public OphidianKnight(Serial serial)
            : base(serial)
        {
        }

        public override int Meat => 2;
        public override Poison PoisonImmunity => Poison.Lethal;
        public override Poison HitPoison => Poison.Lethal;
        public override int TreasureMapLevel => 3;

        public override TribeType Tribe => TribeType.Ophidian;
        public override OppositionGroup OppositionGroup => OppositionGroup.TerathansAndOphidians;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich, 2);
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

    [CorpseName("an ophidian corpse")]
    [TypeAlias("Server.Mobiles.OphidianShaman")]
    public class OphidianMage : BaseCreature
    {
        private static readonly string[] m_Names = new string[]
        {
            "an ophidian apprentice mage",
            "an ophidian shaman"
        };

        [Constructible]
        public OphidianMage()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = m_Names[Utility.Random(m_Names.Length)];
            Body = 85;
            BaseSoundID = 639;

            SetStr(181, 205);
            SetDex(191, 215);
            SetInt(96, 120);

            SetHits(109, 123);

            SetDamage(5, 10);

            SetDamageType(ResistType.Phys, 100);

            SetResist(ResistType.Phys, 25, 35);
            SetResist(ResistType.Fire, 30, 40);
            SetResist(ResistType.Cold, 35, 45);
            SetResist(ResistType.Pois, 40, 50);
            SetResist(ResistType.Engy, 35, 45);

            SetSkill(SkillName.EvalInt, 85.1, 100.0);
            SetSkill(SkillName.Magery, 85.1, 100.0);
            SetSkill(SkillName.MagicResist, 75.0, 97.5);
            SetSkill(SkillName.Tactics, 65.0, 87.5);
            SetSkill(SkillName.Wrestling, 20.2, 60.0);

            Fame = 4000;
            Karma = -4000;

            VirtualArmor = 30;

            PackItem(Loot.PackReg(10));

            switch (Utility.Random(6))
            {
                case 0: PackItem(new PainSpikeScroll()); break;
            }

        }

        public OphidianMage(Serial serial)
            : base(serial)
        {
        }

        public override int Meat => 1;
        public override int TreasureMapLevel => 2;

        public override TribeType Tribe => TribeType.Ophidian;

        public override OppositionGroup OppositionGroup => OppositionGroup.TerathansAndOphidians;
        public override void GenerateLoot()
        {
            AddLoot(LootPack.Average);
            AddLoot(LootPack.LowScrolls);
            AddLoot(LootPack.MedScrolls);
            AddLoot(LootPack.Potions);
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

    [CorpseName("an ophidian corpse")]
    public class OphidianMatriarch : BaseCreature
    {
        [Constructible]
        public OphidianMatriarch()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "an ophidian matriarch";
            Body = 87;
            BaseSoundID = 644;

            SetStr(416, 505);
            SetDex(96, 115);
            SetInt(366, 455);

            SetHits(250, 303);

            SetDamage(11, 13);

            SetDamageType(ResistType.Phys, 100);

            SetResist(ResistType.Phys, 45, 55);
            SetResist(ResistType.Fire, 30, 40);
            SetResist(ResistType.Cold, 35, 45);
            SetResist(ResistType.Pois, 40, 50);
            SetResist(ResistType.Engy, 35, 45);

            SetSkill(SkillName.EvalInt, 90.1, 100.0);
            SetSkill(SkillName.Magery, 90.1, 100.0);
            SetSkill(SkillName.Meditation, 5.4, 25.0);
            SetSkill(SkillName.MagicResist, 90.1, 100.0);
            SetSkill(SkillName.Tactics, 50.1, 70.0);
            SetSkill(SkillName.Wrestling, 60.1, 80.0);

            Fame = 16000;
            Karma = -16000;

            VirtualArmor = 50;
        }

        public OphidianMatriarch(Serial serial)
            : base(serial)
        {
        }

        public override Poison PoisonImmunity => Poison.Greater;
        public override int TreasureMapLevel => 4;

        public override TribeType Tribe => TribeType.Ophidian;
        public override OppositionGroup OppositionGroup => OppositionGroup.TerathansAndOphidians;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);
            AddLoot(LootPack.Average, 2);
            AddLoot(LootPack.MedScrolls, 2);
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

    [CorpseName("an ophidian corpse")]
    public class OphidianWarrior : BaseCreature
    {
        private static readonly string[] m_Names = new string[]
        {
            "an ophidian warrior",
            "an ophidian enforcer"
        };

        [Constructible]
        public OphidianWarrior()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = m_Names[Utility.Random(m_Names.Length)];
            Body = 86;
            BaseSoundID = 634;

            SetStr(150, 320);
            SetDex(94, 190);
            SetInt(64, 160);

            SetHits(128, 155);
            SetMana(0);

            SetDamage(5, 11);

            SetDamageType(ResistType.Phys, 100);

            SetResist(ResistType.Phys, 35, 40);
            SetResist(ResistType.Fire, 20, 30);
            SetResist(ResistType.Cold, 25, 35);
            SetResist(ResistType.Pois, 30, 40);
            SetResist(ResistType.Engy, 25, 35);

            SetSkill(SkillName.MagicResist, 70.1, 85.0);
            SetSkill(SkillName.Swords, 60.1, 85.0);
            SetSkill(SkillName.Tactics, 75.1, 90.0);

            Fame = 4500;
            Karma = -4500;

            VirtualArmor = 36;
        }

        public OphidianWarrior(Serial serial)
            : base(serial)
        {
        }

        public override int Meat => 1;
        public override int TreasureMapLevel => 1;

        public override TribeType Tribe => TribeType.Ophidian;
        public override OppositionGroup OppositionGroup => OppositionGroup.TerathansAndOphidians;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Meager);
            AddLoot(LootPack.Average);
            AddLoot(LootPack.Gems);
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
