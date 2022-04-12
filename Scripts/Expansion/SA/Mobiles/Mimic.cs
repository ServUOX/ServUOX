using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a mimic corpse")]
    public class Mimic : BaseCreature
    {
        [Constructible]
        public Mimic()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a mimic";
            Body = 729;

            SetStr(281);
            SetDex(140);
            SetInt(261);

            SetHits(543);

            SetDamage(13, 20);

            SetDamageType(ResistType.Phys, 20);
            SetDamageType(ResistType.Fire, 20);
            SetDamageType(ResistType.Cold, 20);
            SetDamageType(ResistType.Pois, 20);
            SetDamageType(ResistType.Engy, 20);

            SetResist(ResistType.Phys, 63);
            SetResist(ResistType.Fire, 43);
            SetResist(ResistType.Cold, 36);
            SetResist(ResistType.Pois, 37);
            SetResist(ResistType.Engy, 42);

            SetSkill(SkillName.EvalInt, 100.0);
            SetSkill(SkillName.Magery, 107.5);
            SetSkill(SkillName.Meditation, 100.0);
            SetSkill(SkillName.MagicResist, 126.5);
            SetSkill(SkillName.Tactics, 98.5);
            SetSkill(SkillName.Wrestling, 92.2);

            PackItem(Loot.PackReg(20));
        }

        public Mimic(Serial serial)
            : base(serial)
        {
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich, 4);
            AddLoot(LootPack.MedScrolls);
        }

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);

            if (Utility.RandomDouble() < 0.03)
                c.DropItem(new LuckyCoin());
        }

        public override int GetIdleSound()
        {
            return 1561;
        }

        public override int GetAngerSound()
        {
            return 1558;
        }

        public override int GetHurtSound()
        {
            return 1560;
        }

        public override int GetDeathSound()
        {
            return 1559;
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