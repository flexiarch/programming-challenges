using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScript : MonoBehaviour
{
    public Puzzle[]
        puzzles
    ;

    public Transform[] placeHolders;
    public Timer timer;
    public int rows, collumns;
    public float speed;
    public GameObject YouWin;
    public bool hack = false;

    private GameObject instantiateYouWIn;
    private int
        blankX,
        blankY
    ;

    private Puzzle[,]
        board
    ;

    private Vector3
        blankPuzzlePosition
    ;

    // Use this for initialization
    void Start()
    {
        InitializeBoard();
    }

    private void InitializeBoard()
    {
        board = new Puzzle[collumns, rows];
        bool[] taken = new bool[puzzles.Length];

        do
        {
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < collumns; x++)
                {
                    int i;
                    do
                    {
                        i = Random.Range(0, puzzles.Length);
                    }
                    while (taken[i]);

                    taken[i] = true;

                    var vpos = placeHolders[y * collumns + x].position;
                    if (puzzles[i] is BlankPuzzle)
                    {
                        blankX = x;
                        blankY = y;
                        blankPuzzlePosition = vpos;
                    }
                    else
                    {
                        var go = Instantiate(puzzles[i].gameObject, placeHolders[y * collumns + x], true);
                        go.transform.position = vpos;
                        go.transform.localScale = new Vector3(1, 1);
                        board[x, y] = go.GetComponent<Puzzle>();
                    }
                }
            }

            // reset flags container
            for (int j = 0; j < taken.Length; j++)
            {
                taken[j] = false;
            }
        }
        while (CheckWin());
    }

    private void DestroyBoard()
    {

        for (int y = 0; y < rows; y++)
            for (int x = 0; x < collumns; x++)
            {
                if (board[x, y] != null)
                    Destroy(board[x, y].gameObject);
            }
    }

    private bool CheckWin()
    {
        if (hack) return true;

        int last = 0, scorer=0;
        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < collumns; x++)
            {
                if (board[x, y] == null)
                {
                    scorer = (rows*collumns);
                }
                else
                {
                    scorer = board[x, y].Number;
                }

                if (scorer != ++last)
                {
                    return false;
                }
            }
        }

        return true;
    }

    public void ResetGame()
    {
        HideWinSplash();
        DestroyBoard();
        InitializeBoard();
        ResetTimer();
    }

    public void HitPuzzle(int number)
    {
        if (PossiblePuzzleMove(number) != Direction.None)
        {
            StartTimerOnFirstMove();
            SwapPuzzle(number);
            IfWinShowWinSplash();
        }
    }

    private void IfWinShowWinSplash()
    {
        if (CheckWin())
        {
            StopTimer();
            DestroyBoard();
            instantiateYouWIn = Instantiate(YouWin);
        }
    }

    private void HideWinSplash()
    {
        if (instantiateYouWIn != null)
        {
            Destroy(instantiateYouWIn);
            instantiateYouWIn = null;
        }
    }

    private void SwapPuzzle(int number)
    {
        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < collumns; x++)
            {
                if (board[x, y] != null && board[x, y].Number == number)
                {
                    var puzzle = board[x, y];
                    puzzle.StartMoveSequenceTo(blankPuzzlePosition);

                    blankPuzzlePosition = placeHolders[y * collumns + x].position;

                    board[blankX, blankY] = puzzle;
                    board[x, y] = null;
                    blankX = x;
                    blankY = y;


                    return;
                }
            }
        }
    }

    private Direction PossiblePuzzleMove(int number)
    {
        int x = 0, y;

        for (y = 0; y < rows; y++)
            for (x = 0; x < collumns; x++)
                if (board[x, y] != null && board[x, y].Number == number)
                    goto calc;

        calc:
        int xf = x - blankX;
        int yf = y - blankY;

        if (xf == -1 && yf == 0)
            return Direction.Right;
        else if (xf == 1 && yf == 0)
            return Direction.Left;
        else if (yf == -1 && xf == 0)
            return Direction.Down;
        else if (yf == 1 && xf == 0)
            return Direction.Up;
        else
            return Direction.None;
    }

    bool isFirstMove = true;
    private void StartTimerOnFirstMove()
    {
        if (isFirstMove)
        {
            timer.RunTimer();
            isFirstMove = false;
        }
    }

    private void StopTimer()
    {
        timer.StopTimer();
    }

    private void ResetTimer()
    {
        timer.ResetTimer();
        isFirstMove = true;
    }
}
