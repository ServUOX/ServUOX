using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a minotaur corpse")]
    public class MinotaurScout : BaseCreature
    {
        [Constructible]
        public MinotaurScout()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)// NEED TO CHECK
        {
            Name = "a minotaur scout";
            Body = 281;

            SetStr(353, 375);
            SetDex(111, 130);
            SetInt(34, 50);

            SetHits(354, 383);

            SetDamage(11, 20);

            SetDamageType(ResistType.Physical, 100);

            SetResist(ResistType.Physical, 55, 65);
            SetResist(ResistType.Fire, 25, 35);
            SetResist(ResistType.Cold, 30, 40);
            SetResist(ResistType.Poison, 30, 40);
            SetResist(ResistType.Energy, 30, 40);

            SetSkill(SkillName.Anatomy, 0);
            SetSkill(SkillName.MagicResist, 60.6, 67.5);
            SetSkill(SkillName.Tactics, 86.9, 103.6);
            SetSkill(SkillName.Wrestling, 85.6, 104.5);

            Fame = 5000;
            Karma = -5000;

            VirtualArmor = 28; // Don't know what it should be

            for (int i = 0; i < Utility.RandomMinMax(0, 1); i++)
            {
                PackItem(Loot.RandomScroll(0, Loot.ArcanistScrollTypes.Length, SpellbookType.Arcanist));
            }

            SetWeaponAbility(WeaponAbility.ParalyzingBlow);
        }

        public MinotaurScout(Serial serial)
            : base(serial)
        {
        }

        public override int TreasureMapLevel => 3;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);  // Need to verify
        }

        // Using Tormented Minotaur sounds - Need to veryfy
        public override int GetAngerSound()
        {
            return 0x597;
        }

        public override int GetIdleSound()
        {
            return 0x596;
        }

        public override int GetAttackSound()
        {
            return 0x599;
        }

        public override int GetHurtSound()
        {
            return 0x59a;
        }

        public override int GetDeathSound()
        {
            return 0x59c;
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}