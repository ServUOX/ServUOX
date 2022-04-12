using System;
using Server.Engines.CannedEvil;
using Server.Items;
using System.Collections.Generic;

namespace Server.Mobiles
{
    public class Mephitis : BaseChampion
    {
        [Constructible]
        public Mephitis()
            : base(AIType.AI_Melee)
        {
            Body = 173;
            Name = "Mephitis";

            BaseSoundID = 0x183;

            SetStr(505, 1000);
            SetDex(102, 300);
            SetInt(402, 600);

            SetHits(12000);
            SetStam(105, 600);

            SetDamage(21, 33);

            SetDamageType(ResistType.Phys, 50);
            SetDamageType(ResistType.Pois, 50);

            SetResist(ResistType.Phys, 75, 80);
            SetResist(ResistType.Fire, 60, 70);
            SetResist(ResistType.Cold, 60, 70);
            SetResist(ResistType.Pois, 100);
            SetResist(ResistType.Engy, 60, 70);

            SetSkill(SkillName.MagicResist, 70.7, 140.0);
            SetSkill(SkillName.Tactics, 97.6, 100.0);
            SetSkill(SkillName.Wrestling, 97.6, 100.0);

            Fame = 22500;
            Karma = -22500;

            VirtualArmor = 80;

            SetSpecialAbility(SpecialAbility.Webbing);

            ForceActiveSpeed = 0.3;
            ForcePassiveSpeed = 0.6;
        }

        public Mephitis(Serial serial)
            : base(serial)
        {
        }

        public override ChampionSkullType SkullType => ChampionSkullType.Venom;
        public override Type[] UniqueList => new Type[] { typeof(Calm) };
        public override Type[] SharedList => new Type[] { typeof(OblivionsNeedle), typeof(ANecromancerShroud) };
        public override Type[] DecorativeList => new Type[] { typeof(Web), typeof(MonsterStatuette) };
        public override MonsterStatuetteType[] StatueTypes => new MonsterStatuetteType[] { MonsterStatuetteType.Spider };
        public override Poison PoisonImmunity => Poison.Lethal;
        public override Poison HitPoison => Poison.Lethal;
        public override void GenerateLoot()
        {
            AddLoot(LootPack.UltraRich, 4);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}
