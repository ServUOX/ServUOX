using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("an ore elemental corpse")]
    public class BronzeElemental : BaseCreature
    {
        [Constructible]
        public BronzeElemental()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            // TODO: Gas attack
            Name = "a bronze elemental";
            Body = 108;
            BaseSoundID = 268;

            SetStr(226, 255);
            SetDex(126, 145);
            SetInt(71, 92);

            SetHits(136, 153);

            SetDamage(9, 16);

            SetDamageType(ResistType.Phys, 30);
            SetDamageType(ResistType.Fire, 70);

            SetResist(ResistType.Phys, 30, 40);
            SetResist(ResistType.Fire, 30, 40);
            SetResist(ResistType.Cold, 10, 20);
            SetResist(ResistType.Pois, 70, 80);
            SetResist(ResistType.Engy, 20, 30);

            SetSkill(SkillName.MagicResist, 50.1, 95.0);
            SetSkill(SkillName.Tactics, 60.1, 100.0);
            SetSkill(SkillName.Wrestling, 60.1, 100.0);

            Fame = 5000;
            Karma = -5000;

            VirtualArmor = 29;

            SetAreaEffect(AreaEffect.PoisonBreath);
        }

        public BronzeElemental(Serial serial)
            : base(serial)
        {
        }

        public override bool AutoDispel => true;
        public override bool BleedImmunity => true;
        public override int TreasureMapLevel => 1;

        public override void OnDeath(Container CorpseLoot)
        {
            CorpseLoot.DropItem(new BronzeOre(25));
            //ore.ItemID = 0x19B9;
            base.OnDeath(CorpseLoot);
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
