using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a skeletal corpse")]
    public class BoneKnight : BaseCreature
    {
        [Constructible]
        public BoneKnight()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a bone knight";
            Body = 57;
            BaseSoundID = 451;

            SetStr(196, 250);
            SetDex(76, 95);
            SetInt(36, 60);

            SetHits(118, 150);

            SetDamage(ResistType.Phys, 40, 0, 8, 18);
            SetDamage(ResistType.Cold, 60);

            SetResist(ResistType.Phys, 35, 45);
            SetResist(ResistType.Fire, 20, 30);
            SetResist(ResistType.Cold, 50, 60);
            SetResist(ResistType.Pois, 20, 30);
            SetResist(ResistType.Engy, 30, 40);

            SetSkill(SkillName.MagicResist, 65.1, 80.0);
            SetSkill(SkillName.Tactics, 85.1, 100.0);
            SetSkill(SkillName.Wrestling, 85.1, 95.0);

            Fame = 3000;
            Karma = -3000;

            VirtualArmor = 40;
        }

        public BoneKnight(Serial serial)
            : base(serial)
        {
        }

        public override Poison PoisonImmunity => Poison.Regular;
        public override bool BleedImmunity => true;
        public override TribeType Tribe => TribeType.Undead;
        public override OppositionGroup OppositionGroup => OppositionGroup.FeyAndUndead;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Average);
            AddLoot(LootPack.Meager);
        }

        public override void OnDeath(Container CorpseLoot)
        {
            switch (Utility.Random(6))
            {
                case 0:
                    CorpseLoot.DropItem(new PlateArms());
                    break;
                case 1:
                    CorpseLoot.DropItem(new PlateChest());
                    break;
                case 2:
                    CorpseLoot.DropItem(new PlateGloves());
                    break;
                case 3:
                    CorpseLoot.DropItem(new PlateGorget());
                    break;
                case 4:
                    CorpseLoot.DropItem(new PlateLegs());
                    break;
                case 5:
                    CorpseLoot.DropItem(new PlateHelm());
                    break;
            }

            CorpseLoot.DropItem(new Scimitar());
            CorpseLoot.DropItem(new WoodenShield());
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

    [CorpseName("a skeletal corpse")]
    public class BoneMagi : BaseCreature
    {
        [Constructible]
        public BoneMagi()
            : base(AIType.AI_NecroMage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a bone mage";
            Body = 148;
            BaseSoundID = 451;

            SetStr(76, 100);
            SetDex(56, 75);
            SetInt(186, 210);

            SetHits(46, 60);

            SetDamage(ResistType.Phys, 100, 0, 3, 7);

            SetResist(ResistType.Phys, 35, 40);
            SetResist(ResistType.Fire, 20, 30);
            SetResist(ResistType.Cold, 50, 60);
            SetResist(ResistType.Pois, 20, 30);
            SetResist(ResistType.Engy, 30, 40);

            SetSkill(SkillName.EvalInt, 60.1, 70.0);
            SetSkill(SkillName.Magery, 60.1, 70.0);
            SetSkill(SkillName.MagicResist, 55.1, 70.0);
            SetSkill(SkillName.Tactics, 45.1, 60.0);
            SetSkill(SkillName.Wrestling, 45.1, 55.0);
            SetSkill(SkillName.Necromancy, 89, 99.1);
            SetSkill(SkillName.SpiritSpeak, 90.0, 99.0);

            Fame = 3000;
            Karma = -3000;

            VirtualArmor = 38;

            PackItem(Loot.PackReg(3));
            PackItem(Loot.PackNecroReg(3, 10));
        }

        public BoneMagi(Serial serial)
            : base(serial)
        {
        }

        public override bool BleedImmunity => true;
        public override OppositionGroup OppositionGroup => OppositionGroup.FeyAndUndead;
        public override Poison PoisonImmunity => Poison.Regular;
        public override TribeType Tribe => TribeType.Undead;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Average);
            AddLoot(LootPack.LowScrolls);
            AddLoot(LootPack.Potions);
        }

        public override void OnDeath(Container CorpseLoot)
        {
            if (Core.AOS) CorpseLoot.DropItem(new Bone());
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

    [CorpseName("a ghostly corpse")]
    public class Ghoul : BaseCreature
    {
        [Constructible]
        public Ghoul()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a ghoul";
            Body = 153;
            BaseSoundID = 0x482;

            SetStr(76, 100);
            SetDex(76, 95);
            SetInt(36, 60);

            SetHits(46, 60);
            SetMana(0);

            SetDamage(ResistType.Phys, 100, 0, 7, 9);

            SetResist(ResistType.Phys, 25, 30);
            SetResist(ResistType.Cold, 20, 30);
            SetResist(ResistType.Pois, 5, 10);
            SetResist(ResistType.Engy, 10, 20);

            SetSkill(SkillName.MagicResist, 45.1, 60.0);
            SetSkill(SkillName.Tactics, 45.1, 60.0);
            SetSkill(SkillName.Wrestling, 45.1, 55.0);

            Fame = 2500;
            Karma = -2500;

            VirtualArmor = 28;
        }

        public Ghoul(Serial serial)
            : base(serial)
        {
        }

        public override Poison PoisonImmunity => Poison.Regular;
        public override bool BleedImmunity => true;
        public override TribeType Tribe => TribeType.Undead;
        public override OppositionGroup OppositionGroup => OppositionGroup.FeyAndUndead;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Meager);
            PackItem(Loot.RandomWeapon());
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

    [CorpseName("a liche's corpse")]
    public class Lich : BaseCreature
    {
        [Constructible]
        public Lich()
            : base(AIType.AI_NecroMage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a lich";
            Body = 24;
            BaseSoundID = 0x3E9;

            SetStr(171, 200);
            SetDex(126, 145);
            SetInt(276, 305);

            SetHits(103, 120);

            SetDamage(ResistType.Phys, 10, 0, 24, 26);
            SetDamage(ResistType.Cold, 40);
            SetDamage(ResistType.Engy, 50);

            SetResist(ResistType.Phys, 40, 60);
            SetResist(ResistType.Fire, 20, 30);
            SetResist(ResistType.Cold, 50, 60);
            SetResist(ResistType.Pois, 55, 65);
            SetResist(ResistType.Engy, 40, 50);

            SetSkill(SkillName.Necromancy, 89, 99.1);
            SetSkill(SkillName.SpiritSpeak, 90.0, 99.0);

            SetSkill(SkillName.EvalInt, 100.0);
            SetSkill(SkillName.Magery, 70.1, 80.0);
            SetSkill(SkillName.Meditation, 85.1, 95.0);
            SetSkill(SkillName.MagicResist, 80.1, 100.0);
            SetSkill(SkillName.Tactics, 70.1, 90.0);

            Fame = 8000;
            Karma = -8000;

            VirtualArmor = 50;

            AddItem(new GnarledStaff());
            PackItem(Loot.PackNecroReg(17, 24));
        }

        public Lich(Serial serial)
            : base(serial)
        {
        }

        public override Poison PoisonImmunity => Poison.Deadly;
        public override bool BleedImmunity => true;
        public override TribeType Tribe => TribeType.Undead;
        public override OppositionGroup OppositionGroup => OppositionGroup.FeyAndUndead;
        public override bool CanRummageCorpses => true;
        public override int TreasureMapLevel => 3;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);
            AddLoot(LootPack.MedScrolls, 2);
        }

        public override void OnDeath(Container CorpseLoot)
        {
            switch (Utility.Random(25))
            {
                case 0: CorpseLoot.DropItem(new LichFormScroll()); break;
                case 1: CorpseLoot.DropItem(new PoisonStrikeScroll()); break;
                case 2: CorpseLoot.DropItem(new StrangleScroll()); break;
                case 3: CorpseLoot.DropItem(new VengefulSpiritScroll()); break;
                case 4: CorpseLoot.DropItem(new WitherScroll()); break;
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

    [CorpseName("a liche's corpse")]
    public class LichLord : BaseCreature
    {
        [Constructible]
        public LichLord()
            : base(AIType.AI_NecroMage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a lich lord";
            Body = 79;
            BaseSoundID = 412;

            SetStr(416, 505);
            SetDex(146, 165);
            SetInt(566, 655);

            SetHits(250, 303);

            SetDamage(ResistType.Phys, 0, 0, 11, 13);
            SetDamage(ResistType.Cold, 60);
            SetDamage(ResistType.Engy, 40);

            SetResist(ResistType.Phys, 40, 50);
            SetResist(ResistType.Fire, 30, 40);
            SetResist(ResistType.Cold, 50, 60);
            SetResist(ResistType.Pois, 50, 60);
            SetResist(ResistType.Engy, 40, 50);

            SetSkill(SkillName.Necromancy, 90, 110.0);
            SetSkill(SkillName.SpiritSpeak, 90.0, 110.0);

            SetSkill(SkillName.EvalInt, 90.1, 100.0);
            SetSkill(SkillName.Magery, 90.1, 100.0);
            SetSkill(SkillName.MagicResist, 150.5, 200.0);
            SetSkill(SkillName.Tactics, 50.1, 70.0);
            SetSkill(SkillName.Wrestling, 60.1, 80.0);

            Fame = 18000;
            Karma = -18000;

            VirtualArmor = 50;

            AddItem(new GnarledStaff());
            PackItem(Loot.PackNecroReg(12, 40));
        }

        public LichLord(Serial serial)
            : base(serial)
        {
        }

        public override Poison PoisonImmunity => Poison.Lethal;
        public override bool BleedImmunity => true;
        public override TribeType Tribe => TribeType.Undead;
        public override OppositionGroup OppositionGroup => OppositionGroup.FeyAndUndead;
        public override bool CanRummageCorpses => true;
        public override int TreasureMapLevel => 4;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich);
            AddLoot(LootPack.MedScrolls, 2);
        }

        public override void OnDeath(Container CorpseLoot)
        {
            switch (Utility.Random(15))
            {
                case 0: CorpseLoot.DropItem(new LichFormScroll()); break;
                case 1: CorpseLoot.DropItem(new PoisonStrikeScroll()); break;
                case 2: CorpseLoot.DropItem(new StrangleScroll()); break;
                case 3: CorpseLoot.DropItem(new VengefulSpiritScroll()); break;
                case 4: CorpseLoot.DropItem(new WitherScroll()); break;
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

    [CorpseName("a ghostly corpse")]
    public class Shade : BaseCreature
    {
        [Constructible]
        public Shade()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a shade";
            Body = 26;
            Hue = 0x4001;
            BaseSoundID = 0x482;

            SetStr(76, 100);
            SetDex(76, 95);
            SetInt(36, 60);

            SetHits(46, 60);

            SetDamage(ResistType.Phys, 50, 0, 7, 11);
            SetDamage(ResistType.Cold, 50);

            SetResist(ResistType.Phys, 25, 30);
            SetResist(ResistType.Cold, 15, 25);
            SetResist(ResistType.Pois, 10, 20);

            SetSkill(SkillName.EvalInt, 55.1, 70.0);
            SetSkill(SkillName.Magery, 55.1, 70.0);
            SetSkill(SkillName.MagicResist, 55.1, 70.0);
            SetSkill(SkillName.Tactics, 45.1, 60.0);
            SetSkill(SkillName.Wrestling, 45.1, 55.0);

            Fame = 4000;
            Karma = -4000;

            VirtualArmor = 28;

            PackItem(Loot.PackReg(10));
        }

        public Shade(Serial serial)
            : base(serial)
        {
        }

        public override Poison PoisonImmunity => Poison.Lethal;
        public override bool BleedImmunity => true;
        public override TribeType Tribe => TribeType.Undead;
        public override OppositionGroup OppositionGroup => OppositionGroup.FeyAndUndead;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Meager);
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

    [CorpseName("a skeletal corpse")]
    public class SkeletalKnight : BaseCreature
    {
        [Constructible]
        public SkeletalKnight()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a skeletal knight";
            Body = 147;
            BaseSoundID = 451;

            SetStr(196, 250);
            SetDex(76, 95);
            SetInt(36, 60);

            SetHits(118, 150);

            SetDamage(ResistType.Phys, 40, 0, 8, 18);
            SetDamage(ResistType.Cold, 60);

            SetResist(ResistType.Phys, 35, 45);
            SetResist(ResistType.Fire, 20, 30);
            SetResist(ResistType.Cold, 50, 60);
            SetResist(ResistType.Pois, 20, 30);
            SetResist(ResistType.Engy, 30, 40);

            SetSkill(SkillName.MagicResist, 65.1, 80.0);
            SetSkill(SkillName.Tactics, 85.1, 100.0);
            SetSkill(SkillName.Wrestling, 85.1, 95.0);

            Fame = 3000;
            Karma = -3000;

            VirtualArmor = 40;

            AddItem(new Scimitar());
            AddItem(new WoodenShield());
        }

        public SkeletalKnight(Serial serial)
            : base(serial)
        {
        }

        public override Poison PoisonImmunity => Poison.Lethal;
        public override bool BleedImmunity => true;
        public override TribeType Tribe => TribeType.Undead;
        public override OppositionGroup OppositionGroup => OppositionGroup.FeyAndUndead;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Average);
            AddLoot(LootPack.Meager);
        }

        public override void OnDeath(Container CorpseLoot)
        {
            switch (Utility.Random(6))
            {
                case 0:
                    CorpseLoot.DropItem(new PlateArms());
                    break;
                case 1:
                    CorpseLoot.DropItem(new PlateChest());
                    break;
                case 2:
                    CorpseLoot.DropItem(new PlateGloves());
                    break;
                case 3:
                    CorpseLoot.DropItem(new PlateGorget());
                    break;
                case 4:
                    CorpseLoot.DropItem(new PlateLegs());
                    break;
                case 5:
                    CorpseLoot.DropItem(new PlateHelm());
                    break;
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

    [CorpseName("a skeletal corpse")]
    public class SkeletalMage : BaseCreature
    {
        [Constructible]
        public SkeletalMage()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a skeletal mage";
            Body = 148;
            BaseSoundID = 451;

            SetStr(76, 100);
            SetDex(56, 75);
            SetInt(186, 210);

            SetHits(46, 60);

            SetDamage(ResistType.Phys, 100, 0, 3, 7);

            SetResist(ResistType.Phys, 35, 40);
            SetResist(ResistType.Fire, 20, 30);
            SetResist(ResistType.Cold, 50, 60);
            SetResist(ResistType.Pois, 20, 30);
            SetResist(ResistType.Engy, 30, 40);

            SetSkill(SkillName.EvalInt, 60.1, 70.0);
            SetSkill(SkillName.Magery, 60.1, 70.0);
            SetSkill(SkillName.MagicResist, 55.1, 70.0);
            SetSkill(SkillName.Tactics, 45.1, 60.0);
            SetSkill(SkillName.Wrestling, 45.1, 55.0);
            SetSkill(SkillName.Necromancy, 89, 99.1);
            SetSkill(SkillName.SpiritSpeak, 90.0, 99.0);

            Fame = 3000;
            Karma = -3000;

            VirtualArmor = 38;

            PackItem(Loot.PackReg(3));
            if (Core.AOS)
            {
                PackItem(Loot.PackNecroReg(3, 10));
                PackItem(new Bone());
            }
        }

        public SkeletalMage(Serial serial)
            : base(serial)
        {
        }

        public override bool BleedImmunity => true;
        public override OppositionGroup OppositionGroup => OppositionGroup.FeyAndUndead;
        public override Poison PoisonImmunity => Poison.Lethal;
        public override TribeType Tribe => TribeType.Undead;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Average);
            AddLoot(LootPack.LowScrolls);
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

    [CorpseName("a skeletal corpse")]
    public class Skeleton : BaseCreature
    {
        [Constructible]
        public Skeleton()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a skeleton";
            Body = Utility.RandomList(50, 56);
            BaseSoundID = 0x48D;

            SetStr(56, 80);
            SetDex(56, 75);
            SetInt(16, 40);

            SetHits(34, 48);

            SetDamage(ResistType.Phys, 100, 0, 3, 7);

            SetResist(ResistType.Phys, 15, 20);
            SetResist(ResistType.Fire, 5, 10);
            SetResist(ResistType.Cold, 25, 40);
            SetResist(ResistType.Pois, 25, 35);
            SetResist(ResistType.Engy, 5, 15);

            SetSkill(SkillName.MagicResist, 45.1, 60.0);
            SetSkill(SkillName.Tactics, 45.1, 60.0);
            SetSkill(SkillName.Wrestling, 45.1, 55.0);

            Fame = 450;
            Karma = -450;

            VirtualArmor = 16;
        }

        public Skeleton(Serial serial)
            : base(serial)
        {
        }

        public override bool BleedImmunity => true;
        public override Poison PoisonImmunity => Poison.Regular;
        public override TribeType Tribe => TribeType.Undead;
        public override OppositionGroup OppositionGroup => OppositionGroup.FeyAndUndead;

        public override bool IsEnemy(Mobile m)
        {
            if (Region.IsPartOf("Haven Island"))
            {
                return false;
            }
            return base.IsEnemy(m);
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Poor);
        }

        public override void OnDeath(Container CorpseLoot)
        {
            switch (Utility.Random(5))
            {
                case 0:
                    CorpseLoot.DropItem(new BoneArms());
                    break;
                case 1:
                    CorpseLoot.DropItem(new BoneChest());
                    break;
                case 2:
                    CorpseLoot.DropItem(new BoneGloves());
                    break;
                case 3:
                    CorpseLoot.DropItem(new BoneLegs());
                    break;
                case 4:
                    CorpseLoot.DropItem(new BoneHelm());
                    break;
            }

            base.OnDeath(CorpseLoot);
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

    [CorpseName("a ghostly corpse")]
    public class Spectre : BaseCreature
    {
        [Constructible]
        public Spectre()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a spectre";
            Body = 26;
            Hue = 0x4001;
            BaseSoundID = 0x482;

            SetStr(76, 100);
            SetDex(76, 95);
            SetInt(36, 60);

            SetHits(46, 60);

            SetDamage(ResistType.Phys, 50, 0, 7, 11);
            SetDamage(ResistType.Cold, 50);

            SetResist(ResistType.Phys, 25, 30);
            SetResist(ResistType.Cold, 15, 25);
            SetResist(ResistType.Pois, 10, 20);

            SetSkill(SkillName.EvalInt, 55.1, 70.0);
            SetSkill(SkillName.Magery, 55.1, 70.0);
            SetSkill(SkillName.MagicResist, 55.1, 70.0);
            SetSkill(SkillName.Tactics, 45.1, 60.0);
            SetSkill(SkillName.Wrestling, 45.1, 55.0);

            Fame = 4000;
            Karma = -4000;

            VirtualArmor = 28;

            PackItem(Loot.PackReg(10));
        }

        public Spectre(Serial serial)
            : base(serial)
        {
        }

        public override bool BleedImmunity => true;
        public override Poison PoisonImmunity => Poison.Lethal;
        public override TribeType Tribe => TribeType.Undead;
        public override OppositionGroup OppositionGroup => OppositionGroup.FeyAndUndead;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Meager);
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

    [CorpseName("a ghostly corpse")]
    public class Wraith : BaseCreature
    {
        [Constructible]
        public Wraith()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a wraith";
            Body = 26;
            Hue = 0x4001;
            BaseSoundID = 0x482;

            SetStr(76, 100);
            SetDex(76, 95);
            SetInt(36, 60);

            SetHits(46, 60);

            SetDamage(ResistType.Phys, 50, 0, 7, 11);
            SetDamage(ResistType.Cold, 50);

            SetResist(ResistType.Phys, 25, 30);
            SetResist(ResistType.Cold, 15, 25);
            SetResist(ResistType.Pois, 10, 20);

            SetSkill(SkillName.EvalInt, 55.1, 70.0);
            SetSkill(SkillName.Magery, 55.1, 70.0);
            SetSkill(SkillName.MagicResist, 55.1, 70.0);
            SetSkill(SkillName.Tactics, 45.1, 60.0);
            SetSkill(SkillName.Wrestling, 45.1, 55.0);

            Fame = 4000;
            Karma = -4000;

            VirtualArmor = 28;

            PackItem(Loot.PackReg(10));
        }

        public Wraith(Serial serial)
            : base(serial)
        {
        }
        public override bool BleedImmunity => true;
        public override Poison PoisonImmunity => Poison.Lethal;
        public override TribeType Tribe => TribeType.Undead;
        public override OppositionGroup OppositionGroup => OppositionGroup.FeyAndUndead;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Meager);
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

    [CorpseName("a rotting corpse")]
    public class Zombie : BaseCreature
    {
        [Constructible]
        public Zombie()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a zombie";
            Body = 3;
            BaseSoundID = 471;

            SetStr(46, 70);
            SetDex(31, 50);
            SetInt(26, 40);

            SetHits(28, 42);

            SetDamage(ResistType.Phys, 100, 0, 3, 7);

            SetResist(ResistType.Phys, 15, 20);
            SetResist(ResistType.Cold, 20, 30);
            SetResist(ResistType.Pois, 5, 10);

            SetSkill(SkillName.MagicResist, 15.1, 40.0);
            SetSkill(SkillName.Tactics, 35.1, 50.0);
            SetSkill(SkillName.Wrestling, 35.1, 50.0);

            Fame = 600;
            Karma = -600;

            VirtualArmor = 18;
        }

        public Zombie(Serial serial)
            : base(serial)
        {
        }

        public override bool BleedImmunity => true;
        public override Poison PoisonImmunity => Poison.Lethal;
        public override TribeType Tribe => TribeType.Undead;
        public override OppositionGroup OppositionGroup => OppositionGroup.FeyAndUndead;

        public override bool IsEnemy(Mobile m)
        {
            if (Region.IsPartOf("Haven Island"))
            {
                return false;
            }

            return base.IsEnemy(m);
        }

        public override void OnDeath(Container CorpseLoot)
        {
            CorpseLoot.DropItem(Loot.PackBodyPartOrBones());
            base.OnDeath(CorpseLoot);
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Meager);
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
