using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] Board board;
    List<Tile> selectedTiles = new List<Tile>();
    Unit unit;
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
                        foreach (Tile auxTile in selectedTiles)
                        {
                            auxTile.SetSelectedColor(false);
                        }
                        selectedTiles.Clear();
                    }
                    unit = hit.transform.root.GetComponent<Unit>();
                    selectedTiles = board.Search(unit.placementTile, unit.maxMoveDistance);
                }

                if (hit.transform.tag == "Tile")
                {
                    if ((selectedTiles != null || selectedTiles.Count > 0) && unit != null)
                    {
                        unit.transform.position = hit.transform.GetComponent<Tile>().CenterPos;
                      
                        unit.placementTile = hit.transform.GetComponent<Tile>();
                        unit = null;
                        foreach (Tile auxTile in selectedTiles)
                        {
                            auxTile.SetSelectedColor(false);
                        }
                        selectedTiles.Clear();
                    }

                }
            }
        }
    }
}
