using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a minotaur general corpse")]
    public class MinotaurGeneral : MinotaurCaptain
    {
        [Constructible]
        public MinotaurGeneral()
        {
            Name = "a minotaur general";
            Body = 0x118;

            SetStr(602, 606);
            SetDex(137, 139);
            SetInt(83, 96);

            SetHits(1006, 1041);

            SetDamage(ResistType.Phys, 40, 0, 16, 22);
            SetDamage(ResistType.Cold, 20);
            SetDamage(ResistType.Pois, 20);
            SetDamage(ResistType.Engy, 20);

            SetResist(ResistType.Phys, 77, 80);
            SetResist(ResistType.Fire, 59, 64);
            SetResist(ResistType.Cold, 63, 68);
            SetResist(ResistType.Pois, 61, 66);
            SetResist(ResistType.Engy, 63, 66);

            SetSkill(SkillName.Wrestling, 101.0, 129.7);
            SetSkill(SkillName.Tactics, 92.0, 104.8);
            SetSkill(SkillName.MagicResist, 86.0, 87.0);

            Fame = 18000;
            Karma = -18000;

            for (int i = 0; i < Utility.RandomMinMax(0, 1); i++)
            {
                PackItem(Loot.RandomScroll(0, Loot.ArcanistScrollTypes.Length, SpellbookType.Arcanist));
            }
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.UltraRich, 2);
        }

        public override int TreasureMapLevel => 4;

        public override int GetAngerSound() => 0x599;

        public override int GetIdleSound() => 0x598;

        public override int GetAttackSound() => 0x597;

        public override int GetHurtSound() => 0x59a;

        public override int GetDeathSound() => 0x596;

        public MinotaurGeneral(Serial serial)
            : base(serial)
        {
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
