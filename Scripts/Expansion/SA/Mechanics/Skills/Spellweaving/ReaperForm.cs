using System;
using Server.Network;

namespace Server.Spells.Spellweaving
{
    public class ReaperFormSpell : ArcaneForm
    {
        private static readonly SpellInfo m_Info = new SpellInfo("Reaper Form", "Tarisstree", -1);
        public ReaperFormSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

        public override TimeSpan CastDelayBase => TimeSpan.FromSeconds(2.5);
        public override double RequiredSkill => 24.0;
        public override int RequiredMana => 34;
        public override int Body => 0x11D;
        public override int FireResistOffset => -25;
        public override int PhysResistOffset => 5 + FocusLevel;
        public override int ColdResistOffset => 5 + FocusLevel;
        public override int PoisResistOffset => 5 + FocusLevel;
        public override int NrgyResistOffset => 5 + FocusLevel;
        public virtual int SwingSpeedBonus => 10 + FocusLevel;
        public virtual int SpellDamageBonus => 10 + FocusLevel;
        public static void Initialize()
        {
            if (!Core.SA)
            {
                EventSink.Login += new LoginEventHandler(OnLogin);
            }
        }

        public static void OnLogin(LoginEventArgs e)
        {
            TransformContext context = TransformationSpellHelper.GetContext(e.Mobile);

            if (context != null && context.Type == typeof(ReaperFormSpell))
                e.Mobile.SendSpeedControl(SpeedControlType.WalkSpeed);
        }

        public override void DoEffect(Mobile m)
        {
            m.PlaySound(0x1BA);

            BuffInfo.AddBuff(Caster, new BuffInfo(BuffIcon.ReaperForm, 1071034, 1153781, "10\t10\t5\t5\t5\t5\t25"));

            if (!Core.SA)
            {
                m.SendSpeedControl(SpeedControlType.WalkSpeed);
            }
        }

        public override void RemoveEffect(Mobile m)
        {
            if (!Core.SA)
            {
                m.SendSpeedControl(SpeedControlType.Disable);
            }

            BuffInfo.RemoveBuff(m, BuffIcon.ReaperForm);
        }
    }
}
