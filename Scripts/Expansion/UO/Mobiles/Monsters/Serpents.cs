using System;
using Server.Items;
using System.Linq;
using Server.Factions;

namespace Server.Mobiles
{
    [CorpseName("a giant serpent corpse")]
    [TypeAlias("Server.Mobiles.Serpant")]
    public class GiantSerpent : BaseCreature
    {
        [Constructible]
        public GiantSerpent()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a giant serpent";
            Body = 0x15;
            Hue = Utility.RandomSnakeHue();
            BaseSoundID = 219;

            SetStr(186, 215);
            SetDex(56, 80);
            SetInt(66, 85);

            SetHits(112, 129);
            SetMana(0);

            SetDamage(7, 17);

            SetDamageType(ResistType.Physical, 40);
            SetDamageType(ResistType.Poison, 60);

            SetResist(ResistType.Physical, 30, 35);
            SetResist(ResistType.Fire, 5, 10);
            SetResist(ResistType.Cold, 10, 20);
            SetResist(ResistType.Poison, 70, 90);
            SetResist(ResistType.Energy, 10, 20);

            SetSkill(SkillName.Poisoning, 70.1, 100.0);
            SetSkill(SkillName.MagicResist, 25.1, 40.0);
            SetSkill(SkillName.Tactics, 65.1, 70.0);
            SetSkill(SkillName.Wrestling, 60.1, 80.0);

            Fame = 2500;
            Karma = -2500;

            VirtualArmor = 32;

            PackItem(new Bone());
            // TODO: Body parts
        }

        public GiantSerpent(Serial serial)
            : base(serial)
        {
        }

        public override Poison PoisonImmunity => Poison.Greater;
        public override Poison HitPoison => (0.8 >= Utility.RandomDouble() ? Poison.Greater : Poison.Deadly);
        public override bool DeathAdderCharmable => true;
        public override int Meat => 4;
        public override int Hides => 15;
        public override HideType HideType => HideType.Spined;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Average);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(1);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadInt();
        }
    }

    [CorpseName("a sea serpents corpse")]
    [TypeAlias("Server.Mobiles.Seaserpant")]
    public class SeaSerpent : BaseCreature
    {
        [Constructible]
        public SeaSerpent()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a sea serpent";
            Body = 150;
            BaseSoundID = 447;

            Hue = Utility.Random(0x530, 9);

            SetStr(168, 225);
            SetDex(58, 85);
            SetInt(53, 95);

            SetHits(110, 127);

            SetDamage(7, 13);

            SetDamageType(ResistType.Physical, 100);

            SetResist(ResistType.Physical, 25, 35);
            SetResist(ResistType.Fire, 50, 60);
            SetResist(ResistType.Cold, 30, 40);
            SetResist(ResistType.Poison, 30, 40);
            SetResist(ResistType.Energy, 15, 20);

            SetSkill(SkillName.MagicResist, 60.1, 75.0);
            SetSkill(SkillName.Tactics, 60.1, 70.0);
            SetSkill(SkillName.Wrestling, 60.1, 70.0);

            Fame = 6000;
            Karma = -6000;

            VirtualArmor = 30;
            CanSwim = true;
            CantWalk = true;

            SetSpecialAbility(SpecialAbility.DragonBreath);
        }

        public SeaSerpent(Serial serial)
            : base(serial)
        {
        }

        public override int TreasureMapLevel => Utility.RandomList(1, 2);
        public override int Hides => 10;
        public override HideType HideType => HideType.Horned;
        public override int Scales => 8;
        public override ScaleType ScaleType => ScaleType.Blue;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Meager);
        }

        public override void OnDeath(Container CorpseLoot)
        {
            if (Utility.RandomBool())
                CorpseLoot.DropItem(new SulfurousAsh(4));
            else
                CorpseLoot.DropItem(new BlackPearl(4));

            CorpseLoot.DropItem(new RawFishSteak());

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

    [CorpseName("a silver serpent corpse")]
    [TypeAlias("Server.Mobiles.Silverserpant")]
    public class SilverSerpent : BaseCreature
    {
        [Constructible]
        public SilverSerpent()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Body = 92;
            Name = "a silver serpent";
            BaseSoundID = 219;
            Hue = 1150;

            SetStr(161, 360);
            SetDex(151, 300);
            SetInt(21, 40);

            SetHits(97, 216);

            SetDamage(5, 21);

            SetDamageType(ResistType.Physical, 50);
            SetDamageType(ResistType.Poison, 50);

            SetResist(ResistType.Physical, 35, 45);
            SetResist(ResistType.Fire, 5, 10);
            SetResist(ResistType.Cold, 5, 10);
            SetResist(ResistType.Poison, 100);
            SetResist(ResistType.Energy, 5, 10);

            SetSkill(SkillName.Poisoning, 90.1, 100.0);
            SetSkill(SkillName.MagicResist, 95.1, 100.0);
            SetSkill(SkillName.Tactics, 80.1, 95.0);
            SetSkill(SkillName.Wrestling, 85.1, 100.0);
            SetSkill(SkillName.DetectHidden, 50.0, 55.0);

            Fame = 7000;
            Karma = -7000;

            VirtualArmor = 40;
        }

        public SilverSerpent(Serial serial)
            : base(serial)
        {
        }

        public override Faction FactionAllegiance => TrueBritannians.Instance;
        public override Ethics.Ethic EthicAllegiance => Ethics.Ethic.Hero;
        public override bool DeathAdderCharmable => true;
        public override int Meat => 1;
        public override Poison PoisonImmunity => Poison.Lethal;
        public override Poison HitPoison => Poison.Lethal;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Average);
            AddLoot(LootPack.Gems, 2);
        }

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);

            if (Utility.RandomDouble() < 0.1)
                c.DropItem(new SilverSerpentVenom());
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
