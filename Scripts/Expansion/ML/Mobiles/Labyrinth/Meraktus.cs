using System;
using System.Collections;
using Server.Engines.CannedEvil;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("the remains of Meraktus")]
    public class Meraktus : BaseChampion
    {
        public override ChampionSkullType SkullType => ChampionSkullType.Pain;

        public override Type[] UniqueList => new Type[] { typeof(Subdue) };
        public override Type[] SharedList => new Type[]
                {
                    typeof(RoyalGuardSurvivalKnife),
                    typeof(TheMostKnowledgePerson),
                    typeof(OblivionsNeedle)
                };
        public override Type[] DecorativeList => new Type[]
                {
                    typeof(ArtifactLargeVase),
                    typeof(ArtifactVase),
                    typeof(MinotaurStatueDeed)
                };

        public override MonsterStatuetteType[] StatueTypes => new MonsterStatuetteType[]
                {
                    MonsterStatuetteType.Minotaur
                };

        [Constructible]
        public Meraktus()
            : base(AIType.AI_Melee)
        {
            Name = "Meraktus";
            Title = "the Tormented";
            Body = 263;
            BaseSoundID = 680;
            Hue = 0x835;

            SetStr(1419, 1438);
            SetDex(309, 413);
            SetInt(129, 131);

            SetHits(4100, 4200);

            SetDamage(16, 30);

            SetDamageType(ResistType.Phys, 100);

            SetResist(ResistType.Phys, 65, 90);
            SetResist(ResistType.Fire, 65, 70);
            SetResist(ResistType.Cold, 50, 60);
            SetResist(ResistType.Pois, 40, 60);
            SetResist(ResistType.Engy, 50, 55);

            SetSkill(SkillName.Anatomy, 0);
            SetSkill(SkillName.MagicResist, 107.0, 111.3);
            SetSkill(SkillName.Tactics, 107.0, 117.0);
            SetSkill(SkillName.Wrestling, 100.0, 105.0);

            Fame = 70000;
            Karma = -70000;

            VirtualArmor = 28; // Don't know what it should be

            NoKillAwards = true;

            Timer.DelayCall(TimeSpan.FromSeconds(1), new TimerCallback(SpawnTormented));

            SetWeaponAbility(WeaponAbility.Dismount);
        }

        public override int GetAngerSound() { return 0x597; }
        public override int GetIdleSound() { return 0x596; }
        public override int GetAttackSound() { return 0x599; }
        public override int GetHurtSound() { return 0x59a; }
        public override int GetDeathSound() { return 0x59c; }

        public override int Meat => 2;
        public override int Hides => 10;
        public override HideType HideType => HideType.Regular;
        public override Poison PoisonImmunity => Poison.Regular;
        public override int TreasureMapLevel => 3;
        public override bool BardImmunity => true;
        public override bool Unprovokable => true;
        public override bool Uncalmable => true;

        public virtual void PackResources(Container CorpseLoot, int amount)
        {
            for (int i = 0; i < amount; i++)
                switch (Utility.Random(6))
                {
                    case 0:
                        CorpseLoot.DropItem(new Blight());
                        break;
                    case 1:
                        CorpseLoot.DropItem(new Scourge());
                        break;
                    case 2:
                        CorpseLoot.DropItem(new Taint());
                        break;
                    case 3:
                        CorpseLoot.DropItem(new Putrefaction());
                        break;
                    case 4:
                        CorpseLoot.DropItem(new Corruption());
                        break;
                    case 5:
                        CorpseLoot.DropItem(new Muculent());
                        break;
                }
        }

        public override void OnDeath(Container CorpseLoot)
        {
            for (int i = 0; i < Utility.RandomMinMax(0, 1); i++)
            {
                PackItem(Loot.RandomScroll(0, Loot.ArcanistScrollTypes.Length, SpellbookType.Arcanist));
            }

            if (Core.ML)
            {
                PackResources(CorpseLoot, 8);

                for (int i = 0; i < Utility.Random(3); i++)
                    PackItem(new RandomTalisman());

                CorpseLoot.DropItem(new MalletAndChisel());

                switch (Utility.Random(3))
                {
                    case 0:
                        CorpseLoot.DropItem(new MinotaurHedge());
                        break;
                    case 1:
                        CorpseLoot.DropItem(new BonePile());
                        break;
                    case 2:
                        CorpseLoot.DropItem(new LightYarn());
                        break;
                }

                if (Utility.RandomBool())
                    CorpseLoot.DropItem(new TormentedChains());

                if (Utility.RandomDouble() < 0.025)
                    CorpseLoot.DropItem(new CrimsonCincture());
            }

            base.OnDeath(CorpseLoot);
        }

        public override void GenerateLoot()
        {
            if (Core.ML)
            {
                AddLoot(LootPack.AosSuperBoss, 5);  // Need to verify
            }
            else
            {
                AddLoot(LootPack.FilthyRich, 5);
            }
        }

        public override void OnGaveMeleeAttack(Mobile defender)
        {
            base.OnGaveMeleeAttack(defender);
            if (0.2 >= Utility.RandomDouble())
                Earthquake();
        }

        public void Earthquake()
        {
            Map map = Map;
            if (map == null)
                return;
            ArrayList targets = new ArrayList();
            IPooledEnumerable eable = GetMobilesInRange(8);
            foreach (Mobile m in eable)
            {
                if (m == this || !CanBeHarmful(m))
                    continue;
                if (m is BaseCreature && (((BaseCreature)m).Controlled || ((BaseCreature)m).Summoned || ((BaseCreature)m).Team != Team))
                    targets.Add(m);
                else if (m.Player)
                    targets.Add(m);
            }
            eable.Free();

            PlaySound(0x2F3);
            for (int i = 0; i < targets.Count; ++i)
            {
                Mobile m = (Mobile)targets[i];
                if (m == null || m.Deleted) continue;

                if (m is PlayerMobile pm && pm.Mounted)
                {
                    pm.SetMountBlock(BlockMountType.DismountRecovery, TimeSpan.FromSeconds(10), true);
                }

                double damage = m.Hits * 0.6;//was .6
                if (damage < 10.0)
                    damage = 10.0;
                else if (damage > 75.0)
                    damage = 75.0;
                DoHarmful(m);
                AOS.Damage(m, this, (int)damage, 100, 0, 0, 0, 0);
                if (m.Alive && m.Body.IsHuman && !m.Mounted)
                    m.Animate(20, 7, 1, true, false, 0); // take hit
            }
        }

        public Meraktus(Serial serial)
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

        #region SpawnHelpers
        public void SpawnTormented()
        {
            BaseCreature spawna = new TormentedMinotaur();
            spawna.MoveToWorld(Location, Map);

            BaseCreature spawnb = new TormentedMinotaur();
            spawnb.MoveToWorld(Location, Map);

            BaseCreature spawnc = new TormentedMinotaur();
            spawnc.MoveToWorld(Location, Map);

            BaseCreature spawnd = new TormentedMinotaur();
            spawnd.MoveToWorld(Location, Map);
        }
        #endregion
    }
}
