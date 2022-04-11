using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a chaos daemon corpse")]
    public class ChaosDaemon : BaseCreature
    {
        [Constructible]
        public ChaosDaemon()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a chaos daemon";
            Body = 792;
            BaseSoundID = 0x3E9;

            SetStr(106, 130);
            SetDex(171, 200);
            SetInt(56, 80);

            SetHits(91, 110);

            SetDamage(12, 17);

            SetDamageType(ResistanceType.Physical, 85);
            SetDamageType(ResistanceType.Fire, 15);

            SetResist(ResistanceType.Physical, 50, 60);
            SetResist(ResistanceType.Fire, 60, 70);
            SetResist(ResistanceType.Cold, 40, 50);
            SetResist(ResistanceType.Poison, 20, 30);
            SetResist(ResistanceType.Energy, 20, 30);

            SetSkill(SkillName.MagicResist, 85.1, 95.0);
            SetSkill(SkillName.Tactics, 70.1, 80.0);
            SetSkill(SkillName.Wrestling, 95.1, 100.0);

            Fame = 3000;
            Karma = -4000;

            VirtualArmor = 15;

            SetWeaponAbility(WeaponAbility.CrushingBlow);
        }

        public ChaosDaemon(Serial serial)
            : base(serial)
        {
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Average);
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
            int version = reader.ReadInt();
        }
    }
}