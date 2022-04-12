using System;
using Server.Items;
using System.Collections.Generic;

namespace Server.Mobiles
{
    [CorpseName("a korpre corpse")]
    public class Korpre : BaseVoidCreature
    {
        public override VoidEvolution Evolution => VoidEvolution.None;
        public override int Stage => 0;

        [Constructible]
        public Korpre()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "korpre";
            Body = 51;
            BaseSoundID = 456;

            Hue = 2071;

            SetStr(22, 34);
            SetDex(16, 21);
            SetInt(16, 20);

            SetHits(50, 60);

            SetDamage(1, 5);

            SetDamageType(ResistType.Phys, 100);

            SetResist(ResistType.Phys, 5, 10);
            SetResist(ResistType.Pois, 15, 20);

            SetSkill(SkillName.Poisoning, 36.0, 49.1);
            SetSkill(SkillName.Anatomy, 0);
            SetSkill(SkillName.MagicResist, 15.9, 18.9);
            SetSkill(SkillName.Tactics, 24.6, 26.1);
            SetSkill(SkillName.Wrestling, 24.9, 26.1);

            Fame = 300;
            Karma = -300;

            VirtualArmor = 8;
        }

        public Korpre(Serial serial)
            : base(serial)
        {
        }

        public override Poison PoisonImmunity => Poison.Regular;
        public override Poison HitPoison => Poison.Regular;
        public override FoodType FavoriteFood => FoodType.Fish;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Poor);
            AddLoot(LootPack.Gems);
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
