using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HexGridGenerator : MonoBehaviour
{
    [SerializeField]
    GameObject PrefebObject, ParentObject,catPrefeb, GameOverPanel;

    [SerializeField]
    List<GameObject> AllSpawnObject, EvenObject, OddObject;

    [SerializeField]
    List<int> AllMyMove, AllCatMove;

    [SerializeField]
    Color catColor;

    int middle;
    public static HexGridGenerator instance;

    int count = 0;
    bool flag, catOnBorder, isGameOver;

    GameObject gg;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < 11; i++)
        {
            for (int j = 0; j < 11; j++)
            {
                GameObject g = Instantiate(PrefebObject, ParentObject.transform);
                if (i == 0 || i == 10 || j == 0 || j == 10)
                {
                    g.gameObject.tag = "border";
                }
                if (!flag)
                {
                    g.transform.position = new Vector3((j * 0.9f) + 0.4f, i * 0.85f, 0);
                    OddObject.Add(g);
                }
                else
                {
                    g.transform.position = new Vector3(j * 0.9f, i * 0.85f, 0);
                    EvenObject.Add(g);
                }
                g.gameObject.name = count.ToString();
                g.GetComponent<SpriteRenderer>().color = Color.green;
                count++;
                AllSpawnObject.Add(g);
            }
            flag = !flag;
        }
        ParentObject.transform.position = new Vector3(-0.36f, -1.08f, 0);
        middle = AllSpawnObject.Count / 2;
      gg =  Instantiate(catPrefeb, AllSpawnObject[middle].transform);
       // AllSpawnObject[middle].gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        CatPos();
    }

    void CatPos()
    {
        AllCatMove.Clear();
        if (EvenObject.Contains(AllSpawnObject[middle]))
        {
            if (!AllMyMove.Contains(middle + 1))
                AllCatMove.Add(middle + 1);
            if (!AllMyMove.Contains(middle - 1))
                AllCatMove.Add(middle - 1);
            if (!AllMyMove.Contains(middle - 11))
                AllCatMove.Add(middle - 11);
            if (!AllMyMove.Contains(middle + 11))
                AllCatMove.Add(middle + 11);
            if (!AllMyMove.Contains(middle + 10))
                AllCatMove.Add(middle + 10);
            if (!AllMyMove.Contains(middle - 12))
                AllCatMove.Add(middle - 12);
        }
        else
        {
            if (!AllMyMove.Contains(middle + 1))
                AllCatMove.Add(middle + 1);
            if (!AllMyMove.Contains(middle - 1))
                AllCatMove.Add(middle - 1);
            if (!AllMyMove.Contains(middle - 10))
                AllCatMove.Add(middle - 10);
            if (!AllMyMove.Contains(middle + 11))
                AllCatMove.Add(middle + 11);
            if (!AllMyMove.Contains(middle + 12))
                AllCatMove.Add(middle + 12);
            if (!AllMyMove.Contains(middle - 11))
                AllCatMove.Add(middle - 11);
        }
    }

    public void MyMove(GameObject PassObject)
    {
        int val = int.Parse(PassObject.name);
        AllMyMove.Add(val);
        PassObject.gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
 
            CatPos();
            CatWalk();
    }

    void CatWalk()
    {

        if(AllCatMove.Count == 0)
        {
            GameOver();
        }
        int val = Random.Range(0, AllCatMove.Count);// 0-5
        AllSpawnObject[middle].gameObject.GetComponent<SpriteRenderer>().color = catColor;
        //AllSpawnObject[AllCatMove[val]].gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        gg.gameObject.transform.parent = AllSpawnObject[AllCatMove[val]].gameObject.transform;
        gg.transform.localPosition = Vector3.zero;
        middle = AllCatMove[val];
        if (AllSpawnObject[middle].tag == "border")
        {
            catOnBorder = true;
            GameOver();
        }
        CatPos();
    }

    void GameOver()
    {
        GameOverPanel.SetActive(true);
    }

    public void RetryButtonClicked()
    {
        SceneManager.LoadScene("MainScene");
    }

}
