using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("an ore elemental corpse")]
    public class AgapiteElemental : BaseCreature
    {
        [Constructible]
        public AgapiteElemental()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "an agapite elemental";
            Body = 107;
            BaseSoundID = 268;

            SetStr(226, 255);
            SetDex(126, 145);
            SetInt(71, 92);

            SetHits(136, 153);

            SetDamage(28);

            SetDamageType(ResistanceType.Physical, 100);

            SetResist(ResistanceType.Physical, 30, 40);
            SetResist(ResistanceType.Fire, 40, 50);
            SetResist(ResistanceType.Cold, 40, 50);
            SetResist(ResistanceType.Poison, 30, 40);
            SetResist(ResistanceType.Energy, 10, 20);

            SetSkill(SkillName.MagicResist, 50.1, 95.0);
            SetSkill(SkillName.Tactics, 60.1, 100.0);
            SetSkill(SkillName.Wrestling, 60.1, 100.0);

            Fame = 3500;
            Karma = -3500;

            VirtualArmor = 32;
        }

        public AgapiteElemental(Serial serial)
            : base(serial)
        {
        }

        public override bool AutoDispel => true;
        public override bool BleedImmunity => true;
        public override int TreasureMapLevel => 1;

        public override void OnDeath(Container CorspeLoot)
        {
            CorspeLoot.DropItem(new AgapiteOre(25));
            //ore.ItemID = 0x19B9;
            base.OnDeath(CorspeLoot);
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Average);
            AddLoot(LootPack.Gems, 2);
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
