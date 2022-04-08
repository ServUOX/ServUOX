using Server.Engines.Craft;

namespace Server.Items
{
    public class OrcHelm : BaseArmor, IRepairable
    {
        public CraftSystem RepairSystem => DefTailoring.CraftSystem;

        [Constructible]
        public OrcHelm()
            : base(0x1F0B)
        {
            Weight = 5.0;
        }

        public OrcHelm(Serial serial)
            : base(serial)
        {
        }

        public override int BasePhysicalResistance => 3;
        public override int BaseFireResistance => 1;
        public override int BaseColdResistance => 3;
        public override int BasePoisonResistance => 3;
        public override int BaseEnergyResistance => 5;
        public override int InitMinHits => 30;
        public override int InitMaxHits => 50;
        public override int AosStrReq => 30;
        public override int OldStrReq => 10;
        public override int ArmorBase => 20;
        public override ArmorMaterialType MaterialType => ArmorMaterialType.Bone;
        public override CraftResource DefaultResource => CraftResource.RegularLeather;
        public override ArmorMeditationAllowance DefMedAllowance => ArmorMeditationAllowance.None;
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(1);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}
