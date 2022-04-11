using System;

namespace Server.Mobiles
{
    [CorpseName("an undead guardian corpse")]
    public class UndeadGuardian : BaseCreature
    {
        [Constructible]
        public UndeadGuardian()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "an undead guardian";
            Body = 722;

            SetStr(212);
            SetDex(76);
            SetInt(56);

            SetHits(138);

            SetDamage(8, 18);

            SetDamageType(ResistanceType.Physical, 40);
            SetDamageType(ResistanceType.Cold, 60);

            SetResist(ResistanceType.Physical, 38);
            SetResist(ResistanceType.Fire, 24);
            SetResist(ResistanceType.Cold, 58);
            SetResist(ResistanceType.Poison, 28);
            SetResist(ResistanceType.Energy, 38);

            SetSkill(SkillName.MagicResist, 66.6);
            SetSkill(SkillName.Tactics, 86.2);
            SetSkill(SkillName.Wrestling, 86.9);

            PackItem(Loot.PackNecroReg(10, 15));
        }

        public UndeadGuardian(Serial serial)
            : base(serial)
        {
        }

        public override int Meat => 1;
        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich, 3);
        }

        public override int GetIdleSound()
        {
            return 1609;
        }

        public override int GetAngerSound()
        {
            return 1606;
        }

        public override int GetHurtSound()
        {
            return 1608;
        }

        public override int GetDeathSound()
        {
            return 1607;
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
