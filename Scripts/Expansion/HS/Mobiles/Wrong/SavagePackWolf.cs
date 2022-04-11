using System;

namespace Server.Mobiles
{
    [CorpseName("a wolf corpse")]
    [TypeAlias("Server.Mobiles.SavagePackwolf")]
    public class SavagePackWolf : BaseCreature
    {
        [Constructible]
        public SavagePackWolf()
            : base(AIType.AI_Melee, FightMode.Weakest, 10, 1, 0.2, 0.4)
        {
            Name = "a savage pack wolf";
            Body = 0xE1;
            BaseSoundID = 0xE5;

            SetStr(100, 116);
            SetDex(51, 61);
            SetInt(11, 21);

            SetHits(650, 671);
            SetMana(0);

            SetDamage(9, 12);

            SetDamageType(ResistType.Physical, 100);

            SetResist(ResistType.Physical, 50, 60);
            SetResist(ResistType.Fire, 50, 60);
            SetResist(ResistType.Cold, 60, 70);
            SetResist(ResistType.Poison, 50, 60);
            SetResist(ResistType.Energy, 50, 60);

            SetSkill(SkillName.MagicResist, 60.7, 74.0);
            SetSkill(SkillName.Tactics, 80.9, 94.4);
            SetSkill(SkillName.Wrestling, 89.0, 97.1);

            Fame = 450;
            Karma = -450;

            VirtualArmor = 26;
            Tamable = false;
        }

        public SavagePackWolf(Serial serial)
            : base(serial)
        {
        }

        public override bool AlwaysMurderer => true;
        public override int Meat => 1;
        public override int Hides => 5;
        public override FoodType FavoriteFood => FoodType.Meat;
        public override PackInstinct PackInstinct => PackInstinct.Canine;

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
