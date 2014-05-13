using UnityEngine;
using System.Collections;

public class LevelGenerator : MonoBehaviour {
	private float currentWidth;
	private float currentHeight;

	private bool backgroundGenerated = false;
	private bool platformGenerated = false;
	private bool roomDesignsGenerated = false;

	public GameObject background;
	public GameObject platformTile;
	public GameObject torch;

	// Use this for initialization
	void Start () {
		currentWidth = 0f;
		background = (GameObject)Resources.Load("prefabs/tiles/green_background_tile", typeof(GameObject));
		platformTile = (GameObject)Resources.Load("prefabs/tiles/brown_platform_tile", typeof(GameObject));
		torch = (GameObject)Resources.Load("prefabs/tiles/torch", typeof(GameObject));
	}
	
	// Update is called once per frame
	void Update () {
		//generate background
		if(!backgroundGenerated) {
			for(float x = 0; x <= 10f; x+=0.32f) {
				for(float y = 0; y <= 10f; y+=0.32f) {
					Instantiate(background, new Vector2(x, y), Quaternion.identity);
				}
			}
			backgroundGenerated = true;
		}

		//generate platform
		/*if(!platformGenerated) {
			Debug.Log(platformGenerated);
			for(float x = 0; x <= 10f; x+=0.32f) {
				GameObject tile = (GameObject)Instantiate(platformTile, new Vector2(x, 0f), Quaternion.identity);
				tile.GetComponent<SpriteRenderer>().sortingOrder = 1;
			}
			platformGenerated = true;
		}*/

		//decide prettifier per room-- can be torches, chandeliers, etc
		if(!roomDesignsGenerated) {

		}
	}
}
