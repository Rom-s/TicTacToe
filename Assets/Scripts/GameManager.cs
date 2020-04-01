using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    
    private int[][] _dataMatrix;

    private int _turnCount = 0;

    [NonSerialized] public bool circleIsPlaying;

    [SerializeField] public GameObject cross;
    [SerializeField] public GameObject circle;
    
    [SerializeField] private GameObject crossBackground;
    [SerializeField] private GameObject circleBackground;
    [SerializeField] private GameObject grid;
    
    [SerializeField] private CanvasBehavior canvas;

    public delegate void RestartButtonClicked();
    public static event RestartButtonClicked RestartEvent;
    
    void Awake()
    {
        if (_instance == null)
            _instance = this;
        _dataMatrix = new[] {new[] { 0,0,0 }, new[] { 0,0,0 }, new[] { 0,0,0 }};

        crossBackground = Instantiate(crossBackground);
        circleBackground = Instantiate(circleBackground);
        grid = Instantiate(grid);
       
        crossBackground.SetActive(true);
        circleBackground.SetActive(false);
    }
    
    public static GameManager GetInstance()
    {
        return _instance;
    }

    private void ChangeTurn()
    {
        ++_turnCount;
        circleIsPlaying = !circleIsPlaying;
        crossBackground.SetActive(!circleIsPlaying);
        circleBackground.SetActive(circleIsPlaying);
    }

    public void SavePlayAndCheckEnd(Vector2Int cooordinate)
    {
        int state = circleIsPlaying ? 2 : 1;
        _dataMatrix[cooordinate.x][cooordinate.y] = state;
        
        if (_turnCount >= 4)
        {
            //check col
            for(int i = 0; i < 3; i++){
                if(_dataMatrix[cooordinate.x][i] != state)
                    break;
                if(i == 2){
                    GameOver(state);
                    return;
                }
            }

            //check row
            for(int i = 0; i < 3; i++){
                if(_dataMatrix[i][cooordinate.y] != state)
                    break;
                if(i == 2){
                    GameOver(state);
                    return;
                }
            }

            //check diag
            if(cooordinate.x == cooordinate.y){
                for(int i = 0; i < 3; i++){
                    if(_dataMatrix[i][i] != state)
                        break;
                    if(i == 2){
                        GameOver(state);
                        return;
                    }
                }
            }

            //check anti diag 
            if(cooordinate.x + cooordinate.y == 2){
                for(int i = 0; i < 3; i++){
                    if(_dataMatrix[i][2-i] != state)
                        break;
                    if(i == 2){
                        GameOver(state);
                        return;
                    }
                }
            }

            //check draw
            if(_turnCount == 8){
                GameOver(0);
                return;
            }
        }
        ChangeTurn();
    }


    void GameOver(int who)
    {
        switch (who)
        {
            case 2:
                canvas.GetComponent<CanvasBehavior>().SetText("Victoire des ronds !");
                break;
            case 1:
                canvas.GetComponent<CanvasBehavior>().SetText("Victoire des croix !");
                break;
            default:
                canvas.GetComponent<CanvasBehavior>().SetText("Égalité");
                crossBackground.SetActive(false);
                circleBackground.SetActive(false);
                break;
        }

        grid.SetActive(false);
        canvas.ChangeState();
    }

    public void Restart()
    {
        canvas.ChangeState();
        crossBackground.SetActive(true);
        circleBackground.SetActive(false);
        grid.SetActive(true);
        
        if (RestartEvent != null) RestartEvent();
        circleIsPlaying = false;
        _turnCount = 0;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                _dataMatrix[i][j] = 0;
            }
        }
                
        
    }
}
