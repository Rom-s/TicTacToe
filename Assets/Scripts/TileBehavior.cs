using System;
using UnityEngine;

public class TileBehavior : MonoBehaviour
{
    private SpriteRenderer _renderer;

    [SerializeField]
    private Vector2Int coordinates;

    private bool _isEmpty = true;
    
    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        
    }

    private void OnEnable()
    {
        GameManager.RestartEvent += Restart;
    }

    private void OnDisable()
    {
        GameManager.RestartEvent -= Restart;
    }

    private void OnMouseEnter()
    {
        if (_isEmpty)
            _renderer.color = new Color(1f, 1f, 0.5f);
    }

    private void OnMouseExit()
    {
        _renderer.color = Color.white;
    }

    private void OnMouseDown()
    {
        if (_isEmpty)
        {
            GameManager gameManager = GameManager.GetInstance();
            Instantiate(gameManager.circleIsPlaying ? gameManager.circle : gameManager.cross, transform);
            _isEmpty = false;
            _renderer.color = Color.white;
            gameManager.SavePlayAndCheckEnd(coordinates);
        }
    }
    
    private void Restart()
    {
        _isEmpty = true;
        if (transform.childCount > 0)
            Destroy(transform.GetChild(0).gameObject);
    }
}
