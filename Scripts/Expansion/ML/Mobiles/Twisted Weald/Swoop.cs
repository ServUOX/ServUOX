using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a Swoop corpse")]
    public class Swoop : Eagle
    {
        [Constructible]
        public Swoop()
        {
            Name = "Swoop";
            Hue = 0xE0;

            AI = AIType.AI_Melee;

            SetStr(100, 150);
            SetDex(400, 480);
            SetInt(75, 90);

            SetHits(1350, 1550);

            SetDamage(20, 30);

            SetDamageType(ResistType.Physical, 100);

            SetResist(ResistType.Physical, 75, 90);
            SetResist(ResistType.Fire, 60, 70);
            SetResist(ResistType.Cold, 70, 85);
            SetResist(ResistType.Poison, 55, 60);
            SetResist(ResistType.Energy, 50, 60);

            SetSkill(SkillName.Wrestling, 120.0, 140.0);
            SetSkill(SkillName.Tactics, 120.0, 140.0);
            SetSkill(SkillName.MagicResist, 95.0, 105.0);

            Fame = 18000;
            Karma = 0;

            PackItem(Loot.PackReg(4));

            Tamable = false;

            for (int i = 0; i < Utility.RandomMinMax(0, 1); i++)
            {
                PackItem(Loot.RandomScroll(0, Loot.ArcanistScrollTypes.Length, SpellbookType.Arcanist));
            }

            SetSpecialAbility(SpecialAbility.GraspingClaw);
        }

        public override bool CanBeParagon => false;

        public override void OnDeath(Container CorpseLoot)
        {
            base.OnDeath(CorpseLoot);

            if (Utility.RandomDouble() < 0.025)
            {
                switch (Utility.Random(20))
                {
                    case 0: CorpseLoot.DropItem(new AssassinChest()); break;
                    case 1: CorpseLoot.DropItem(new AssassinArms()); break;
                    case 2: CorpseLoot.DropItem(new DeathChest()); break;
                    case 3: CorpseLoot.DropItem(new MyrmidonArms()); break;
                    case 4: CorpseLoot.DropItem(new MyrmidonLegs()); break;
                    case 5: CorpseLoot.DropItem(new MyrmidonGorget()); break;
                    case 6: CorpseLoot.DropItem(new LeafweaveGloves()); break;
                    case 7: CorpseLoot.DropItem(new LeafweaveLegs()); break;
                    case 8: CorpseLoot.DropItem(new LeafweavePauldrons()); break;
                    case 9: CorpseLoot.DropItem(new PaladinGloves()); break;
                    case 10: CorpseLoot.DropItem(new PaladinGorget()); break;
                    case 11: CorpseLoot.DropItem(new PaladinArms()); break;
                    case 12: CorpseLoot.DropItem(new HunterArms()); break;
                    case 13: CorpseLoot.DropItem(new HunterGloves()); break;
                    case 14: CorpseLoot.DropItem(new HunterLegs()); break;
                    case 15: CorpseLoot.DropItem(new HunterChest()); break;
                    case 16: CorpseLoot.DropItem(new GreymistArms()); break;
                    case 17: CorpseLoot.DropItem(new GreymistGloves()); break;
                    case 18: CorpseLoot.DropItem(new GreymistLegs()); break;
                    case 19: CorpseLoot.DropItem(new MyrmidonChest()); break;
                }
            }

            if (Utility.RandomDouble() < 0.1)
                CorpseLoot.DropItem(new ParrotItem());
        }

        public Swoop(Serial serial)
            : base(serial)
        {
        }

        public override bool CanFly => true;
        public override bool GivesMLMinorArtifact => true;
        public override int Feathers => 72;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.UltraRich, 2);
            AddLoot(LootPack.HighScrolls);
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
