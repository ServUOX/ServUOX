using System;
using Server;
using Server.Items;
using Server.Engines.MyrmidexInvasion;

namespace Server.Mobiles
{
    [CorpseName("a myrmidex corpse")]
    public class MyrmidexLarvae : BaseCreature
    {
        [Constructible]
        public MyrmidexLarvae()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, .2, .4)
        {
            Name = "a myrmidex larvae";

            Body = 1293;
            BaseSoundID = 959;

            SetStr(79, 100);
            SetDex(82, 95);
            SetInt(38, 75);

            SetDamage(5, 13);

            SetHits(446, 588);
            SetMana(0);

            SetResist(ResistType.Phys, 20);
            SetResist(ResistType.Fire, 10, 20);
            SetResist(ResistType.Cold, 10, 20);
            SetResist(ResistType.Pois, 30, 40);
            SetResist(ResistType.Engy, 10, 20);

            SetDamageType(ResistType.Phys, 50);
            SetDamageType(ResistType.Pois, 50);

            SetSkill(SkillName.MagicResist, 30.1, 43.5);
            SetSkill(SkillName.Tactics, 30.1, 49.0);
            SetSkill(SkillName.Wrestling, 40, 50);

            PackItem(Loot.PackGold(20, 40));

            Fame = 2500;
            Karma = -2500;
        }

        public override Poison HitPoison => Poison.Lesser;
        public override Poison PoisonImmunity => Poison.Lesser;
        public override int TreasureMapLevel => 1;

        public override bool IsEnemy(Mobile m)
        {
            if (MyrmidexInvasionSystem.Active && MyrmidexInvasionSystem.IsAlliedWithEodonTribes(m))
                return true;

            if (MyrmidexInvasionSystem.Active && MyrmidexInvasionSystem.IsAlliedWithMyrmidex(m))
                return false;

            return base.IsEnemy(m);
        }

        public MyrmidexLarvae(Serial serial) : base(serial)
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
