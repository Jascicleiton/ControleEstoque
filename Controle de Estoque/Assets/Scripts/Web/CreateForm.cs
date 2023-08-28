using Assets.Scripts.Inventory.Database;
using Assets.Scripts.Misc;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Web
{

    /// <summary>
    /// Create all the necessary WWWForms
    /// </summary>
    public class CreateForm
    {
        public static WWWForm GetInventarioForm(string appPassword, List<string> parameters)
        {
            WWWForm inventario = new WWWForm();
            inventario.AddField("apppassword", appPassword);
            switch (InternalDatabase.Instance.currentEstoque)
            {
                case CurrentEstoque.Concert:
                    inventario.AddField("aquisicao", parameters[0]);
                    inventario.AddField("entrada", parameters[1]);
                    inventario.AddField("patrimonio", parameters[2]);
                    inventario.AddField("status", parameters[3]);
                    inventario.AddField("serial", parameters[4]);
                    inventario.AddField("categoria", parameters[5]);
                    inventario.AddField("fabricante", parameters[6]);
                    inventario.AddField("modelo", parameters[7]);
                    inventario.AddField("local", parameters[8]);
                    inventario.AddField("Pessoa", parameters[9]);
                    inventario.AddField("Centro_de_Custo", parameters[10]);
                    inventario.AddField("saida", parameters[11]);
                    break;
                default:
                    inventario.AddField("aquisicao", parameters[0]);
                    inventario.AddField("entrada", parameters[1]);
                    inventario.AddField("patrimonio", parameters[2]);
                    inventario.AddField("status", parameters[3]);
                    inventario.AddField("serial", parameters[4]);
                    inventario.AddField("categoria", parameters[5]);
                    inventario.AddField("fabricante", parameters[6]);
                    inventario.AddField("modelo", parameters[7]);
                    inventario.AddField("local", parameters[8]);
                    inventario.AddField("saida", parameters[9]);
                    inventario.AddField("observacao", parameters[10]);
                    break;
            }
            return inventario;
        }

        public static WWWForm GetExportInventoryForm()
        {
            WWWForm exportForm = new WWWForm();
            //exportForm.AddField("", Intern)
            return exportForm;
        }

        #region Categories Forms

        public static WWWForm GetAdaptadorACForm(string appPassword, List<string> parameters)
        {
            WWWForm inventario = new WWWForm();
            inventario.AddField("apppassword", appPassword);
            HelperMethods.RemoveWhiteSpacesFromMultipleStrings(parameters, out parameters);
            switch (InternalDatabase.Instance.currentEstoque)
            {
                case CurrentEstoque.SnPro:
                    inventario.AddField("modelo", parameters[0]);
                    inventario.AddField("ondefunciona", parameters[1]);
                    inventario.AddField("voltagem", parameters[2]);
                    inventario.AddField("amperagem", parameters[3]);
                    break;
                default:
                    inventario.AddField("patrimonio", parameters[0]);
                    inventario.AddField("modelo", parameters[1]);
                    inventario.AddField("voltagem", parameters[2]);
                    inventario.AddField("amperagem", parameters[3]);
                    break;
            }
            return inventario;
        }

        public static WWWForm GetCarregadorForm(string appPassword, List<string> parameters)
        {
            WWWForm inventario = new WWWForm();
            inventario.AddField("apppassword", appPassword);
            HelperMethods.RemoveWhiteSpacesFromMultipleStrings(parameters, out parameters);
            switch (InternalDatabase.Instance.currentEstoque)
            {
                case CurrentEstoque.SnPro:
                    inventario.AddField("modelo", parameters[0]);
                    inventario.AddField("ondefunciona", parameters[1]);
                    inventario.AddField("voltagem", parameters[2]);
                    inventario.AddField("amperagem", parameters[3]);
                    break;
                default:
                    inventario.AddField("patrimonio", parameters[0]);
                    inventario.AddField("modelo", parameters[1]);
                    inventario.AddField("voltagem", parameters[2]);
                    inventario.AddField("amperagem", parameters[3]);
                    break;
            }
            return inventario;
        }

        public static WWWForm GetDesktopForm(string appPassword, List<string> parameters)
        {
            WWWForm inventario = new WWWForm();
            inventario.AddField("apppassword", appPassword);
            HelperMethods.RemoveWhiteSpacesFromMultipleStrings(parameters, out parameters);
            switch (InternalDatabase.Instance.currentEstoque)
            {
                case CurrentEstoque.SnPro:
                case CurrentEstoque.Testing:
                    inventario.AddField("patrimonio", parameters[0]);
                    inventario.AddField("modeloplacamae", parameters[1]);
                    inventario.AddField("fonte", parameters[2]);
                    inventario.AddField("memoria", parameters[3]);
                    inventario.AddField("hd", parameters[4]);
                    inventario.AddField("placavideo", parameters[5]);
                    inventario.AddField("placarede", parameters[6]);
                    inventario.AddField("leitordvd", parameters[7]);
                    inventario.AddField("processador", parameters[8]);
                    break;
                default:
                    inventario.AddField("patrimonio", parameters[0]);
                    inventario.AddField("modelo", parameters[1]);
                    inventario.AddField("fabricante", parameters[2]);
                    inventario.AddField("hd", parameters[3]);
                    inventario.AddField("memoria", parameters[4]);
                    inventario.AddField("processador", parameters[5]);
                    inventario.AddField("windows", parameters[6]);
                    break;
            }
            return inventario;
        }

        public static WWWForm GetFonteForm(string appPassword, string modelo, string watts, string ondefunciona, string conectores)
        {
            WWWForm inventario = new WWWForm();
            HelperMethods.RemoveWhiteSpacesFromSingleString(modelo, out modelo);
            HelperMethods.RemoveWhiteSpacesFromSingleString(watts, out watts);
            HelperMethods.RemoveWhiteSpacesFromSingleString(ondefunciona, out ondefunciona);
            HelperMethods.RemoveWhiteSpacesFromSingleString(conectores, out conectores);
            inventario.AddField("apppassword", appPassword);
            inventario.AddField("modelo", modelo);
            inventario.AddField("watts", watts);
            inventario.AddField("ondefunciona", ondefunciona);
            inventario.AddField("conectores", conectores);

            return inventario;
        }

        public static WWWForm GetGBICForm(string appPassword, string modelo, string fabricante, string desempenho)
        {
            WWWForm inventario = new WWWForm();
            inventario.AddField("apppassword", appPassword);
            HelperMethods.RemoveWhiteSpacesFromSingleString(modelo, out modelo);
            HelperMethods.RemoveWhiteSpacesFromSingleString(fabricante, out fabricante);
            HelperMethods.RemoveWhiteSpacesFromSingleString(desempenho, out desempenho);
            inventario.AddField("modelo", modelo);
            inventario.AddField("fabricante", fabricante);
            inventario.AddField("desempenho", desempenho);

            return inventario;
        }

        public static WWWForm GetHDForm(string appPassword, string modelo, string fabricante, string Interface, string tamanho, string formaDeArmazenamento,
           string Capacidade, string RPM, string VelocidadeLeitura, string Enterprise)
        {
            WWWForm inventario = new WWWForm();
            inventario.AddField("apppassword", appPassword);
            HelperMethods.RemoveWhiteSpacesFromSingleString(modelo, out modelo);
            inventario.AddField("modelo", modelo);
            HelperMethods.RemoveWhiteSpacesFromSingleString(fabricante, out fabricante);
            inventario.AddField("fabricante", fabricante);
            HelperMethods.RemoveWhiteSpacesFromSingleString(Interface, out Interface);
            inventario.AddField("interface", Interface);
            HelperMethods.RemoveWhiteSpacesFromSingleString(tamanho, out tamanho);
            inventario.AddField("tamanho", tamanho);
            HelperMethods.RemoveWhiteSpacesFromSingleString(formaDeArmazenamento, out formaDeArmazenamento);
            inventario.AddField("formaarmazenamento", formaDeArmazenamento);
            HelperMethods.RemoveWhiteSpacesFromSingleString(Capacidade, out Capacidade);
            inventario.AddField("capacidade", Capacidade);
            HelperMethods.RemoveWhiteSpacesFromSingleString(RPM, out RPM);
            inventario.AddField("rpm", RPM);
            HelperMethods.RemoveWhiteSpacesFromSingleString(VelocidadeLeitura, out VelocidadeLeitura);
            inventario.AddField("velocidade", VelocidadeLeitura);
            HelperMethods.RemoveWhiteSpacesFromSingleString(Enterprise, out Enterprise);
            inventario.AddField("enterprise", Enterprise);

            return inventario;
        }

        public static WWWForm GetiDracForm(string appPassword, string modelo, string fabricante, string porta, string velocidade,
         string entradasd, string servidoressuportados)
        {
            WWWForm inventario = new WWWForm();
            inventario.AddField("apppassword", appPassword);
            HelperMethods.RemoveWhiteSpacesFromSingleString(modelo, out modelo);
            inventario.AddField("modelo", modelo);
            HelperMethods.RemoveWhiteSpacesFromSingleString(fabricante, out fabricante);
            inventario.AddField("fabricante", fabricante);
            HelperMethods.RemoveWhiteSpacesFromSingleString(porta, out porta);
            inventario.AddField("porta", porta);
            HelperMethods.RemoveWhiteSpacesFromSingleString(velocidade, out velocidade);
            inventario.AddField("velocidade", velocidade);
            HelperMethods.RemoveWhiteSpacesFromSingleString(entradasd, out entradasd);
            inventario.AddField("entradasd", entradasd);
            HelperMethods.RemoveWhiteSpacesFromSingleString(servidoressuportados, out servidoressuportados);
            inventario.AddField("servidoressuportados", servidoressuportados);

            return inventario;
        }

        public static WWWForm GetMemoriaForm(string appPassword, string modelo, string fabricante, string tipo, string capacidade, string velocidade,
           string lowvoltage, string rank, string dimm, string taxatransmissao, string simbolo)
        {
            WWWForm inventario = new WWWForm();
            inventario.AddField("apppassword", appPassword);
            HelperMethods.RemoveWhiteSpacesFromSingleString(modelo, out modelo);
            inventario.AddField("modelo", modelo);
            HelperMethods.RemoveWhiteSpacesFromSingleString(fabricante, out fabricante);
            inventario.AddField("fabricante", fabricante);
            HelperMethods.RemoveWhiteSpacesFromSingleString(tipo, out tipo);
            inventario.AddField("tipo", tipo);
            HelperMethods.RemoveWhiteSpacesFromSingleString(capacidade, out capacidade);
            inventario.AddField("capacidade", capacidade);
            HelperMethods.RemoveWhiteSpacesFromSingleString(velocidade, out velocidade);
            inventario.AddField("velocidade", velocidade);
            HelperMethods.RemoveWhiteSpacesFromSingleString(lowvoltage, out lowvoltage);
            inventario.AddField("lowvoltage", lowvoltage);
            HelperMethods.RemoveWhiteSpacesFromSingleString(rank, out rank);
            inventario.AddField("rank", rank);
            HelperMethods.RemoveWhiteSpacesFromSingleString(dimm, out dimm);
            inventario.AddField("dimm", dimm);
            HelperMethods.RemoveWhiteSpacesFromSingleString(taxatransmissao, out taxatransmissao);
            inventario.AddField("taxatransmissao", taxatransmissao);
            HelperMethods.RemoveWhiteSpacesFromSingleString(simbolo, out simbolo);
            inventario.AddField("simbolo", simbolo);

            return inventario;
        }

        public static WWWForm GetMonitorForm(string appPassword, List<string> parameters)
        {
            WWWForm inventario = new WWWForm();
            inventario.AddField("apppassword", appPassword);
            HelperMethods.RemoveWhiteSpacesFromMultipleStrings(parameters, out parameters);
            switch (InternalDatabase.Instance.currentEstoque)
            {
                case CurrentEstoque.SnPro:
                    inventario.AddField("modelo", parameters[0]);
                    inventario.AddField("fabricante", parameters[1]);
                    inventario.AddField("polegadas", parameters[2]);
                    inventario.AddField("tiposentradas", parameters[3]);
                    break;
                default:
                    inventario.AddField("patrimonio", parameters[0]);
                    inventario.AddField("modelo", parameters[1]);
                    inventario.AddField("fabricante", parameters[2]);
                    inventario.AddField("polegadas", parameters[3]);
                    inventario.AddField("tiposentradas", parameters[4]);
                    break;
            }
            return inventario;
        }

        public static WWWForm GetNotebookForm(string appPassword, List<string> parameters)
        {
            WWWForm inventario = new WWWForm();
            inventario.AddField("apppassword", appPassword);
            HelperMethods.RemoveWhiteSpacesFromMultipleStrings(parameters, out parameters);
            switch (InternalDatabase.Instance.currentEstoque)
            {
                case CurrentEstoque.SnPro:
                    inventario.AddField("patrimonio", parameters[0]);
                    inventario.AddField("modelo", parameters[1]);
                    inventario.AddField("fabricante", parameters[2]);
                    inventario.AddField("hd", parameters[3]);
                    inventario.AddField("memoria", parameters[4]);
                    inventario.AddField("EntradaRJ45", parameters[5]);
                    inventario.AddField("bateria", parameters[6]);
                    inventario.AddField("adaptadorac", parameters[7]);
                    inventario.AddField("windows", parameters[8]);
                    break;
                default:
                    inventario.AddField("patrimonio", parameters[0]);
                    inventario.AddField("modelo", parameters[1]);
                    inventario.AddField("fabricante", parameters[2]);
                    inventario.AddField("hd", parameters[3]);
                    inventario.AddField("memoria", parameters[4]);
                    inventario.AddField("processador", parameters[5]);
                    inventario.AddField("windows", parameters[6]);
                    break;
            }
            return inventario;
        }

        public static WWWForm GetPlacaControladoraForm(string appPassword, string modelo, string tipoconexao, string quantidadeportas,
        string tiporaid, string tipohd, string capacidademaxhd, string quantoshd, string bateriainclusa, string barramento)
        {
            WWWForm inventario = new WWWForm();
            inventario.AddField("apppassword", appPassword);
            HelperMethods.RemoveWhiteSpacesFromSingleString(modelo, out modelo);
            inventario.AddField("modelo", modelo);
            HelperMethods.RemoveWhiteSpacesFromSingleString(tipoconexao, out tipoconexao);
            inventario.AddField("tipoconexao", tipoconexao);
            HelperMethods.RemoveWhiteSpacesFromSingleString(quantidadeportas, out quantidadeportas);
            inventario.AddField("quantidadeportas", quantidadeportas);
            HelperMethods.RemoveWhiteSpacesFromSingleString(tiporaid, out tiporaid);
            inventario.AddField("tiporaid", tiporaid);
            HelperMethods.RemoveWhiteSpacesFromSingleString(tipohd, out tipohd);
            inventario.AddField("tipohd", tipohd);
            HelperMethods.RemoveWhiteSpacesFromSingleString(capacidademaxhd, out capacidademaxhd);
            inventario.AddField("capacidademaxhd", capacidademaxhd);
            HelperMethods.RemoveWhiteSpacesFromSingleString(quantoshd, out quantoshd);
            inventario.AddField("quantoshd", quantoshd);
            HelperMethods.RemoveWhiteSpacesFromSingleString(bateriainclusa, out bateriainclusa);
            inventario.AddField("bateriainclusa", bateriainclusa);
            HelperMethods.RemoveWhiteSpacesFromSingleString(barramento, out barramento);
            inventario.AddField("barramento", barramento);

            return inventario;
        }

        public static WWWForm GetPlacaDeCapturaDeVideoForm(string appPassword, string modelo, string quantasentradas)
        {
            WWWForm inventario = new WWWForm();
            inventario.AddField("apppassword", appPassword);
            HelperMethods.RemoveWhiteSpacesFromSingleString(modelo, out modelo);
            inventario.AddField("modelo", modelo);
            HelperMethods.RemoveWhiteSpacesFromSingleString(quantasentradas, out quantasentradas);
            inventario.AddField("quantasentradas", quantasentradas);

            return inventario;
        }

        public static WWWForm GetPlacaDeRedeForm(string appPassword, string modelo, string fabricante, string Interface, string quantidadeportas,
        string quaisconexoes, string suportafibraoptica, string desempenho)
        {
            WWWForm inventario = new WWWForm();
            inventario.AddField("apppassword", appPassword);
            HelperMethods.RemoveWhiteSpacesFromSingleString(modelo, out modelo);
            inventario.AddField("modelo", modelo);
            HelperMethods.RemoveWhiteSpacesFromSingleString(fabricante, out fabricante);
            inventario.AddField("fabricante", fabricante);
            HelperMethods.RemoveWhiteSpacesFromSingleString(Interface, out Interface);
            inventario.AddField("interface", Interface);
            HelperMethods.RemoveWhiteSpacesFromSingleString(quantidadeportas, out quantidadeportas);
            inventario.AddField("quantidadeportas", quantidadeportas);
            HelperMethods.RemoveWhiteSpacesFromSingleString(quaisconexoes, out quaisconexoes);
            inventario.AddField("quaisconexoes", quaisconexoes);
            HelperMethods.RemoveWhiteSpacesFromSingleString(suportafibraoptica, out suportafibraoptica);
            inventario.AddField("suportafibraoptica", suportafibraoptica);
            HelperMethods.RemoveWhiteSpacesFromSingleString(desempenho, out desempenho);
            inventario.AddField("desempenho", desempenho);

            return inventario;
        }

        public static WWWForm GetPlacaSomForm(string appPassword, string modelo, string quantoscanais)
        {
            WWWForm inventario = new WWWForm();
            inventario.AddField("apppassword", appPassword);
            HelperMethods.RemoveWhiteSpacesFromSingleString(modelo, out modelo);
            inventario.AddField("modelo", modelo);
            HelperMethods.RemoveWhiteSpacesFromSingleString(quantoscanais, out quantoscanais);
            inventario.AddField("quantoscanais", quantoscanais);

            return inventario;
        }

        public static WWWForm GetPlacaVideoForm(string appPassword, string modelo, string quantasentradas, string quaisentradas)
        {
            WWWForm inventario = new WWWForm();
            inventario.AddField("apppassword", appPassword);
            HelperMethods.RemoveWhiteSpacesFromSingleString(modelo, out modelo);
            inventario.AddField("modelo", modelo);
            HelperMethods.RemoveWhiteSpacesFromSingleString(quantasentradas, out quantasentradas);
            inventario.AddField("quantasentradas", quantasentradas);
            HelperMethods.RemoveWhiteSpacesFromSingleString(quaisentradas, out quaisentradas);
            inventario.AddField("quaisentradas", quaisentradas);

            return inventario;
        }

        public static WWWForm GetProcessadorForm(string appPassword, string modelo, string soquete, string nucleosfisicos, string nucleoslogicos)
        {
            WWWForm inventario = new WWWForm();
            inventario.AddField("apppassword", appPassword);
            inventario.AddField("modelo", modelo);
            HelperMethods.RemoveWhiteSpacesFromSingleString(soquete, out soquete);
            inventario.AddField("soquete", soquete);
            HelperMethods.RemoveWhiteSpacesFromSingleString(nucleosfisicos, out nucleosfisicos);
            inventario.AddField("nucleosfisicos", nucleosfisicos);
            HelperMethods.RemoveWhiteSpacesFromSingleString(nucleoslogicos, out nucleoslogicos);
            inventario.AddField("nucleoslogicos", nucleoslogicos);

            return inventario;
        }

        public static WWWForm GetRoteadorForm(string appPassword, List<string> parameters)
        {
            WWWForm inventario = new WWWForm();
            inventario.AddField("apppassword", appPassword);
            HelperMethods.RemoveWhiteSpacesFromMultipleStrings(parameters, out parameters);
            switch (InternalDatabase.Instance.currentEstoque)
            {
                case CurrentEstoque.SnPro:
                    inventario.AddField("modelo", parameters[0]);
                    inventario.AddField("wireless", parameters[1]);
                    inventario.AddField("quantasentradas", parameters[2]);
                    inventario.AddField("bandamax", parameters[3]);
                    inventario.AddField("voltagem", parameters[4]);
                    break;
                default:
                    inventario.AddField("patrimonio", parameters[0]);
                    inventario.AddField("modelo", parameters[1]);
                    inventario.AddField("fabricante", parameters[2]);
                    inventario.AddField("wireless", parameters[3]);
                    inventario.AddField("quantasentradas", parameters[4]);
                    break;
            }
            return inventario;
        }

        public static WWWForm GetServidorForm(string appPassword, List<string> parameters)
        {
            WWWForm inventario = new WWWForm();
            inventario.AddField("apppassword", appPassword);
            HelperMethods.RemoveWhiteSpacesFromMultipleStrings(parameters, out parameters);
            switch (InternalDatabase.Instance.currentEstoque)
            {
                case CurrentEstoque.SnPro:
                    inventario.AddField("patrimonio", parameters[0]);
                    inventario.AddField("modelo", parameters[1]);
                    inventario.AddField("fabricante", parameters[2]);
                    inventario.AddField("modeloplacamae", parameters[3]);
                    inventario.AddField("fonte", parameters[4]);
                    inventario.AddField("memoria", parameters[5]);
                    inventario.AddField("hd", parameters[6]);
                    inventario.AddField("placadevideo", parameters[7]);
                    inventario.AddField("placaderede", parameters[8]);
                    inventario.AddField("processador", parameters[9]);
                    inventario.AddField("memoriassuportadas", parameters[10]);
                    inventario.AddField("quantasmemorias", parameters[11]);
                    inventario.AddField("ordemdasmemorias", parameters[12]);
                    inventario.AddField("capacidaderamtotal", parameters[13]);
                    inventario.AddField("soquete", parameters[14]);
                    inventario.AddField("placacontroladora", parameters[15]);
                    inventario.AddField("atequantoshds", parameters[16]);
                    inventario.AddField("tiposdehd", parameters[17]);
                    inventario.AddField("tiposderaid", parameters[18]);
                    break;
                default:
                    inventario.AddField("patrimonio", parameters[0]);
                    inventario.AddField("modelo", parameters[1]);
                    inventario.AddField("fabricante", parameters[2]);
                    inventario.AddField("hd", parameters[3]);
                    inventario.AddField("memoria", parameters[4]);
                    inventario.AddField("processador", parameters[5]);
                    inventario.AddField("windows", parameters[6]);
                    break;
            }
            return inventario;
        }

        public static WWWForm GetStorageNASForm(string appPassword, string modelo, string tamanhohd, string tiporaid,
           string tipohd, string capacidademaxhd, string quantoshd)
        {
            WWWForm inventario = new WWWForm();
            inventario.AddField("apppassword", appPassword);
            inventario.AddField("modelo", modelo);
            HelperMethods.RemoveWhiteSpacesFromSingleString(tamanhohd, out tamanhohd);
            inventario.AddField("tamanhohd", tamanhohd);
            HelperMethods.RemoveWhiteSpacesFromSingleString(tiporaid, out tiporaid);
            inventario.AddField("tiporaid", tiporaid);
            HelperMethods.RemoveWhiteSpacesFromSingleString(tipohd, out tipohd);
            inventario.AddField("tipohd", tipohd);
            HelperMethods.RemoveWhiteSpacesFromSingleString(capacidademaxhd, out capacidademaxhd);
            inventario.AddField("capacidademaxhd", capacidademaxhd);
            HelperMethods.RemoveWhiteSpacesFromSingleString(quantoshd, out quantoshd);
            inventario.AddField("quantoshd", quantoshd);

            return inventario;
        }

        public static WWWForm GetSwitchForm(string appPassword, List<string> parameters)
        {
            WWWForm inventario = new WWWForm();
            inventario.AddField("apppassword", appPassword);
            HelperMethods.RemoveWhiteSpacesFromMultipleStrings(parameters, out parameters);
            switch (InternalDatabase.Instance.currentEstoque)
            {
                case CurrentEstoque.SnPro:
                    inventario.AddField("modelo", parameters[0]);
                    inventario.AddField("quantasquaisportas", parameters[1]);
                    inventario.AddField("capacidademaxporta", parameters[2]);
                    break;
                default:
                    inventario.AddField("patrimonio", parameters[0]);
                    inventario.AddField("modelo", parameters[1]);
                    inventario.AddField("fabricante", parameters[2]);
                    inventario.AddField("quantasquaisportas", parameters[3]);
                    break;
            }
            return inventario;
        }
        #endregion

        #region Consult Forms
        public static WWWForm GetConsultPatrimonioForm(string appPassword, string patrimonio)
        {
            WWWForm inventario = new WWWForm();
            inventario.AddField("apppassword", appPassword);
            inventario.AddField("patrimonio", patrimonio);

            return inventario;
        }

        public static WWWForm GetConsultSerialForm(string appPassword, string serial)
        {
            WWWForm inventario = new WWWForm();
            inventario.AddField("apppassword", appPassword);
            inventario.AddField("serial", serial);

            return inventario;
        }
        #endregion

        #region Movements
        public static WWWForm GetMoveItemForm(string appPassword, string patrimonio, string serial, string usuario, string data,
            string deOnde, string paraOnde)
        {
            WWWForm inventario = new WWWForm();
            inventario.AddField("apppassword", appPassword);
            inventario.AddField("patrimonio", patrimonio);
            inventario.AddField("serial", serial);
            inventario.AddField("usuario", usuario);
            inventario.AddField("data", data);
            inventario.AddField("deonde", deOnde);
            inventario.AddField("paraonde", paraOnde);
            return inventario;
        }
        public static WWWForm GetMoveNoPaNoSeItemForm(string appPassword, string nome, int quantidade, string usuario, string data,
            string deOnde, string paraOnde)
        {
            WWWForm inventario = new WWWForm();
            inventario.AddField("apppassword", appPassword);
            inventario.AddField("itemname", nome);
            inventario.AddField("itemQuantity", quantidade);
            inventario.AddField("usuario", usuario);
            inventario.AddField("data", data);
            inventario.AddField("deonde", deOnde);
            inventario.AddField("paraonde", paraOnde);
            return inventario;
        }

        public static WWWForm GetNoPaNoSeForm(string appPassword, string itemName, int itemQuantity)
        {
            WWWForm item = new WWWForm();
            item.AddField("apppassword", appPassword);
            item.AddField("itemname", itemName);
            item.AddField("itemQuantity", itemQuantity);
            return item;
        }

        //public static WWWForm GetMoveItemForm(string appPassword, string itemName, int itemQuantity, string usuario,
        //    string data, string deOnde, string paraOnde)
        //{
        //    WWWForm item = new WWWForm();
        //    item.AddField("apppassword", appPassword);
        //    item.AddField("itemname", itemName);
        //    item.AddField("itemQuantity", itemQuantity.ToString());
        //    item.AddField("usuario", usuario);
        //    item.AddField("data", data);
        //    item.AddField("deonde", deOnde);
        //    item.AddField("paraonde", paraOnde);

        //    return item;
        //}

        /// <summary>
        /// Form used to get all the movements from a specific item for both NoPaNoSe item and regular item
        /// </summary>
        public static WWWForm GetMovementsForm(string appPassword, string parameter)
        {
            WWWForm movementForm = new WWWForm();
            movementForm.AddField("apppassword", appPassword);
            movementForm.AddField("parameter", parameter);

            return movementForm;
        }

        /// <summary>
        /// Form used to get all the movements for both NoPaNoSe items and regular items
        /// </summary>
        public static WWWForm GetAllMovements(string appPassword)
        {
            WWWForm item = new WWWForm();
            item.AddField("apppassword", appPassword);
            return item;
        }
        #endregion

        #region Users
        public static WWWForm GetCheckAccessLevelForm(string appPassword, string username, string password)
        {
            WWWForm checkAccesssLevelForm = new WWWForm();
            checkAccesssLevelForm.AddField("apppassword", appPassword);
            checkAccesssLevelForm.AddField("username", username);
            checkAccesssLevelForm.AddField("password", password);

            return checkAccesssLevelForm;
        }

        public static WWWForm GetUsersForm(string appPassword)
        {
            WWWForm user = new WWWForm();
            user.AddField("apppassword", appPassword);
            return user;
        }
        #endregion
    }
}