
namespace Server.Items
{
    public class ArmorIgnore : WeaponAbility
    {
        public ArmorIgnore()
        {
        }

        public override int BaseMana => 30;
        public override double DamageScalar => 0.9;

        public override void OnHit(Mobile attacker, Mobile defender, int damage)
        {
            if (!Validate(attacker) || !CheckMana(attacker, true))
                return;

            ClearCurrentAbility(attacker);

            attacker.SendLocalizedMessage(1060076); // Your attack penetrates their armor!
            defender.SendLocalizedMessage(1060077); // The blow penetrated your armor!

            defender.PlaySound(0x56);
            defender.FixedParticles(0x3728, 200, 25, 9942, EffectLayer.Waist);
        }
    }
}
