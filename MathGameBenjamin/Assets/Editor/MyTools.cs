using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MyTools : MonoBehaviour
{
    //[SerializeField]GameObject coinPrefab;

    [MenuItem("GameObject/My Custom Asset")]
    public static void Create()
    {
        var coinPrefab = Resources.Load("Coin");
        coinPickup[] result = FindObjectsOfType<coinPickup>();
        List<float> xList = new List<float>();
        List<float> yList = new List<float>();

        foreach (coinPickup c in result)
        {
            xList.Add(c.transform.position.x);
            yList.Add(c.transform.position.y);
            DestroyImmediate(c.gameObject);
        }


        for (int i = 0; i < xList.Count; i++)
        {
            var newCoinPrefab = new SerializedObject(coinPrefab);
            GameObject newObj = (GameObject)Instantiate(coinPrefab, new Vector2(xList[i], yList[i]), Quaternion.identity);
            Undo.RecordObject(newObj.transform, "My Custom Asset");
            newObj.transform.parent = GameObject.Find("Pickups").transform;
            newObj.name = "Coin" + i.ToString();
            PrefabUtility.RecordPrefabInstancePropertyModifications(newObj.transform);
        }
        
    }
}
