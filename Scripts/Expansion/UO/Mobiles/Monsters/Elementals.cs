using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("an air elemental corpse")]
    public class AirElemental : BaseCreature
    {
        [Constructible]
        public AirElemental()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "an air elemental";
            Body = 13;
            Hue = 0x4001;
            BaseSoundID = 655;

            SetStr(126, 155);
            SetDex(166, 185);
            SetInt(101, 125);

            SetHits(76, 93);

            SetDamage(8, 10);

            SetDamageType(ResistType.Phys, 20);
            SetDamageType(ResistType.Cold, 40);
            SetDamageType(ResistType.Engy, 40);

            SetResist(ResistType.Phys, 35, 45);
            SetResist(ResistType.Fire, 15, 25);
            SetResist(ResistType.Cold, 10, 20);
            SetResist(ResistType.Pois, 10, 20);
            SetResist(ResistType.Engy, 25, 35);

            SetSkill(SkillName.EvalInt, 60.1, 75.0);
            SetSkill(SkillName.Magery, 60.1, 75.0);
            SetSkill(SkillName.MagicResist, 60.1, 75.0);
            SetSkill(SkillName.Tactics, 60.1, 80.0);
            SetSkill(SkillName.Wrestling, 60.1, 80.0);

            Fame = 4500;
            Karma = -4500;

            VirtualArmor = 40;

            ControlSlots = 2;
        }

        public AirElemental(Serial serial)
            : base(serial)
        {
        }

        public override double DispelDifficulty => 117.5;
        public override double DispelFocus => 45.0;
        public override bool BleedImmunity => true;
        public override int TreasureMapLevel => 1;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Average);
            AddLoot(LootPack.Meager);
            AddLoot(LootPack.LowScrolls);
            AddLoot(LootPack.MedScrolls);
        }

        public override void OnDeath(Container CorpseLoot)
        {
            switch (Utility.Random(24))
            {
                case 0: CorpseLoot.DropItem(new PainSpikeScroll()); break;
                case 1: CorpseLoot.DropItem(new PoisonStrikeScroll()); break;
                case 2: CorpseLoot.DropItem(new StrangleScroll()); break;
                case 3: CorpseLoot.DropItem(new VengefulSpiritScroll()); break;
            }
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

    [CorpseName("a blood elemental corpse")]
    public class BloodElemental : BaseCreature, IBloodCreature
    {
        [Constructible]
        public BloodElemental()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a blood elemental";
            Body = 159;
            BaseSoundID = 278;

            SetStr(526, 615);
            SetDex(66, 85);
            SetInt(226, 350);

            SetHits(316, 369);

            SetDamage(17, 27);

            SetDamageType(ResistType.Phys, 0);
            SetDamageType(ResistType.Pois, 50);
            SetDamageType(ResistType.Engy, 50);

            SetResist(ResistType.Phys, 55, 65);
            SetResist(ResistType.Fire, 20, 30);
            SetResist(ResistType.Cold, 40, 50);
            SetResist(ResistType.Pois, 50, 60);
            SetResist(ResistType.Engy, 30, 40);

            SetSkill(SkillName.EvalInt, 85.1, 100.0);
            SetSkill(SkillName.Magery, 85.1, 100.0);
            SetSkill(SkillName.Meditation, 10.4, 50.0);
            SetSkill(SkillName.MagicResist, 80.1, 95.0);
            SetSkill(SkillName.Tactics, 80.1, 100.0);
            SetSkill(SkillName.Wrestling, 80.1, 100.0);

            Fame = 12500;
            Karma = -12500;

            VirtualArmor = 60;
        }

        public BloodElemental(Serial serial)
            : base(serial)
        {
        }

        public override int TreasureMapLevel => 5;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich);
            AddLoot(LootPack.Rich);
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

    [CorpseName("an earth elemental corpse")]
    public class EarthElemental : BaseCreature
    {
        [Constructible]
        public EarthElemental()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "an earth elemental";
            Body = 14;
            BaseSoundID = 268;

            SetStr(126, 155);
            SetDex(66, 85);
            SetInt(71, 92);

            SetHits(76, 93);

            SetDamage(9, 16);

            SetDamageType(ResistType.Phys, 100);

            SetResist(ResistType.Phys, 30, 35);
            SetResist(ResistType.Fire, 10, 20);
            SetResist(ResistType.Cold, 10, 20);
            SetResist(ResistType.Pois, 15, 25);
            SetResist(ResistType.Engy, 15, 25);

            SetSkill(SkillName.MagicResist, 50.1, 95.0);
            SetSkill(SkillName.Tactics, 60.1, 100.0);
            SetSkill(SkillName.Wrestling, 60.1, 100.0);

            Fame = 3500;
            Karma = -3500;

            VirtualArmor = 34;
            ControlSlots = 2;
        }

        public EarthElemental(Serial serial)
            : base(serial)
        {
        }

        public override double DispelDifficulty => 117.5;  //?
        public override double DispelFocus => 45.0;
        public override bool BleedImmunity => true;
        public override int TreasureMapLevel => 1;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Average);
            AddLoot(LootPack.Meager);
            AddLoot(LootPack.Gems);
        }

        public override void OnDeath(Container CorpseLoot)
        {
            CorpseLoot.DropItem(new MandrakeRoot());
            if (Core.AOS) CorpseLoot.DropItem(new FertileDirt(Utility.RandomMinMax(1, 4)));

            CorpseLoot.DropItem(new IronOre(5));
            // ore.ItemID = 0x19B7;
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

    [CorpseName("a fire elemental corpse")]
    public class FireElemental : BaseCreature
    {
        [Constructible]
        public FireElemental()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a fire elemental";
            Body = 15;
            BaseSoundID = 838;

            SetStr(126, 155);
            SetDex(166, 185);
            SetInt(101, 125);

            SetHits(76, 93);

            SetDamage(7, 9);

            SetDamageType(ResistType.Phys, 25);
            SetDamageType(ResistType.Fire, 75);

            SetResist(ResistType.Phys, 35, 45);
            SetResist(ResistType.Fire, 60, 80);
            SetResist(ResistType.Cold, 5, 10);
            SetResist(ResistType.Pois, 30, 40);
            SetResist(ResistType.Engy, 30, 40);

            SetSkill(SkillName.EvalInt, 60.1, 75.0);
            SetSkill(SkillName.Magery, 60.1, 75.0);
            SetSkill(SkillName.MagicResist, 75.2, 105.0);
            SetSkill(SkillName.Tactics, 80.1, 100.0);
            SetSkill(SkillName.Wrestling, 70.1, 100.0);

            Fame = 4500;
            Karma = -4500;

            VirtualArmor = 40;
            ControlSlots = 4;

            AddItem(new LightSource());
        }

        public FireElemental(Serial serial)
            : base(serial)
        {
        }

        public override double DispelDifficulty => 117.5;  //?
        public override double DispelFocus => 45.0;
        public override bool BleedImmunity => true;
        public override int TreasureMapLevel => 1;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Average);
            AddLoot(LootPack.Meager);
            AddLoot(LootPack.Gems);
        }

        public override void OnDeath(Container CorpseLoot)
        {
            CorpseLoot.DropItem(new SulfurousAsh(3));
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

    [CorpseName("a poison elementals corpse")]
    public class PoisonElemental : BaseCreature
    {
        [Constructible]
        public PoisonElemental()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a poison elemental";
            Body = 162;
            BaseSoundID = 263;

            SetStr(426, 515);
            SetDex(166, 185);
            SetInt(361, 435);

            SetHits(256, 309);

            SetDamage(12, 18);

            SetDamageType(ResistType.Phys, 10);
            SetDamageType(ResistType.Pois, 90);

            SetResist(ResistType.Phys, 60, 70);
            SetResist(ResistType.Fire, 20, 30);
            SetResist(ResistType.Cold, 20, 30);
            SetResist(ResistType.Pois, 100);
            SetResist(ResistType.Engy, 40, 50);

            SetSkill(SkillName.EvalInt, 80.1, 95.0);
            SetSkill(SkillName.Magery, 80.1, 95.0);
            SetSkill(SkillName.Meditation, 80.2, 120.0);
            SetSkill(SkillName.Poisoning, 90.1, 100.0);
            SetSkill(SkillName.MagicResist, 85.2, 115.0);
            SetSkill(SkillName.Tactics, 80.1, 100.0);
            SetSkill(SkillName.Wrestling, 70.1, 90.0);

            Fame = 12500;
            Karma = -12500;

            VirtualArmor = 70;
        }

        public PoisonElemental(Serial serial)
            : base(serial)
        {
        }

        public override double DispelDifficulty => 117.5;  //?
        public override double DispelFocus => 45.0;
        public override bool BleedImmunity => true;
        public override int TreasureMapLevel => 5;

        public override Poison PoisonImmunity => Poison.Lethal;
        public override Poison HitPoison => Poison.Lethal;
        public override double HitPoisonChance => 0.75;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich);
            AddLoot(LootPack.Rich);
            AddLoot(LootPack.MedScrolls);
        }

        public override void OnDeath(Container CorpseLoot)
        {
            CorpseLoot.DropItem(new Nightshade(4));
            CorpseLoot.DropItem(new LesserPoisonPotion());
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

    [CorpseName("a water elemental corpse")]
    public class WaterElemental : BaseCreature
    {
        [CommandProperty(AccessLevel.GameMaster)]
        public bool HasDecanter { get; set; } = true;

        [Constructible]
        public WaterElemental()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a water elemental";
            Body = 16;
            BaseSoundID = 278;

            SetStr(126, 155);
            SetDex(66, 85);
            SetInt(101, 125);

            SetHits(76, 93);

            SetDamage(7, 9);

            SetDamageType(ResistType.Phys, 100);

            SetResist(ResistType.Phys, 35, 45);
            SetResist(ResistType.Fire, 10, 25);
            SetResist(ResistType.Cold, 10, 25);
            SetResist(ResistType.Pois, 60, 70);
            SetResist(ResistType.Engy, 5, 10);

            SetSkill(SkillName.EvalInt, 60.1, 75.0);
            SetSkill(SkillName.Magery, 60.1, 75.0);
            SetSkill(SkillName.MagicResist, 100.1, 115.0);
            SetSkill(SkillName.Tactics, 50.1, 70.0);
            SetSkill(SkillName.Wrestling, 50.1, 70.0);

            Fame = 4500;
            Karma = -4500;

            VirtualArmor = 40;
            ControlSlots = 3;
            CanSwim = true;
        }

        public WaterElemental(Serial serial)
            : base(serial)
        {
        }

        public override double DispelDifficulty => 117.5;  //?
        public override double DispelFocus => 45.0;
        public override bool BleedImmunity => true;
        public override int TreasureMapLevel => 2;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Average);
            AddLoot(LootPack.Meager);
            AddLoot(LootPack.Potions);
        }

        public override void OnDeath(Container CorpseLoot)
        {
            CorpseLoot.DropItem(new BlackPearl(3));
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(1);

            writer.Write(HasDecanter);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

            switch (version)
            {
                case 0:
                    break;
                case 1:
                    HasDecanter = reader.ReadBool();
                    break;
            }
        }
    }
}
