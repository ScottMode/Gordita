using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.Linq;

public class LevelBuilder : MonoBehaviour
{
	public GameObject tilePrefab, exitUiPrefab, playerStartGO;
	public Vector3 tileScale;

	public TextAsset file;
	public List<List<GridTile>> tiles = new List<List<GridTile>>();

	const char k_EMPTY_TILE = ' ';
	const char k_START = 's';
	const char k_EXIT = 'x';
	const char k_NORTH = 'N';
	const char k_SOUTH = 'S';
	const char k_EAST = 'E';
	const char k_WEST = 'W';

	public Player player;

	// Use this for initialization
	void Start ()
	{
		BuildLevel(file);
	}

	void BuildLevel(TextAsset levelFile)
	{
		List<string> rows = file.text.Split('\n').Select(p => p.TrimEnd('\r')).ToList();

		Vector3 tilePos = Vector3.zero;

		for(int z = 0; z < rows.Count; ++z)
		{
			for(int x = 0; x < rows[z].Length; ++x)
			{
				if(!rows[z][x].Equals(k_EMPTY_TILE))
				{
					GameObject cloneTile = (GameObject)Instantiate(tilePrefab, tilePos, Quaternion.identity, transform);

					cloneTile.transform.localScale = tileScale;

					cloneTile.GetComponent<GridTile>().SetWalls(GetNeighborsCoords(x, z, rows));

					#region PLayer Start
					if(rows[z][x].Equals(k_START))
					{
						playerStartGO.transform.position = tilePos;
                        CardinalCoordinates mask = cloneTile.GetComponent<GridTile>().neighborsMask;

                        if ((mask & CardinalCoordinates.North) != CardinalCoordinates.None)
                        {
                            playerStartGO.transform.Rotate(Vector3.up, 0f);
                        }
                        else if ((mask & CardinalCoordinates.South) != CardinalCoordinates.None)
                        {
							playerStartGO.transform.Rotate(Vector3.up, 180f);
						}
						else if ((mask & CardinalCoordinates.East) != CardinalCoordinates.None)
                        {
							playerStartGO.transform.Rotate(Vector3.up, 90f);
						}
						else if((mask & CardinalCoordinates.West) != CardinalCoordinates.None)
                        {
							playerStartGO.transform.Rotate(Vector3.up, 270f);
						}

						player.currentTile = cloneTile.GetComponent<GridTile>();
					}
					#endregion
					#region Exit
					else if(rows[z][x].Equals(k_EXIT))
					{
						GameObject cloneUI = (GameObject)Instantiate(exitUiPrefab, cloneTile.transform);

						cloneUI.transform.localPosition = Vector3.zero;
						cloneUI.transform.localScale = Vector3.one;

                        CardinalCoordinates mask = cloneTile.GetComponent<GridTile>().neighborsMask;

                        if ((mask & CardinalCoordinates.North) != CardinalCoordinates.None)
                        {
                            cloneUI.transform.Rotate(Vector3.up, 0f);
                        }
                        else if ((mask & CardinalCoordinates.South) != CardinalCoordinates.None)
                        {
                            cloneUI.transform.Rotate(Vector3.up, 180f);
                        }
                        else if ((mask & CardinalCoordinates.East) != CardinalCoordinates.None)
                        {
                            cloneUI.transform.Rotate(Vector3.up, 90f);
                        }
                        else if ((mask & CardinalCoordinates.West) != CardinalCoordinates.None)
                        {
                            cloneUI.transform.Rotate(Vector3.up, 270f);
                        }

                        cloneTile.GetComponent<GridTile>().Ui = cloneUI;
					}
					#endregion
				}

				tilePos.x += tileScale.x;
			}

			tilePos.x = 0;
			tilePos.z -= tileScale.z;
		}

		Reset();
	}

	public void Reset()
	{
		player.Setup(playerStartGO.transform.position, playerStartGO.transform.rotation);
	}

	CardinalCoordinates GetNeighborsCoords(int col, int row, List<string> rows)
	{
		CardinalCoordinates neighbors = 0;

		//	Check North
		if(row > 0 && !rows[row - 1][col].Equals(k_EMPTY_TILE))
		{
			neighbors = neighbors | CardinalCoordinates.North;
		}

		//	Check South
		if(row < rows.Count - 1 && !rows[row + 1][col].Equals(k_EMPTY_TILE))
		{
			neighbors = neighbors | CardinalCoordinates.South;
		}

		//	Check East
		if(col < rows[row].Length - 1 && !rows[row][col + 1].Equals(k_EMPTY_TILE))
		{
			neighbors = neighbors | CardinalCoordinates.East;
		}

		//	Check West
		if(col > 0 && !rows[row][col - 1].Equals(k_EMPTY_TILE))
		{
			neighbors = neighbors | CardinalCoordinates.West;
		}

		return neighbors;
	}
}
