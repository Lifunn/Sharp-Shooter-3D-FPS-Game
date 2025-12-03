using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.UI;
public class PlayerSetup : MonoBehaviourPun

{
    public TextMeshProUGUI playerNameText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (photonView.IsMine)
        {
            transform.GetComponent<MovementController>().enabled = true;
            transform.GetComponent<MovementController>().joystick.gameObject.SetActive(true);
        }
        else
        {
            transform.GetComponent<MovementController>().enabled = false;
            transform.GetComponent<MovementController>().joystick.gameObject.SetActive(false);
        }
        SetPlayerName();
    }

    void SetPlayerName()
    {
        if(playerNameText!=null)
        {
            if (photonView.IsMine)
            {
                playerNameText.text = "You";
                playerNameText.color = Color.green;
            }
            else
            {
                playerNameText.text = photonView.Owner.NickName;
            }
            playerNameText.text = photonView.Owner.NickName;
        }
    }
}
