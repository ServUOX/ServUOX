using System;

namespace Server.Mobiles
{
    public class Silvani : BaseCreature
    {
        [Constructible]
        public Silvani()
            : base(AIType.AI_Mage, FightMode.Evil, 18, 1, 0.1, 0.2)
        {
            Name = "Silvani";
            Body = 176;
            BaseSoundID = 0x467;

            SetStr(253, 400);
            SetDex(157, 850);
            SetInt(503, 800);

            SetHits(600);

            SetDamage(ResistType.Phys, 75, 0, 27, 38);
            SetDamage(ResistType.Cold, 25);

            SetResist(ResistType.Phys, 45, 55);
            SetResist(ResistType.Fire, 30, 40);
            SetResist(ResistType.Cold, 30, 40);
            SetResist(ResistType.Pois, 40, 50);
            SetResist(ResistType.Engy, 40, 50);

            SetSkill(SkillName.EvalInt, 100.0);
            SetSkill(SkillName.Magery, 97.6, 107.5);
            SetSkill(SkillName.Meditation, 100.0);
            SetSkill(SkillName.MagicResist, 100.5, 150.0);
            SetSkill(SkillName.Tactics, 97.6, 100.0);
            SetSkill(SkillName.Wrestling, 97.6, 100.0);

            Fame = 20000;
            Karma = 20000;

            VirtualArmor = 50;
        }

        public Silvani(Serial serial)
            : base(serial)
        {
        }

        public override TribeType Tribe => TribeType.Fey;

        public override OppositionGroup OppositionGroup => OppositionGroup.FeyAndUndead;
        public override bool CanFly => true;
        public override bool Unprovokable => true;
        public override Poison PoisonImmunity => Poison.Regular;
        public override int TreasureMapLevel => 5;
        public override void GenerateLoot()
        {
            AddLoot(LootPack.UltraRich, 2);
        }

        public void SpawnPixies(Mobile target)
        {
            Map map = Map;

            if (map == null)
                return;

            int newPixies = Utility.RandomMinMax(3, 6);

            for (int i = 0; i < newPixies; ++i)
            {
                Pixie pixie = new Pixie
                {
                    Team = Team,
                    FightMode = FightMode.Closest
                };

                bool validLocation = false;
                Point3D loc = Location;

                for (int j = 0; !validLocation && j < 10; ++j)
                {
                    int x = X + Utility.Random(3) - 1;
                    int y = Y + Utility.Random(3) - 1;
                    int z = map.GetAverageZ(x, y);

                    if (validLocation = map.CanFit(x, y, Z, 16, false, false))
                        loc = new Point3D(x, y, Z);
                    else if (validLocation = map.CanFit(x, y, z, 16, false, false))
                        loc = new Point3D(x, y, z);
                }

                pixie.MoveToWorld(loc, map);
                pixie.Combatant = target;
            }
        }

        public override void AlterDamageScalarFrom(Mobile caster, ref double scalar)
        {
            if (0.1 >= Utility.RandomDouble())
                SpawnPixies(caster);
        }

        public override void OnGaveMeleeAttack(Mobile defender)
        {
            base.OnGaveMeleeAttack(defender);

            defender.Damage(Utility.Random(20, 10), this);
            defender.Stam -= Utility.Random(20, 10);
            defender.Mana -= Utility.Random(20, 10);
        }

        public override void OnGotMeleeAttack(Mobile attacker)
        {
            base.OnGotMeleeAttack(attacker);

            if (0.1 >= Utility.RandomDouble())
                SpawnPixies(attacker);
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