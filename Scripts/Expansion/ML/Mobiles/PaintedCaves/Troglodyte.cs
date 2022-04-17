using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a troglodyte corpse")]
    public class Troglodyte : BaseCreature
    {
        public override double HealChance => 1.0;

        [Constructible]
        public Troglodyte()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)// NEED TO CHECK
        {
            Name = "a troglodyte";
            Body = 267;
            BaseSoundID = 0x59F;

            SetStr(148, 217);
            SetDex(91, 120);
            SetInt(51, 70);

            SetHits(302, 340);

            SetDamage(11, 14);

            SetDamageType(ResistType.Phys, 100);

            SetResist(ResistType.Phys, 30, 35);
            SetResist(ResistType.Fire, 20, 30);
            SetResist(ResistType.Cold, 35, 40);
            SetResist(ResistType.Pois, 30, 40);
            SetResist(ResistType.Engy, 30, 40);

            SetSkill(SkillName.Anatomy, 70.5, 94.8);
            SetSkill(SkillName.MagicResist, 51.8, 65.0);
            SetSkill(SkillName.Tactics, 80.4, 94.7);
            SetSkill(SkillName.Wrestling, 70.2, 93.5);
            SetSkill(SkillName.Healing, 70.0, 95.0);

            Fame = 5000;
            Karma = -5000;

            VirtualArmor = 28;
        }

        public Troglodyte(Serial serial)
            : base(serial)
        {
        }

        public override int TreasureMapLevel => 2;

        public override void OnDeath(Container CorpseLoot)
        {
            if (Utility.RandomDouble() < 0.1)
                CorpseLoot.DropItem(new PrimitiveFetish());

            CorpseLoot.DropItem(new Bandage(5));
            CorpseLoot.DropItem(new Ribs());

            for (int i = 0; i < Utility.RandomMinMax(0, 1); i++)
            {
                CorpseLoot.DropItem(Loot.RandomScroll(0, Loot.ArcanistScrollTypes.Length, SpellbookType.Arcanist));
            }

            base.OnDeath(CorpseLoot);
        }

        public override void GenerateLoot() => AddLoot(LootPack.Rich);

        public override int GetAngerSound() => 0x5A0;

        public override int GetIdleSound() => 0x59F;

        public override int GetAttackSound() => 0x59E;

        public override int GetHurtSound() => 0x5A1;

        public override int GetDeathSound() => 0x59D;

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
