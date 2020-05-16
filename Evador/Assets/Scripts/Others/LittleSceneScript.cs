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
        leftPanel.color = new Color(1, 1, 1, 1);
        rightPanel.color = new Color(1, 1, 1, 1);

        leftFinger.transform.localScale = new Vector3(0.3f, 1, 1);
        rightFinger.transform.localScale = new Vector3(0.3f, 1, 1);

        player.transform.position = start;
        leftFinger.transform.localScale *= .8f;
        StartCoroutine(startMoving());
    }

    /// <summary>
    /// Метод начинает периодическое движение в обучении
    /// </summary>
    /// <returns></returns>
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