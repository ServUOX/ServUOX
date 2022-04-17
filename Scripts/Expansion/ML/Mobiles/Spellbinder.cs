using System;

namespace Server.Mobiles
{
    [CorpseName("a ghostly corpse")]
    public class Spellbinder : Spectre
    {
        [Constructible]
        public Spellbinder()
            : base()
        {
            Name = "a spectral spellbinder";
            AI = AIType.AI_Spellbinder;

            SetStr(46, 70);
            SetDex(47, 65);
            SetInt(187, 210);

            SetHits(36, 50);

            SetDamage(ResistType.Phys, 100, 0, 3, 6);

            SetResist(ResistType.Phys, 20, 30);
            SetResist(ResistType.Cold, 15, 25);
            SetResist(ResistType.Pois, 10, 20);

            SetSkill(SkillName.MagicResist, 35.1, 45.0);
            SetSkill(SkillName.Tactics, 35.1, 50.0);
            SetSkill(SkillName.Wrestling, 35.1, 50.0);

            Fame = 2500;
            Karma = -2500;

            VirtualArmor = 28;
        }

        public Spellbinder(Serial serial)
            : base(serial)
        {
        }

        public override bool BleedImmunity => true;
        public override Poison PoisonImmunity => Poison.Regular;
        public override OppositionGroup OppositionGroup => OppositionGroup.FeyAndUndead;
        public override void GenerateLoot()
        {
            AddLoot(LootPack.Meager);
            PackItem(Loot.RandomWeapon());
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
