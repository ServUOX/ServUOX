using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("an ore elemental corpse")]
    public class ShadowIronElemental : BaseCreature
    {
        [Constructible]
        public ShadowIronElemental()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a shadow iron elemental";
            Body = 111;
            BaseSoundID = 268;

            SetStr(226, 255);
            SetDex(126, 145);
            SetInt(71, 92);

            SetHits(136, 153);

            SetDamage(ResistType.Phys, 100, 0, 9, 16);

            SetResist(ResistType.Phys, 30, 40);
            SetResist(ResistType.Fire, 30, 40);
            SetResist(ResistType.Cold, 20, 30);
            SetResist(ResistType.Pois, 10, 20);
            SetResist(ResistType.Engy, 30, 40);

            SetSkill(SkillName.MagicResist, 50.1, 95.0);
            SetSkill(SkillName.Tactics, 60.1, 100.0);
            SetSkill(SkillName.Wrestling, 60.1, 100.0);

            Fame = 4500;
            Karma = -4500;

            VirtualArmor = 23;
        }

        public ShadowIronElemental(Serial serial)
            : base(serial)
        {
        }

        public override bool AutoDispel => true;
        public override bool BleedImmunity => true;
        public override int TreasureMapLevel => 1;
        public override Poison PoisonImmunity => Poison.Deadly;
        public override bool BreathImmune => true;

        public override void OnDeath(Container CorpseLoot)
        {
            CorpseLoot.DropItem(new ShadowIronOre(25));
            //ore.ItemID = 0x19B9;
            base.OnDeath(CorpseLoot);
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Average);
            AddLoot(LootPack.Gems, 2);
        }

        public override void AlterMeleeDamageFrom(Mobile from, ref int damage)
        {
            if (from is BaseCreature)
            {
                BaseCreature bc = (BaseCreature)from;

                if (bc.Controlled || bc.BardTarget == this)
                    damage = 0; // Immune to pets and provoked creatures
            }
        }

        public override void AlterDamageScalarFrom(Mobile caster, ref double scalar)
        {
            if (caster is BaseCreature && ((BaseCreature)caster).GetMaster() is PlayerMobile)
            {
                scalar = 0.0; // Immune to magic
            }
        }

        public override void AlterSpellDamageFrom(Mobile from, ref int damage)
        {
            if (from is BaseCreature && ((BaseCreature)from).GetMaster() is PlayerMobile)
            {
                damage = 0;
            }
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
