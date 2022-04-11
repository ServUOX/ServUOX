using System;

namespace Server.Mobiles
{
    [CorpseName("a white wyrm corpse")]
    public class WhiteWyrm : BaseCreature
    {
        public override double AverageThreshold => 0.25;

        [Constructible]
        public WhiteWyrm()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Body = Utility.RandomBool() ? 180 : 49;
            Name = "a white wyrm";
            BaseSoundID = 362;

            SetStr(721, 760);
            SetDex(101, 130);
            SetInt(386, 425);

            SetHits(433, 456);

            SetDamage(17, 25);

            SetDamageType(ResistType.Physical, 50);
            SetDamageType(ResistType.Cold, 50);

            SetResist(ResistType.Physical, 55, 70);
            SetResist(ResistType.Fire, 15, 25);
            SetResist(ResistType.Cold, 80, 90);
            SetResist(ResistType.Poison, 40, 50);
            SetResist(ResistType.Energy, 40, 50);

            SetSkill(SkillName.EvalInt, 99.1, 100.0);
            SetSkill(SkillName.Magery, 99.1, 100.0);
            SetSkill(SkillName.MagicResist, 99.1, 100.0);
            SetSkill(SkillName.Tactics, 97.6, 100.0);
            SetSkill(SkillName.Wrestling, 90.1, 100.0);

            Fame = 18000;
            Karma = -18000;

            VirtualArmor = 64;

            Tamable = true;
            ControlSlots = 3;
            MinTameSkill = 96.3;
        }

        public WhiteWyrm(Serial serial)
            : base(serial)
        {
        }

        public override bool ReacquireOnMovement => true;
        public override int TreasureMapLevel => 4;
        public override int Meat => 19;
        public override int DragonBlood => 8;
        public override int Hides => 20;
        public override HideType HideType => HideType.Barbed;
        public override int Scales => 9;
        public override ScaleType ScaleType => ScaleType.White;
        public override FoodType FavoriteFood => FoodType.Meat | FoodType.Gold;
        public override bool CanAngerOnTame => true;
        public override bool CanFly => true;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich, 2);
            AddLoot(LootPack.Average);
            AddLoot(LootPack.Gems, Utility.Random(1, 5));
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadInt();
        }
    }
}
