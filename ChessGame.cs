using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
ECS or MVC conception is applied on the small game
Firstly, entities and their states are defined as the game model.
And then, give some components/controls which can modify the game model.
The end, system handler OnGUI called by game circle provides a game UI view.
*/
public class ChessGame : MonoBehaviour {

    // Constants for better readability
    private const int BoardSize = 3;
    private const int ButtonSize = 70;
    private const int OffsetX = 255;
    private const int OffsetY = 50;

    // Entities and their states / Model
    private static int player;
    private static int count;
    private int winner;
    private int[,] chessBoard = new int[BoardSize, BoardSize];

    // System Handlers
    void Start () {
        Init();
    }

    // View render entities / models
    // Here! you cannot modify model directly, use components/controls to do it
    void OnGUI() {
        GUI.Box(new Rect(210, 25, 300, 300), "");
        if (GUI.Button(new Rect(310, 270, 100, 30), "Restart")) Init();
        if (!GameOver()) {
            for (int i = 0; i < BoardSize; i++) {
                for (int j = 0; j < BoardSize; j++) {
                    if (chessBoard[i, j] == 0 && GUI.Button(new Rect(OffsetX + j * ButtonSize, OffsetY + i * ButtonSize, ButtonSize, ButtonSize), "")) {
                        PutChess(i, j);
                    } else if (chessBoard[i, j] == 1) {
                        GUI.Button(new Rect(OffsetX + j * ButtonSize, OffsetY + i * ButtonSize, ButtonSize, ButtonSize), "O");
                    } else if (chessBoard[i, j] == 2) {
                        GUI.Button(new Rect(OffsetX + j * ButtonSize, OffsetY + i * ButtonSize, ButtonSize, ButtonSize), "X");
                    }
                }
            }
        } else {
            if (winner != 0)
                GUI.Box(new Rect(260, 50, 200, 200), "\n\n\n\n\nCongratulations!\n Player " + winner + " has won.");
            else
                GUI.Box(new Rect(260, 50, 200, 200), "\n\n\n\n\n\nThis is a draw!");
        }
    }

    // Components
    // here! any ui can not be referenced
    void Init() {
        player = 1;
        winner = 0;
        count = 0;
        chessBoard = new int[BoardSize, BoardSize];
    }

    void PutChess(int i, int j) {
        chessBoard[i, j] = player;
        player = 3 - player;
        count++;       
    }

    bool GameOver() {
        for (int i = 0; i < BoardSize; i++) {
            if (CheckLine(chessBoard[i, 0], chessBoard[i, 1], chessBoard[i, 2])) winner = chessBoard[i, 0];
            if (CheckLine(chessBoard[0, i], chessBoard[1, i], chessBoard[2, i])) winner = chessBoard[0, i];
        }
        if (CheckLine(chessBoard[0, 0], chessBoard[1, 1], chessBoard[2, 2])) winner = chessBoard[0, 0];
        if (CheckLine(chessBoard[0, 2], chessBoard[1, 1], chessBoard[2, 0])) winner = chessBoard[0, 2];

        return count >= 9 || winner != 0;
    }

    bool CheckLine(int a, int b, int c) {
        return a != 0 && a == b && a == c;
    }
}
