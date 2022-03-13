using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] Board board;
    List<Tile> selectedTiles = new List<Tile>();

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Tile")
                {
                    if (selectedTiles != null || selectedTiles.Count > 0)
                    {
                        foreach (Tile auxTile in selectedTiles)
                        {
                            auxTile.SetSelectedColor(false);
                        }
                        selectedTiles.Clear();
                    }
                    selectedTiles = board.Search(hit.transform.GetComponent<Tile>(), 2);
                }
            }
        }
    }
}
