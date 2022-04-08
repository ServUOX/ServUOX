using System;
using System.Collections.Generic;
using Server.Items;
using Server.Engines.Quests;
using Server.Regions;

namespace Server.Mobiles
{
    public class BexilPunchingBag : BaseCreature
    {
        public override bool InitialInnocent => true;

        private Dictionary<Mobile, int> _Table = new Dictionary<Mobile, int>();
        private DateTime _NextTeleport;

        public BexilPunchingBag()
            : base(AIType.AI_Animal, FightMode.None, 10, 1, 0.4, 0.8)
        {
            Name = "Bexil";
            Title = "the Dream Serpent";

            Body = 0xCE;
            Hue = 1976;
            BaseSoundID = 0x5A;

            SetHits(1000000);
        }

        private IDamageable _Combatant;

        public override IDamageable Combatant
        {
            get { return _Combatant; }
            set { _Combatant = value; }
        }

        public override void OnThink()
        {
            base.OnThink();

            if (Combatant is Mobile && _NextTeleport < DateTime.UtcNow)
            {
                var map = Map;
                var c = (Mobile)Combatant;

                Point3D p;

                do
                {
                    int x = X + Utility.RandomMinMax(-10, 10);
                    int y = Y + Utility.RandomMinMax(-10, 10);

                    p = new Point3D(x, y, map.GetAverageZ(x, y));
                }
                while (!map.CanSpawnMobile(p.X, p.Y, map.GetAverageZ(p.X, p.Y)) || !Region.Find(p, map).IsPartOf<BexilRegion>());

                Effects.SendLocationParticles(EffectItem.Create(Location, map, EffectItem.DefaultDuration), 0x3728, 10, 10, 2023);
                MoveToWorld(p, map);
                Effects.SendLocationParticles(EffectItem.Create(Location, map, EffectItem.DefaultDuration), 0x3728, 10, 10, 5023);

                _NextTeleport = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(1, 3));
            }
        }

        public override int Damage(int amount, Mobile from, bool informMount, bool checkDisrupt)
        {
            if (from is PlayerMobile)
            {
                var quest = QuestHelper.GetQuest<CatchMeIfYouCanQuest>((PlayerMobile)from);

                if (quest != null)
                {
                    quest.Objectives[0].Update(this);

                    if (quest.Completed)
                    {
                        DreamSerpentCharm.CompleteQuest(from);
                    }
                }
            }

            return 0;
        }

        public override void Delete()
        {
            var bex = new BexilPunchingBag();
            bex.MoveToWorld(new Point3D(403, 3391, 38), Map.TerMur);

            base.Delete();
        }

        public BexilPunchingBag(Serial serial) : base(serial)
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
            reader.ReadInt(); // version

            if (!Core.SA)
            {
                Delete();
            }
        }
    }
}
