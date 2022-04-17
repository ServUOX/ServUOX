using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a human corpse")]
    public class Protector : BaseCreature
    {
        [Constructible]
        public Protector()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Body = 401;
            Female = true;
            Hue = Race.Human.RandomSkinHue();
            HairItemID = Race.Human.RandomHair(this);
            HairHue = Race.Human.RandomHairHue();

            Name = "A Protector";
            Title = "The Mystic Llamaherder";

            SetStr(700, 800);
            SetDex(100, 150);
            SetInt(50, 75);

            SetHits(350, 450);

            SetDamage(ResistType.Phys, 100, 0, 6, 12);

            SetResist(ResistType.Phys, 30, 40);
            SetResist(ResistType.Fire, 20, 30);
            SetResist(ResistType.Cold, 35, 40);
            SetResist(ResistType.Pois, 30, 40);
            SetResist(ResistType.Engy, 30, 40);

            SetSkill(SkillName.Wrestling, 70.0, 100.0);
            SetSkill(SkillName.Tactics, 80.0, 100.0);
            SetSkill(SkillName.MagicResist, 50.0, 70.0);
            SetSkill(SkillName.Anatomy, 70.0, 100.0);

            Fame = 10000;
            Karma = -10000;

            Item boots = new ThighBoots
            {
                Movable = false,
                Hue = Utility.Random(2)
            };
            AddItem(boots);

            Item shroud = new Item(0x204E)
            {
                Layer = Layer.OuterTorso,
                Movable = false,
                Hue = Utility.Random(2)
            }; 
            AddItem(shroud);
        }

        public Protector(Serial serial)
            : base(serial)
        {
        }

        public override bool AlwaysMurderer => true;
        public override bool PropertyTitle => false;
        public override bool DisplayFameTitle => false;

        public override void GenerateLoot(bool spawning)
        {
            if (spawning)
                return;

            base.GenerateLoot(true);
            base.GenerateLoot(false);
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich);
        }

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);

            if (Utility.RandomDouble() < 0.4)
                c.DropItem(new ProtectorsEssence());
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
