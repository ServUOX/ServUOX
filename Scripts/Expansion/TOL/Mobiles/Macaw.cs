using System;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{
    public class Macaw : BaseCreature
    {
        [CommandProperty(AccessLevel.GameMaster)]
        public MacawSpawner MacawSpawner { get; set; }

        [Constructible]
        public Macaw()
            : this(null)
        {
        }

        public Macaw(MacawSpawner spawner)
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, .15, .03)
        {
            MacawSpawner = spawner;

            Name = "vicious macaw";
            Body = 5;
            Hue = Utility.RandomBirdHue();

            SetStr(100, 150);
            SetDex(400, 500);
            SetInt(80, 90);

            SetHits(700, 800);

            SetDamage(15, 25);

            SetDamageType(ResistType.Phys, 100);

            SetResist(ResistType.Phys, 80, 90);
            SetResist(ResistType.Fire, 60, 77);
            SetResist(ResistType.Cold, 70, 85);
            SetResist(ResistType.Pois, 55, 85);
            SetResist(ResistType.Engy, 50, 60);

            SetSkill(SkillName.Wrestling, 120.0, 140.0);
            SetSkill(SkillName.Tactics, 120.0, 140.0);
            SetSkill(SkillName.MagicResist, 95.0, 105.0);

            Fame = 7000;
            Karma = -7000;
        }

        public override int GetIdleSound() { return 0x2EF; }
        public override int GetAttackSound() { return 0x2EE; }
        public override int GetAngerSound() { return 0x2EF; }
        public override int GetHurtSound() { return 0x2F1; }
        public override int GetDeathSound() { return 0x2F2; }

        public override MeatType MeatType => MeatType.Bird;
        public override int Feathers => 32;
        public override int Meat => 1;
        public override FoodType FavoriteFood => FoodType.FruitsAndVegies | FoodType.GrainsAndHay;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich, 2);
        }

        protected override void OnLocationChange(Point3D oldLocation)
        {
            base.OnLocationChange(oldLocation);

            if (MacawSpawner != null && !InRange(MacawSpawner.Location, 20))
            {
                MacawSpawner.Spawn.Remove(this);
                MacawSpawner = null;
            }
        }

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);

            if (0.25 > Utility.RandomDouble())
                c.DropItem(new GoldFoil());
        }

        public override void Delete()
        {
            base.Delete();

            if (MacawSpawner != null)
                MacawSpawner.Spawn.Remove(this);
        }

        public Macaw(Serial serial)
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