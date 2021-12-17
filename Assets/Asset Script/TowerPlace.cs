using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlace : MonoBehaviour
{
    private void OnMouseUp()
    {
        Debug.Log("Tower clicked");

        BuildingManager bm = GameObject.FindObjectOfType<BuildingManager>();
        if (bm.selectedTower != null)
        {
            ScoreManager sm = GameObject.FindObjectOfType<ScoreManager>();
            if (sm.coin < bm.selectedTower.GetComponent<Tower>().cost)
            {
                Debug.Log("Duetmu kurang");
                return;
            }

            sm.coin -= bm.selectedTower.GetComponent<Tower>().cost;

            Instantiate(bm.selectedTower, transform.parent.position, transform.parent.rotation);
            Destroy(transform.parent.gameObject);
        }
    }
}
