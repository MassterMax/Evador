using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LittleSceneScript : MonoBehaviour
{
    [SerializeField] Image leftPanel, rightPanel;
    [SerializeField] GameObject player;
    Vector2 start, finish;
    float dtime = 0.01f;
    float speed = 1.5f;
    bool left = true;

    void Awake()
    {
        start = player.transform.position;
        finish = new Vector2(-start.x, start.y);
        //Debug.Log("!");
        //StartTheShow();
    }

    public void StartTheShow()
    {
        player.transform.position = start;
        StartCoroutine(startMoving());
    }

    IEnumerator startMoving()
    {
        leftPanel.color *= 0.8f;
        while (true)
        {
            //Debug.Log(finish + " " + player.transform.position);
            yield return new WaitForSeconds(dtime);
            player.transform.position = Vector3.MoveTowards(player.transform.position, finish, speed * dtime);

            if ((Vector2)player.transform.position == finish)
            {
                if (left)
                    leftPanel.color *= 1.25f;
                else
                    rightPanel.color *= 1.25f;

                yield return new WaitForSeconds(.8f);
                finish = new Vector2(-finish.x, finish.y);

                if (left)
                    rightPanel.color *= .8f;
                else
                    leftPanel.color *= .8f;
                left = !left;
            }
        }
    }


}
