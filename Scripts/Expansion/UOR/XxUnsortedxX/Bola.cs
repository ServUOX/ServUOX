using System;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;
using Server.Spells.Ninjitsu;

namespace Server.Items
{
    public class Bola : Item
    {
        [Constructible]
        public Bola()
            : this(1)
        {
        }

        [Constructible]
        public Bola(int amount)
            : base(0x26AC)
        {
            Weight = 4.0;
            Stackable = true;
            Amount = amount;
        }

        public Bola(Serial serial)
            : base(serial)
        {
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!IsChildOf(from.Backpack))
            {
                PrivateOverheadMessage(MessageType.Regular, 946, 1040019, from.NetState); // The bola must be in your pack to use it.
            }
            else if (!from.CanBeginAction(typeof(Bola)))
            {
                PrivateOverheadMessage(MessageType.Regular, 946, 1049624, from.NetState); // // You have to wait a few moments before you can use another bola!
            }
            else if (from.Target is BolaTarget)
            {
                PrivateOverheadMessage(MessageType.Regular, 946, 1049631, from.NetState); // This bola is already being used.
            }
            else if (from.Mounted)
            {
                PrivateOverheadMessage(MessageType.Regular, 946, 1042053, from.NetState); // You can't use this while on a mount!
            }
            else if (from.Flying)
            {
                PrivateOverheadMessage(MessageType.Regular, 946, 1113414, from.NetState); // You can't use this while flying!
            }
            else if (AnimalForm.UnderTransformation(from))
            {
                PrivateOverheadMessage(MessageType.Regular, 946, 1070902, from.NetState); // You can't use this while in an animal form!
            }
            else
            {
                EtherealMount.StopMounting(from);

                if (Core.AOS)
                {
                    Item one = from.FindItemOnLayer(Layer.OneHanded);
                    Item two = from.FindItemOnLayer(Layer.TwoHanded);

                    if (one != null)
                        from.AddToBackpack(one);

                    if (two != null)
                        from.AddToBackpack(two);
                }

                from.Target = new BolaTarget(this);
                from.LocalOverheadMessage(MessageType.Emote, 201, 1049632); // * You begin to swing the bola...*
                from.NonlocalOverheadMessage(MessageType.Emote, 201, 1049633, from.Name); // ~1_NAME~ begins to menacingly swing a bola...
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
            _ = reader.ReadInt();
        }

        private static void ReleaseBolaLock(object state)
        {
            ((Mobile)state).EndAction(typeof(Bola));
        }

        private static void FinishThrow(object state)
        {
            object[] states = (object[])state;

            Mobile from = (Mobile)states[0];
            Mobile to = (Mobile)states[1];
            Item bola = (Item)states[2];

            if (!from.Alive)
            {
                return;
            }
            if (!bola.IsChildOf(from.Backpack))
            {
                bola.PrivateOverheadMessage(MessageType.Regular, 946, 1040019, from.NetState); // The bola must be in your pack to use it.
            }
            else if (!from.InRange(to, 15) || !from.InLOS(to) || !from.CanSee(to))
            {
                from.PrivateOverheadMessage(MessageType.Regular, 946, 1042060, from.NetState); // You cannot see that target!
            }
            else if (!to.Mounted && !to.Flying && (!Core.ML || !AnimalForm.UnderTransformation(to)))
            {
                to.PrivateOverheadMessage(MessageType.Regular, 946, 1049628, from.NetState); // You have no reason to throw a bola at that.
            }
            else
            {
                bola.Consume();

                from.Direction = from.GetDirectionTo(to);
                from.Animate(AnimationType.Attack, 4);
                from.MovingEffect(to, 0x26AC, 10, 0, false, false);

                new Bola().MoveToWorld(to.Location, to.Map);

                if (CheckHit(to, from))
                {
                    to.Damage(Utility.RandomMinMax(10, 20), from);
                    //    Dismount(from, to, BlockMountType.BolaRecovery, TimeSpan.FromSeconds(10.0), true);
                    Server.Items.Dismount.DoDismount(from, to, BlockMountType.BolaRecovery, TimeSpan.FromSeconds(10.0), true);
                }
            }
        }

        private static bool CheckHit(Mobile to, Mobile from)
        {
            if (!Core.TOL) return true;

            double toChance = Math.Min(45 + BaseArmor.GetRefinedDefenseChance(to), AosAttributes.GetValue(to, AosAttribute.DefendChance)) + 1;
            double fromChance = AosAttributes.GetValue(from, AosAttribute.AttackChance) + 1;

            double hitChance = toChance / (fromChance * 2);

            if (Utility.RandomDouble() < hitChance)
            {
                if (BaseWeapon.CheckParry(to))
                {
                    to.FixedEffect(0x37B9, 10, 16);
                    to.Animate(AnimationType.Parry, 0);
                    return false;
                }

                return true;
            }

            to.NonlocalOverheadMessage(MessageType.Emote, 0x3B2, false, "*miss*");
            return false;
        }

        public static void Dismount(Mobile dismounter, Mobile dismounted, BlockMountType blockmounttype, TimeSpan delay, bool message)
        {
            if (Core.ML && AnimalForm.UnderTransformation(dismounted))
            {
                AnimalForm.RemoveContext(dismounted, true);
                if (dismounted.Player)
                    dismounted.SendLocalizedMessage(1114066, dismounter.Name); // ~1_NAME~ knocked you out of animal form!
            }
            else
            {
                if (!dismounted.Mounted) return;

                if (dismounted is Neira || dismounted is ChaosDragoon || dismounted is ChaosDragoonElite)
                {
                    if (dismounter.Player)
                        dismounter.SendLocalizedMessage(1042047); // You fail to knock the rider from its mount.
                    return;
                }

                IMount mount = dismounted.Mount;

                if (mount != null)
                {
                    if (dismounter is PlayerMobile)
                    {
                        dismounter.SendLocalizedMessage(1060082); // The force of your attack has dislodged them from their mount!
                        ((PlayerMobile)dismounter).SetMountBlock(BlockMountType.DismountRecovery, TimeSpan.FromSeconds(Core.TOL && dismounter.Weapon is BaseRanged ? 8 : 10), false);
                    }

                    mount.Rider = null;

                    new BaseMount.DespawnTimer(mount, TimeSpan.FromMinutes(1.0)).Start();
                    if (mount is Mobile newMob)
                    {
                        // if (dismounter.Aggressor) newMob.Aggressed = dismounter;
                        newMob?.Attack(dismounter);
                    }

                    if (message)
                    {
                        if (dismounted.Flying)
                        {
                            if (dismounted.Player)
                                dismounted.LocalOverheadMessage(MessageType.Regular, 0x3B2, 1113590, dismounter.Name); // You have been grounded by ~1_NAME~!

                            if (!BaseMount.OnFlightPath(dismounted))
                            {
                                dismounted.Flying = false;
                                dismounted.Freeze(TimeSpan.FromSeconds(1));
                                dismounted.Animate(AnimationType.Land, 0);
                                BuffInfo.RemoveBuff(dismounted, BuffIcon.Fly);
                            }
                        }
                        else
                        {
                            // defender.SendLocalizedMessage(1060083); // You fall off of your mount and take damage!
                            if (dismounted.Player)
                                dismounted.LocalOverheadMessage(MessageType.Regular, 0x3B2, 1049623, dismounter.Name); // You have been knocked off of your mount by ~1_NAME~!
                        }
                    }

                    if (Core.ML)
                    {
                        if (dismounter is BaseCreature)
                        {
                            BaseCreature bc = dismounter as BaseCreature;

                            if (bc.ControlMaster is PlayerMobile)
                            {
                                PlayerMobile pm = bc.ControlMaster as PlayerMobile;
                                pm.SetMountBlock(BlockMountType.DismountRecovery, TimeSpan.FromSeconds(10.0), false);
                            }
                        }
                    }

                    if (delay != TimeSpan.MinValue)
                    {
                        BaseMount.SetMountPrevention(dismounted, mount, blockmounttype, delay);
                    }
                }
            }
        }

        private class BolaTarget : Target
        {
            private readonly Bola m_Bola;
            public BolaTarget(Bola bola)
                : base(20, false, TargetFlags.Harmful)
            {
                m_Bola = bola;
            }

            protected override void OnTarget(Mobile from, object obj)
            {
                if (m_Bola.Deleted)
                    return;

                if ((obj is Item))
                {
                    ((Item)obj).PrivateOverheadMessage(MessageType.Regular, 0x3B2, 1049628, from.NetState); // You have no reason to throw a bola at that.
                    return;
                }

                if (obj is Mobile)
                {
                    Mobile to = (Mobile)obj;

                    if (!m_Bola.IsChildOf(from.Backpack))
                    {
                        m_Bola.PrivateOverheadMessage(MessageType.Regular, 946, 1040019, from.NetState); // The bola must be in your pack to use it.
                    }
                    else if (from.Mounted)
                    {
                        m_Bola.PrivateOverheadMessage(MessageType.Regular, 946, 1042053, from.NetState); // You can't use this while on a mount!
                    }
                    else if (from.Flying)
                    {
                        m_Bola.PrivateOverheadMessage(MessageType.Regular, 946, 1113414, from.NetState); // You can't use this while flying!
                    }
                    else if (from == to)
                    {
                        from.SendLocalizedMessage(1005576); // You can't throw this at yourself.
                    }
                    else if (AnimalForm.UnderTransformation(from))
                    {
                        from.PrivateOverheadMessage(MessageType.Regular, 946, 1070902, from.NetState); // You can't use this while in an animal form!
                    }
                    else if (!to.Mounted && !to.Flying && (!Core.ML || !AnimalForm.UnderTransformation(to)))
                    {
                        to.PrivateOverheadMessage(MessageType.Regular, 946, 1049628, from.NetState); // You have no reason to throw a bola at that.
                    }
                    else if (!from.CanBeHarmful(to))
                    {
                    }
                    else if (from.BeginAction(typeof(Bola)))
                    {
                        from.RevealingAction();

                        EtherealMount.StopMounting(from);

                        Item one = from.FindItemOnLayer(Layer.OneHanded);
                        Item two = from.FindItemOnLayer(Layer.TwoHanded);

                        if (one != null)
                            from.AddToBackpack(one);

                        if (two != null)
                            from.AddToBackpack(two);

                        from.DoHarmful(to);

                        BaseMount.SetMountPrevention(from, BlockMountType.BolaRecovery, TimeSpan.FromSeconds(10.0));

                        Timer.DelayCall(TimeSpan.FromSeconds(10.0), new TimerStateCallback(ReleaseBolaLock), from);
                        Timer.DelayCall(TimeSpan.FromSeconds(3.0), new TimerStateCallback(FinishThrow), new object[] { from, to, m_Bola });
                    }
                    else
                    {
                        m_Bola.PrivateOverheadMessage(MessageType.Regular, 946, 1049624, from.NetState); // You have to wait a few moments before you can use another bola!
                    }
                }
            }
        }
    }
}
