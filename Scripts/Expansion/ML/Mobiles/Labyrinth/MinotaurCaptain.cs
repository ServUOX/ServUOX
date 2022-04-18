using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a minotaur corpse")]
    public class MinotaurCaptain : BaseCreature
    {
        [Constructible]
        public MinotaurCaptain()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)// NEED TO CHECK
        {
            Name = "a minotaur captain";
            Body = 280;

            SetStr(401, 425);
            SetDex(91, 110);
            SetInt(31, 50);

            SetHits(401, 440);

            SetDamage(ResistType.Phys, 100, 0, 11, 20);

            SetResist(ResistType.Phys, 65, 75);
            SetResist(ResistType.Fire, 35, 45);
            SetResist(ResistType.Cold, 40, 50);
            SetResist(ResistType.Pois, 40, 50);
            SetResist(ResistType.Engy, 40, 50);

            SetSkill(SkillName.Meditation, 0);
            SetSkill(SkillName.EvalInt, 0);
            SetSkill(SkillName.Magery, 0);
            SetSkill(SkillName.Poisoning, 0);
            SetSkill(SkillName.Anatomy, 0, 6.3);
            SetSkill(SkillName.MagicResist, 66.1, 73.6);
            SetSkill(SkillName.Tactics, 93.0, 109.9);
            SetSkill(SkillName.Wrestling, 92.6, 107.2);

            Fame = 7000;
            Karma = -7000;

            VirtualArmor = 28; // Don't know what it should be

            for (int i = 0; i < Utility.RandomMinMax(0, 1); i++)
            {
                PackItem(Loot.RandomScroll(0, Loot.ArcanistScrollTypes.Length, SpellbookType.Arcanist));
            }

            SetWeaponAbility(WeaponAbility.ParalyzingBlow);
        }

        public MinotaurCaptain(Serial serial)
            : base(serial)
        {
        }

        public override int TreasureMapLevel => 3;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);
        }

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
