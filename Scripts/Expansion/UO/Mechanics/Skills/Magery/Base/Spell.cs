using Server.Items;
using Server.Misc;
using Server.Mobiles;
using Server.Network;
using Server.Spells.Bushido;
using Server.Spells.Chivalry;
using Server.Spells.First;
using Server.Spells.Fourth;
using Server.Spells.Mysticism;
using Server.Spells.Necromancy;
using Server.Spells.Ninjitsu;
using Server.Spells.Second;
using Server.Targeting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Server.Spells
{
    public abstract class Spell : ISpell
    {
        public int ID => SpellRegistry.GetRegistryNumber(this);

        public SpellState State { get; set; }

        public Mobile Caster { get; }
        public SpellInfo Info { get; }
        public string Name => Info.Name;
        public string Mantra => Info.Mantra;
        public Type[] Reagents => Info.Reagents;
        public Item Scroll { get; }
        public long StartCastTime { get; private set; }

        public IDamageable InstantTarget { get; set; }

        private static readonly TimeSpan NextSpellDelay = TimeSpan.FromSeconds(0.75);
        private static TimeSpan AnimateDelay = TimeSpan.FromSeconds(1.5);

        public virtual SkillName CastSkill => SkillName.Magery;
        public virtual SkillName DamageSkill => SkillName.EvalInt;

        public virtual bool RevealOnCast => true;
        public virtual bool ClearHandsOnCast => true;
        public virtual bool ShowHandMovement => true;

        public virtual bool DelayedDamage => false;
        public virtual Type[] DelayDamageFamily => null;
        // DelayDamageFamily can define spells so they don't stack, even though they are different spells
        // Right now, magic arrow and nether bolt are the only ones that have this functionality

        public virtual bool DelayedDamageStacking => true;
        //In reality, it's ANY delayed Damage spell Post-AoS that can't stack, but, only 
        //Expo & Magic Arrow have enough delay and a short enough cast time to bring up 
        //the possibility of stacking 'em.  Note that a MA & an Explosion will stack, but
        //of course, two MA's won't.

        public virtual DamageType SpellDamageType => DamageType.Spell;

        private static readonly Dictionary<Type, DelayedDamageContextWrapper> m_ContextTable =
            new Dictionary<Type, DelayedDamageContextWrapper>();

        private class DelayedDamageContextWrapper
        {
            private readonly Dictionary<IDamageable, Timer> m_Contexts = new Dictionary<IDamageable, Timer>();

            public void Add(IDamageable d, Timer t)
            {

                if (m_Contexts.TryGetValue(d, out Timer oldTimer))
                {
                    oldTimer.Stop();
                    m_Contexts.Remove(d);
                }

                m_Contexts.Add(d, t);
            }

            public bool Contains(IDamageable d)
            {
                return m_Contexts.ContainsKey(d);
            }

            public void Remove(IDamageable d)
            {
                m_Contexts.Remove(d);
            }
        }

        public void StartDelayedDamageContext(IDamageable d, Timer t)
        {
            if (DelayedDamageStacking)
            {
                return; //Sanity
            }


            if (!m_ContextTable.TryGetValue(GetType(), out DelayedDamageContextWrapper contexts))
            {
                contexts = new DelayedDamageContextWrapper();
                Type type = GetType();

                m_ContextTable.Add(type, contexts);

                if (DelayDamageFamily != null)
                {
                    foreach (var familyType in DelayDamageFamily)
                    {
                        m_ContextTable.Add(familyType, contexts);
                    }
                }
            }

            contexts.Add(d, t);
        }

        public bool HasDelayContext(IDamageable d)
        {
            if (DelayedDamageStacking)
            {
                return false; //Sanity
            }

            Type t = GetType();

            if (m_ContextTable.ContainsKey(t))
            {
                return m_ContextTable[t].Contains(d);
            }

            return false;
        }

        public void RemoveDelayedDamageContext(IDamageable d)
        {
            Type type = GetType();

            if (!m_ContextTable.TryGetValue(type, out DelayedDamageContextWrapper contexts))
            {
                return;
            }

            contexts.Remove(d);

            if (DelayDamageFamily != null)
            {
                foreach (var t in DelayDamageFamily)
                {
                    if (m_ContextTable.TryGetValue(t, out contexts))
                    {
                        contexts.Remove(d);
                    }
                }
            }
        }

        public void HarmfulSpell(IDamageable d)
        {
            if (d is BaseCreature)
            {
                ((BaseCreature)d).OnHarmfulSpell(Caster);
            }
            else if (d is IDamageableItem)
            {
                ((IDamageableItem)d).OnHarmfulSpell(Caster);
            }

            NegativeAttributes.OnCombatAction(Caster);

            if (d is Mobile)
            {
                if ((Mobile)d != Caster)
                {
                    NegativeAttributes.OnCombatAction((Mobile)d);
                }

                EvilOmenSpell.TryEndEffect((Mobile)d);
            }
        }

        public Spell(Mobile caster, Item scroll, SpellInfo info)
        {
            Caster = caster;
            Scroll = scroll;
            Info = info;
        }

        public virtual int GetNewAosDamage(int bonus, int dice, int sides, IDamageable singleTarget)
        {
            if (singleTarget != null)
            {
                return GetNewAosDamage(bonus, dice, sides, (Caster.Player && singleTarget is PlayerMobile), GetDamageScalar(singleTarget as Mobile), singleTarget);
            }
            else
            {
                return GetNewAosDamage(bonus, dice, sides, false, null);
            }
        }

        public virtual int GetNewAosDamage(int bonus, int dice, int sides, bool playerVsPlayer, IDamageable damageable)
        {
            return GetNewAosDamage(bonus, dice, sides, playerVsPlayer, 1.0, damageable);
        }

        public virtual int GetNewAosDamage(int bonus, int dice, int sides, bool playerVsPlayer, double scalar, IDamageable damageable)
        {
            Mobile target = damageable as Mobile;

            int damage = Utility.Dice(dice, sides, bonus) * 100;

            int inscribeSkill = GetInscribeFixed(Caster);
            int scribeBonus = inscribeSkill >= 1000 ? 10 : inscribeSkill / 200;

            int damageBonus = scribeBonus +
                              (Caster.Int / 10) +
                              SpellHelper.GetSpellDamageBonus(Caster, target, CastSkill, playerVsPlayer);

            int evalSkill = GetDamageFixed(Caster);
            int evalScale = 30 + ((9 * evalSkill) / 100);

            damage = AOS.Scale(damage, evalScale);
            damage = AOS.Scale(damage, 100 + damageBonus);
            damage = AOS.Scale(damage, (int)(scalar * 100));

            return damage / 100;
        }

        public virtual bool IsCasting => State == SpellState.Casting;

        public virtual void OnCasterHurt()
        {
            CheckCasterDisruption(false, 0, 0, 0, 0, 0);
        }

        public virtual void CheckCasterDisruption(bool checkElem = false, int phys = 0, int fire = 0, int cold = 0, int pois = 0, int nrgy = 0)
        {
            if (!Caster.Player || Caster.AccessLevel > AccessLevel.Player)
            {
                return;
            }

            if (IsCasting)
            {
                object o = ProtectionSpell.Registry[Caster];
                bool disturb = true;

                if (o != null && o is double)
                {
                    if (((double)o) > Utility.RandomDouble() * 100.0)
                    {
                        disturb = false;
                    }
                }

                #region Stygian Abyss
                int focus = SAAbsorptionAttributes.GetValue(Caster, SAAbsorptionAttribute.CastingFocus);

                if (BaseFishPie.IsUnderEffects(Caster, FishPieEffect.CastFocus))
                {
                    focus += 2;
                }

                if (focus > 12)
                {
                    focus = 12;
                }

                focus += Caster.Skills[SkillName.Inscribe].Value >= 50 ? GetInscribeFixed(Caster) / 200 : 0;

                if (focus > 0 && focus > Utility.Random(100))
                {
                    disturb = false;
                    Caster.SendLocalizedMessage(1113690); // You regain your focus and continue casting the spell.
                }
                else if (checkElem)
                {
                    int res = 0;

                    if (phys == 100)
                    {
                        res = Math.Min(40, SAAbsorptionAttributes.GetValue(Caster, SAAbsorptionAttribute.ResonanceKinetic));
                    }
                    else if (fire == 100)
                    {
                        res = Math.Min(40, SAAbsorptionAttributes.GetValue(Caster, SAAbsorptionAttribute.ResonanceFire));
                    }
                    else if (cold == 100)
                    {
                        res = Math.Min(40, SAAbsorptionAttributes.GetValue(Caster, SAAbsorptionAttribute.ResonanceCold));
                    }
                    else if (pois == 100)
                    {
                        res = Math.Min(40, SAAbsorptionAttributes.GetValue(Caster, SAAbsorptionAttribute.ResonancePoison));
                    }
                    else if (nrgy == 100)
                    {
                        res = Math.Min(40, SAAbsorptionAttributes.GetValue(Caster, SAAbsorptionAttribute.ResonanceEnergy));
                    }

                    if (res > Utility.Random(100))
                    {
                        disturb = false;
                    }
                }
                #endregion

                if (disturb)
                {
                    Disturb(DisturbType.Hurt, false, true);
                }
            }
        }

        public virtual void OnCasterKilled()
        {
            Disturb(DisturbType.Kill);
        }

        public virtual void OnConnectionChanged()
        {
            FinishSequence();
        }

        /// <summary>
        /// Pre-ML code where mobile can change directions, but doesn't move
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
		public virtual bool OnCasterMoving(Direction d)
        {
            if (IsCasting && BlocksMovement && (!(Caster is BaseCreature) || ((BaseCreature)Caster).FreezeOnCast))
            {
                Caster.SendLocalizedMessage(500111); // You are frozen and can not move.
                return false;
            }

            return true;
        }

        /// <summary>
        /// Post ML code where player is frozen in place while casting.
        /// </summary>
        /// <param name="caster"></param>
        /// <returns></returns>
        public virtual bool CheckMovement(Mobile caster)
        {
            if (IsCasting && BlocksMovement && (!(Caster is BaseCreature) || ((BaseCreature)Caster).FreezeOnCast))
            {
                return false;
            }

            return true;
        }

        public virtual bool OnCasterEquiping(Item item)
        {
            if (IsCasting)
            {
                if ((item.Layer == Layer.OneHanded || item.Layer == Layer.TwoHanded) && item.AllowEquipedCast(Caster))
                {
                    return true;
                }

                Disturb(DisturbType.EquipRequest);
            }

            return true;
        }

        public virtual bool OnCasterUsingObject(object o)
        {
            if (State == SpellState.Sequencing)
            {
                Disturb(DisturbType.UseRequest);
            }

            return true;
        }

        public virtual bool OnCastInTown(Region r)
        {
            return Info.AllowTown;
        }

        public virtual bool ConsumeReagents()
        {
            if ((Scroll != null && !(Scroll is SpellStone)) || !Caster.Player)
            {
                return true;
            }

            if (AosAttributes.GetValue(Caster, AosAttribute.LowerRegCost) > Utility.Random(100))
            {
                return true;
            }

            Container pack = Caster.Backpack;

            if (pack == null)
            {
                return false;
            }

            if (pack.ConsumeTotal(Info.Reagents, Info.Amounts) == -1)
            {
                return true;
            }

            return false;
        }

        public virtual double GetInscribeSkill(Mobile m)
        {
            // There is no chance to gain
            // m.CheckSkill( SkillName.Inscribe, 0.0, 120.0 );
            return m.Skills[SkillName.Inscribe].Value;
        }

        public virtual int GetInscribeFixed(Mobile m)
        {
            // There is no chance to gain
            // m.CheckSkill( SkillName.Inscribe, 0.0, 120.0 );
            return m.Skills[SkillName.Inscribe].Fixed;
        }

        public virtual int GetDamageFixed(Mobile m)
        {
            //m.CheckSkill( DamageSkill, 0.0, m.Skills[DamageSkill].Cap );
            return m.Skills[DamageSkill].Fixed;
        }

        public virtual double GetDamageSkill(Mobile m)
        {
            //m.CheckSkill( DamageSkill, 0.0, m.Skills[DamageSkill].Cap );
            return m.Skills[DamageSkill].Value;
        }

        public virtual double GetResistSkill(Mobile m)
        {
            return m.Skills[SkillName.MagicResist].Value - EvilOmenSpell.GetResistMalus(m);
        }

        public virtual double GetDamageScalar(Mobile target)
        {
            double scalar = 1.0;

            if (target == null)
            {
                return scalar;
            }

            if (!Core.AOS) //EvalInt stuff for AoS is handled elsewhere
            {
                double casterEI = Caster.Skills[DamageSkill].Value;
                double targetRS = target.Skills[SkillName.MagicResist].Value;

                /*
				if( Core.AOS )
				targetRS = 0;
				*/

                //m_Caster.CheckSkill( DamageSkill, 0.0, 120.0 );

                if (casterEI > targetRS)
                {
                    scalar = (1.0 + ((casterEI - targetRS) / 500.0));
                }
                else
                {
                    scalar = (1.0 + ((casterEI - targetRS) / 200.0));
                }

                // magery damage bonus, -25% at 0 skill, +0% at 100 skill, +5% at 120 skill
                scalar += (Caster.Skills[CastSkill].Value - 100.0) / 400.0;

                if (!target.Player && !target.Body.IsHuman /*&& !Core.AOS*/)
                {
                    scalar *= 2.0; // Double magery damage to monsters/animals if not AOS
                }
            }

            if (target is BaseCreature)
            {
                ((BaseCreature)target).AlterDamageScalarFrom(Caster, ref scalar);
            }

            if (Caster is BaseCreature)
            {
                ((BaseCreature)Caster).AlterDamageScalarTo(target, ref scalar);
            }

            if (Core.SE)
            {
                scalar *= GetSlayerDamageScalar(target);
            }

            target.Region.SpellDamageScalar(Caster, target, ref scalar);

            if (Evasion.CheckSpellEvasion(target)) //Only single target spells an be evaded
            {
                scalar = 0;
            }

            return scalar;
        }

        public virtual double GetSlayerDamageScalar(Mobile defender)
        {
            Spellbook atkBook = Spellbook.FindEquippedSpellbook(Caster);

            double scalar = 1.0;
            if (atkBook != null)
            {
                SlayerEntry atkSlayer = SlayerGroup.GetEntryByName(atkBook.Slayer);
                SlayerEntry atkSlayer2 = SlayerGroup.GetEntryByName(atkBook.Slayer2);

                if (atkSlayer == null && atkSlayer2 == null)
                {
                    atkSlayer = SlayerGroup.GetEntryByName(SlayerSocket.GetSlayer(atkBook));
                }

                if (atkSlayer != null && atkSlayer.Slays(defender) || atkSlayer2 != null && atkSlayer2.Slays(defender))
                {
                    defender.FixedEffect(0x37B9, 10, 5);

                    bool isSuper = false;

                    if (atkSlayer != null && atkSlayer == atkSlayer.Group.Super)
                    {
                        isSuper = true;
                    }
                    else if (atkSlayer2 != null && atkSlayer2 == atkSlayer2.Group.Super)
                    {
                        isSuper = true;
                    }

                    scalar = isSuper ? 2.0 : 3.0;
                }


                TransformContext context = TransformationSpellHelper.GetContext(defender);

                if ((atkBook.Slayer == SlayerName.Silver || atkBook.Slayer2 == SlayerName.Silver) && context != null && context.Type != typeof(HorrificBeastSpell))
                {
                    scalar += .25; // Every necromancer transformation other than horrific beast take an additional 25% damage
                }

                if (scalar != 1.0)
                {
                    return scalar;
                }
            }

            ISlayer defISlayer = Spellbook.FindEquippedSpellbook(defender);

            if (defISlayer == null)
            {
                defISlayer = defender.Weapon as ISlayer;
            }

            if (defISlayer != null)
            {
                SlayerEntry defSlayer = SlayerGroup.GetEntryByName(defISlayer.Slayer);
                SlayerEntry defSlayer2 = SlayerGroup.GetEntryByName(defISlayer.Slayer2);

                if (defSlayer != null && defSlayer.Group.OppositionSuperSlays(Caster) ||
                    defSlayer2 != null && defSlayer2.Group.OppositionSuperSlays(Caster))
                {
                    scalar = 2.0;
                }
            }

            return scalar;
        }

        public virtual void DoFizzle()
        {
            Caster.LocalOverheadMessage(MessageType.Regular, 0x3B2, 502632); // The spell fizzles.

            if (Caster.Player)
            {
                if (Core.AOS)
                {
                    Caster.FixedParticles(0x3735, 1, 30, 9503, EffectLayer.Waist);
                }
                else
                {
                    Caster.FixedEffect(0x3735, 6, 30);
                }

                Caster.PlaySound(0x5C);
            }
        }

        private CastTimer m_CastTimer;
        private AnimTimer m_AnimTimer;

        public void Disturb(DisturbType type)
        {
            Disturb(type, true, false);
        }

        public virtual bool CheckDisturb(DisturbType type, bool firstCircle, bool resistable)
        {
            if (resistable && Scroll is BaseWand)
            {
                return false;
            }

            return true;
        }

        public void Disturb(DisturbType type, bool firstCircle, bool resistable)
        {
            if (!CheckDisturb(type, firstCircle, resistable))
            {
                return;
            }

            if (State == SpellState.Casting)
            {
                if (!firstCircle && !Core.AOS && this is MagerySpell && ((MagerySpell)this).Circle == SpellCircle.First)
                {
                    return;
                }

                State = SpellState.None;
                Caster.Spell = null;
                Caster.Delta(MobileDelta.Flags);

                OnDisturb(type, true);

                if (m_CastTimer != null)
                {
                    m_CastTimer.Stop();
                }

                if (m_AnimTimer != null)
                {
                    m_AnimTimer.Stop();
                }

                if (Core.AOS && Caster.Player && type == DisturbType.Hurt)
                {
                    DoHurtFizzle();
                }

                Caster.NextSpellTime = Core.TickCount + (int)GetDisturbRecovery().TotalMilliseconds;
            }
            else if (State == SpellState.Sequencing)
            {
                if (!firstCircle && !Core.AOS && this is MagerySpell && ((MagerySpell)this).Circle == SpellCircle.First)
                {
                    return;
                }

                State = SpellState.None;
                Caster.Spell = null;
                Caster.Delta(MobileDelta.Flags);

                OnDisturb(type, false);

                Target.Cancel(Caster);

                if (Core.AOS && Caster.Player && type == DisturbType.Hurt)
                {
                    DoHurtFizzle();
                }
            }
        }

        public virtual void DoHurtFizzle()
        {
            Caster.FixedEffect(0x3735, 6, 30);
            Caster.PlaySound(0x5C);
        }

        public virtual void OnDisturb(DisturbType type, bool message)
        {
            if (message)
            {
                Caster.SendLocalizedMessage(500641); // Your concentration is disturbed, thus ruining thy spell.
            }
        }

        public virtual bool CheckCast()
        {
            #region High Seas
            if (Multis.BaseBoat.IsDriving(Caster) && Caster.AccessLevel == AccessLevel.Player)
            {
                Caster.SendLocalizedMessage(1049616); // You are too busy to do that at the moment.
                return false;
            }
            #endregion

            return true;
        }

        public virtual void SayMantra()
        {
            if (Scroll is SpellStone)
            {
                return;
            }

            if (Scroll is BaseWand)
            {
                return;
            }

            if (Info.Mantra != null && Info.Mantra.Length > 0 && (Caster.Player || (Caster is BaseCreature && ((BaseCreature)Caster).ShowSpellMantra)))
            {
                Caster.PublicOverheadMessage(MessageType.Spell, Caster.SpeechHue, true, Info.Mantra, false);
            }
        }

        public virtual bool BlockedByHorrificBeast
        {
            get
            {
                if (TransformationSpellHelper.UnderTransformation(Caster, typeof(HorrificBeastSpell)) &&
                    SpellHelper.HasSpellFocus(Caster, CastSkill))
                {
                    return false;
                }

                return true;
            }
        }

        public virtual bool BlockedByAnimalForm => true;
        public virtual bool BlocksMovement => true;

        public virtual bool CheckNextSpellTime => !(Scroll is BaseWand);

        public virtual bool Cast()
        {
            StartCastTime = Core.TickCount;

            if (Core.AOS && Caster.Spell is Spell && ((Spell)Caster.Spell).State == SpellState.Sequencing)
            {
                ((Spell)Caster.Spell).Disturb(DisturbType.NewCast);
            }

            if (!Caster.CheckAlive())
            {
                return false;
            }
            else if (Caster is PlayerMobile && ((PlayerMobile)Caster).Peaced)
            {
                Caster.SendLocalizedMessage(1072060); // You cannot cast a spell while calmed.
            }
            else if (Scroll is BaseWand && Caster.Spell != null && Caster.Spell.IsCasting)
            {
                Caster.SendLocalizedMessage(502643); // You can not cast a spell while frozen.
            }
            else if (SkillHandlers.SpiritSpeak.IsInSpiritSpeak(Caster) || (Caster.Spell != null && Caster.Spell.IsCasting))
            {
                Caster.SendLocalizedMessage(502642); // You are already casting a spell.
            }
            else if (BlockedByHorrificBeast && TransformationSpellHelper.UnderTransformation(Caster, typeof(HorrificBeastSpell)) ||
                     (BlockedByAnimalForm && AnimalForm.UnderTransformation(Caster)))
            {
                Caster.SendLocalizedMessage(1061091); // You cannot cast that spell in this form.
            }
            else if (!(Scroll is BaseWand) && (Caster.Paralyzed || Caster.Frozen))
            {
                Caster.SendLocalizedMessage(502643); // You can not cast a spell while frozen.
            }
            else if (CheckNextSpellTime && Core.TickCount - Caster.NextSpellTime < 0)
            {
                Caster.SendLocalizedMessage(502644); // You have not yet recovered from casting a spell.
            }
            else if (Caster is PlayerMobile && ((PlayerMobile)Caster).PeacedUntil > DateTime.UtcNow)
            {
                Caster.SendLocalizedMessage(1072060); // You cannot cast a spell while calmed.
            }
            else if (Caster.Mana >= ScaleMana(GetMana()))
            {
                #region Stygian Abyss
                if (Caster.Race == Race.Gargoyle && Caster.Flying)
                {
                    if (BaseMount.OnFlightPath(Caster))
                    {
                        if (Caster.IsPlayer())
                        {
                            Caster.SendLocalizedMessage(1113750); // You may not cast spells while flying over such precarious terrain.
                            return false;
                        }
                        else
                        {
                            Caster.SendMessage("Your staff level allows you to cast while flying over precarious terrain.");
                        }
                    }
                }
                #endregion

                if (Caster.Spell == null && Caster.CheckSpellCast(this) && CheckCast() &&
                    Caster.Region.OnBeginSpellCast(Caster, this))
                {
                    State = SpellState.Casting;
                    Caster.Spell = this;

                    Caster.Delta(MobileDelta.Flags);

                    if (!(Scroll is BaseWand) && RevealOnCast)
                    {
                        Caster.RevealingAction();
                    }

                    SayMantra();

                    /*
                     * EA seems to use some type of spell variation, of -100 ms + timer resolution.
                     * Using the below millisecond dropoff with a 50ms timer resolution seems to be exact
                     * to EA.
                     */

                    TimeSpan castDelay = GetCastDelay().Subtract(TimeSpan.FromMilliseconds(100));

                    if (ShowHandMovement && !(Scroll is SpellStone) && (Caster.Body.IsHuman || (Caster.Player && Caster.Body.IsMonster)))
                    {
                        int count = (int)Math.Ceiling(castDelay.TotalSeconds / AnimateDelay.TotalSeconds);

                        if (count != 0)
                        {
                            m_AnimTimer = new AnimTimer(this, count);
                            m_AnimTimer.Start();
                        }

                        if (Info.LeftHandEffect > 0)
                        {
                            Caster.FixedParticles(0, 10, 5, Info.LeftHandEffect, EffectLayer.LeftHand);
                        }

                        if (Info.RightHandEffect > 0)
                        {
                            Caster.FixedParticles(0, 10, 5, Info.RightHandEffect, EffectLayer.RightHand);
                        }
                    }

                    if (ClearHandsOnCast)
                    {
                        Caster.ClearHands();
                    }

                    if (Core.ML)
                    {
                        WeaponAbility.ClearCurrentAbility(Caster);
                    }

                    m_CastTimer = new CastTimer(this, castDelay);
                    //m_CastTimer.Start();

                    OnBeginCast();

                    if (castDelay > TimeSpan.Zero)
                    {
                        m_CastTimer.Start();
                    }
                    else
                    {
                        m_CastTimer.Tick();
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                Caster.LocalOverheadMessage(MessageType.Regular, 0x22, 502625, ScaleMana(GetMana()).ToString()); // Insufficient mana. You must have at least ~1_MANA_REQUIREMENT~ Mana to use this spell.
            }

            return false;
        }

        public abstract void OnCast();

        #region Enhanced Client
        public bool OnCastInstantTarget()
        {
            if (InstantTarget == null)
            {
                return false;
            }

            Type spellType = GetType();

            var types = spellType.GetNestedTypes(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            if (types != null)
            {
                Type targetType = types.FirstOrDefault(t => t.IsSubclassOf(typeof(Server.Targeting.Target)));

                if (targetType != null)
                {
                    Target t = null;

                    try
                    {
                        t = Activator.CreateInstance(targetType, this) as Target;
                    }
                    catch
                    {
                        LogBadConstructorForInstantTarget();
                    }

                    if (t != null)
                    {
                        t.Invoke(Caster, InstantTarget);
                        return true;
                    }
                }
            }

            return false;
        }
        #endregion

        public virtual void OnBeginCast()
        {
            SendCastEffect();
        }

        public virtual void SendCastEffect()
        { }

        public virtual void GetCastSkills(out double min, out double max)
        {
            min = max = 0; //Intended but not required for overriding.
        }

        public virtual bool CheckFizzle()
        {
            if (Scroll is BaseWand)
            {
                return true;
            }


            GetCastSkills(out double minSkill, out double maxSkill);

            if (DamageSkill != CastSkill && DamageSkill != SkillName.Imbuing)
            {
                Caster.CheckSkill(DamageSkill, 0.0, Caster.Skills[DamageSkill].Cap);
            }

            bool skillCheck = Caster.CheckSkill(CastSkill, minSkill, maxSkill);

            return Caster is BaseCreature || skillCheck;
        }

        public abstract int GetMana();

        public virtual int ScaleMana(int mana)
        {
            double scalar = 1.0;

            if (ManaPhasingOrb.IsInManaPhase(Caster))
            {
                ManaPhasingOrb.RemoveFromTable(Caster);
                return 0;
            }

            if (!MindRotSpell.GetMindRotScalar(Caster, ref scalar))
            {
                scalar = 1.0;
            }

            if (Mysticism.PurgeMagicSpell.IsUnderCurseEffects(Caster))
            {
                scalar += .5;
            }

            // Lower Mana Cost = 40%
            int lmc = AosAttributes.GetValue(Caster, AosAttribute.LowerManaCost);

            if (lmc > 40)
            {
                lmc = 40;
            }

            lmc += BaseArmor.GetInherentLowerManaCost(Caster);

            scalar -= (double)lmc / 100;

            return (int)(mana * scalar);
        }

        public virtual TimeSpan GetDisturbRecovery()
        {
            if (Core.AOS)
            {
                return TimeSpan.Zero;
            }

            double delay = 1.0 - Math.Sqrt((Core.TickCount - StartCastTime) / 1000.0 / GetCastDelay().TotalSeconds);

            if (delay < 0.2)
            {
                delay = 0.2;
            }

            return TimeSpan.FromSeconds(delay);
        }

        public virtual int CastRecoveryBase => 6;
        public virtual int CastRecoveryFastScalar => 1;
        public virtual int CastRecoveryPerSecond => 4;
        public virtual int CastRecoveryMinimum => 0;

        public virtual TimeSpan GetCastRecovery()
        {
            if (!Core.AOS)
            {
                return NextSpellDelay;
            }

            int fcr = AosAttributes.GetValue(Caster, AosAttribute.CastRecovery);

            int fcrDelay = -(CastRecoveryFastScalar * fcr);

            int delay = CastRecoveryBase + fcrDelay;

            if (delay < CastRecoveryMinimum)
            {
                delay = CastRecoveryMinimum;
            }

            return TimeSpan.FromSeconds((double)delay / CastRecoveryPerSecond);
        }

        public abstract TimeSpan CastDelayBase { get; }

        public virtual double CastDelayFastScalar => 1;
        public virtual double CastDelaySecondsPerTick => 0.25;
        public virtual TimeSpan CastDelayMinimum => TimeSpan.FromSeconds(0.25);

        public virtual TimeSpan GetCastDelay()
        {
            if (Scroll is SpellStone)
            {
                return TimeSpan.Zero;
            }

            if (Scroll is BaseWand)
            {
                return Core.ML ? CastDelayBase : TimeSpan.Zero; // TODO: Should FC apply to wands?
            }

            // Faster casting cap of 2 (if not using the protection spell) 
            // Faster casting cap of 0 (if using the protection spell) 
            // Paladin spells are subject to a faster casting cap of 4 
            // Paladins with magery of 70.0 or above are subject to a faster casting cap of 2 
            int fcMax = 4;

            if (CastSkill == SkillName.Magery || CastSkill == SkillName.Necromancy || CastSkill == SkillName.Mysticism ||
                (CastSkill == SkillName.Chivalry && (Caster.Skills[SkillName.Magery].Value >= 70.0 || Caster.Skills[SkillName.Mysticism].Value >= 70.0)))
            {
                fcMax = 2;
            }

            int fc = AosAttributes.GetValue(Caster, AosAttribute.CastSpeed);

            if (fc > fcMax)
            {
                fc = fcMax;
            }

            if (ProtectionSpell.Registry.ContainsKey(Caster) || EodonianPotion.IsUnderEffects(Caster, PotionEffect.Urali))
            {
                fc = Math.Min(fcMax - 2, fc - 2);
            }

            TimeSpan baseDelay = CastDelayBase;

            TimeSpan fcDelay = TimeSpan.FromSeconds(-(CastDelayFastScalar * fc * CastDelaySecondsPerTick));

            //int delay = CastDelayBase + circleDelay + fcDelay;
            TimeSpan delay = baseDelay + fcDelay;

            if (delay < CastDelayMinimum)
            {
                delay = CastDelayMinimum;
            }

            if (DreadHorn.IsUnderInfluence(Caster))
            {
                delay.Add(delay);
            }

            //return TimeSpan.FromSeconds( (double)delay / CastDelayPerSecond );
            return delay;
        }

        public virtual void FinishSequence()
        {
            SpellState oldState = State;

            State = SpellState.None;

            if (oldState == SpellState.Casting)
            {
                Caster.Delta(MobileDelta.Flags);
            }

            if (Caster.Spell == this)
            {
                Caster.Spell = null;
            }
        }

        public virtual int ComputeKarmaAward()
        {
            return 0;
        }

        public virtual bool CheckSequence()
        {
            int mana = ScaleMana(GetMana());

            if (Caster.Deleted || !Caster.Alive || Caster.Spell != this || State != SpellState.Sequencing)
            {
                DoFizzle();
            }
            else if (Scroll != null && !(Scroll is Runebook) &&
                     (Scroll.Amount <= 0 || Scroll.Deleted || Scroll.RootParent != Caster ||
                      (Scroll is BaseWand && (((BaseWand)Scroll).Charges <= 0 || Scroll.Parent != Caster))))
            {
                DoFizzle();
            }
            else if (!ConsumeReagents())
            {
                Caster.LocalOverheadMessage(MessageType.Regular, 0x22, 502630); // More reagents are needed for this spell.
            }
            else if (Caster.Mana < mana)
            {
                Caster.LocalOverheadMessage(MessageType.Regular, 0x22, 502625, mana.ToString()); // Insufficient mana for this spell.
            }
            else if (Core.AOS && (Caster.Frozen || Caster.Paralyzed))
            {
                Caster.SendLocalizedMessage(502646); // You cannot cast a spell while frozen.
                DoFizzle();
            }
            else if (Caster is PlayerMobile && ((PlayerMobile)Caster).PeacedUntil > DateTime.UtcNow)
            {
                Caster.SendLocalizedMessage(1072060); // You cannot cast a spell while calmed.
                DoFizzle();
            }
            else if (CheckFizzle())
            {
                Caster.Mana -= mana;

                if (Scroll is SpellStone)
                {
                    ((SpellStone)Scroll).Use(Caster);
                }

                if (Scroll is SpellScroll)
                {
                    Scroll.Consume();
                }

                else if (Scroll is BaseWand)
                {
                    ((BaseWand)Scroll).ConsumeCharge(Caster);
                    Caster.RevealingAction();
                }

                if (Scroll is BaseWand)
                {
                    bool m = Scroll.Movable;

                    Scroll.Movable = false;

                    if (ClearHandsOnCast)
                    {
                        Caster.ClearHands();
                    }

                    Scroll.Movable = m;
                }
                else
                {
                    if (ClearHandsOnCast)
                    {
                        Caster.ClearHands();
                    }
                }

                int karma = ComputeKarmaAward();

                if (karma != 0)
                {
                    Titles.AwardKarma(Caster, karma, true);
                }

                if (TransformationSpellHelper.UnderTransformation(Caster, typeof(VampiricEmbraceSpell)))
                {
                    bool garlic = false;

                    for (int i = 0; !garlic && i < Info.Reagents.Length; ++i)
                    {
                        garlic = (Info.Reagents[i] == Reagent.Garlic);
                    }

                    if (garlic)
                    {
                        Caster.SendLocalizedMessage(1061651); // The garlic burns you!
                        AOS.Damage(Caster, Utility.RandomMinMax(17, 23), 100, 0, 0, 0, 0);
                    }
                }

                return true;
            }
            else
            {
                DoFizzle();
            }

            return false;
        }

        public bool CheckBSequence(Mobile target)
        {
            return CheckBSequence(target, false);
        }

        public bool CheckBSequence(Mobile target, bool allowDead)
        {
            if (!target.Alive && !allowDead)
            {
                Caster.SendLocalizedMessage(501857); // This spell won't work on that!
                return false;
            }
            else if (Caster.CanBeBeneficial(target, true, allowDead) && CheckSequence())
            {
                if (ValidateBeneficial(target))
                {
                    Caster.DoBeneficial(target);
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckHSequence(IDamageable target)
        {
            if (!target.Alive || (target is IDamageableItem && !((IDamageableItem)target).CanDamage))
            {
                Caster.SendLocalizedMessage(501857); // This spell won't work on that!
                return false;
            }
            else if (Caster.CanBeHarmful(target) && CheckSequence())
            {
                Caster.DoHarmful(target);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ValidateBeneficial(Mobile target)
        {
            if (target == null)
            {
                return true;
            }

            if (this is HealSpell || this is GreaterHealSpell || this is CloseWoundsSpell)
            {
                return target.Hits < target.HitsMax;
            }

            if (this is CureSpell || this is CleanseByFireSpell)
            {
                return target.Poisoned;
            }

            return true;
        }

        public virtual IEnumerable<IDamageable> AcquireIndirectTargets(IPoint3D pnt, int range)
        {
            return SpellHelper.AcquireIndirectTargets(Caster, pnt, Caster.Map, range);
        }

        private class AnimTimer : Timer
        {
            private readonly Spell m_Spell;

            public AnimTimer(Spell spell, int count)
                : base(TimeSpan.Zero, AnimateDelay, count)
            {
                m_Spell = spell;

                Priority = TimerPriority.FiftyMS;
            }

            protected override void OnTick()
            {
                if (m_Spell.State != SpellState.Casting || m_Spell.Caster.Spell != m_Spell)
                {
                    Stop();
                    return;
                }

                if (!m_Spell.Caster.Mounted && m_Spell.Info.Action >= 0)
                {
                    if (Core.SA)
                    {
                        m_Spell.Caster.Animate(AnimationType.Spell, 0);
                    }
                    else
                    {
                        if (m_Spell.Caster.Body.IsHuman)
                        {
                            m_Spell.Caster.Animate(m_Spell.Info.Action, 7, 1, true, false, 0);
                        }
                        else if (m_Spell.Caster.Player && m_Spell.Caster.Body.IsMonster)
                        {
                            m_Spell.Caster.Animate(12, 7, 1, true, false, 0);
                        }
                    }
                }

                if (!Running)
                {
                    m_Spell.m_AnimTimer = null;
                }
            }
        }

        private class CastTimer : Timer
        {
            private readonly Spell m_Spell;

            public CastTimer(Spell spell, TimeSpan castDelay)
                : base(castDelay)
            {
                m_Spell = spell;

                Priority = TimerPriority.FiftyMS;
            }

            protected override void OnTick()
            {
                if (m_Spell == null || m_Spell.Caster == null)
                {
                    return;
                }
                else if (m_Spell.State == SpellState.Casting && m_Spell.Caster.Spell == m_Spell)
                {
                    m_Spell.State = SpellState.Sequencing;
                    m_Spell.m_CastTimer = null;
                    m_Spell.Caster.OnSpellCast(m_Spell);

                    m_Spell.Caster.Delta(MobileDelta.Flags);

                    if (m_Spell.Caster.Region != null)
                    {
                        m_Spell.Caster.Region.OnSpellCast(m_Spell.Caster, m_Spell);
                    }

                    m_Spell.Caster.NextSpellTime = Core.TickCount + (int)m_Spell.GetCastRecovery().TotalMilliseconds;

                    Target originalTarget = m_Spell.Caster.Target;

                    if (m_Spell.InstantTarget == null || !m_Spell.OnCastInstantTarget())
                    {
                        m_Spell.OnCast();
                    }

                    if (m_Spell.Caster.Player && m_Spell.Caster.Target != originalTarget && m_Spell.Caster.Target != null)
                    {
                        m_Spell.Caster.Target.BeginTimeout(m_Spell.Caster, TimeSpan.FromSeconds(30.0));
                    }

                    m_Spell.m_CastTimer = null;
                }
            }

            public void Tick()
            {
                OnTick();
            }
        }

        public void LogBadConstructorForInstantTarget()
        {
            try
            {
                using (System.IO.StreamWriter op = new System.IO.StreamWriter("InstantTargetErr.log", true))
                {
                    op.WriteLine("# {0}", DateTime.UtcNow);
                    op.WriteLine("Target with bad contructor args:");
                    op.WriteLine("Offending Spell: {0}", ToString());
                    op.WriteLine("_____");
                }
            }
            catch
            { }
        }
    }
}
