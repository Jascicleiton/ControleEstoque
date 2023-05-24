using System;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine;
using System.Linq;

namespace Assets.Scripts.UI
{
    public class ItemInformationPanelControler1 : MonoBehaviour
    {
        private VisualElement root;
        private VisualElement[] itemBoxes;
        private List<Label> parameterNames;
                /// <summary>
        /// Patrimônio = 2, Fabricante = 6, Modelo = 7
        /// </summary>
        private List<TextField> parameterValues;
        private Label[] parameterValuesText;
        private TabInputHandler tabInputHandler;

        private int numberOfActiveBoxes = 0;

        private void Start()
        {
            if (tabInputHandler == null)
            {
                tabInputHandler = GetComponent<TabInputHandler>();
            }
            if (tabInputHandler != null)
            {
                tabInputHandler.isWithItemInformationPanelController = true;
            }
        }

        private void OnEnable()
        {
            GetUIReferences();
            SubscribeToEvents();
        }

        private void OnDisable()
        {
            UnsubscribeToEvents();
        }

        private void GetUIReferences()
        {
            root = GetComponent<UIDocument>().rootVisualElement;
            itemBoxes = root.Q<VisualElement>("ParametersContainer").Children().ToArray<VisualElement>();
            parameterNames = root.Query(name: "ParametersContainer").Descendents<Label>().ToList();
            parameterValues = root.Query(name: "ParametersContainer").Descendents<TextField>().ToList();
          
        }

        private void SubscribeToEvents()
        {
            EventHandler.EnableInput += SetInputEnabled;
        }

        private void UnsubscribeToEvents()
        {
            EventHandler.EnableInput -= SetInputEnabled;
        }

        /// <summary>
        /// Enable or disable input. Called by EnableInput event.
        /// </summary>
        private void SetInputEnabled(bool inputEnabled)
        {
            foreach (var item in parameterValues)
            {
                item.isReadOnly = inputEnabled;
            }
        }

        /// <summary>
        /// Hide all item boxes that have a empty name
        /// </summary>
        private void HideEmptyItemBox()
        {
            for (int i = 0; i < parameterNames.Count; i++)
            {
                if (parameterNames[i] != null && parameterNames[i].text == "")
                {
                    itemBoxes[i].style.display = DisplayStyle.None;
                }
            }
        }

        /// <summary>
        /// Activate all item boxes
        /// </summary>
        private void ActivateAllItemBoxes()
        {
            for (int i = 0; i < itemBoxes.Length; i++)
            {
                itemBoxes[i].style.display = DisplayStyle.Flex;
                if (parameterValues.Count > 0)
                {
                    parameterValues[i].isReadOnly = false;
                }
            }
        }

        /// <summary>
        /// Fill the names of all item boxes that should get a name
        /// </summary>
        private void FillNames(List<string> names)
        {
            for (int i = 0; i < parameterNames.Count; i++)
            {
                if (i < names.Count)
                {
                    parameterNames[i].text = names[i];
                }
                else
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Fill the values of all item boxes that should get a value. Used when the item box have an inputField object
        /// </summary>
        private void FillValues(List<string> values)
        {
            for (int i = 0; i < parameterValues.Count; i++)
            {
                if (i < values.Count)
                {
                    parameterValues[i].value = values[i];
                }
                else
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Fill the values of all item boxes that should get a value. Used when the item box have an text object
        /// </summary>
        private void FillValuesTexts(List<string> values)
        {
            for (int i = 0; i < parameterValues.Count; i++)
            {
                if (i < values.Count)
                {
                    parameterValues[i].value = values[i];
                }
                else
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Fill the placeholders of input fields
        /// </summary>
        private void FillPlaceHolders(List<string> values)
        {
            for (int i = 0; i < parameterValues.Count; i++)
            {
                if (i < values.Count)
                {
                    parameterValues[i].value = values[i] + "...";
                }
                else
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Get the number of active boxes
        /// </summary>
        public int GetNumberOfActiveBoxes()
        {
            numberOfActiveBoxes = 0;
            for (int i = 0; i < itemBoxes.Length; i++)
            {
                if (itemBoxes[i].style.display == DisplayStyle.Flex)
                {
                    numberOfActiveBoxes++;
                }
            }
            return numberOfActiveBoxes;
        }

        /// <summary>
        /// Reset the values of all item boxes to empty
        /// </summary>
        public void ResetValues()
        {
            if (parameterValues != null && parameterValues.Count > 0)
            {
                for (int i = 0; i < parameterValues.Count; i++)
                {
                    if (parameterValues[i] != null)
                    {
                        parameterValues[i].value = "";
                        parameterValues[i].isReadOnly = false;
                    }
                }
            }
            if (parameterValues != null && parameterValues.Count > 0)
            {
                for (int i = 0; i < parameterValues.Count; i++)
                {
                    if (parameterValues[i] != null)
                    {
                        parameterValues[i].value = "";
                    }
                }
            }
        }

        /// <summary>
        /// Reset the names of all item boxes to ""
        /// </summary>
        private void ResetNames()
        {
            for (int i = 0; i < parameterNames.Count; i++)
            {
                if (parameterNames[i] != null)
                {
                    parameterNames[i].text = "";
                }
            }
        }

        /// <summary>
        /// Resets the names and values of all item boxes
        /// </summary>
        public void ResetItems()
        {
            for (int i = 0; i < itemBoxes.Length; i++)
            {
                itemBoxes[i].style.display = DisplayStyle.Flex;
            }
            ResetNames();
            ResetValues();
            for (int i = 0; i < itemBoxes.Length; i++)
            {
                itemBoxes[i].style.display = DisplayStyle.None;
            }
        }

        /// <summary>
        /// Show all informations of a specific item.
        /// </summary>
        public void ShowItem(ItemColumns itemToShow)
        {
            ActivateAllItemBoxes();
            if (itemToShow != null)
            {
                Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
                dictionary = HelperMethods.GetParameterValuesNamesPlaceholders(itemToShow, itemToShow.Categoria);
                List<string> names = new List<string>();
                List<string> values = new List<string>();
                List<string> placeholders = new List<string>();
                dictionary.TryGetValue("Names", out names);
                dictionary.TryGetValue("Values", out values);
                dictionary.TryGetValue("Placeholders", out placeholders);
                FillNames(names);
                FillValues(values);
                //FillPlaceHolders(placeholders);
            }
            else
            {
                print("Item to show is null");
            }
            HideEmptyItemBox();

          //  ChangeSize();
        }

        /// <summary>
        /// Show all informations of a specific item on the consult scene
        /// </summary>
        public void ShowItemConsult(ItemColumns itemToShow)
        {
            ActivateAllItemBoxes();
            if (itemToShow != null)
            {
                Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
                dictionary = HelperMethods.GetParameterValuesNamesPlaceholders(itemToShow, itemToShow.Categoria);
                List<string> names = new List<string>();
                List<string> values = new List<string>();
                dictionary.TryGetValue("Names", out names);
                dictionary.TryGetValue("Values", out values);
                FillNames(names);
                FillValuesTexts(values);
            }
            else
            {
                print("Item to show is null");
            }
            HideEmptyItemBox();
        }

        /// <summary>
        /// Show the template of an item based on it's category.
        /// </summary>
        public void ShowCategoryItemTemplate(string category)
        {
            ActivateAllItemBoxes();
            ResetNames();
            ResetValues();
            Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
            dictionary = HelperMethods.GetParameterValuesNamesPlaceholders(null, category);
            List<string> names = new List<string>();
            List<string> placeholders = new List<string>();
            dictionary.TryGetValue("Names", out names);
            dictionary.TryGetValue("Placeholders", out placeholders);
            FillNames(names);
           // FillPlaceHolders(placeholders);
            parameterValues[0].value = DateTime.Now.ToString("dd/MM/yyyy");
            parameterValues[1].value = DateTime.Now.ToString("dd/MM/yyyy");
            parameterValues[8].value = "Estoque";
            HideEmptyItemBox();
        }

        /// <summary>
        /// Disable the input of certain item boxes  based on the category of the item
        /// </summary>
        public void DisableItemsForAdd(string category)
        {
            if (category == ConstStrings.Outros)
            {
                itemBoxes[5].style.display = DisplayStyle.Flex;
            }
            else
            {
                itemBoxes[5].style.display = DisplayStyle.None;
            }
            switch (InternalDatabase.Instance.currentEstoque)
            {
                case CurrentEstoque.Concert:
                    itemBoxes[11].style.display = DisplayStyle.None;
                    break;
                default:
                    itemBoxes[9].style.display = DisplayStyle.None;
                    break;
            }
        }

        /// <summary>
        /// Get all the inventory values that are/were set on the input boxes
        /// </summary>
        public List<string> GetInventoryValues()
        {
           // print("Number of parameters found by iteminformationpanelcontroller: " + parameterValues.Count);
            List<string> valuesList = new List<string>();
            int index = 0;
            foreach (var item in parameterValues)
            {
                if (item != null)
                {
                    print(index + ": " + item.value);
                    valuesList.Add(item.text);
                    index++;
                }
                //else
                //{
                //    print("not active");
                //}
            }
            return valuesList;
        }

        /// <summary>
        /// Get all the values that are specific to each category that are/were set on the input boxes
        /// </summary>
        public List<string> GetCategoryValues(string category)
        {
            print(parameterValues[7].text);
            print(parameterValues[11].text);
            print(parameterValues[12].text);
            print(parameterValues[13].text);
            List<string> valuesList = new List<string>();
            switch (category)
            {
                #region Adaptador AC
                case ConstStrings.AdaptadorAC:
                    valuesList.Clear();
                    switch (InternalDatabase.Instance.currentEstoque)
                    {
                        case CurrentEstoque.SnPro:
                        case CurrentEstoque.Testing:
                            valuesList.Add(parameterValues[7].text);
                            valuesList.Add(parameterValues[11].text);
                            valuesList.Add(parameterValues[12].text);
                            valuesList.Add(parameterValues[13].text);
                            break;
                        case CurrentEstoque.Fumsoft:
                        case CurrentEstoque.ESF:
                        case CurrentEstoque.Clientes:
                            valuesList.Add(parameterValues[2].text);
                            valuesList.Add(parameterValues[7].text);
                            valuesList.Add(parameterValues[11].text);
                            valuesList.Add(parameterValues[12].text);
                            break;
                        case CurrentEstoque.Concert:
                            valuesList.Add(parameterValues[2].text);
                            valuesList.Add(parameterValues[7].text);
                            valuesList.Add(parameterValues[12].text);
                            valuesList.Add(parameterValues[13].text);
                            break;
                        default:
                            break;
                    }
                    break;
                #endregion
                #region Carregador
                case ConstStrings.Carregador:
                    valuesList.Clear();
                    switch (InternalDatabase.Instance.currentEstoque)
                    {
                        case CurrentEstoque.Fumsoft:
                        case CurrentEstoque.ESF:
                        case CurrentEstoque.Clientes:
                            valuesList.Add(parameterValues[2].text);
                            valuesList.Add(parameterValues[7].text);
                            valuesList.Add(parameterValues[11].text);
                            valuesList.Add(parameterValues[12].text);
                            break;
                        case CurrentEstoque.Concert:
                            valuesList.Add(parameterValues[2].text);
                            valuesList.Add(parameterValues[7].text);
                            valuesList.Add(parameterValues[12].text);
                            valuesList.Add(parameterValues[13].text);
                            break;
                        default:
                            valuesList.Add(parameterValues[7].text);
                            valuesList.Add(parameterValues[11].text);
                            valuesList.Add(parameterValues[12].text);
                            valuesList.Add(parameterValues[13].text);
                            break;
                    }
                    break;
                #endregion
                #region Desktop
                case ConstStrings.Desktop:
                    valuesList.Clear();
                    switch (InternalDatabase.Instance.currentEstoque)
                    {
                        case CurrentEstoque.Concert:
                            valuesList.Add(parameterValues[2].text);
                            valuesList.Add(parameterValues[7].text);
                            valuesList.Add(parameterValues[6].text);
                            valuesList.Add(parameterValues[12].text);
                            valuesList.Add(parameterValues[13].text);
                            valuesList.Add(parameterValues[14].text);
                            valuesList.Add(parameterValues[15].text);
                            valuesList.Add(parameterValues[16].text);
                            valuesList.Add(parameterValues[17].text);
                            break;
                        case CurrentEstoque.Fumsoft:
                        case CurrentEstoque.ESF:
                            valuesList.Add(parameterValues[2].text);
                            valuesList.Add(parameterValues[7].text);
                            valuesList.Add(parameterValues[6].text);
                            valuesList.Add(parameterValues[11].text);
                            valuesList.Add(parameterValues[12].text);
                            valuesList.Add(parameterValues[13].text);
                            valuesList.Add(parameterValues[14].text);
                            valuesList.Add(parameterValues[15].text);
                            valuesList.Add(parameterValues[16].text);
                            break;
                        default:
                            valuesList.Add(parameterValues[2].text);
                            valuesList.Add(parameterValues[11].text);
                            valuesList.Add(parameterValues[12].text);
                            valuesList.Add(parameterValues[13].text);
                            valuesList.Add(parameterValues[14].text);
                            valuesList.Add(parameterValues[15].text);
                            valuesList.Add(parameterValues[16].text);
                            valuesList.Add(parameterValues[17].text);
                            valuesList.Add(parameterValues[18].text);
                            break;
                    }
                    break;
                #endregion
                #region Fonte
                case ConstStrings.Fonte:
                    valuesList.Clear();
                    switch (InternalDatabase.Instance.currentEstoque)
                    {
                        case CurrentEstoque.Concert:
                            valuesList.Add(parameterValues[7].text);
                            valuesList.Add(parameterValues[12].text);
                            valuesList.Add(parameterValues[13].text);
                            valuesList.Add(parameterValues[14].text);
                            break;
                        default:
                            valuesList.Add(parameterValues[7].text);
                            valuesList.Add(parameterValues[11].text);
                            valuesList.Add(parameterValues[12].text);
                            valuesList.Add(parameterValues[13].text);
                            break;
                    }
                    break;
                #endregion
                #region GBIC
                case ConstStrings.Gbic:
                    valuesList.Clear();
                    switch (InternalDatabase.Instance.currentEstoque)
                    {
                        case CurrentEstoque.Concert:
                            valuesList.Add(parameterValues[7].text);
                            valuesList.Add(parameterValues[6].text);
                            valuesList.Add(parameterValues[12].text);
                            break;
                        default:
                            valuesList.Add(parameterValues[7].text);
                            valuesList.Add(parameterValues[6].text);
                            valuesList.Add(parameterValues[11].text);
                            break;
                    }
                    break;
                #endregion
                #region HD
                case ConstStrings.HD:
                    valuesList.Clear();
                    valuesList.Add(parameterValues[7].text);
                    valuesList.Add(parameterValues[6].text);
                    valuesList.Add(parameterValues[11].text);
                    valuesList.Add(parameterValues[12].text);
                    valuesList.Add(parameterValues[13].text);
                    valuesList.Add(parameterValues[14].text);
                    valuesList.Add(parameterValues[15].text);
                    valuesList.Add(parameterValues[16].text);
                    valuesList.Add(parameterValues[17].text);
                    break;
                #endregion
                #region iDRAC
                case ConstStrings.Idrac:
                    valuesList.Clear();
                    valuesList.Add(parameterValues[7].text);
                    valuesList.Add(parameterValues[6].text);
                    valuesList.Add(parameterValues[11].text);
                    valuesList.Add(parameterValues[12].text);
                    valuesList.Add(parameterValues[13].text);
                    valuesList.Add(parameterValues[14].text);
                    break;
                #endregion
                #region Memória
                case ConstStrings.Memoria:
                    valuesList.Clear();
                    valuesList.Add(parameterValues[7].text);
                    valuesList.Add(parameterValues[6].text);
                    valuesList.Add(parameterValues[11].text);
                    valuesList.Add(parameterValues[12].text);
                    valuesList.Add(parameterValues[13].text);
                    valuesList.Add(parameterValues[14].text);
                    valuesList.Add(parameterValues[15].text);
                    valuesList.Add(parameterValues[16].text);
                    valuesList.Add(parameterValues[17].text);
                    valuesList.Add(parameterValues[18].text);
                    break;
                #endregion
                #region Monitor
                case ConstStrings.Monitor:
                    valuesList.Clear();
                    switch (InternalDatabase.Instance.currentEstoque)
                    {
                        case CurrentEstoque.Concert:
                            valuesList.Add(parameterValues[2].text);
                            valuesList.Add(parameterValues[7].text);
                            valuesList.Add(parameterValues[6].text);
                            valuesList.Add(parameterValues[12].text);
                            valuesList.Add(parameterValues[13].text);
                            break;
                        case CurrentEstoque.SnPro:
                        case CurrentEstoque.Testing:
                            valuesList.Add(parameterValues[7].text);
                            valuesList.Add(parameterValues[6].text);
                            valuesList.Add(parameterValues[11].text);
                            valuesList.Add(parameterValues[12].text);
                            break;
                        default:
                            valuesList.Add(parameterValues[2].text);
                            valuesList.Add(parameterValues[7].text);
                            valuesList.Add(parameterValues[6].text);
                            valuesList.Add(parameterValues[11].text);
                            valuesList.Add(parameterValues[12].text);
                            break;
                    }
                    break;
                #endregion
                #region Notebook
                case ConstStrings.Notebook:
                    valuesList.Clear();
                    switch (InternalDatabase.Instance.currentEstoque)
                    {
                        case CurrentEstoque.Concert:
                            valuesList.Add(parameterValues[2].text);
                            valuesList.Add(parameterValues[7].text);
                            valuesList.Add(parameterValues[6].text);
                            valuesList.Add(parameterValues[12].text);
                            valuesList.Add(parameterValues[13].text);
                            valuesList.Add(parameterValues[14].text);
                            valuesList.Add(parameterValues[15].text);
                            valuesList.Add(parameterValues[16].text);
                            valuesList.Add(parameterValues[17].text);
                            break;
                        default:
                            valuesList.Add(parameterValues[2].text);
                            valuesList.Add(parameterValues[7].text);
                            valuesList.Add(parameterValues[6].text);
                            valuesList.Add(parameterValues[11].text);
                            valuesList.Add(parameterValues[12].text);
                            valuesList.Add(parameterValues[13].text);
                            valuesList.Add(parameterValues[14].text);
                            valuesList.Add(parameterValues[15].text);
                            valuesList.Add(parameterValues[16].text);
                            break;
                    }
                    break;
                #endregion
                #region Placa controladora
                case ConstStrings.PlacaControladora:
                    valuesList.Clear();
                    valuesList.Add(parameterValues[7].text);
                    valuesList.Add(parameterValues[11].text);
                    valuesList.Add(parameterValues[12].text);
                    valuesList.Add(parameterValues[13].text);
                    valuesList.Add(parameterValues[14].text);
                    valuesList.Add(parameterValues[15].text);
                    valuesList.Add(parameterValues[16].text);
                    valuesList.Add(parameterValues[17].text);
                    valuesList.Add(parameterValues[18].text);
                    break;
                #endregion
                #region Placa de captura de vídeo
                case ConstStrings.PlacaDeCapturaDeVideo:
                    valuesList.Clear();
                    valuesList.Add(parameterValues[7].text);
                    valuesList.Add(parameterValues[11].text);
                    break;
                #endregion
                #region Placa de rede
                case ConstStrings.PlacaDeRede:
                    valuesList.Clear();
                    valuesList.Add(parameterValues[7].text);
                    valuesList.Add(parameterValues[6].text);
                    valuesList.Add(parameterValues[11].text);
                    valuesList.Add(parameterValues[12].text);
                    valuesList.Add(parameterValues[13].text);
                    valuesList.Add(parameterValues[14].text);
                    valuesList.Add(parameterValues[15].text);
                    break;
                #endregion
                #region Placa de som
                case ConstStrings.PlacaDeSom:
                    valuesList.Clear();
                    valuesList.Add(parameterValues[7].text);
                    valuesList.Add(parameterValues[11].text);
                    break;
                #endregion
                #region Placa de vídeo
                case ConstStrings.PlacaDeVideo:
                    valuesList.Clear();
                    valuesList.Add(parameterValues[7].text);
                    valuesList.Add(parameterValues[11].text);
                    valuesList.Add(parameterValues[12].text);
                    break;
                #endregion
                #region Placa SAS
                case ConstStrings.PlacaSAS:
                    valuesList.Clear();
                    valuesList.Add(parameterValues[7].text);
                    valuesList.Add(parameterValues[11].text);
                    valuesList.Add(parameterValues[12].text);
                    break;
                #endregion
                #region Processador
                case ConstStrings.Processador:
                    valuesList.Clear();
                    valuesList.Add(parameterValues[7].text);
                    valuesList.Add(parameterValues[11].text);
                    valuesList.Add(parameterValues[12].text);
                    valuesList.Add(parameterValues[13].text);
                    break;
                #endregion
                #region Roteador
                case ConstStrings.Roteador:
                    valuesList.Clear();
                    switch (InternalDatabase.Instance.currentEstoque)
                    {
                        case CurrentEstoque.Concert:
                            valuesList.Add(parameterValues[2].text);
                            valuesList.Add(parameterValues[7].text);
                            valuesList.Add(parameterValues[6].text);
                            valuesList.Add(parameterValues[12].text);
                            valuesList.Add(parameterValues[13].text);
                            break;
                        case CurrentEstoque.SnPro:
                            valuesList.Add(parameterValues[7].text);
                            valuesList.Add(parameterValues[11].text);
                            valuesList.Add(parameterValues[12].text);
                            valuesList.Add(parameterValues[13].text);
                            valuesList.Add(parameterValues[14].text);
                            break;
                        default:
                            valuesList.Add(parameterValues[2].text);
                            valuesList.Add(parameterValues[7].text);
                            valuesList.Add(parameterValues[6].text);
                            valuesList.Add(parameterValues[11].text);
                            valuesList.Add(parameterValues[12].text);
                            break;
                    }
                    break;
                #endregion
                #region Servidor
                case ConstStrings.Servidor:
                    valuesList.Clear();
                    switch (InternalDatabase.Instance.currentEstoque)
                    {
                        case CurrentEstoque.Concert:
                            valuesList.Add(parameterValues[2].text);
                            valuesList.Add(parameterValues[7].text);
                            valuesList.Add(parameterValues[6].text);
                            valuesList.Add(parameterValues[12].text);
                            valuesList.Add(parameterValues[13].text);
                            valuesList.Add(parameterValues[14].text);
                            valuesList.Add(parameterValues[15].text);
                            valuesList.Add(parameterValues[16].text);
                            valuesList.Add(parameterValues[17].text);
                            break;
                        case CurrentEstoque.SnPro:
                            valuesList.Add(parameterValues[2].text);
                            valuesList.Add(parameterValues[7].text);
                            valuesList.Add(parameterValues[6].text);
                            valuesList.Add(parameterValues[11].text);
                            valuesList.Add(parameterValues[12].text);
                            valuesList.Add(parameterValues[13].text);
                            valuesList.Add(parameterValues[14].text);
                            valuesList.Add(parameterValues[15].text);
                            valuesList.Add(parameterValues[16].text);
                            valuesList.Add(parameterValues[17].text);
                            valuesList.Add(parameterValues[18].text);
                            valuesList.Add(parameterValues[19].text);
                            valuesList.Add(parameterValues[20].text);
                            valuesList.Add(parameterValues[21].text);
                            valuesList.Add(parameterValues[22].text);
                            valuesList.Add(parameterValues[23].text);
                            valuesList.Add(parameterValues[24].text);
                            valuesList.Add(parameterValues[25].text);
                            valuesList.Add(parameterValues[26].text);
                            break;
                        default:
                            valuesList.Add(parameterValues[2].text);
                            valuesList.Add(parameterValues[7].text);
                            valuesList.Add(parameterValues[6].text);
                            valuesList.Add(parameterValues[11].text);
                            valuesList.Add(parameterValues[12].text);
                            valuesList.Add(parameterValues[13].text);
                            valuesList.Add(parameterValues[14].text);
                            valuesList.Add(parameterValues[15].text);
                            valuesList.Add(parameterValues[16].text);
                            break;
                    }
                    break;
                #endregion
                #region Storage NAS
                case ConstStrings.StorageNAS:
                    valuesList.Clear();
                    valuesList.Add(parameterValues[7].text);
                    valuesList.Add(parameterValues[11].text);
                    valuesList.Add(parameterValues[12].text);
                    valuesList.Add(parameterValues[13].text);
                    valuesList.Add(parameterValues[14].text);
                    valuesList.Add(parameterValues[15].text);
                    break;
                #endregion
                #region Switch
                case ConstStrings.Switch:
                    valuesList.Clear();
                    switch (InternalDatabase.Instance.currentEstoque)
                    {
                        case CurrentEstoque.Concert:
                            valuesList.Add(parameterValues[2].text);
                            valuesList.Add(parameterValues[7].text);
                            valuesList.Add(parameterValues[6].text);
                            valuesList.Add(parameterValues[12].text);
                            break;
                        case CurrentEstoque.SnPro:
                            valuesList.Add(parameterValues[7].text);
                            valuesList.Add(parameterValues[11].text);
                            valuesList.Add(parameterValues[12].text);
                            break;
                        default:
                            valuesList.Add(parameterValues[2].text);
                            valuesList.Add(parameterValues[7].text);
                            valuesList.Add(parameterValues[6].text);
                            valuesList.Add(parameterValues[11].text);
                            break;
                    }
                    break;
                #endregion
                default:
                    break;
            }
            return valuesList;
        }

        /// <summary>
        /// Force the TabInputHandler to get the active inputs, to prevent bugs where the active inputs are incorrectly set
        /// </summary>
        public void GetTabActiveInputs()
        {
            if (tabInputHandler != null)
            {
                tabInputHandler.GetActiveInputs();
            }
        }
    }
}