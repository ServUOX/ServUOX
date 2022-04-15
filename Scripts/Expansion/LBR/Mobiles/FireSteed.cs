using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a fire steed corpse")]
    public class FireSteed : BaseMount
    {
        [Constructible]
        public FireSteed()
            : this("a fire steed")
        {
        }

        [Constructible]
        public FireSteed(string name)
            : base(name, 0xBE, 0x3E9E, AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            BaseSoundID = 0xA8;
            Hue = 1161;

            SetStr(376, 400);
            SetDex(91, 120);
            SetInt(291, 300);

            SetHits(226, 240);

            SetDamage(ResistType.Phys, 20 0, 11, 30);
            SetDamage(ResistType.Fire, 80);

            SetResist(ResistType.Phys, 30, 40);
            SetResist(ResistType.Fire, 70, 80);
            SetResist(ResistType.Cold, 20, 30);
            SetResist(ResistType.Pois, 30, 40);
            SetResist(ResistType.Engy, 30, 40);

            SetSkill(SkillName.MagicResist, 100.0, 120.0);
            SetSkill(SkillName.Tactics, 100.0);
            SetSkill(SkillName.Wrestling, 100.0);

            Fame = 20000;
            Karma = -20000;

            Tamable = true;
            ControlSlots = 2;
            MinTameSkill = 106.0;

            SetSpecialAbility(SpecialAbility.DragonBreath);
        }

        public FireSteed(Serial serial)
            : base(serial)
        {
        }

        public override FoodType FavoriteFood => FoodType.Meat;
        public override PackInstinct PackInstinct => PackInstinct.Daemon | PackInstinct.Equine;

        public override void OnDeath(Container CorpseLoot)
        {
            CorpseLoot.DropItem(new SulfurousAsh(Utility.RandomMinMax(151, 300)));
            CorpseLoot.DropItem(new Ruby(Utility.RandomMinMax(16, 30)));

            base.OnDeath(CorpseLoot);
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich, 2);
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
