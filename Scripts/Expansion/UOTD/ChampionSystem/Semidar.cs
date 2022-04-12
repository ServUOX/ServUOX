using System;
using System.Collections;
using Server.Engines.CannedEvil;
using Server.Items;

namespace Server.Mobiles
{
    public class Semidar : BaseChampion
    {
        [Constructible]
        public Semidar()
            : base(AIType.AI_Mage)
        {
            Name = "Semidar";
            Body = 174;
            BaseSoundID = 0x4B0;

            SetStr(502, 600);
            SetDex(102, 200);
            SetInt(601, 750);

            SetHits(10000);
            SetStam(103, 250);

            SetDamage(29, 35);

            SetDamageType(ResistType.Phys, 75);
            SetDamageType(ResistType.Fire, 25);

            SetResist(ResistType.Phys, 75, 90);
            SetResist(ResistType.Fire, 65, 75);
            SetResist(ResistType.Cold, 60, 70);
            SetResist(ResistType.Pois, 65, 75);
            SetResist(ResistType.Engy, 65, 75);

            SetSkill(SkillName.EvalInt, 95.1, 100.0);
            SetSkill(SkillName.Magery, 90.1, 105.0);
            SetSkill(SkillName.Meditation, 95.1, 100.0);
            SetSkill(SkillName.MagicResist, 120.2, 140.0);
            SetSkill(SkillName.Tactics, 90.1, 105.0);
            SetSkill(SkillName.Wrestling, 90.1, 105.0);

            Fame = 24000;
            Karma = -24000;

            VirtualArmor = 20;
            SetSpecialAbility(SpecialAbility.LifeDrain);

            ForceActiveSpeed = 0.3;
            ForcePassiveSpeed = 0.6;
        }

        public Semidar(Serial serial)
            : base(serial)
        {
        }

        public override ChampionSkullType SkullType => ChampionSkullType.Pain;
        public override Type[] UniqueList => new Type[] { typeof(GladiatorsCollar) };
        public override Type[] SharedList => new Type[] { typeof(RoyalGuardSurvivalKnife), typeof(TheMostKnowledgePerson), typeof(LieutenantOfTheBritannianRoyalGuard) };
        public override Type[] DecorativeList => new Type[] { typeof(LavaTile), typeof(DemonSkull) };
        public override MonsterStatuetteType[] StatueTypes => new MonsterStatuetteType[] { };
        public override Poison PoisonImmunity => Poison.Lethal;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.UltraRich, 4);
            AddLoot(LootPack.FilthyRich);
        }

        public override void CheckReflect(Mobile caster, ref bool reflect)
        {
            if (!caster.Female && !caster.IsBodyMod)
                reflect = true; // Always reflect if caster isn't female
        }

        /*public override void AlterDamageScalarFrom(Mobile caster, ref double scalar)
        {
            if (caster.Body.IsMale)
                scalar = 20; // Male bodies always reflect.. damage scaled 20x
        }*/

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}
