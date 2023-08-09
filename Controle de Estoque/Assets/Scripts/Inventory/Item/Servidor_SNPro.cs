namespace Assets.Scripts.Inventory.Item
{
    public class Servidor_SNPro : Servidor
    {
        protected override void Awake()
        {
            base.Awake();
            allParameters.Add(ConstStrings.ModeloPlacaMae, default);
            allParameters.Add(ConstStrings.Fonte, default);
            allParameters.Add(ConstStrings.PlacaDeVideo, default);
            allParameters.Add(ConstStrings.PlacaDeRede, default);
            allParameters.Add(ConstStrings.MemoriasSuportadas, default);
            allParameters.Add(ConstStrings.AteQuantasMemorias_I, default);
            allParameters.Add(ConstStrings.OrdemDasMemorias, default);
            allParameters.Add(ConstStrings.CapacidadeRamTotal_I, default);
            allParameters.Add(ConstStrings.Soquete, default);
            allParameters.Add(ConstStrings.PlacaControladora, default);
            allParameters.Add(ConstStrings.AteQuantosHds_I, default);
            allParameters.Add(ConstStrings.TiposDeHd, default);
            allParameters.Add(ConstStrings.TiposDeRaid, default);
        }
    }
}