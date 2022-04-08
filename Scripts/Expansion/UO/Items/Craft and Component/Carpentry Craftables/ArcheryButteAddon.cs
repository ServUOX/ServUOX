using Server.Gumps;
using Server.Network;
using System;
using System.Collections;

namespace Server.Items
{
    [Flipable(0x100A, 0x100B)]
    public class ArcheryButte : AddonComponent
    {
        private static readonly TimeSpan UseDelay = TimeSpan.FromSeconds(2.0);
        private Hashtable m_Entries;

        public ArcheryButte() : this(0x100A)
        {
        }

        public ArcheryButte(int itemID) : base(itemID)
        {
            MinSkill = -25.0;
            MaxSkill = +25.0;
        }

        public ArcheryButte(Serial serial)
            : base(serial)
        {
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public double MinSkill { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public double MaxSkill { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public DateTime LastUse { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool FacingEast
        {
            get => ItemID == 0x100A;
            set => ItemID = value ? 0x100A : 0x100B;
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Arrows { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Bolts { get; set; }

        public override void OnDoubleClick(Mobile from)
        {
            if (Core.SA && (from.Weapon is Boomerang || from.Weapon is Cyclone || from.Weapon is BaseThrown) && from.InRange(GetWorldLocation(), 1))
            {
                Fire(from);
            }

            if ((Arrows > 0 || Bolts > 0) && from.InRange(GetWorldLocation(), 1))
            {
                Gather(from);
            }
            else
            {
                Fire(from);
            }
        }

        public void Gather(Mobile from)
        {
            from.LocalOverheadMessage(MessageType.Regular, 0x3B2, 500592); // You gather the arrows and bolts.

            if (Arrows > 0)
            {
                from.AddToBackpack(new Arrow(Arrows));
            }

            if (Bolts > 0)
            {
                from.AddToBackpack(new Bolt(Bolts));
            }

            Arrows = 0;
            Bolts = 0;

            m_Entries = null;
        }

        public void Fire(Mobile from)
        {
            BaseRanged ranged = from.Weapon as BaseRanged;

            if (ranged == null)
            {
                SendLocalizedMessageTo(from, 500593); // You must practice with ranged weapons on 
                return;
            }

            if (DateTime.UtcNow < (LastUse + UseDelay))
            {
                return;
            }

            Point3D worldLoc = GetWorldLocation();

            if (FacingEast ? from.X <= worldLoc.X : from.Y <= worldLoc.Y)
            {
                from.LocalOverheadMessage(MessageType.Regular, 0x3B2, 500596); // You would do better to stand in front of the archery butte.
                return;
            }

            if (FacingEast ? from.Y != worldLoc.Y : from.X != worldLoc.X)
            {
                from.LocalOverheadMessage(MessageType.Regular, 0x3B2, 500597); // You aren't properly lined up with the archery butte to get an accurate shot.
                return;
            }

            if (!from.InRange(worldLoc, 6))
            {
                from.LocalOverheadMessage(MessageType.Regular, 0x3B2, 500598); // You are too far away from the archery butte to get an accurate shot.
                return;
            }

            if (from.InRange(worldLoc, 4))
            {
                from.LocalOverheadMessage(MessageType.Regular, 0x3B2, 500599); // You are too close to the target.
                return;
            }

            Type ammoType = ranged.AmmoType;

            bool isArrow = ammoType == typeof(Arrow);
            bool isBolt = ammoType == typeof(Bolt);


            if (ammoType == null && ranged as BaseThrown == null)
            {
                isArrow = ranged.Animation == WeaponAnimation.ShootBow;
                isBolt = ranged.Animation == WeaponAnimation.ShootXBow;
            }

            bool isKnown = isArrow || isBolt;

            if (ranged as BaseThrown == null)
            {
                Container pack = from.Backpack;

                if (pack == null || ammoType == null || !pack.ConsumeTotal(ammoType, 1))
                {
                    if (isArrow)
                    {
                        from.LocalOverheadMessage(MessageType.Regular, 0x3B2, 500594); // You do not have any arrows with which to practice.
                    }
                    else if (isBolt)
                    {
                        from.LocalOverheadMessage(MessageType.Regular, 0x3B2, 500595); // You do not have any crossbow bolts with which to practice.
                    }
                    else
                    {
                        SendLocalizedMessageTo(from, 500593); // You must practice with ranged weapons on 
                    }

                    return;
                }
            }

            LastUse = DateTime.UtcNow;

            from.MovingEffect(this, ranged.EffectID, 18, 1, false, false);
            from.Direction = from.GetDirectionTo(GetWorldLocation());
            ranged.PlaySwingAnimation(from);

            ScoreEntry se = GetEntryFor(from);

            if (!from.CheckSkill(ranged.Skill, MinSkill, MaxSkill))
            {
                from.PlaySound(ranged.MissSound);

                PublicOverheadMessage(MessageType.Regular, 0x3B2, 500604, from.Name); // You miss the target altogether.

                se.Record(0);

                if (se.Count == 1)
                {
                    PublicOverheadMessage(MessageType.Regular, 0x3B2, 1062719, se.Total.ToString());
                }
                else
                {
                    PublicOverheadMessage(MessageType.Regular, 0x3B2, 1042683, string.Format("{0}\t{1}", se.Total, se.Count));
                }

                return;
            }

            Effects.PlaySound(Location, Map, 0x2B1);

            double rand = Utility.RandomDouble();

            int area, score, splitScore;

            if (0.10 > rand)
            {
                area = 0; // bullseye
                score = 50;
                splitScore = 100;
            }
            else if (0.25 > rand)
            {
                area = 1; // inner ring
                score = 10;
                splitScore = 20;
            }
            else if (0.50 > rand)
            {
                area = 2; // middle ring
                score = 5;
                splitScore = 15;
            }
            else
            {
                area = 3; // outer ring
                score = 2;
                splitScore = 5;
            }

            bool split = isKnown && ((Arrows + Bolts) * 0.02) > Utility.RandomDouble();

            if (split)
            {
                PublicOverheadMessage(MessageType.Regular, 0x3B2, 1010027 + area, $"{from.Name}\t{(isArrow ? "arrow" : "bolt")}");
            }
            else
            {
                PublicOverheadMessage(MessageType.Regular, 0x3B2, 1010035 + area, from.Name);

                if (ammoType != null)
                {
                    if (isArrow)
                    {
                        ++Arrows;
                    }
                    else if (isBolt)
                    {
                        ++Bolts;
                    }
                }
            }

            se.Record(split ? splitScore : score);

            if (se.Count == 1)
            {
                PublicOverheadMessage(MessageType.Regular, 0x3B2, 1062719, se.Total.ToString());
            }
            else
            {
                PublicOverheadMessage(MessageType.Regular, 0x3B2, 1042683, $"{se.Total}\t{se.Count}");
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
            writer.Write(MinSkill);
            writer.Write(MaxSkill);
            writer.Write(Arrows);
            writer.Write(Bolts);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

            switch (version)
            {
                case 0:
                    {
                        MinSkill = reader.ReadDouble();
                        MaxSkill = reader.ReadDouble();
                        Arrows = reader.ReadInt();
                        Bolts = reader.ReadInt();

                        if (MinSkill == 0.0 && MaxSkill == 30.0)
                        {
                            MinSkill = -25.0;
                            MaxSkill = +25.0;
                        }
                        break;
                    }
            }
        }

        private ScoreEntry GetEntryFor(Mobile from)
        {
            if (m_Entries == null)
            {
                m_Entries = new Hashtable();
            }

            ScoreEntry e = (ScoreEntry)m_Entries[from];

            if (e == null)
            {
                m_Entries[from] = e = new ScoreEntry();
            }

            return e;
        }

        private class ScoreEntry
        {
            public int Total { get; set; }

            public int Count { get; set; }

            public void Record(int score)
            {
                Total += score;
                Count += 1;
            }
        }
    }

    public class ArcheryButteAddon : BaseAddon
    {
        [Constructible]
        public ArcheryButteAddon(AddonFacing facing)
        {
            switch (facing)
            {
                case AddonFacing.East: AddComponent(new ArcheryButte(0x100A), 0, 0, 0); break;
                case AddonFacing.South: AddComponent(new ArcheryButte(0x100B), 0, 0, 0); break;
            }
        }

        public ArcheryButteAddon(Serial serial) : base(serial)
        {
        }

        public override BaseAddonDeed Deed => new ArcheryButteDeed();

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            reader.ReadInt();
        }
    }

    public class ArcheryButteDeed : BaseAddonDeed, IRewardOption
    {
        private AddonFacing Facing { get; set; }

        [Constructible]
        public ArcheryButteDeed()
        {
        }

        public ArcheryButteDeed(Serial serial) : base(serial)
        {
        }

        public override BaseAddon Addon => new ArcheryButteAddon(Facing);
        public override int LabelNumber => 1080205;// archery butte

        public override void OnDoubleClick(Mobile from)
        {
            if (IsChildOf(from.Backpack))
            {
                from.CloseGump(typeof(RewardOptionGump));
                from.SendGump(new RewardOptionGump(this));
            }
            else
            {
                from.SendLocalizedMessage(1062334); // This item must be in your backpack to be used.       	
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            reader.ReadInt();
        }

        public void GetOptions(RewardOptionList list)
        {
            list.Add((int)AddonFacing.South, 1080204);
            list.Add((int)AddonFacing.East, 1080203);
        }

        public void OnOptionSelected(Mobile from, int choice)
        {
            Facing = (AddonFacing)choice;

            if (!Deleted)
            {
                base.OnDoubleClick(from);
            }
        }
    }
}
