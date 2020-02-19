using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TreasureHunter : MonoBehaviour
{
    
    public GameObject leftPointerObject;
    public GameObject rightPointerObject;
    public collectible[] collectiblesInScene;
    public TreasureHunterInventory inventory;
    public int score = 0;
    public TextMesh scoreSummary;
    public TextMesh itemSummary;
    public GameObject playerCamera;
    public RaycastHit outHit;
    public collectible collectedItem;
    public int numOfItems = 0;


    // Update is called once per frame
    void Update()
    {
        int layerMask = 1 << 10;
        layerMask = ~layerMask;

        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.TransformDirection(Vector3.forward), out outHit, 100.0f, layerMask))
            {              
                if (Input.GetKeyDown("e")) 
                {                  
                    Debug.Log("Pressed e");  
                    collectedItem = outHit.collider.gameObject.GetComponent<collectible>();
                    if ((collectedItem.name == "Cube")||(collectedItem.name == "Cylinder")||(collectedItem.name == "Capsule")) 
                    {   
                        GameObject objSelected = (GameObject)Resources.Load(GetGameObjectPath(collectedItem), typeof(GameObject));
                        collectible currentCollectible = objSelected.gameObject.GetComponent<collectible>();
                        if (inventory.itemsCollected.ContainsKey(currentCollectible)) 
                        {
                            inventory.itemsCollected[currentCollectible]++;
                        } 
                        else 
                        {
                            inventory.itemsCollected.Add(currentCollectible, 1);
                        }
                        score = score + currentCollectible.points;
                        numOfItems++;
                    }                        
                    Destroy(outHit.collider.gameObject); 
                    }                  
            }      
            scoreSummary.text = "Your Score: " + score;         
            itemSummary.text = "Number of Items: " + numOfItems;          
    }
    public static string GetGameObjectPath(collectible obj)
    {
        string path = obj.name;
        return path;
    }

    
}
