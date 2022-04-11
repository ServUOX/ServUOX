using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("an abscess's corpse")]
    public class Abscess : BaseCreature
    {
        [Constructible]
        public Abscess()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "Abscess";
            Body = 0x109;
            Hue = 0x8FD;
            BaseSoundID = 0x16A;

            SetStr(845, 871);
            SetDex(121, 134);
            SetInt(128, 142);

            SetHits(7470, 7540);

            SetDamage(26, 31);

            SetDamageType(ResistType.Physical, 60);
            SetDamageType(ResistType.Fire, 10);
            SetDamageType(ResistType.Cold, 10);
            SetDamageType(ResistType.Poison, 10);
            SetDamageType(ResistType.Energy, 10);

            SetResist(ResistType.Physical, 65, 75);
            SetResist(ResistType.Fire, 70, 80);
            SetResist(ResistType.Cold, 25, 35);
            SetResist(ResistType.Poison, 35, 45);
            SetResist(ResistType.Energy, 35, 45);

            SetSkill(SkillName.Wrestling, 132.3, 143.8);
            SetSkill(SkillName.Tactics, 121.0, 130.5);
            SetSkill(SkillName.MagicResist, 102.9, 119.0);
            SetSkill(SkillName.Anatomy, 91.8, 94.3);

            for (int i = 0; i < Utility.RandomMinMax(1, 2); i++)
            {
                PackItem(Loot.RandomScroll(0, Loot.ArcanistScrollTypes.Length, SpellbookType.Arcanist));
            }

            SetSpecialAbility(SpecialAbility.DragonBreath);
        }

        public Abscess(Serial serial)
            : base(serial)
        {
        }

        public override int Hides => 40;
        public override int Meat => 19;
        public override bool GivesMLMinorArtifact => true;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.AosUltraRich, 4);
        }

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);

            c.DropItem(new AbscessTail());

            if (Paragon.ChestChance > Utility.RandomDouble())
                c.DropItem(new ParagonChest(Name, 5));
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
