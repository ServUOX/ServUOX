using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("an interred grizzle corpse")]
    public class InterredGrizzle : BaseCreature
    {
        [Constructible]
        public InterredGrizzle()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "an interred grizzle";
            Body = 259;

            SetStr(451, 500);
            SetDex(201, 250);
            SetInt(801, 850);

            SetHits(1500);
            SetStam(150);

            SetDamage(16, 19);

            SetDamageType(ResistType.Phys, 30);
            SetDamageType(ResistType.Fire, 70);

            SetResist(ResistType.Phys, 35, 55);
            SetResist(ResistType.Fire, 20, 65);
            SetResist(ResistType.Cold, 55, 80);
            SetResist(ResistType.Pois, 20, 35);
            SetResist(ResistType.Engy, 60, 80);

            SetSkill(SkillName.Meditation, 77.7, 84.0);
            SetSkill(SkillName.EvalInt, 72.2, 79.6);
            SetSkill(SkillName.Magery, 83.7, 89.6);
            SetSkill(SkillName.Poisoning, 0);
            SetSkill(SkillName.Anatomy, 0);
            SetSkill(SkillName.MagicResist, 80.2, 87.3);
            SetSkill(SkillName.Tactics, 104.5, 105.1);
            SetSkill(SkillName.Wrestling, 105.1, 109.4);

            Fame = 3700;  // Guessed
            Karma = -3700;  // Guessed
        }

        public InterredGrizzle(Serial serial)
        : base(serial)
        {
        }

        public override bool CanBeParagon => false;
        public override int TreasureMapLevel => 4;

        public override int GetAngerSound() { return 0x581; }
        public override int GetIdleSound() { return 0x582; }
        public override int GetAttackSound() { return 0x580; }
        public override int GetHurtSound() { return 0x583; }
        public override int GetDeathSound() { return 0x584; }

        /*
        public override bool OnBeforeDeath()
        {
        SpillAcid( 1, 4, 10, 6, 10 );

        return base.OnBeforeDeath();
        }
        */

        public override void OnDeath(Container CorpseLoot)
        {
            for (int i = 0; i < Utility.RandomMinMax(0, 1); i++)
            {
                CorpseLoot.DropItem(Loot.RandomScroll(0, Loot.ArcanistScrollTypes.Length, SpellbookType.Arcanist));
            }

            base.OnDeath(CorpseLoot);
        }  

        public override void GenerateLoot() // -- Need to verify
        {
            AddLoot(LootPack.FilthyRich);
        }

        public override void OnDamage(int amount, Mobile from, bool willKill)
        {
            if (Utility.RandomDouble() < 0.04)
                SpillAcid(null, Utility.RandomMinMax(1, 3));

            base.OnDamage(amount, from, willKill);
        }

        public override Item NewHarmfulItem()
        {
            return new InfernalOoze(this, false, Utility.RandomMinMax(6, 10));
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
