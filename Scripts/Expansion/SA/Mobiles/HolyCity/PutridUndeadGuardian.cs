using System;

namespace Server.Mobiles
{
    [CorpseName("an putrid undead guardian corpse")]
    public class PutridUndeadGuardian : BaseCreature
    {
        [Constructible]
        public PutridUndeadGuardian()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "an putrid undead guardian";
            Body = 722;

            SetStr(79);
            SetDex(63);
            SetInt(187);

            SetHits(553);

            SetDamage(3, 7);

            SetDamageType(ResistType.Phys, 100);

            SetResist(ResistType.Phys, 40);
            SetResist(ResistType.Fire, 23);
            SetResist(ResistType.Cold, 57);
            SetResist(ResistType.Pois, 29);
            SetResist(ResistType.Engy, 39);

            SetSkill(SkillName.MagicResist, 62.7);
            SetSkill(SkillName.Tactics, 45.4);
            SetSkill(SkillName.Wrestling, 50.7);

            Fame = 3000;
            Karma = -3000;

            PackItem(Loot.PackNecroReg(10, 15));
        }

        public PutridUndeadGuardian(Serial serial)
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
