using System;
using Server.Items;
using Server.Services;

namespace Server.Mobiles
{
    [CorpseName("a fire ant corpse")]
    public class FireAnt : BaseCreature
    {
        [Constructible]
        public FireAnt() : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a fire ant";
            Body = 738;

            SetStr(225);
            SetDex(108);
            SetInt(25);

            SetHits(299);

            SetDamage(15, 18);

            SetDamageType(ResistanceType.Physical, 40);
            SetDamageType(ResistanceType.Fire, 60);

            SetResist(ResistanceType.Physical, 52);
            SetResist(ResistanceType.Fire, 96);
            SetResist(ResistanceType.Cold, 36);
            SetResist(ResistanceType.Poison, 40);
            SetResist(ResistanceType.Energy, 36);

            SetSkill(SkillName.Anatomy, 8.7);
            SetSkill(SkillName.MagicResist, 53.1);
            SetSkill(SkillName.Tactics, 77.2);
            SetSkill(SkillName.Wrestling, 75.4);

            SetAreaEffect(AreaEffect.ExplosiveGoo);
        }

        public FireAnt(Serial serial) : base(serial)
        {
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Average, 2);
        }

        public override int TreasureMapLevel => 3;

        public override int GetIdleSound()
        {
            return 846;
        }

        public override int GetAngerSound()
        {
            return 849;
        }

        public override int GetHurtSound()
        {
            return 852;
        }

        public override int GetDeathSound()
        {
            return 850;
        }

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);

            if (Utility.RandomDouble() < 0.25)
            {
                c.DropItem(new SearedFireAntGoo());
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
            var version = reader.ReadInt();
        }
    }
}