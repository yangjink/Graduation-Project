using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manager : BaseView {
    
    public GameObject[] m_player = new GameObject[3];
    //public UILabel Buzhu;
    public UIButton m_Buchu;//不出
    public UIButton m_Hint;//提示
    public UIButton m_PlayHand;//出牌
    public static short BUCHUCLIPCOUNT = 4;//不出的音效个数
    public AudioClip[] m_audioClipBuchu = new AudioClip[BUCHUCLIPCOUNT];//不出的音效
    public UILabel m_alarm;//闹钟

    public GameObject m_Fapai;
    public GameObject m_Xipai;

    public Card[] m_cardArr = new Card[20];//当前的20张牌
    private List<CardData> m_playCard = new List<CardData>();//打出的牌

    public Card[] m_wang = new Card[4];//王或者两张普通牌
    public Card[] m_landlordCard = new Card[5];//坑里牌
    private AudioSource audioBuchu;


    public GameObject m_playHandItem;
    public GameObject m_playHandWangItem;
    //private Random rd = new Random();

    public CardRules.CardRelesType m_cardRelesType = CardRules.CardRelesType.Single;//出牌的类型
    void Awake()
    {
        EventDelegate.Add(m_Buchu.onClick, NoPlayHand);
        EventDelegate.Add(m_Hint.onClick,OnClickHint);
        //EventDelegate.Add(m_PlayHand.onClick, OnClickPlayHand);
        AddButtonEvent(m_PlayHand.gameObject, OnClickPlayHand);
        for(int i = 0;i < m_cardArr.Length;i++)
        {
            AddButtonEvent(m_cardArr[i].gameObject,OnClickCard,i);
        }
        for (int i = 0; i < m_wang.Length-2; i++)
        {
            AddButtonEvent(m_wang[i].gameObject, OnClickCard, i);
        }
        for (int i = 2; i < m_wang.Length; i++)
        {
            AddButtonEvent(m_wang[i].gameObject, OnClickCard, i-2);
        }
        initCard();
        AddButtonEvent(m_Fapai, OnClickFaPai);
        AddButtonEvent(m_Xipai, OnClickXipai);
       
    }
    private short time = 30;
    void OnEnable()
    {
        audioBuchu = m_Buchu.GetComponent<AudioSource>();
        m_alarm.text = time.ToString();
        lastTime = Time.time;
        nowTime = Time.time;

        for (int i = 0; i < m_cardArr.Length; i++)
        {
            m_cardArr[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < m_wang.Length; i++)
        {
            m_wang[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < m_landlordCard.Length; i++)
        {
            m_landlordCard[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < _vectorCard.Length; i++)
        {
            if (i < 4)
            {
                _vectorCard[i].x = m_wang[i].transform.position.x;
                _vectorCard[i].y = m_wang[i].transform.position.y;
            }
            else
            {
                _vectorCard[i].x = m_cardArr[i - 2].transform.position.x;
                _vectorCard[i].y = m_cardArr[i - 2].transform.position.y;
            }
        }
    }

   
    bool startFapai = false;
    void OnClickFaPai(GameObject btn, object sender = null)
    {
       
        PlayCard();
        //_player1.Sort((a,b)=>b.m_num.CompareTo(a.m_num));
        _player1.Sort(SoryCard);
        ShowPlayerCard(_player1);
        startFapai = true;
        dealTime = Time.time;
    }
    void OnClickXipai(GameObject btn, object sender = null)
    {
        _player1.Clear();
        _player2.Clear();
        _player3.Clear();
        for (int i = 0; i < LeftCardNum; i++)
        {
            m_cardArr[i].gameObject.SetActive(false);
        }
        LeftCardNum = 0;
    }

    void OnClickCard(GameObject btn,object sender = null)
    {
        Vector2 cardPosition = btn.transform.position;
        bool isSelect = m_cardArr[(int)sender].GetIsSelect();
        if (isSelect)
        {
            btn.transform.position = new Vector2(cardPosition.x, cardPosition.y - 0.08f);   
        }
        else
        {
            btn.transform.position = new Vector2(cardPosition.x, cardPosition.y + 0.08f);
        }
        m_cardArr[(int)sender].SetIsSelect(!isSelect);      
    }

    private List<Card> _playCardArr = new List<Card>();
    private Vector2[] _vectorCard = new Vector2[22];//大王，小王，开始的20张牌
    void OnClickPlayHand(GameObject btn, object sender = null)//向中靠骑先不做，做的话，利用transform来做，保存位置
    {
        //先将所有牌回归位置；
        for (int i = 0; i < _vectorCard.Length;i++ )
        {
            if (i < 4)
            {
                m_wang[i].transform.position = _vectorCard[i];
            }
            else
            {
                m_cardArr[i-2].transform.position = _vectorCard[i];
            }
        }
        _playCardArr.Clear();
        m_playCard.Clear();
        int len = m_cardArr.Length-1;

        bool isSelectCard = false;
        for (int i = 0; i < _player1.Count;i++ )
        {
            if (m_cardArr[i].GetIsSelect())
            {
                //CD = _player1[i];
                isSelectCard = true;
                m_playCard.Add(_player1[i]);
                //OnClickCard(m_cardArr[i].gameObject, i);
                m_cardArr[i].SetIsSelect(false);
                if (m_cardArr[i].GetCardNum() >= (int)Card.cardNum.XiaoWang)
                {
                    _playCardArr.Add(m_wang[(m_cardArr.Length-1)-(len--)]);//需要王上去
                }
                else
                {
                    _playCardArr.Add(m_cardArr[len--]);
                }
            }
        }
        m_playCard.Reverse();
        if (CardRules.PopEnable(m_playCard, out m_cardRelesType) && isSelectCard)
        {
            for (int i = 0; i < m_playCard.Count; i++)
            {
                _player1.Remove(m_playCard[i]);
            }
            PlayHandShow(_player1);
            MoveCardPosition(_playCardArr);
        }
        else
        {
            PlayHandShow(_player1);
        }
    }
    void MoveCardPosition(List<Card> card)
    {
        float len = 0.11f*(card.Count/2);//从中间开始算，如果王 的情况没有考虑
        
        for (int i = 0; i < card.Count; i++)
        {
            Vector2 cardPosition1 = card[i].transform.position;
            card[i].transform.position = new Vector2(len,cardPosition1.y+1.0f);
            card[i].setCard(m_playCard[i].m_cardColor,m_playCard[i].m_num);
            card[i].gameObject.SetActive(true);
            len -= 0.11f;
        }

    }
    void PlayHandShow(List<CardData> cardList)
    {
        for (int i = 0; i < m_cardArr.Length; i++)
        {
            m_cardArr[i].gameObject.SetActive(false);
        }
        ShowPlayerCard(cardList);
        for (int i = 0; i < cardList.Count;i++ )
        {
            m_cardArr[i].gameObject.SetActive(true);
        }
    }
    void OnClickHint()
    {
        //m_player[2].GetComponent<UISprite>().spriteName = "dizhu";
       // m_cardArr[1].gameObject.SetActive(false);
        m_wang[0].transform.position = m_cardArr[10].transform.position;
        m_wang[0].gameObject.SetActive(true);
    }
    void NoPlayHand()
    {
        m_player[0].GetComponent<UISprite>().spriteName = "dizhu";
        //audioBuchu.Play();
        audioBuchu.clip = m_audioClipBuchu[Random.Range(0, BUCHUCLIPCOUNT)];
        audioBuchu.Play();
    }
    void Start()
    {
        
    }
    void FixedUpdate()
    {

    }
    private float nowTime = 0;
    private float lastTime = 0;
    private float dealTime = 0;
    int LeftCardNum = 0;
    int playerCardNum = 17;
	void Update () {
        nowTime = Time.time;
        if (nowTime >= (lastTime + 1.0f))//闹钟
        {
            if (time != 0 )
            {
                m_alarm.text = time.ToString();
                time--;
            }
            else
            {
                time = 30;
            }
            lastTime = nowTime;
        }
        if (startFapai)//发牌使用
        {
            if (nowTime >= (dealTime + 0.2f))
            {
                m_cardArr[LeftCardNum].gameObject.SetActive(true);
                LeftCardNum++;
                //Debug.LogError(j.ToString());
                if (LeftCardNum == playerCardNum)
                {
                    startFapai = false;

                }
                dealTime = nowTime;
            }

        }

        /*
        if (Input.GetKeyDown("a"))
        {
            m_player[0].GetComponent<UISprite>().spriteName = "dizhu";
        }
        if (Input.GetKeyDown("b"))
        {
            m_player[1].GetComponent<UISprite>().spriteName = "dizhu";
        }
        if (Input.GetKeyDown("c"))
        {
            m_player[2].GetComponent<UISprite>().spriteName = "dizhu";
        }*/

	}
    private List<CardData> _player1 = new List<CardData>();
    private List<CardData> _player2 = new List<CardData>();
    private List<CardData> _player3 = new List<CardData>();

    private CardData[] _card = new CardData[54];

    //发牌
    void PlayCard()
    {
        int tag = 0;
        //先抽三张出来
        for (int i = 0; i < 3; i++)
        {
            tag = Random.Range(0,_card.Length);
            CardData tmp = _card[_card.Length - 1 - i];
            _card[_card.Length - 1 - i] = _card[tag];
            _card[tag] = tmp;
        }
        for (int i = 0; i < _card.Length - 3; i++)
        {

            tag = Random.Range(0, 3);
            if (tag == 0)
            {
                if (_player1.Count < 17)
                {
                    _player1.Add(_card[i]);
                    continue;
                }
                tag = 1;
            }
            if (tag == 1)
            {
                if (_player2.Count < 17)
                {
                    _player2.Add(_card[i]);
                    continue;
                }
                if (_player1.Count < 17)
                {
                    _player1.Add(_card[i]);
                    continue;
                }
                tag = 2;
            }
            if (tag == 2)
            {
                if (_player3.Count < 17)
                {
                    _player3.Add(_card[i]);
                    continue;
                }
                if (_player2.Count < 17)
                {
                    _player2.Add(_card[i]);
                    continue;
                }
                if (_player1.Count < 17)
                {
                    _player1.Add(_card[i]);
                    continue;
                }
            }
            Debug.LogError("发牌总数超过");
        }

    }

    void ShowPlayerCard(List<CardData> player)
    {
        for (int i = 0 ; i < 2 && i < player.Count;i++)
        {
            if (player[i].m_num >= (int)Card.cardNum.XiaoWang)
            {
                m_cardArr[i] = m_wang[i];
            }
            else
            {
                m_cardArr[i] = m_wang[i + 2];
            }
            m_cardArr[i].setCard(player[i].m_cardColor, player[i].m_num);
        }
        for (int i = 2; i < player.Count;i++ )
        {

            m_cardArr[i].setCard(player[i].m_cardColor,player[i].m_num);
        }
    }

    void initCard()
    {
        int color = 1;
        for(int i = 0;i <_card.Length-2;i++)
        {

            CardData tmp = new CardData();
            if (color == 0)
            {
                _card[i] = tmp;
                _card[i].m_cardColor = Card.cardColor.HONGTAO;
                _card[i].m_num = i % 13 + 1;
                
            }
            else if(color == 1)
            {
                _card[i] = tmp;
                _card[i].m_cardColor = Card.cardColor.HEITAO;
                _card[i].m_num = i % 13 + 1;
            }
            else if(color == 2)
            {
                _card[i] = tmp;
                _card[i].m_cardColor = Card.cardColor.FANGKUI;
                _card[i].m_num = i % 13 + 1;
            }
            else if(color == 3)
            {
                _card[i] = tmp; 
                _card[i].m_cardColor = Card.cardColor.MEIHUA;
                _card[i].m_num = i % 13 + 1;
            }
            color++;
            color %= 4;
        }
        _card[_card.Length - 2] = new CardData();
        _card[_card.Length - 2].m_cardColor = Card.cardColor.XIAOWANG;
        _card[_card.Length - 2].m_num = 14;
        _card[_card.Length - 1] = new CardData();
        _card[_card.Length - 1].m_cardColor = Card.cardColor.DAWANG;
        _card[_card.Length - 1].m_num = 15;


        
    }//初始化总卡牌数组
    void Deal()
    {
 
    }
    int SoryCard(CardData a, CardData b)//牌的排序
    {
        if (a.m_num > b.m_num)
        {
            return -1;
        }
        else if (a.m_num == b.m_num)
        {
            if (a.m_cardColor > b.m_cardColor)
            {
                return -1;
            }
            else if (a.m_cardColor < b.m_cardColor)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        else if (a.m_num < b.m_num)
        {
            return 1;
        }
        else
        {
            return 0;
        }
        //return 0;
    }
}
