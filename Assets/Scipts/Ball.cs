using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    private bool _isDragging = false;
    private float _startMouseX;
    private float _startObjX;
    private bool _canDrag = true; //Kiểm tra có thể kéo bóng không
    private Vector3 _offset;
    private Rigidbody2D _rg2d;
    private bool _finish = false; //Kiểm tra bóng rơi xong chưa để init new ball
    private bool _isDead = false; //Kiểm tra bóng bị huỷ chưa
    private float _xMax;
    private float delayTime = 0.5f;
    private static float _delayTime ;

    public static bool IsGameOver {get; set; }
    
    public bool Finish
    {
        get => _finish;
        set => _finish = value;
    }

    public float XMax
    {
        get => _xMax;
        set => _xMax = value;
    }

    public bool CanDrag
    {
        get => _canDrag;
        set => _canDrag = value;
    }

    private void Awake()
    {
        IsGameOver = false;
        _rg2d = GetComponent<Rigidbody2D>();
    }
    
    // Update is called once per frame
    void Update()
    {
        _delayTime -= Time.deltaTime;
        MoveBall();
    }

    void MoveBall()
    {
        if (IsGameOver) return;
        if (!GameController.Instance.isActive) return;
        if (Input.GetMouseButtonDown(0) && !_isDragging && _canDrag)
        {
            StartDragging();
        }
        else if (Input.GetMouseButtonUp(0) && _isDragging)
        {
            StopDragging();
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            _canDrag = false;
            AudioManager.Instance.PlayCollisionSound();
        }
            
        if (_isDragging)
        {
            DragObject();
        }
    }
    
    void StartDragging()
    {
        _isDragging = true;
        _startMouseX = Input.mousePosition.x;
        _startObjX = transform.position.x;
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = 0;
        if (Camera.main != null) return Camera.main.ScreenToWorldPoint(mousePoint);
        return Vector3.zero;
    }
    
    void StopDragging()
    {
        _isDragging = false;
    }

    void DragObject()
    {
        float currentMouseX = Input.mousePosition.x;
        float deltaX = currentMouseX - _startMouseX;
        var transform1 = transform;
        Vector3 newPosition = transform1.position;
        newPosition.x = _startObjX + deltaX * 0.01f; // Nhân deltaX với hệ số để điều chỉnh tốc độ di chuyển
        if (newPosition.x >= _xMax) newPosition.x = _xMax;
        if (newPosition.x <= -_xMax) newPosition.x = -_xMax;
        transform1.position = newPosition;
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (gameObject.CompareTag(other.gameObject.tag) && !_canDrag)
        {
            if (_delayTime <= 0)
            {
                _delayTime = delayTime;
                bool isOtherDead = other.gameObject.GetComponent<Ball>()._isDead;
                if (!_isDead && !isOtherDead)
                {
                    string scoreString = other.gameObject.GetComponentInChildren<TextMeshProUGUI>().text;
                    int score = Int32.Parse(scoreString)/2;
                    GameController.Instance.IncreaseScore(score);
                    Debug.Log(GameController.Instance.Score);
                    TranformBall(other.gameObject.GetComponent<Ball>());
                }
            }
            _finish = true;
        }

        if (other.gameObject.GetComponent<EdgeCollider2D>() || !other.gameObject.CompareTag("Untagged") && !_canDrag)
        {
            _finish = true;
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (gameObject.CompareTag(other.gameObject.tag) && !_canDrag)
        {
            bool isOtherDead = other.gameObject.GetComponent<Ball>()._isDead;
            if (!_isDead && !isOtherDead)
            {
                string scoreString = other.gameObject.GetComponentInChildren<TextMeshProUGUI>().text;
                int score = Int32.Parse(scoreString)/2;
                GameController.Instance.IncreaseScore(score);
                Debug.Log(GameController.Instance.Score);
                TranformBall(other.gameObject.GetComponent<Ball>());
            }  
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (gameObject.GetComponent<Rigidbody2D>().velocity.magnitude <= 0.01f && other.gameObject.CompareTag("Finish"))
        {
            Debug.Log(gameObject.GetComponent<Rigidbody2D>().velocity.magnitude);
            IsGameOver = true;
            GameController.Instance.ShowGameOverPanel();
        }
    }

    private void TranformBall(Ball other)
    {
        if (!gameObject.CompareTag(other.gameObject.tag)) return;
        BallController.Instance.MergeBall(this, other);
        _isDead = true;
        other._isDead = true;

    }
    private void LateUpdate()
    {
        if (_isDead)    Destroy(gameObject);
    }
}
