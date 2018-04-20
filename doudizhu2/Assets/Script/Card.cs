using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour {

    public GameObject m_color;
    public GameObject m_num;
    public enum cardColor
    {
        HONGTAO,
        HEITAO,
        FANGKUI,
        MEIHUA,
        XIAOWANG,
        DAWANG
    }
    public enum cardNum
    {
        THREE = 1,
        FOUR = 2,
        FIVE = 3,
        SIX = 4,
        SEVEN = 5,
        EIGHT = 6,
        NINE = 7,
        TEN = 8,
        J = 9,
        Q = 10,
        K = 11,
        ONE = 12,
        TWO = 13,
        XiaoWang = 14,
        DaWang = 15,
    }

    /*
    private Transform _numPosition;
    private UISprite _numSprite;
    private Transform _colorPosition;
    public UISprite _colorSprite;
    
    //正常牌
    private float _colorPositionX1 = -0.7073233f;
    private float _colorPositionY1 = -0.6239167f;
    private int _colorWidth1 = 28;
    private int _colorHeight1 = 30;
    private float _numPositionX1 = -0.7073233f;
    private float _numPositionY1 = -0.5331366f;
    private int _numWidth1 = 38;
    private int _numHeight1 = 38;
    //大小王
    private float _colorPositionX2 = -0.5632f;
    private float _colorPositionY2 = -0.6746666f;
    private int _colorWidth2 = 86;
    private int _colorHeight2 = 152;
    private float _numPositionX2 = -0.8352f;
    private float _numPositionY2 = -0.6693333f;
    private int _numWidth2 = 24;
    private int _numHeight2 = 140;*/
    void Awake()
    {
    }
    void OnEnable()
    {
 
    }
    void Start()
    {

    }
    public bool setCard(cardColor color,int num)
    {
        if (num <= 0 || num > 15)
        {
            return false;
        }
       /* _numPosition = m_num.GetComponent<Transform>();
        _colorPosition = m_color.GetComponent<Transform>();
        _numSprite = m_num.GetComponent<UISprite>();
        _colorSprite = m_color.GetComponent<UISprite>();
        if (num < 13)
        {

           _numPosition.position = new Vector2(_numPositionX1, _numPositionY1);
           _colorPosition.position = new Vector2(_colorPositionX1,_colorPositionY1);
           _numSprite.width = _numWidth1;
           _numSprite.height = _numHeight1;
        }
        else
        {
            _numPosition.position = new Vector2(_numPosition.position.x, _numPositionY2);
            _colorPosition.position = new Vector2(_colorPosition.position.x, _colorPositionY2);
            _numSprite.width = _numWidth2;
            _numSprite.height = _numHeight2;
        }
        * */
        string tmpC = null;
        bool isRed = true;
        switch(color)
        {
            case cardColor.HONGTAO:
                tmpC = "hongtao";
                break;
            case cardColor.HEITAO:
                isRed = false;
                tmpC = "heitao";
                break;
            case cardColor.FANGKUI:
                tmpC = "fangkuai";
                break;
            case cardColor.MEIHUA:
                isRed = false;
                tmpC = "meihua";
                break;
            case cardColor.XIAOWANG:
                tmpC = "xiaowang";
                break;
            case cardColor.DAWANG:
                tmpC = "dawang";
                break;
            default:
                return false;
        }
        m_color.GetComponent<UISprite>().spriteName = tmpC;
        _color = color;
        string tmpN = null;
        /*switch(num)
        {
            case cardNum.ONE:
                tmpN = "1";
                break;
            case cardNum.TWO:
                tmpN = "2";
                break;
            case cardNum.THREE:
                tmpN = "3";
                break;
            case cardNum.FOUR:
                tmpN = "4";
                break;
            case cardNum.FIVE:
                tmpN = "5";
                break;
            case cardNum.SIX:
                tmpN = "6";
                break;
            case cardNum.SEVEN:
                tmpN = "7";
                break;
            case cardNum.EIGHT:
                tmpN = "8";
                break;
            case cardNum.NINE:
                tmpN = "9";
                break;
            case cardNum.TEN:
                tmpN = "10";
                break;
            case cardNum.ELEVEN:
                tmpN = "11";
                break;
            case cardNum.TWELVE:
                tmpN = "12";
                break;
            case cardNum.THIRTEEN:
                tmpN = "13";
                break;
            case cardNum.FOURTEEN:
                tmpN = "14";
                break;
            case cardNum.FIFTEEN:
                tmpN = "15";
                break;
            default:
                return false;
        }*/
        if (!isRed)
        {
            tmpN += 'h';
        }
        tmpN += num.ToString();

        m_num.GetComponent<UISprite>().spriteName = tmpN;
        _num = num;
        return true;
    }

    private cardColor _color = 0;
    private int _num = 0;
    //CardData _card;
    public int GetCardNum()
    {
        return _num;
    }
    public cardColor GetCardColor()
    {
        return _color;
    }

    private bool _isSelect = false;//true 选中 false 没选中

    public void SetIsSelect(bool isSele)
    {
        _isSelect = isSele;
    }
    public bool GetIsSelect()
    {
        return _isSelect;
    }

	

	void Update () {
		
	}
}
