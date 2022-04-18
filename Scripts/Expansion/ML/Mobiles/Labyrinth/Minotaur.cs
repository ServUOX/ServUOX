using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a minotaur corpse")]
    public class Minotaur : BaseCreature
    {
        [Constructible]
        public Minotaur()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)// NEED TO CHECK
        {
            Name = "a minotaur";
            Body = 263;

            SetStr(301, 340);
            SetDex(91, 110);
            SetInt(31, 50);

            SetHits(301, 340);

            SetDamage(ResistType.Phys, 100, 0, 11, 20);

            SetResist(ResistType.Phys, 55, 65);
            SetResist(ResistType.Fire, 25, 35);
            SetResist(ResistType.Cold, 30, 40);
            SetResist(ResistType.Pois, 30, 40);
            SetResist(ResistType.Engy, 30, 40);

            SetSkill(SkillName.Meditation, 0);
            SetSkill(SkillName.EvalInt, 0);
            SetSkill(SkillName.Magery, 0);
            SetSkill(SkillName.Poisoning, 0);
            SetSkill(SkillName.Anatomy, 0);
            SetSkill(SkillName.MagicResist, 56.1, 64.0);
            SetSkill(SkillName.Tactics, 93.3, 97.8);
            SetSkill(SkillName.Wrestling, 90.4, 92.1);

            Fame = 5000;
            Karma = -5000;

            VirtualArmor = 28;

            for (int i = 0; i < Utility.RandomMinMax(0, 1); i++)
            {
                PackItem(Loot.RandomScroll(0, Loot.ArcanistScrollTypes.Length, SpellbookType.Arcanist));
            }

            SetWeaponAbility(WeaponAbility.ParalyzingBlow);
        }

        public Minotaur(Serial serial)
            : base(serial)
        {
        }

        public override int TreasureMapLevel => 3;

        public override void GenerateLoot() => AddLoot(LootPack.Rich);

        public override int GetAngerSound() => 0x59A;
        public override int GetIdleSound() => 0x599;
        public override int GetAttackSound() => 0x598;
        public override int GetHurtSound() => 0x59b;
        public override int GetDeathSound() => 0x597;

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
