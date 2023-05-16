using System;
using Core.Inventory_And_Item.Controllers;
using Core.Save_And_Load.Commons;
using Core.Save_And_Load.Interfaces;
using Newtonsoft.Json.Linq;
using QFramework;
using UnityEngine;

namespace Core
{
    public class PlayerExample : MonoBehaviour
    {
        private void Update()
        {
            float hor = Input.GetAxis("Horizontal");
            float ver = Input.GetAxis("Vertical");
            if (hor != 0 || ver != 0)
            {
                transform.Translate(hor * Time.deltaTime, ver * Time.deltaTime, 0);
            }
        }
        
    }
}