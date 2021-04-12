using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LittleSceneScript : MonoBehaviour
{
    [SerializeField] Image leftPanel, rightPanel;
    [SerializeField] GameObject player;
    [SerializeField] GameObject leftFinger, rightFinger;
    Vector2 start, finish;
    float dtime = 0.01f;
    float speed = 1.5f;
    bool left = true; // Двигаемся влево?

    void Awake()
    {
        start = player.transform.position;
        finish = new Vector2(-start.x, start.y);
    }

    /// <summary>
    /// Метод, который запускает анимацию в маленьком окошке обучения
    /// </summary>
    public void StartTheShow()
    {
        left = true;
        leftPanel.color = new Color(1, 1, 1, 1);
        rightPanel.color = new Color(1, 1, 1, 1);

        //Debug.Log(leftFinger.transform.localScale + " " + rightFinger.transform.localScale);
        leftFinger.transform.localScale = new Vector3(0.3f, 1, 1);
        rightFinger.transform.localScale = new Vector3(0.3f, 1, 1);
        //Debug.Log("after scaling " + leftFinger.transform.localScale + " " + rightFinger.transform.localScale);
        //Debug.Log(left);

        player.transform.position = start;
        finish = new Vector2(-start.x, start.y);
        leftFinger.transform.localScale *= .8f;
        StartCoroutine(startMoving());
    }

    /// <summary>
    /// Метод начинает периодическое движение в обучении
    /// </summary>
    /// <returns></returns>
    IEnumerator startMoving()
    {
        //Debug.Log("cor");
        leftPanel.color *= 0.8f;
        while (true)
        {
            yield return new WaitForSeconds(dtime);
            player.transform.position = Vector3.MoveTowards(player.transform.position, finish, speed * dtime);
            //Debug.Log(finish + " " + player.transform.position);

            if ((Vector2)player.transform.position == finish)
            {
                //Debug.Log(left);
                if (left)
                { // Возвращем цвет панелек
                    leftPanel.color *= 1.25f;
                    leftFinger.transform.localScale *= 1.25f;
                }
                else
                {
                    rightPanel.color *= 1.25f;
                    rightFinger.transform.localScale *= 1.25f;
                }

                yield return new WaitForSeconds(.8f);
                finish = new Vector2(-finish.x, finish.y);

                if (left)
                {
                    rightFinger.transform.localScale *= .8f;
                    rightPanel.color *= .8f;
                }
                else
                {
                    leftFinger.transform.localScale *= .8f;
                    leftPanel.color *= .8f;
                }
                left = !left;
            }
        }
    }
}