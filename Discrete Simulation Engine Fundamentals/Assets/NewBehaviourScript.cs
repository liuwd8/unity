using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {
    private int count;
    public Texture2D iconTrue;
    public Texture2D iconFalse;
    private int[] flag = { 0,0,0,0,0,0,0,0,0};
    private bool isEnd,iseq;

    private void Start()
    {
        count = 0;
        isEnd = false;
        iseq = false;
    }

    void OnGUI()
    {
        GUI.Label(new Rect(Screen.width / 2 - 25, Screen.height / 2 - 60 - 50, 50, 25), "井字棋");
        if (GUI.Button(new Rect(Screen.width / 2 - 25, Screen.height / 2 - 60-25, 50,25),"重置"))
        {
            for (int i = 0; i < 9; ++i)
                flag[i] = 0;
            count = 0;
            isEnd = false;
            iseq = false;
        }
        for(int i=0;i<3;++i)
        {
            for(int j=0;j<3;++j)
            {
                if(flag[i*3+j] == 0)
                {
                    if (GUI.Button(new Rect(Screen.width/2-60+(i * 40), Screen.height/2 - 60+(j * 40), 40, 40), "")&& !isEnd)
                    {
                        flag[i * 3 + j] = (count % 2) + 1;
                        count++;
                    }
                }
                else if(flag[i * 3 + j] == 1)
                {
                    GUI.Button(new Rect(Screen.width/2 - 60+ (i * 40), Screen.height/2 - 60 + (j * 40), 40, 40), iconTrue);
                }
                else if(flag[i * 3 + j] == 2)
                {
                    GUI.Button(new Rect(Screen.width/2 - 60+ (i * 40), Screen.height/2 - 60 + (j * 40), 40, 40), iconFalse);
                }
            }
        }
        for (int i = 0; i < 3; ++i)
        {
            if (flag[i*3]!=0&&flag[i * 3] == flag[i * 3 + 1] && flag[i * 3] == flag[i * 3 + 2])
            {
                isEnd = true;
                GUI.Label(new Rect(Screen.width / 2 - 20, Screen.height / 2 + 60, 40, 40), new GUIContent(flag[i * 3] == 1 ? iconTrue : iconFalse));
            }
        }
        for (int j = 0; j < 3; ++j)
        {
            if (flag[j]!=0&&flag[j] == flag[3 + j] && flag[j] == flag[6 + j])
            {
                isEnd = true;
                GUI.Label(new Rect(Screen.width / 2 - 20, Screen.height / 2 + 60, 40, 40), new GUIContent(flag[j] == 1 ? iconTrue : iconFalse));
            }
        }
        if(flag[4]!=0&&((flag[0]==flag[8]&&flag[0] == flag[4])||(flag[2] == flag[6] && flag[2] == flag[4])))
        {
            isEnd = true;
            GUI.Label(new Rect(Screen.width / 2 - 20, Screen.height / 2 + 60, 40, 40), new GUIContent(flag[4] == 1 ? iconTrue : iconFalse));
        }
        if (isEnd&&!iseq)
            GUI.Label(new Rect(Screen.width / 2 - 20, Screen.height / 2 + 100, 40, 40), "Win!");
        for(int i=0;i<9; ++i)
        {
            if (flag[i] == 0)
            {
                iseq = false;
                break;
            }
            else
                iseq = true;
        }
        if(iseq&&!isEnd)
        {
            GUI.Label(new Rect(Screen.width / 2 - 20, Screen.height / 2 + 100, 40, 40),"Draw");
        }
    }
}