using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine.UI;
using UniRx;          //UniRX使用
using UniRx.Triggers; //UniRXの??使用

public class NotesSpawner : MonoBehaviour
{
    //ノーツオブジェクト宣言＆bool宣言**********************************************
    [SerializeField] GameObject Notes_A;       //ノーツ
    private bool A_fire;
    [SerializeField] GameObject Notes_B;       //ノーツ
    private bool B_fire;
    [SerializeField] GameObject Notes_C;       //ノーツ
    private bool C_fire;
    [SerializeField] GameObject Notes_D;       //ノーツ
    private bool D_fire;

    //public Transform[] Points;

    public Transform Points_1;
    [SerializeField] Transform Points_2;
    [SerializeField] Transform Points_3;
    [SerializeField] Transform Points_4;
    //******************************************************************************

    private float beat;  //計算後のBPM
    private float timer; //時間計測
    public float BPM;    //BPM
    private float StartTime; //ゲーム時間
    private bool PlayMusic;

    //Json譜面読込用宣言*************************************************************
    private List<GameObject> JsonNotes; //1曲分のノーツデータを格納
    private List<float> NoteTimings;    //ノーツタイミングを入れる
    [SerializeField] string FilePath;   //読み込むJsonファイル
    [SerializeField] string ClipPath;   //参照する楽曲データ
    private AudioSource MusicClip;
    private JsonNode json;
    private string Title;               //曲のタイトルを格納
    private string[] BarData;             //1小節内のノーツデータ
    private float DefultSpeed;          //BPMから計算してノーツを配置
    private float NotesOffset;          //最初のノーツが判定ラインに到達する時間
    private float StartMusisTime;       //音楽をかける時間
    //********************************************************************************

    //譜面解読用変数宣言**************************************************************
    private bool NotesComp = false;
    private int BarCount;                   //小節カウント
    private int[] DataCount;
    private int SlashCount;
    private int NotesCount;           //for文用
    private int CountLine;             //1ラインのノーツ数カウント
    private char type;                //ノーツの種類
    private int cnt;
    private float NotesDistance;        //ノーツの間隔
    private GameObject Note;
    private float NotePosition;
    bool b = true;
    int i = 0;
    int y = 0;
    private float[] NoteBeat;
    //********************************************************************************




    //シーン起動時==========================================================================
    void Start()
    {
        //変数初期化******************************************************
        DataCount = new int[9999];            //小節数分の配列を作る(仮)
        NoteBeat = new float[999999];         //ノーツの数分作る(仮)
        BarCount = 0;
        BarData = new string[999999];

        A_fire = false;
        B_fire = false;
        C_fire = false;
        D_fire = false;

        StartTime = 0.0f;
        PlayMusic = false;
        //****************************************************************

        MusicClip = GameObject.Find("GameMusic").GetComponent<AudioSource>();

        loadChart(); //JsonDataを読み込む

        
        
        
        Debug.Log(MusicClip);

        beat = (60 / BPM) * 4; //60をBPMで割った値を軸にノーツを出す。  //30 = 8拍 60 = 4拍 120 = 2拍
    }



    public void Update()
    {
        StartTime += Time.deltaTime;
        //Debug.Log(StartTime);
        //Debug.Log(StartMusisTime);

        if (!PlayMusic)
        {
            if (StartMusisTime <= StartTime)
            {
                MusicClip.Play();
                PlayMusic = true;

            }
        }


            if (!NotesComp)
        {
            int x = 0;
            //ノーツの識別========================================================================================
            foreach (var note in json["SCORE"]) //"SCORE"(譜面)に続く配列を確認する                           
            {


                BarData[x] = note["BAR"].Get<string>();       //BARに続く文字
                DataCount[BarCount] = BarData[x].Length;           //1小節内の音符数(int換算)
                                                                   //Debug.Log(DataCount[BarCount]);


                //音符の間隔をとるため１小節の , を数える
                for (NotesCount = 0, SlashCount = 0; NotesCount < DataCount[BarCount]; NotesCount++)
                {
                    type = BarData[x][NotesCount]; //配列中身を順に見る

                    if (type == '/')
                    {
                        SlashCount++;
                    }


                    if (type == ',') //BarData内の","を数える
                    {

                        //NoteBeat[DataCount[BarCount]] = beat / (DataCount[BarCount] - 1); //1小節内のノーツ数(","除く)からノーツ間隔を算出
                        NoteBeat[DataCount[BarCount]] = beat / (SlashCount); //1小節内のノーツ数(","除く)からノーツ間隔を算出

                        BarCount++;
                    }
                }
                //Debug.Log(x + "小節目のノーツ:" + BarData[x]);
                x++;

            }
            //==================================================================================================================================  

            NotesComp = true;
        }

        //Debug.Log(BarData);
        if (y < BarCount) //小節ぶん回す
        {

            if (i < DataCount[y] - 1) //1小節内のノーツ数(","除く)ぶん回す
            {

                if (timer > beat)
                {

                    switch (BarData[y][i]) //1文字ずつ確認する
                    {
                        case '1':
                            A_fire = true;
                            //GameObject cube1 = Instantiate(Notes_A, new Vector3(-3.0f, 10.0f, -1.1f), Quaternion.identity);
                            //Debug.Log("A");
                            break;
                        case '2':
                            B_fire = true;
                            //GameObject cube2 = Instantiate(Notes_B, new Vector3(-1.0f, 10.0f, -1.1f), Quaternion.identity);
                            //Debug.Log("B");
                            break;
                        case '3':
                            C_fire = true;
                            //GameObject cube3 = Instantiate(Notes_C, new Vector3( 1.0f, 10.0f, -1.1f), Quaternion.identity);
                            //Debug.Log("C");
                            break;
                        case '4':
                            D_fire = true;
                            //GameObject cube4 = Instantiate(Notes_D, new Vector3( 3.0f, 10.0f, -1.1f), Quaternion.identity);
                            //Debug.Log("D");
                            break;
                        case '0':
                            //休符
                            //Debug.Log("無し");
                            break;

                    }

                    if (BarData[y][i] == '/')
                    {
                        //Debug.Log(A_fire);
                        if (A_fire == true) Instantiate(Notes_A, new Vector3(-0.3f, 1.3f, 20.0f), Quaternion.identity);
                        if (B_fire == true) Instantiate(Notes_B, new Vector3(0.3f, 1.3f, 20.0f), Quaternion.identity);
                        if (C_fire == true) Instantiate(Notes_C, new Vector3(0.1f, 1.3f, 20.0f), Quaternion.identity);
                        if (D_fire == true) Instantiate(Notes_D, new Vector3(0.0f, 1.3f, 20.0f), Quaternion.identity);

                        A_fire = false;
                        B_fire = false;
                        C_fire = false;
                        D_fire = false;
                        //Debug.Log(NoteBeat[DataCount[y]]);
                        timer -= NoteBeat[DataCount[y]];
                    }



                    i++;
                }

            }
            else
            {
                //Debug.Log("次");
                i = 0;
                y++;
            }
        }


        timer += Time.deltaTime;

    }

    //Json譜面データの読み込み部==========================================================================================================
    void loadChart()
    {
        JsonNotes = new List<GameObject>();  //
        NoteTimings = new List<float>();       //

        //読込============================================================================================================================
        string jsonText = Resources.Load<TextAsset>("Charts\\" + FilePath).ToString();  //jsonTextに、Chart内のFilePathのデータを入れる       
        MusicClip.clip = (AudioClip)Resources.Load("Charts\\" + ClipPath);             //Music.clipに、Chart内のClipPathのデータを入れる
        //================================================================================================================================

        //解読============================================================================================================================
        json = JsonNode.Parse(jsonText);    //jsonText(FilePath)に入れたJsonファイルを展開する
        //================================================================================================================================

        //割り当て========================================================================================================================
        // "コマンドテキスト": "指示", で読み込む
        Title = json["title"].Get<string>();                    //タイトル (例 "title": に続く「文字」を「,」まで入れる)       
        BPM = int.Parse(json["bpm"].Get<string>());           //BPM
        DefultSpeed = 60 / BPM;                                       //ノーツを流す基本スピード
        NotesOffset = float.Parse(json["offset"].Get<string>());      //ノーツを流し始める時間
        StartMusisTime = float.Parse(json["STARTMUSIC"].Get<string>());  //曲を流し始める時間
        //================================================================================================================================
    }
    //====================================================================================================================================

    void OutputNotes()
    {

    }
}

