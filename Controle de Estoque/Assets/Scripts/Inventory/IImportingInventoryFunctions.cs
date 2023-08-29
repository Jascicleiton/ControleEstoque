using SimpleJSON;

namespace Assets.Scripts.Inventory
{
    public interface IImportingInventoryFunctions
    {
        void ImportAdaptadorAC(JSONNode inventario, out Sheet importSheet);
        void ImportCarregador(JSONNode inventario, out Sheet importSheet);
        void ImportDesktop(JSONNode inventario, out Sheet importSheet);
        void ImportFonte(JSONNode inventario, out Sheet importSheet);
        void ImportFonteDeAlimentacao(JSONNode inventario);
        void ImportGBIC(JSONNode inventario, out Sheet importSheet);
        void ImportHD(JSONNode inventario, out Sheet importSheet);
        void ImportIdrac(JSONNode inventario, out Sheet importSheet);
        void ImportInventory(JSONNode inventario, out Sheet importSheet);
        void ImportMemoria(JSONNode inventario, out Sheet importSheet);
        void ImportMonitor(JSONNode inventario, out Sheet importSheet);
        void ImportNotebook(JSONNode inventario, out Sheet importSheet);
        void ImportPlacaControladora(JSONNode inventario, out Sheet importSheet);
        void ImportPlacaDeCapturaDeVideo(JSONNode inventario, out Sheet importSheet);
        void ImportPlacaDeRede(JSONNode inventario, out Sheet importSheet);
        void ImportPlacaDeSom(JSONNode inventario, out Sheet importSheet);
        void ImportPlacaDeVideo(JSONNode inventario, out Sheet importSheet);
        void ImportPlacaSAS(JSONNode inventario, out Sheet importSheet);
        void ImportProcessador(JSONNode inventario, out Sheet importSheet);
        void ImportRoteador(JSONNode inventario, out Sheet importSheet);
        void ImportServidor(JSONNode inventario, out Sheet importSheet);
        void ImportStorageNas(JSONNode inventario, out Sheet importSheet);
        void ImportSwitch(JSONNode inventario, out Sheet importSheet);
    }
}