using TMPro;
using UnityEngine;

public class MovementUIController : MonoBehaviour
{
    [SerializeField] TMP_Text patrimonioName;
    [SerializeField] TMP_Text serialQuantity;
    [SerializeField] TMP_Text username;
    [SerializeField] TMP_Text date;
    [SerializeField] TMP_Text fromWhere;
    [SerializeField] TMP_Text toWhere;

    [SerializeField] TMP_Text firstText;
    [SerializeField] TMP_Text secondText;

    /// <summary>
    /// Used to set the information of an item that is going to be moved and have a "Patrimônio"
    /// </summary>
    public void SetMovementInfo(MovementRecords regularMovementToShow)
    {
        firstText.text = "Patrimônio";
        secondText.text = "Serial";
        patrimonioName.text = regularMovementToShow.item.Patrimonio.ToString();
        serialQuantity.text = regularMovementToShow.item.Serial;
        username.text = regularMovementToShow.username;
        date.text = regularMovementToShow.date;
        fromWhere.text = regularMovementToShow.fromWhere;
        toWhere.text = regularMovementToShow.toWhere;
    }

    /// <summary>
    /// Used to set the information of an item that is going to be moved and does not have a "Patrimônio"
    /// </summary>
    public void SetMovementInfo(NoPaNoSeMovementRecords noPaNoSeMovementToShow)
    {
        firstText.text = "Nome";
        secondText.text = "Quantidade movida";
        patrimonioName.text = noPaNoSeMovementToShow.itemName;
        serialQuantity.text = noPaNoSeMovementToShow.quantity;
        username.text = noPaNoSeMovementToShow.username;
        date.text = noPaNoSeMovementToShow.date;
        fromWhere.text = noPaNoSeMovementToShow.fromWhere;
        toWhere.text = noPaNoSeMovementToShow.toWhere;
    }
}
