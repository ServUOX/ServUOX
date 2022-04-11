using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a greater phoenix corpse")]
    public class GreaterPhoenix : BaseCreature
    {
        [Constructible]
        public GreaterPhoenix()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, .2, .4)
        {
            Name = "a greater phoenix";
            Body = 832;
            BaseSoundID = 0x8F;

            SetStr(332, 386);
            SetDex(97, 113);
            SetInt(182, 258);

            SetDamage(11, 14);

            SetHits(119, 240);

            SetResist(ResistanceType.Physical, 45, 55);
            SetResist(ResistanceType.Fire, 70, 80);
            SetResist(ResistanceType.Cold, 20, 30);
            SetResist(ResistanceType.Poison, 40, 50);
            SetResist(ResistanceType.Energy, 35, 45);

            SetDamageType(ResistanceType.Physical, 40);
            SetDamageType(ResistanceType.Fire, 60);

            SetSkill(SkillName.Wrestling, 60, 77);
            SetSkill(SkillName.MagicResist, 90, 105);
            SetSkill(SkillName.Tactics, 50, 70);
            SetSkill(SkillName.Magery, 90, 100);
            SetSkill(SkillName.EvalInt, 90, 100);

            PackItem(Loot.PackGold(500, 700));

            Fame = 10000;
            Karma = -10000;

            SetSpecialAbility(SpecialAbility.GraspingClaw);
        }

        public GreaterPhoenix(Serial serial) : base(serial)
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
            int version = reader.ReadInt();
        }
    }
}
