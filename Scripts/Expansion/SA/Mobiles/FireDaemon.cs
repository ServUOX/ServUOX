using System;

namespace Server.Mobiles
{
    [CorpseName("a fire daemon corpse")]
    public class FireDaemon : BaseCreature, IAuraCreature
    {
        [Constructible]
        public FireDaemon()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a fire daemon";
            Body = 9;
            BaseSoundID = 0x47D;
            Hue = 1636;

            SetStr(504, 539);
            SetDex(126, 145);
            SetInt(329, 364);

            SetHits(1026, 1174);

            SetDamage(7, 14);

            SetDamageType(ResistType.Phys, 20);
            SetDamageType(ResistType.Fire, 80);

            SetResist(ResistType.Phys, 45, 60);
            SetResist(ResistType.Fire, 100);
            SetResist(ResistType.Cold, -10, 0);
            SetResist(ResistType.Pois, 20, 30);
            SetResist(ResistType.Engy, 30, 40);

            SetSkill(SkillName.Anatomy, 75.5, 84.9);
            SetSkill(SkillName.MagicResist, 95.7, 109.8);
            SetSkill(SkillName.Tactics, 81.0, 98.6);
            SetSkill(SkillName.Wrestling, 40.2, 78.7);
            SetSkill(SkillName.EvalInt, 91.1, 104.5);
            SetSkill(SkillName.Magery, 91.3, 105.0);
            SetSkill(SkillName.Meditation, 90.1, 103.7);
            SetSkill(SkillName.DetectHidden, 66.0);

            Fame = 15000;
            Karma = -15000;

            VirtualArmor = 58;

            SetSpecialAbility(SpecialAbility.DragonBreath);
            SetAreaEffect(AreaEffect.AuraDamage);
        }

        public FireDaemon(Serial serial)
            : base(serial)
        {
        }

        public override bool CanRummageCorpses => true;
        public override Poison PoisonImmunity => Poison.Regular;
        public override int TreasureMapLevel => 4;
        public override int Meat => 1;

        public void AuraEffect(Mobile m)
        {
            m.SendLocalizedMessage(1008112); // The intense heat is damaging you!
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich);
            AddLoot(LootPack.Rich);
        }

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
