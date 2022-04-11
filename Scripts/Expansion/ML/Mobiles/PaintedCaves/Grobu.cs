using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a Grobu corpse")]
    public class Grobu : BlackBear
    {
        [Constructible]
        public Grobu()
        {
            Name = "Grobu";
            Hue = 0x455;

            AI = AIType.AI_Melee;
            FightMode = FightMode.Closest;

            SetStr(192, 210);
            SetDex(132, 150);
            SetInt(50, 52);

            SetHits(1235, 1299);
            SetStam(132, 150);
            SetMana(9);

            SetDamage(15, 18);

            SetDamageType(ResistType.Physical, 100);

            SetResist(ResistType.Physical, 40, 45);
            SetResist(ResistType.Fire, 20, 40);
            SetResist(ResistType.Cold, 32, 35);
            SetResist(ResistType.Poison, 25, 30);
            SetResist(ResistType.Energy, 22, 34);

            SetSkill(SkillName.Wrestling, 96.4, 119.0);
            SetSkill(SkillName.Tactics, 96.2, 116.5);
            SetSkill(SkillName.MagicResist, 66.2, 83.7);

            Fame = 1000;
            Karma = 1000;

            Tamable = false;
        }

        public Grobu(Serial serial)
            : base(serial)
        {
        }
        public override bool CanBeParagon => false;

        public override void OnDeath(Container CorpseLoot)
        {
            for (int i = 0; i < Utility.RandomMinMax(0, 1); i++)
            {
                CorpseLoot.DropItem(Loot.RandomScroll(0, Loot.ArcanistScrollTypes.Length, SpellbookType.Arcanist));
            }

            CorpseLoot.DropItem(new GrobusFur());

            base.OnDeath(CorpseLoot);
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich, 2);
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
