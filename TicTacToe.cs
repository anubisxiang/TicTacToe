using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicTacToe : MonoBehaviour
{
    private int [,] board = new int [3,3];
    private int turn = 0;
    private int square_size = Screen.width / 10;
    private int menu_width = Screen.width / 5, menu_height = Screen.width / 10;
    private int mode = 0;
    
    private GUIStyle bigStyle, yellowStyle, redStyle;
    public Texture2D empty, icon1, icon2;
    public Texture2D background;
    
    void Start()
    {
        bigStyle = new GUIStyle();
        bigStyle.normal.textColor = Color.white;
        bigStyle.normal.background = null;
        bigStyle.fontSize = 50;

        //yellow
        yellowStyle = new GUIStyle();
        yellowStyle.normal.textColor = Color.yellow;
        yellowStyle.normal.background = null;
        yellowStyle.fontSize = 50;

        redStyle = new GUIStyle();
        redStyle.normal.textColor = Color.red;
        redStyle.normal.background = null;
        redStyle.fontSize = 30;
    }

    void Update()
    {
        
    }

    void OnGUI() {
        switch(mode) {
            case 0:
                mainMenu();
                break;
            case 1:
                playerVsPlayer();
                break;
            case 2:
                playerVsPlayer();
                break;           
        }       
    }

    void mainMenu() {

        string aa = "";
        GUIStyle bb = new GUIStyle();
        bb.normal.background = background;
        GUI.Label(new Rect(0, 0, Screen.width, Screen.height), aa, bb);

        GUI.Label(new Rect(Screen.width / 2 - menu_width * 0.8f, Screen.height * 0.1f, menu_width, menu_height), "Main Menu", bigStyle);
        if (GUI.Button(new Rect(Screen.width / 2 - menu_width / 2, Screen.height * 2 / 7, menu_width, menu_height), "Start!",redStyle)) {
            mode = 1;
        }
    }
    int checkresult() {
        int res = -1;
        for (int i = 0; i < 3; ++i) {
            if (board[i,0] != 0 && board[i,0] == board[i,1] && board[i,0] == board[i,2]) {
                res = board[i,0];
                break;
            }
        }
        if (res == -1)
            for (int j = 0; j < 3; ++j) {
                if (board[0,j] != 0 && board[0,j] == board[1,j] && board[0,j] == board[2,j]) {
                    res = board[0,j];
                    break;
                }
            }
        if (res == -1)
            if (board[1,1] != 0 && (board[0,0] == board[1,1] && board[1,1] == board[2,2] || board[0,2] == board[1,1] && board[2,0] == board[1,1])) {
                res = board[1,1];
            }
        if (res == -1) {
            int cnt = 0;
            for (int i = 0; i < 3; ++i) {
                for (int j = 0; j < 3; ++j){
                    if (board[i,j] == 0) {
                        cnt++;
                        break;
                    }
                }
            }
            if (cnt == 0) {
                res = 3;
            }
        }
        if (res == -1) return 0;
        return res;
    }
    void checkState() {
        int res = checkresult();
        if (res == 0) return ;

        if (res == 1) {
            if (mode == 1 || mode == 2) {
                GUI.Label(new Rect(Screen.width / 2 - 3 * square_size, Screen.height / 2, square_size * 1.5f, square_size * 0.8f), "Sunflower wins!", yellowStyle);
                mode = 2;
            }
        }
        else if (res == 2) {
            if (mode == 1 || mode == 2) {
                GUI.Label(new Rect(Screen.width / 2 - 3 * square_size, Screen.height / 2, square_size * 1.5f, square_size * 0.8f), "Peashooter wins!", yellowStyle);
                mode = 2;
            }
        }
        else if (res == 3) {
            if (mode == 1 || mode == 2) {
                GUI.Label(new Rect(Screen.width / 2 - 3 * square_size, Screen.height / 2, square_size * 1.5f, square_size * 0.8f), "Tie!", yellowStyle);
                mode = 2;
        }
        }      
    }

    void playerVsPlayer() {

        string aa = "";
        GUIStyle bb = new GUIStyle();
        bb.normal.background = background;
        GUI.Label(new Rect(0, 0, Screen.width, Screen.height), aa, bb);


        if(mode == 1 || mode == 2)
        {
            for (int i = 0; i < 3; ++i) {
                    for (int j = 0; j < 3; ++j) {
                        switch(board[i,j]) {
                            case 0:
                                if (GUI.Button(new Rect(Screen.width / 2 + (i - 1.5f) * square_size, Screen.height / 2 + (j - 1.5f)* square_size, square_size, square_size), empty)) {
                                    if(mode != 2)
                                    {
                                        board[i,j] = turn + 1;
                                        turn = 1 - turn;
                                    }
                                }
                                break;
                            case 1:
                                GUI.Button(new Rect(Screen.width / 2 + (i - 1.5f) * square_size, Screen.height / 2 + (j - 1.5f) * square_size, square_size, square_size), icon1);
                                break;
                            case 2:
                                GUI.Button(new Rect(Screen.width / 2 + (i - 1.5f) * square_size, Screen.height / 2 + (j - 1.5f) * square_size, square_size, square_size), icon2);
                                break;
                        }
                    }
                }
        }
        
        checkState();
        
        
        if(mode == 1 || mode ==2)
        {
            if (GUI.Button(new Rect(Screen.width - square_size , Screen.height - square_size * 0.7f, square_size, square_size * 0.7f), "Reset")) {
                    reset();
                    mode = 1;//注意
                }
                if (GUI.Button(new Rect(0 , Screen.height - square_size * 0.7f, square_size * 1.6f, square_size * 0.7f), "Return to Menu")) {
                    reset();
                    mode = 0;
                }
        }
    }

    void reset() {
        for (int i = 0; i < 3; ++i) {
            for (int j = 0; j < 3; ++j) {
                board[i,j] = 0;
            }
        }
        turn = 0;
    }
}
