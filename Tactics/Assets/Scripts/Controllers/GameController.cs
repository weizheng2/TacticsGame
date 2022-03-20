using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    List<Tile> selectedTiles = new List<Tile>();
    Unit unit;
    LTDescr moveTween;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Unit")
                {
                    if (selectedTiles != null || selectedTiles.Count > 0)
                    {
                        Board.GetInstance().ToggleSelectedTiles(selectedTiles, false);
                        selectedTiles.Clear();
                    }
                    unit = hit.transform.parent.parent.GetComponent<Unit>();
                    selectedTiles = unit.movement.GetTilesInRange();
                    Board.GetInstance().ToggleSelectedTiles(selectedTiles, true);
                }

                if (hit.transform.tag == "Tile")
                {
                    if ((selectedTiles != null || selectedTiles.Count > 0) && unit != null)
                    {
                        //unit.transform.localPosition = hit.transform.GetComponent<Tile>().CenterPos;
                        unit.SetPlace(hit.transform.GetComponent<Tile>());
                   
                        StartCoroutine(unit.movement.Move(hit.transform.GetComponent<Tile>()));
                        unit = null;
                        Board.GetInstance().ToggleSelectedTiles(selectedTiles, false);

                        selectedTiles.Clear();

            
                    }

                }
            }
        }
    }
}
