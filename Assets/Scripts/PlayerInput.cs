using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BtnClick_Attack()
    {
        playerController.Attack();
    }

    public void BtnClick_Block()
    {
        playerController.Block();
    }
}
