using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a Master Jonath corpse")]
    public class MasterJonath : BoneMagi
    {
        [Constructible]
        public MasterJonath()
        {
            Name = "Master Jonath";
            Hue = 0x455;

            SetStr(109, 131);
            SetDex(98, 110);
            SetInt(232, 259);

            SetHits(766, 920);

            SetDamage(10, 15);

            SetDamageType(ResistType.Phys, 100);

            SetResist(ResistType.Phys, 55, 60);
            SetResist(ResistType.Fire, 43, 49);
            SetResist(ResistType.Cold, 45, 80);
            SetResist(ResistType.Pois, 41, 45);
            SetResist(ResistType.Engy, 54, 55);

            SetSkill(SkillName.Wrestling, 80.5, 88.6);
            SetSkill(SkillName.Tactics, 88.5, 95.1);
            SetSkill(SkillName.MagicResist, 102.7, 102.9);
            SetSkill(SkillName.Magery, 100.0, 106.6);
            SetSkill(SkillName.EvalInt, 99.6, 106.9);
            SetSkill(SkillName.Necromancy, 100.0, 106.6);
            SetSkill(SkillName.SpiritSpeak, 99.6, 106.9);

            Fame = 18000;
            Karma = -18000;

            PackItem(Loot.PackReg(7));
            PackItem(Loot.PackReg(7));
            PackItem(Loot.PackReg(8));
        }

        public MasterJonath(Serial serial)
            : base(serial)
        {
        }
        public override bool CanBeParagon => false;

        public override void OnDeath(Container c)
        {
            for (int i = 0; i < Utility.RandomMinMax(0, 1); i++)
            {
                c.DropItem(Loot.RandomScroll(0, Loot.ArcanistScrollTypes.Length, SpellbookType.Arcanist));
            }

            if (Utility.RandomDouble() < 0.05)
                c.DropItem(new ParrotItem());

            if (Utility.RandomDouble() < 0.15)
                c.DropItem(new DisintegratingThesisNotes());

            base.OnDeath(c);
        }

        public override int TreasureMapLevel => 5;
        public override void GenerateLoot()
        {
            AddLoot(LootPack.UltraRich, 3);
            AddLoot(LootPack.MedScrolls, 2);
            AddLoot(LootPack.HighScrolls, 2);
        }

        // TODO: Special move?
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
