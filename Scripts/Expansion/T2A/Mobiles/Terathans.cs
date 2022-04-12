using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a terathan avenger corpse")]
    public class TerathanAvenger : BaseCreature
    {
        [Constructible]
        public TerathanAvenger()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a terathan avenger";
            Body = 152;
            BaseSoundID = 0x24D;

            SetStr(467, 645);
            SetDex(77, 95);
            SetInt(126, 150);

            SetHits(296, 372);
            SetMana(46, 70);

            SetDamage(ResistType.Phys, 50, 0, 18, 22);
            SetDamage(ResistType.Pois, 50);

            SetResist(ResistType.Phys, 40, 50);
            SetResist(ResistType.Fire, 30, 40);
            SetResist(ResistType.Cold, 35, 45);
            SetResist(ResistType.Pois, 90, 100);
            SetResist(ResistType.Engy, 35, 45);

            SetSkill(SkillName.EvalInt, 70.3, 100.0);
            SetSkill(SkillName.Magery, 70.3, 100.0);
            SetSkill(SkillName.Poisoning, 60.1, 80.0);
            SetSkill(SkillName.MagicResist, 65.1, 80.0);
            SetSkill(SkillName.Tactics, 90.1, 100.0);
            SetSkill(SkillName.Wrestling, 90.1, 100.0);

            Fame = 15000;
            Karma = -15000;

            VirtualArmor = 50;
        }

        public TerathanAvenger(Serial serial)
            : base(serial)
        {
        }

        public override Poison PoisonImmunity => Poison.Deadly;
        public override Poison HitPoison => Poison.Deadly;
        public override int TreasureMapLevel => 3;
        public override int Meat => 2;

        public override TribeType Tribe => TribeType.Terathan;
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

    [CorpseName("a terathan drone corpse")]
    public class TerathanDrone : BaseCreature
    {
        [Constructible]
        public TerathanDrone()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a terathan drone";
            Body = 71;
            BaseSoundID = 594;

            SetStr(36, 65);
            SetDex(96, 145);
            SetInt(21, 45);

            SetHits(22, 39);
            SetMana(0);

            SetDamage(ResistType.Phys, 100, 0, 6, 12);

            SetResist(ResistType.Phys, 20, 25);
            SetResist(ResistType.Fire, 10, 20);
            SetResist(ResistType.Cold, 15, 25);
            SetResist(ResistType.Pois, 30, 40);
            SetResist(ResistType.Engy, 15, 25);

            SetSkill(SkillName.Poisoning, 40.1, 60.0);
            SetSkill(SkillName.MagicResist, 30.1, 45.0);
            SetSkill(SkillName.Tactics, 30.1, 50.0);
            SetSkill(SkillName.Wrestling, 40.1, 50.0);

            Fame = 2000;
            Karma = -2000;

            VirtualArmor = 24;

            PackItem(new SpidersSilk(2));
        }

        public TerathanDrone(Serial serial)
            : base(serial)
        {
        }

        public override int Meat => 4;
        public override TribeType Tribe => TribeType.Terathan;
        public override OppositionGroup OppositionGroup => OppositionGroup.TerathansAndOphidians;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Meager);
            // TODO: weapon?
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

    [CorpseName("a terathan matriarch corpse")]
    public class TerathanMatriarch : BaseCreature
    {
        [Constructible]
        public TerathanMatriarch()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a terathan matriarch";
            Body = 72;
            BaseSoundID = 599;

            SetStr(316, 405);
            SetDex(96, 115);
            SetInt(366, 455);

            SetHits(190, 243);

            SetDamage(ResistType.Phys, 100, 0, 11, 14);

            SetResist(ResistType.Phys, 45, 55);
            SetResist(ResistType.Fire, 30, 40);
            SetResist(ResistType.Cold, 35, 45);
            SetResist(ResistType.Pois, 40, 50);
            SetResist(ResistType.Engy, 35, 45);

            SetSkill(SkillName.EvalInt, 90.1, 100.0);
            SetSkill(SkillName.Magery, 90.1, 100.0);
            SetSkill(SkillName.MagicResist, 90.1, 100.0);
            SetSkill(SkillName.Tactics, 50.1, 70.0);
            SetSkill(SkillName.Wrestling, 60.1, 80.0);

            Fame = 10000;
            Karma = -10000;

            PackItem(new SpidersSilk(5));
            PackItem(Loot.PackNecroReg(4, 10));
        }

        public TerathanMatriarch(Serial serial)
            : base(serial)
        {
        }

        public override int TreasureMapLevel => 4;
        public override TribeType Tribe => TribeType.Terathan;
        public override OppositionGroup OppositionGroup => OppositionGroup.TerathansAndOphidians;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);
            AddLoot(LootPack.Average, 2);
            AddLoot(LootPack.MedScrolls, 2);
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

    [CorpseName("a terathan warrior corpse")]
    public class TerathanWarrior : BaseCreature
    {
        [Constructible]
        public TerathanWarrior()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a terathan warrior";
            Body = 70;
            BaseSoundID = 589;

            SetStr(166, 215);
            SetDex(96, 145);
            SetInt(41, 65);

            SetHits(100, 129);
            SetMana(0);

            SetDamage(ResistType.Phys, 100, 0, 7, 17);

            SetResist(ResistType.Phys, 30, 35);
            SetResist(ResistType.Fire, 20, 30);
            SetResist(ResistType.Cold, 25, 35);
            SetResist(ResistType.Pois, 30, 40);
            SetResist(ResistType.Engy, 25, 35);

            SetSkill(SkillName.Poisoning, 60.1, 80.0);
            SetSkill(SkillName.MagicResist, 60.1, 75.0);
            SetSkill(SkillName.Tactics, 80.1, 100.0);
            SetSkill(SkillName.Wrestling, 80.1, 90.0);

            Fame = 4000;
            Karma = -4000;

            VirtualArmor = 30;

            if (Core.ML && Utility.RandomDouble() < .33)
                PackItem(Engines.Plants.Seed.RandomPeculiarSeed(4));
        }

        public TerathanWarrior(Serial serial)
            : base(serial)
        {
        }

        public override int TreasureMapLevel => 1;
        public override int Meat => 4;
        public override TribeType Tribe => TribeType.Terathan;
        public override OppositionGroup OppositionGroup => OppositionGroup.TerathansAndOphidians;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Average);
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
