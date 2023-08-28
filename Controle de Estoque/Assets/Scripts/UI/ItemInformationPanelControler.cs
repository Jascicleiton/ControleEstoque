using System;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine;
using System.Linq;
using Assets.Scripts.Inventory.Database;
using Assets.Scripts.Inventory;
using Assets.Scripts.Misc;

namespace Assets.Scripts.UI
{
    public class ItemInformationPanelControler : MonoBehaviour, IItemInformationPanelControler
    {
        private VisualElement[] _itemBoxes;
        private List<Label> _parameterNames;
        /// <summary>
        /// Patrimônio = 2, Fabricante = 6, Modelo = 7
        /// </summary>
        private List<TextField> _parameterValues;

        private int _numberOfActiveBoxes = 0;

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
            VisualElement root = GetComponent<UIDocument>().rootVisualElement;
            _itemBoxes = root.Q<VisualElement>("ParametersContainer").Children().ToArray<VisualElement>();
            _parameterNames = root.Query(name: "ParametersContainer").Descendents<Label>().ToList();
            _parameterValues = root.Query(name: "ParametersContainer").Descendents<TextField>().ToList();

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
            foreach (var item in _parameterValues)
            {
                item.isReadOnly = inputEnabled;
            }
        }

        /// <summary>
        /// Hide all item boxes that have a empty name
        /// </summary>
        private void HideEmptyItemBox()
        {
            for (int i = 0; i < _parameterNames.Count; i++)
            {
                if (_parameterNames[i] != null && _parameterNames[i].text == "")
                {
                    _itemBoxes[i].style.display = DisplayStyle.None;
                }
            }
        }

        /// <summary>
        /// Activate all item boxes
        /// </summary>
        private void ActivateAllItemBoxes()
        {
            for (int i = 0; i < _itemBoxes.Length; i++)
            {
                _itemBoxes[i].style.display = DisplayStyle.Flex;
                if (_parameterValues.Count > 0)
                {
                    _parameterValues[i].isReadOnly = false;
                }
            }
        }

        /// <summary>
        /// Fill the names of all item boxes that should get a name
        /// </summary>
        private void FillNames(List<string> names)
        {
            for (int i = 0; i < _parameterNames.Count; i++)
            {
                if (i < names.Count)
                {
                    _parameterNames[i].text = names[i];
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
            for (int i = 0; i < _parameterValues.Count; i++)
            {
                if (i < values.Count)
                {
                    _parameterValues[i].value = values[i];
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
            for (int i = 0; i < _parameterValues.Count; i++)
            {
                if (i < values.Count)
                {
                    _parameterValues[i].value = values[i];
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
            for (int i = 0; i < _parameterValues.Count; i++)
            {
                if (i < values.Count)
                {
                    _parameterValues[i].value = values[i] + "...";
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
            _numberOfActiveBoxes = 0;
            for (int i = 0; i < _itemBoxes.Length; i++)
            {
                if (_itemBoxes[i].style.display == DisplayStyle.Flex)
                {
                    _numberOfActiveBoxes++;
                }
            }
            return _numberOfActiveBoxes;
        }

        /// <summary>
        /// Reset the values of all item boxes to empty
        /// </summary>
        public void ResetValues()
        {
            if (_parameterValues != null && _parameterValues.Count > 0)
            {
                for (int i = 0; i < _parameterValues.Count; i++)
                {
                    if (_parameterValues[i] != null)
                    {
                        _parameterValues[i].value = "";
                        _parameterValues[i].isReadOnly = false;
                    }
                }
            }
            if (_parameterValues != null && _parameterValues.Count > 0)
            {
                for (int i = 0; i < _parameterValues.Count; i++)
                {
                    if (_parameterValues[i] != null)
                    {
                        _parameterValues[i].value = "";
                    }
                }
            }
        }

        /// <summary>
        /// Reset the names of all item boxes to ""
        /// </summary>
        private void ResetNames()
        {
            for (int i = 0; i < _parameterNames.Count; i++)
            {
                if (_parameterNames[i] != null)
                {
                    _parameterNames[i].text = "";
                }
            }
        }

        /// <summary>
        /// Resets the names and values of all item boxes
        /// </summary>
        public void ResetItems()
        {
            for (int i = 0; i < _itemBoxes.Length; i++)
            {
                _itemBoxes[i].style.display = DisplayStyle.Flex;
            }
            ResetNames();
            ResetValues();
            for (int i = 0; i < _itemBoxes.Length; i++)
            {
                _itemBoxes[i].style.display = DisplayStyle.None;
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
            _parameterValues[0].value = DateTime.Now.ToString("dd/MM/yyyy");
            _parameterValues[1].value = DateTime.Now.ToString("dd/MM/yyyy");
            _parameterValues[8].value = "Estoque";
            HideEmptyItemBox();
        }

        /// <summary>
        /// Disable the input of certain item boxes  based on the category of the item
        /// </summary>
        public void DisableItemsForAdd(string category)
        {
            if (category == ConstStrings.C_Outros)
            {
                _itemBoxes[5].style.display = DisplayStyle.Flex;
            }
            else
            {
                _itemBoxes[5].style.display = DisplayStyle.None;
            }
            switch (InternalDatabase.Instance.currentEstoque)
            {
                case CurrentEstoque.Concert:
                    _itemBoxes[11].style.display = DisplayStyle.None;
                    break;
                default:
                    _itemBoxes[9].style.display = DisplayStyle.None;
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
            //   int index = 0;
            foreach (var item in _parameterValues)
            {
                if (item != null)
                {
                    valuesList.Add(item.text);
                    //         print(index + ": " + valuesList[index]);
                    //       index++;
                }

            }
            return valuesList;
        }

        /// <summary>
        /// Get all the values that are specific to each category that are/were set on the input boxes
        /// </summary>
        public List<string> GetCategoryValues(string category)
        {
            List<string> valuesList = new List<string>();
            switch (category)
            {
                #region Adaptador AC
                case ConstStrings.C_AdaptadorAC:
                    valuesList.Clear();
                    switch (InternalDatabase.Instance.currentEstoque)
                    {
                        case CurrentEstoque.SnPro:
                        case CurrentEstoque.Testing:
                            valuesList.Add(_parameterValues[7].text);
                            valuesList.Add(_parameterValues[11].text);
                            valuesList.Add(_parameterValues[12].text);
                            valuesList.Add(_parameterValues[13].text);
                            break;
                        case CurrentEstoque.Fumsoft:
                        case CurrentEstoque.ESF:
                        case CurrentEstoque.Clientes:
                            valuesList.Add(_parameterValues[2].text);
                            valuesList.Add(_parameterValues[7].text);
                            valuesList.Add(_parameterValues[11].text);
                            valuesList.Add(_parameterValues[12].text);
                            break;
                        case CurrentEstoque.Concert:
                            valuesList.Add(_parameterValues[2].text);
                            valuesList.Add(_parameterValues[7].text);
                            valuesList.Add(_parameterValues[12].text);
                            valuesList.Add(_parameterValues[13].text);
                            break;
                        default:
                            break;
                    }
                    break;
                #endregion
                #region Carregador
                case ConstStrings.C_Carregador:
                    valuesList.Clear();
                    switch (InternalDatabase.Instance.currentEstoque)
                    {
                        case CurrentEstoque.Fumsoft:
                        case CurrentEstoque.ESF:
                        case CurrentEstoque.Clientes:
                            valuesList.Add(_parameterValues[2].text);
                            valuesList.Add(_parameterValues[7].text);
                            valuesList.Add(_parameterValues[11].text);
                            valuesList.Add(_parameterValues[12].text);
                            break;
                        case CurrentEstoque.Concert:
                            valuesList.Add(_parameterValues[2].text);
                            valuesList.Add(_parameterValues[7].text);
                            valuesList.Add(_parameterValues[12].text);
                            valuesList.Add(_parameterValues[13].text);
                            break;
                        default:
                            valuesList.Add(_parameterValues[7].text);
                            valuesList.Add(_parameterValues[11].text);
                            valuesList.Add(_parameterValues[12].text);
                            valuesList.Add(_parameterValues[13].text);
                            break;
                    }
                    break;
                #endregion
                #region Desktop
                case ConstStrings.C_Desktop:
                    valuesList.Clear();
                    switch (InternalDatabase.Instance.currentEstoque)
                    {
                        case CurrentEstoque.Concert:
                            valuesList.Add(_parameterValues[2].text);
                            valuesList.Add(_parameterValues[7].text);
                            valuesList.Add(_parameterValues[6].text);
                            valuesList.Add(_parameterValues[12].text);
                            valuesList.Add(_parameterValues[13].text);
                            valuesList.Add(_parameterValues[14].text);
                            valuesList.Add(_parameterValues[15].text);
                            valuesList.Add(_parameterValues[16].text);
                            valuesList.Add(_parameterValues[17].text);
                            break;
                        case CurrentEstoque.Fumsoft:
                        case CurrentEstoque.ESF:
                            valuesList.Add(_parameterValues[2].text);
                            valuesList.Add(_parameterValues[7].text);
                            valuesList.Add(_parameterValues[6].text);
                            valuesList.Add(_parameterValues[11].text);
                            valuesList.Add(_parameterValues[12].text);
                            valuesList.Add(_parameterValues[13].text);
                            valuesList.Add(_parameterValues[14].text);
                            valuesList.Add(_parameterValues[15].text);
                            valuesList.Add(_parameterValues[16].text);
                            break;
                        default:
                            valuesList.Add(_parameterValues[2].text);
                            valuesList.Add(_parameterValues[11].text);
                            valuesList.Add(_parameterValues[12].text);
                            valuesList.Add(_parameterValues[13].text);
                            valuesList.Add(_parameterValues[14].text);
                            valuesList.Add(_parameterValues[15].text);
                            valuesList.Add(_parameterValues[16].text);
                            valuesList.Add(_parameterValues[17].text);
                            valuesList.Add(_parameterValues[18].text);
                            break;
                    }
                    break;
                #endregion
                #region Fonte
                case ConstStrings.C_Fonte:
                    valuesList.Clear();
                    switch (InternalDatabase.Instance.currentEstoque)
                    {
                        case CurrentEstoque.Concert:
                            valuesList.Add(_parameterValues[7].text);
                            valuesList.Add(_parameterValues[12].text);
                            valuesList.Add(_parameterValues[13].text);
                            valuesList.Add(_parameterValues[14].text);
                            break;
                        default:
                            valuesList.Add(_parameterValues[7].text);
                            valuesList.Add(_parameterValues[11].text);
                            valuesList.Add(_parameterValues[12].text);
                            valuesList.Add(_parameterValues[13].text);
                            break;
                    }
                    break;
                #endregion
                #region GBIC
                case ConstStrings.C_Gbic:
                    valuesList.Clear();
                    switch (InternalDatabase.Instance.currentEstoque)
                    {
                        case CurrentEstoque.Concert:
                            valuesList.Add(_parameterValues[7].text);
                            valuesList.Add(_parameterValues[6].text);
                            valuesList.Add(_parameterValues[12].text);
                            break;
                        default:
                            valuesList.Add(_parameterValues[7].text);
                            valuesList.Add(_parameterValues[6].text);
                            valuesList.Add(_parameterValues[11].text);
                            break;
                    }
                    break;
                #endregion
                #region HD
                case ConstStrings.C_HD:
                    valuesList.Clear();
                    valuesList.Add(_parameterValues[7].text);
                    valuesList.Add(_parameterValues[6].text);
                    valuesList.Add(_parameterValues[11].text);
                    valuesList.Add(_parameterValues[12].text);
                    valuesList.Add(_parameterValues[13].text);
                    valuesList.Add(_parameterValues[14].text);
                    valuesList.Add(_parameterValues[15].text);
                    valuesList.Add(_parameterValues[16].text);
                    valuesList.Add(_parameterValues[17].text);
                    break;
                #endregion
                #region iDRAC
                case ConstStrings.C_Idrac:
                    valuesList.Clear();
                    valuesList.Add(_parameterValues[7].text);
                    valuesList.Add(_parameterValues[6].text);
                    valuesList.Add(_parameterValues[11].text);
                    valuesList.Add(_parameterValues[12].text);
                    valuesList.Add(_parameterValues[13].text);
                    valuesList.Add(_parameterValues[14].text);
                    break;
                #endregion
                #region Memória
                case ConstStrings.C_Memoria:
                    valuesList.Clear();
                    valuesList.Add(_parameterValues[7].text);
                    valuesList.Add(_parameterValues[6].text);
                    valuesList.Add(_parameterValues[11].text);
                    valuesList.Add(_parameterValues[12].text);
                    valuesList.Add(_parameterValues[13].text);
                    valuesList.Add(_parameterValues[14].text);
                    valuesList.Add(_parameterValues[15].text);
                    valuesList.Add(_parameterValues[16].text);
                    valuesList.Add(_parameterValues[17].text);
                    valuesList.Add(_parameterValues[18].text);
                    break;
                #endregion
                #region Monitor
                case ConstStrings.C_Monitor:
                    valuesList.Clear();
                    switch (InternalDatabase.Instance.currentEstoque)
                    {
                        case CurrentEstoque.Concert:
                            valuesList.Add(_parameterValues[2].text);
                            valuesList.Add(_parameterValues[7].text);
                            valuesList.Add(_parameterValues[6].text);
                            valuesList.Add(_parameterValues[12].text);
                            valuesList.Add(_parameterValues[13].text);
                            break;
                        case CurrentEstoque.SnPro:
                        case CurrentEstoque.Testing:
                            valuesList.Add(_parameterValues[7].text);
                            valuesList.Add(_parameterValues[6].text);
                            valuesList.Add(_parameterValues[11].text);
                            valuesList.Add(_parameterValues[12].text);
                            break;
                        default:
                            valuesList.Add(_parameterValues[2].text);
                            valuesList.Add(_parameterValues[7].text);
                            valuesList.Add(_parameterValues[6].text);
                            valuesList.Add(_parameterValues[11].text);
                            valuesList.Add(_parameterValues[12].text);
                            break;
                    }
                    break;
                #endregion
                #region Notebook
                case ConstStrings.C_Notebook:
                    valuesList.Clear();
                    switch (InternalDatabase.Instance.currentEstoque)
                    {
                        case CurrentEstoque.Concert:
                            valuesList.Add(_parameterValues[2].text);
                            valuesList.Add(_parameterValues[7].text);
                            valuesList.Add(_parameterValues[6].text);
                            valuesList.Add(_parameterValues[12].text);
                            valuesList.Add(_parameterValues[13].text);
                            valuesList.Add(_parameterValues[14].text);
                            valuesList.Add(_parameterValues[15].text);
                            valuesList.Add(_parameterValues[16].text);
                            valuesList.Add(_parameterValues[17].text);
                            break;
                        default:
                            valuesList.Add(_parameterValues[2].text);
                            valuesList.Add(_parameterValues[7].text);
                            valuesList.Add(_parameterValues[6].text);
                            valuesList.Add(_parameterValues[11].text);
                            valuesList.Add(_parameterValues[12].text);
                            valuesList.Add(_parameterValues[13].text);
                            valuesList.Add(_parameterValues[14].text);
                            valuesList.Add(_parameterValues[15].text);
                            valuesList.Add(_parameterValues[16].text);
                            break;
                    }
                    break;
                #endregion
                #region Placa controladora
                case ConstStrings.C_PlacaControladora:
                    valuesList.Clear();
                    valuesList.Add(_parameterValues[7].text);
                    valuesList.Add(_parameterValues[11].text);
                    valuesList.Add(_parameterValues[12].text);
                    valuesList.Add(_parameterValues[13].text);
                    valuesList.Add(_parameterValues[14].text);
                    valuesList.Add(_parameterValues[15].text);
                    valuesList.Add(_parameterValues[16].text);
                    valuesList.Add(_parameterValues[17].text);
                    valuesList.Add(_parameterValues[18].text);
                    break;
                #endregion
                #region Placa de captura de vídeo
                case ConstStrings.C_PlacaDeCapturaDeVideo:
                    valuesList.Clear();
                    valuesList.Add(_parameterValues[7].text);
                    valuesList.Add(_parameterValues[11].text);
                    break;
                #endregion
                #region Placa de rede
                case ConstStrings.C_PlacaDeRede:
                    valuesList.Clear();
                    valuesList.Add(_parameterValues[7].text);
                    valuesList.Add(_parameterValues[6].text);
                    valuesList.Add(_parameterValues[11].text);
                    valuesList.Add(_parameterValues[12].text);
                    valuesList.Add(_parameterValues[13].text);
                    valuesList.Add(_parameterValues[14].text);
                    valuesList.Add(_parameterValues[15].text);
                    break;
                #endregion
                #region Placa de som
                case ConstStrings.C_PlacaDeSom:
                    valuesList.Clear();
                    valuesList.Add(_parameterValues[7].text);
                    valuesList.Add(_parameterValues[11].text);
                    break;
                #endregion
                #region Placa de vídeo
                case ConstStrings.C_PlacaDeVideo:
                    valuesList.Clear();
                    valuesList.Add(_parameterValues[7].text);
                    valuesList.Add(_parameterValues[11].text);
                    valuesList.Add(_parameterValues[12].text);
                    break;
                #endregion
                #region Placa SAS
                case ConstStrings.C_PlacaSAS:
                    valuesList.Clear();
                    valuesList.Add(_parameterValues[7].text);
                    valuesList.Add(_parameterValues[11].text);
                    valuesList.Add(_parameterValues[12].text);
                    break;
                #endregion
                #region Processador
                case ConstStrings.C_Processador:
                    valuesList.Clear();
                    valuesList.Add(_parameterValues[7].text);
                    valuesList.Add(_parameterValues[11].text);
                    valuesList.Add(_parameterValues[12].text);
                    valuesList.Add(_parameterValues[13].text);
                    break;
                #endregion
                #region Roteador
                case ConstStrings.C_Roteador:
                    valuesList.Clear();
                    switch (InternalDatabase.Instance.currentEstoque)
                    {
                        case CurrentEstoque.Concert:
                            valuesList.Add(_parameterValues[2].text);
                            valuesList.Add(_parameterValues[7].text);
                            valuesList.Add(_parameterValues[6].text);
                            valuesList.Add(_parameterValues[12].text);
                            valuesList.Add(_parameterValues[13].text);
                            break;
                        case CurrentEstoque.SnPro:
                            valuesList.Add(_parameterValues[7].text);
                            valuesList.Add(_parameterValues[11].text);
                            valuesList.Add(_parameterValues[12].text);
                            valuesList.Add(_parameterValues[13].text);
                            valuesList.Add(_parameterValues[14].text);
                            break;
                        default:
                            valuesList.Add(_parameterValues[2].text);
                            valuesList.Add(_parameterValues[7].text);
                            valuesList.Add(_parameterValues[6].text);
                            valuesList.Add(_parameterValues[11].text);
                            valuesList.Add(_parameterValues[12].text);
                            break;
                    }
                    break;
                #endregion
                #region Servidor
                case ConstStrings.C_Servidor:
                    valuesList.Clear();
                    switch (InternalDatabase.Instance.currentEstoque)
                    {
                        case CurrentEstoque.Concert:
                            valuesList.Add(_parameterValues[2].text);
                            valuesList.Add(_parameterValues[7].text);
                            valuesList.Add(_parameterValues[6].text);
                            valuesList.Add(_parameterValues[12].text);
                            valuesList.Add(_parameterValues[13].text);
                            valuesList.Add(_parameterValues[14].text);
                            valuesList.Add(_parameterValues[15].text);
                            valuesList.Add(_parameterValues[16].text);
                            valuesList.Add(_parameterValues[17].text);
                            break;
                        case CurrentEstoque.SnPro:
                            valuesList.Add(_parameterValues[2].text);
                            valuesList.Add(_parameterValues[7].text);
                            valuesList.Add(_parameterValues[6].text);
                            valuesList.Add(_parameterValues[11].text);
                            valuesList.Add(_parameterValues[12].text);
                            valuesList.Add(_parameterValues[13].text);
                            valuesList.Add(_parameterValues[14].text);
                            valuesList.Add(_parameterValues[15].text);
                            valuesList.Add(_parameterValues[16].text);
                            valuesList.Add(_parameterValues[17].text);
                            valuesList.Add(_parameterValues[18].text);
                            valuesList.Add(_parameterValues[19].text);
                            valuesList.Add(_parameterValues[20].text);
                            valuesList.Add(_parameterValues[21].text);
                            valuesList.Add(_parameterValues[22].text);
                            valuesList.Add(_parameterValues[23].text);
                            valuesList.Add(_parameterValues[24].text);
                            valuesList.Add(_parameterValues[25].text);
                            valuesList.Add(_parameterValues[26].text);
                            break;
                        default:
                            valuesList.Add(_parameterValues[2].text);
                            valuesList.Add(_parameterValues[7].text);
                            valuesList.Add(_parameterValues[6].text);
                            valuesList.Add(_parameterValues[11].text);
                            valuesList.Add(_parameterValues[12].text);
                            valuesList.Add(_parameterValues[13].text);
                            valuesList.Add(_parameterValues[14].text);
                            valuesList.Add(_parameterValues[15].text);
                            valuesList.Add(_parameterValues[16].text);
                            break;
                    }
                    break;
                #endregion
                #region Storage NAS
                case ConstStrings.C_StorageNAS:
                    valuesList.Clear();
                    valuesList.Add(_parameterValues[7].text);
                    valuesList.Add(_parameterValues[11].text);
                    valuesList.Add(_parameterValues[12].text);
                    valuesList.Add(_parameterValues[13].text);
                    valuesList.Add(_parameterValues[14].text);
                    valuesList.Add(_parameterValues[15].text);
                    break;
                #endregion
                #region Switch
                case ConstStrings.C_Switch:
                    valuesList.Clear();
                    switch (InternalDatabase.Instance.currentEstoque)
                    {
                        case CurrentEstoque.Concert:
                            valuesList.Add(_parameterValues[2].text);
                            valuesList.Add(_parameterValues[7].text);
                            valuesList.Add(_parameterValues[6].text);
                            valuesList.Add(_parameterValues[12].text);
                            break;
                        case CurrentEstoque.SnPro:
                            valuesList.Add(_parameterValues[7].text);
                            valuesList.Add(_parameterValues[11].text);
                            valuesList.Add(_parameterValues[12].text);
                            break;
                        default:
                            valuesList.Add(_parameterValues[2].text);
                            valuesList.Add(_parameterValues[7].text);
                            valuesList.Add(_parameterValues[6].text);
                            valuesList.Add(_parameterValues[11].text);
                            break;
                    }
                    break;
                #endregion
                default:
                    break;
            }
            return valuesList;
        }
    }
}