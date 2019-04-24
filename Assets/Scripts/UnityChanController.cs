using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UnityChanController : MonoBehaviour
{

    //SceneManagerクラスのSceneChange関数を呼ぶために実体化させる変数
    SceneChanger sceneChange = new SceneChanger();

    //アニメーションするためのコンポーネントを入れる
    private Animator myAnimator;
    //Unityちゃんを移動させるコンポーネントを入れる
    private Rigidbody myRigidbody;
    //前進するための力
    private float forwardForce = 800.0f;
    //左右に移動するための力
    private float turnForce = 500.0f;
    //ジャンプするための力
    private float upForce = 500.0f;
    //左右の移動できる範囲
    private float movableRange = 3.4f;
    //動きを減速させる係数
    private float coefficient = 0.95f;

    //ゲーム終了の判定
    private bool isEnd = false;

    //ゲーム終了時に表示するテキスト
    private GameObject stateText;
    private GameObject stateText_2;
    //スコアを表示するテキスト
    private GameObject scoreText;
    //得点
    private int score = 0;

    //左ボタン押下の判定
    private bool isLButtonDown = false;
    //右ボタン押下の判定
    private bool isRButtonDown = false;

    //ジャンプした時の声
    public AudioClip jumpVoice;
    //障害物に衝突したときの声
    public AudioClip achingVoice;
    //ゴールした時の声
    public AudioClip yahoo;
    //ゴールした時の効果音
    public AudioClip clapping;
    //コイン獲得時の効果音
    public AudioClip money_drop;

    private AudioSource univoice;


    //紙吹雪パーティクル
    public GameObject paperParticle;


    // Use this for initialization
    void Start()
    {

        //Animatorコンポネントを取得
        this.myAnimator = GetComponent<Animator>();

        //走るアニメーションを開始
        this.myAnimator.SetFloat("Speed", 1);
        //倒れるアニメーションは無効
        this.myAnimator.SetBool("Damage", false);


        //Rigidbodyコンポーネントを取得
        this.myRigidbody = GetComponent<Rigidbody>();

        //Audioコンポーネントを取得
        univoice = GetComponent<AudioSource>();

        //シーン中のstateTextオブジェクトを取得
        this.stateText = GameObject.Find("GameResultText");
        this.stateText_2 = GameObject.Find("GameResultText_2");

        //シーン中のscoreTextオブジェクトを取得
        this.scoreText = GameObject.Find("ScoreText");

    }

    // Update is called once per frame
    void Update()
    {

        //ゲーム終了ならUnityちゃんの動きを減衰する
        if (this.isEnd)
        {
            this.forwardForce *= this.coefficient;
            this.turnForce *= this.coefficient;
            this.upForce *= this.coefficient;
            this.myAnimator.speed *= this.coefficient;

            //シーン遷移（リトライ）
            sceneChange.SceneChange();
        }


        //Unityちゃんに前方向の力を加える
        this.myRigidbody.AddForce(this.transform.forward * this.forwardForce);

        //Unityちゃんを矢印キーまたはボタンに応じて左右に移動させる
        if ((Input.GetKey(KeyCode.LeftArrow) || this.isLButtonDown) && -this.movableRange < this.transform.position.x)
        {
            //左に移動
            this.myRigidbody.AddForce(-this.turnForce, 0, 0);
        }
        else if ((Input.GetKey(KeyCode.RightArrow) || isRButtonDown) && this.transform.position.x < this.movableRange)
        {
            //右に移動
            this.myRigidbody.AddForce(this.turnForce, 0, 0);
        }

        //Jupmステートの場合はJumpにfalseをセットする
        if (this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            this.myAnimator.SetBool("Jump", false);
        }

        //ジャンプしていない時にスペースが押されたらジャンプする
        if (Input.GetKeyDown(KeyCode.Space) && this.transform.position.y < 0.5f)
        {
            //ジャンプアニメを再生
            this.myAnimator.SetBool("Jump", true);
            //Unityちゃんに上方向の力を加える
            this.myRigidbody.AddForce(this.transform.up * this.upForce);
            //ジャンプ時に声を出す
            gameObject.GetComponent<AudioSource>().PlayOneShot(jumpVoice);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //障害物に衝突した場合
        if (other.gameObject.tag == "CarTag" || other.gameObject.tag == "TrafficConeTag")
        {

            //倒れるアニメーションを再生
            this.myAnimator.SetBool("Damage", true);

            this.isEnd = true;
            //stateTextにGAME OVERを表示
            this.stateText.GetComponent<Text>().text = "GAME OVER";
            this.stateText_2.GetComponent<Text>().text = "RETRY?\nPush SPACE or ENTER KEY";

            //衝突時に声を出す
            gameObject.GetComponent<AudioSource>().PlayOneShot(achingVoice);

        }

        //ゴール地点に到達した場合
        if (other.gameObject.tag == "GoalTag")
        {
            this.isEnd = true;
            this.stateText.GetComponent<Text>().text = "Congratulation !!";
            this.stateText_2.GetComponent<Text>().text = "RETRY?\nPush SPACE or ENTER KEY";

            //ゴールした時の声
            gameObject.GetComponent<AudioSource>().PlayOneShot(yahoo);
            //ゴールした時の効果音
            gameObject.GetComponent<AudioSource>().PlayOneShot(clapping);
            //紙吹雪を再生
            Instantiate(paperParticle, transform.position, transform.rotation);
        }

        //コインに衝突した場合
        if (other.gameObject.tag == "CoinTag")
        {
            //スコアを加算
            this.score += 10;

            //ScoreText獲得した点数を表示
            this.scoreText.GetComponent<Text>().text = "Score" + this.score + "pt";

            //パーティクルを再生
            GetComponent<ParticleSystem>().Play();

            //コイン接触時の効果音
            gameObject.GetComponent<AudioSource>().PlayOneShot(money_drop);

            //接触したコインのオブジェクトを破棄
            Destroy(other.gameObject);
        }
    }

    //ジャンプボタンを押した場合の処理
    public void GetMyJumpButtonDown()
    {
        if (this.transform.position.y < 0.5f)
        {
            this.myAnimator.SetBool("Jump", true);
            this.myRigidbody.AddForce(this.transform.up * this.upForce);
            //ジャンプ時に声を出す
            gameObject.GetComponent<AudioSource>().PlayOneShot(jumpVoice);
        }
    }

    //左ボタンを押し続けた場合の処理
    public void GetMyLeftButtonDown()
    {
        this.isLButtonDown = true;
    }
    //左ボタンを離した場合の処理
    public void GetMyLeftButtonUp()
    {
        this.isLButtonDown = false;
    }

    //右ボタンを押し続けた場合の処理
    public void GetMyRightButtonDown()
    {
        this.isRButtonDown = true;
    }
    //右ボタンを反した場合の処理
    public void GetMyRightButtonUp()
    {
        this.isRButtonDown = false;
    }
}
