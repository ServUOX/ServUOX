using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a maddening horror corpse")]
    public class MaddeningHorror : BaseCreature
    {
        [Constructible]
        public MaddeningHorror()
            : base(AIType.AI_NecroMage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a maddening horror";
            Body = 721;

            SetStr(270, 290);
            SetDex(80, 100);
            SetInt(850);

            SetHits(660);

            SetDamage(15, 27);

            SetDamageType(ResistType.Physical, 20);
            SetDamageType(ResistType.Cold, 40);
            SetDamageType(ResistType.Energy, 40);

            SetResist(ResistType.Physical, 55, 65);
            SetResist(ResistType.Fire, 20, 30);
            SetResist(ResistType.Cold, 50, 60);
            SetResist(ResistType.Poison, 40, 50);
            SetResist(ResistType.Energy, 50, 60);

            SetSkill(SkillName.EvalInt, 120.0, 130.0);
            SetSkill(SkillName.Magery, 120.0, 130.0);
            SetSkill(SkillName.Meditation, 100.0, 110.0);
            SetSkill(SkillName.MagicResist, 180.0, 195.0);
            SetSkill(SkillName.Tactics, 95.0, 100.0);
            SetSkill(SkillName.Wrestling, 80.0, 85.0);
            SetSkill(SkillName.Poisoning, 110.0);
            SetSkill(SkillName.DetectHidden, 100.0);
            SetSkill(SkillName.Necromancy, 120.0);
            SetSkill(SkillName.SpiritSpeak, 120.0);

            Fame = 23000;
            Karma = -23000;

            SetSpecialAbility(SpecialAbility.ManaDrain);
        }

        public MaddeningHorror(Serial serial)
            : base(serial)
        {
        }

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);

            if (0.2 > Utility.RandomDouble())
                c.DropItem(new VileTentacles());
        }

        public override int GetIdleSound()
        {
            return 1553;
        }

        public override int GetAngerSound()
        {
            return 1550;
        }

        public override int GetHurtSound()
        {
            return 1552;
        }

        public override int GetDeathSound()
        {
            return 1551;
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
