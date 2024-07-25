using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallController : MonoBehaviour
{
    public static BallController Instance;
    public List<Ball> ballsPrefab;
    public Transform ballsContainerTransform;

    private List<Ball> _ballsContainer;
    private void Awake()
    {
        _ballsContainer = new List<Ball>();
        if (!Instance) Instance = this;
        else Destroy(Instance);
    }

    private void Update()
    {
        if (IsCanSpawnBall()) SpawnBallRandom(0, 3);
    }

    private Ball SpawnBallRandom(int min, int max)
    {
        int index = Random.Range(min, max);
        Ball ball = Instantiate(ballsPrefab[index], ballsContainerTransform);
//        ball.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        ball.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        
        float xMax = ballsPrefab[index].transform.position.x;
        ball.XMax = xMax;
        Vector3 pos = ball.gameObject.transform.position;
        ball.gameObject.transform.position = new Vector3(0, 2.13f, pos.z);
        _ballsContainer.Add(ball);
        return ball;
    }

    public void MergeBall(Ball ball1, Ball ball2)
    {
        if (!ball1.gameObject.CompareTag(ball2.gameObject.tag)) return ;
        var position = ball1.transform.position;
        var position1 = ball2.transform.position;
        var pos = position.y > position1.y ? position : position1;
        pos = new Vector3(pos.x, pos.y + 0.2f, pos.z);
        Ball ballObject = null;
        Ball ball = null;
        switch (ball1.tag)
        {
            case "Ball2":
                ballObject = Instantiate(ballsPrefab[1], pos, Quaternion.identity, ballsContainerTransform);
//                vfx = ballObject.gameObject.transform.GetChild(2);
//                vfx.gameObject.SetActive(true);
                ball = ballObject.transform.GetComponentInChildren<Ball>();
                ball.CanDrag = false;
                break;
            case "Ball4":
                ballObject = Instantiate(ballsPrefab[2], pos, Quaternion.identity, ballsContainerTransform);
//                vfx = ballObject.gameObject.transform.GetChild(2);
//                vfx.gameObject.SetActive(true);
                ball = ballObject.transform.GetComponentInChildren<Ball>();
                ball.CanDrag = false;
                break;
            case "Ball8":
                ballObject = Instantiate(ballsPrefab[3], pos, Quaternion.identity, ballsContainerTransform);
//                vfx = ballObject.gameObject.transform.GetChild(2);
//                vfx.gameObject.SetActive(true);
                ball = ballObject.transform.GetComponentInChildren<Ball>();
                ball.CanDrag = false;
                break;
            case "Ball16":
                ballObject = Instantiate(ballsPrefab[4], pos, Quaternion.identity, ballsContainerTransform);
//                vfx = ballObject.gameObject.transform.GetChild(2);
//                vfx.gameObject.SetActive(true);
                ball = ballObject.transform.GetComponentInChildren<Ball>();
                ball.CanDrag = false;
                break;
            case "Ball32":
                ballObject = Instantiate(ballsPrefab[5], pos, Quaternion.identity, ballsContainerTransform);
//                vfx = ballObject.gameObject.transform.GetChild(2);
//                vfx.gameObject.SetActive(true);
                ball = ballObject.transform.GetComponentInChildren<Ball>();
                ball.CanDrag = false;
                break;
            case "Ball64":
                ballObject = Instantiate(ballsPrefab[6], pos, Quaternion.identity, ballsContainerTransform);
//                vfx = ballObject.gameObject.transform.GetChild(2);
//                vfx.gameObject.SetActive(true);
                ball = ballObject.transform.GetComponentInChildren<Ball>();
                ball.CanDrag = false;
                break;
            case "Ball128":
                ballObject = Instantiate(ballsPrefab[7], pos, Quaternion.identity, ballsContainerTransform);
//                vfx = ballObject.gameObject.transform.GetChild(2);
//                vfx.gameObject.SetActive(true);
                ball = ballObject.transform.GetComponentInChildren<Ball>();
                ball.CanDrag = false;
                break;
            case "Ball256":
                ballObject = Instantiate(ballsPrefab[8], pos, Quaternion.identity, ballsContainerTransform);
//                vfx = ballObject.gameObject.transform.GetChild(2);
//                vfx.gameObject.SetActive(true);
                ball = ballObject.transform.GetComponentInChildren<Ball>();
                ball.CanDrag = false;
                break;
            case "Ball512":
                ballObject = Instantiate(ballsPrefab[9], pos, Quaternion.identity, ballsContainerTransform);
//                vfx = ballObject.gameObject.transform.GetChild(2);
//                vfx.gameObject.SetActive(true);
                ball = ballObject.transform.GetComponentInChildren<Ball>();
                ball.CanDrag = false;
                break;
            case "Ball1024":
                ballObject = Instantiate(ballsPrefab[10], pos, Quaternion.identity, ballsContainerTransform);
//                vfx = ballObject.gameObject.transform.GetChild(2);
//                vfx.gameObject.SetActive(true);
                ball = ballObject.transform.GetComponentInChildren<Ball>();
                ball.CanDrag = false;
                break;
        }

        _ballsContainer.Remove(ball1);
        _ballsContainer.Remove(ball2);
        _ballsContainer.Add(ballObject);
        AudioManager.Instance.PlayTransferSound();
    }
    
    private bool IsCanSpawnBall()
    {
        if (_ballsContainer.Count == 0) return true;
        foreach (var ball in _ballsContainer)
        {
            if (!ball.Finish || Ball.IsGameOver) return false; //Không sinh ra bóng
        }

        //Sinh ra bóng
        return true;
    }

    public void ClearAllBall()
    {
        foreach (var ball in _ballsContainer)
        {
            if (ball) Destroy(ball.gameObject);
        }
        _ballsContainer.Clear();
        
        SpawnBallRandom(0, 3);
        
    }
}
