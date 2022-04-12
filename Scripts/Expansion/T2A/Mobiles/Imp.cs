using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("an imp corpse")]
    public class Imp : BaseCreature
    {
        [Constructible]
        public Imp()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "an imp";
            Body = 74;
            BaseSoundID = 422;

            SetStr(91, 115);
            SetDex(61, 80);
            SetInt(86, 105);

            SetHits(55, 70);

            SetDamage(10, 14);

            SetDamageType(ResistType.Phys, 0);
            SetDamageType(ResistType.Fire, 50);
            SetDamageType(ResistType.Pois, 50);

            SetResist(ResistType.Phys, 25, 35);
            SetResist(ResistType.Fire, 40, 50);
            SetResist(ResistType.Cold, 20, 30);
            SetResist(ResistType.Pois, 30, 40);
            SetResist(ResistType.Engy, 30, 40);

            SetSkill(SkillName.EvalInt, 20.1, 30.0);
            SetSkill(SkillName.Magery, 90.1, 100.0);
            SetSkill(SkillName.MagicResist, 30.1, 50.0);
            SetSkill(SkillName.Tactics, 42.1, 50.0);
            SetSkill(SkillName.Wrestling, 40.1, 44.0);
            SetSkill(SkillName.Necromancy, 20);
            SetSkill(SkillName.SpiritSpeak, 20);

            Fame = 2500;
            Karma = -2500;

            VirtualArmor = 30;

            Tamable = true;
            ControlSlots = 2;
            MinTameSkill = 83.1;
        }

        public Imp(Serial serial)
            : base(serial)
        {
        }

        public override int Meat => 1;
        public override int Hides => 6;
        public override HideType HideType => HideType.Spined;
        public override FoodType FavoriteFood => FoodType.Meat;
        public override PackInstinct PackInstinct => PackInstinct.Daemon;
        public override bool CanFly => true;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Meager);
            AddLoot(LootPack.MedScrolls, 2);
        }

        public override void OnDeath(Container CorpseLoot)
        {
            switch (Utility.Random(104))
            {
                case 0: PackItem(new BloodOathScroll()); break;
                case 1: PackItem(new CorpseSkinScroll()); break;
                case 2: PackItem(new CurseWeaponScroll()); break;
                case 3: PackItem(new EvilOmenScroll()); break;
                case 4: PackItem(new HorrificBeastScroll()); break;
                case 5: PackItem(new LichFormScroll()); break;
                case 6: PackItem(new MindRotScroll()); break;
                case 7: PackItem(new PainSpikeScroll()); break;
                case 8: PackItem(new PoisonStrikeScroll()); break;
                case 9: PackItem(new StrangleScroll()); break;
                case 10: PackItem(new SummonFamiliarScroll()); break;
                case 11: PackItem(new WitherScroll()); break;
                case 12: PackItem(new WraithFormScroll()); break;
            }
            base.OnDeath(CorpseLoot);
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
