using System;
using Server;
using Server.Items;
using Server.Engines.MyrmidexInvasion;

namespace Server.Mobiles
{
    [CorpseName("a myrmidex corpse")]
    public class DescicatedMyrmidexLarvae : BaseCreature
    {
        [Constructible]
        public DescicatedMyrmidexLarvae()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, .2, .4)
        {
            Name = "a descicated myrmidex larvae";
            Hue = 2949;

            Body = 1293;
            BaseSoundID = 959;

            SetStr(350, 450);
            SetDex(80, 95);
            SetInt(15, 25);

            SetDamage(5, 10);

            SetHits(446, 588);
            SetMana(20, 50);

            SetResist(ResistType.Phys, 20, 25);
            SetResist(ResistType.Fire, 10, 20);
            SetResist(ResistType.Cold, 15, 25);
            SetResist(ResistType.Pois, 40, 50);
            SetResist(ResistType.Engy, 10, 20);

            SetDamageType(ResistType.Phys, 60);
            SetDamageType(ResistType.Pois, 40);

            SetSkill(SkillName.MagicResist, 30.1, 43.5);
            SetSkill(SkillName.Tactics, 60, 70);
            SetSkill(SkillName.Wrestling, 55, 60);
            SetSkill(SkillName.Poisoning, 80, 100);
            SetSkill(SkillName.DetectHidden, 30, 40);

            PackItem(Loot.PackGold(20, 40));

            Fame = 2500;
            Karma = -2500;
        }

        public override Poison HitPoison => Poison.Lesser;
        public override Poison PoisonImmunity => Poison.Lesser;

        public DescicatedMyrmidexLarvae(Serial serial)
            : base(serial)
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
