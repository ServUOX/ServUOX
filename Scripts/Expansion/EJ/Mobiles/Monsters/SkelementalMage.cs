using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a skeletal corpse")]
    public class SkelementalMage : BaseCreature
    {
        [Constructible]
        public SkelementalMage()
            : this(Utility.RandomBool() ? SkelementalKnight.SkeletalType.Cold : SkelementalKnight.SkeletalType.Fire)
        {
        }

        [Constructible]
        public SkelementalMage(SkelementalKnight.SkeletalType type)
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "Skelemental Mage";
            Body = 0x32;
            BaseSoundID = 451;

            int fire = 100, cold = 100, poison = 100, energy = 100;

            switch (type)
            {
                case SkelementalKnight.SkeletalType.Fire:
                    {
                        Hue = 2634;
                        SetDamageType(ResistType.Fire, 100);
                        cold = 5;
                        break;
                    }
                case SkelementalKnight.SkeletalType.Cold:
                    {
                        Hue = 2581;
                        SetDamageType(ResistType.Cold, 100);
                        fire = 5;
                        break;
                    }
                case SkelementalKnight.SkeletalType.Poison:
                    {
                        Hue = 2688;
                        SetDamageType(ResistType.Pois, 100);
                        energy = 5;
                        break;
                    }
                case SkelementalKnight.SkeletalType.Energy:
                    {
                        Hue = 2717;
                        SetDamageType(ResistType.Engy, 100);
                        poison = 5;
                        break;
                    }
            }

            SetStr(200, 250);
            SetDex(70, 100);
            SetInt(100, 130);

            SetHits(100, 150);

            SetDamage(8, 18);

            SetDamageType(ResistType.Phys, 0);

            SetResist(ResistType.Phys, 95);
            SetResist(ResistType.Fire, fire);
            SetResist(ResistType.Cold, cold);
            SetResist(ResistType.Pois, poison);
            SetResist(ResistType.Engy, energy);

            SetSkill(SkillName.MagicResist, 60.0, 80.0);
            SetSkill(SkillName.Tactics, 75.0, 100.0);
            SetSkill(SkillName.Wrestling, 85.0, 100.0);
            SetSkill(SkillName.DetectHidden, 50.0);
            SetSkill(SkillName.Magery, 110.0, 120.0);
            SetSkill(SkillName.Meditation, 150.0, 155.0);
            SetSkill(SkillName.Focus, 0.0, 60.0);

            Fame = 3000;
            Karma = -3000;

            VirtualArmor = 38;

            PackItem(Loot.PackReg(3));
            PackItem(Loot.PackNecroReg(3, 10));

            PackItem(new Bone());
        }

        public SkelementalMage(Serial serial)
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

        public override void OnBeforeDamage(Mobile from, ref int totalDamage, Server.DamageType type)
        {
            if (Region.IsPartOf("Khaldun") && IsChampionSpawn && !Caddellite.CheckDamage(from, type))
            {
                totalDamage = 0;
            }

            base.OnBeforeDamage(from, ref totalDamage, type);
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
