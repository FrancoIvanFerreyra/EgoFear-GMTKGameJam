using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    bool _playerIsOut = false, _egoIsOut = false;
    bool playerIsOut
    {
        get
        {
            return _playerIsOut;
        }
        set
        {
            if (_playerIsOut != value)
            {
                _playerIsOut = value;
                //If both entities passed the trigger, its a win
                if (playerIsOut && egoIsOut)
                {
                    GameManager.instance.Win();
                }
            }
        }
    }

    bool egoIsOut
    {
        get
        {
            return _egoIsOut;
        }
        set
        {
            if (_egoIsOut != value)
            {
                _egoIsOut = value;
                //If both entities passed the trigger, its a win
                if (playerIsOut && egoIsOut)
                {
                    GameManager.instance.Win();
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
           playerIsOut = collision.transform.position.x > transform.position.x;
        }
        else if (collision.gameObject.tag == "Ego")
        {
           egoIsOut = collision.transform.position.x > transform.position.x;
        }
    }
}
