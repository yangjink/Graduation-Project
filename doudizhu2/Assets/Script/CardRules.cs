using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardRules 
{
    public  enum CardRelesType
    {
        Invalid = -1,    //无效

        Single,//单
        Double,//对子
        Straight,//顺子
        DoubleStraight,//双顺子
        TripleStraight,//飞机
        OnlyThree,//三个不带
        ThreeAndOne,//三带一
        ThreeAndTwo,//三带二
        Boom,//炸弹
        JokerBoom,//王炸
    }
    //static CardRules instance = null;


    //public static CardRules Instance
    //{
    //    get 
    //    {
    //        if (null == instance)
    //        {
    //            instance = new CardRules();
    //        }
    //        return instance;
    //    }
    //}

    //public void ClearCardRulesData()
    //{
    //    instance = null;
    //}


    /// <summary>
    /// 是否是单
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public static bool IsSingle(List<CardData> cards)
    {
        if (cards.Count == 1)
            return true;
        else
            return false;
    }

    /// <summary>
    /// 是否是对子
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public static bool IsDouble(List<CardData> cards)
    {
        if (cards.Count == 2)
        {
            if (cards[0].m_num == cards[1].m_num)
                return true;
        }

        return false;
    }

    /// <summary>
    /// 是否是顺子
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public static bool IsStraight(List<CardData> cards)
    {
        if (cards.Count < 5 || cards.Count > 12)
            return false;
        for (int i = 0; i < cards.Count - 1; i++)
        {
            int w = cards[i].m_num;
            if (cards[i + 1].m_num - w != 1)
                return false;

            //不能超过A
            if (w > (int)Card.cardNum.ONE || cards[i + 1].m_num > (int)Card.cardNum.ONE)
                return false;
        }

        return true;
    }

    /// <summary>
    /// 是否是双顺子
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public static bool IsDoubleStraight(List<CardData> cards)
    {
        if (cards.Count < 6 || cards.Count % 2 != 0)
            return false;

        for (int i = 0; i < cards.Count; i += 2)
        {
            if (cards[i + 1].m_num != cards[i].m_num)
                return false;

            if (i < cards.Count - 2)
            {
                if (cards[i + 2].m_num - cards[i].m_num != 1)
                    return false;

                //不能超过A
                if (cards[i].m_num > (int)Card.cardNum.ONE || cards[i + 2].m_num > (int)Card.cardNum.ONE)
                    return false;
            }
        }

        return true;
    }

    /// <summary>
    /// 飞机不带
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public static bool IsTripleStraight(List<CardData> cards)
    {
        if (cards.Count < 6 || cards.Count % 3 != 0)
            return false;

        for (int i = 0; i < cards.Count; i += 3)
        {
            if (cards[i + 1].m_num != cards[i].m_num)
                return false;
            if (cards[i + 2].m_num != cards[i].m_num)
                return false;
            if (cards[i + 1].m_num != cards[i + 2].m_num)
                return false;

            if (i < cards.Count - 3)
            {
                if (cards[i + 3].m_num - cards[i].m_num != 1)
                    return false;

                //不能超过A
                if (cards[i].m_num > (int)Card.cardNum.ONE || cards[i + 3].m_num > (int)Card.cardNum.ONE)
                    return false;
            }
        }

        return true;
    }

    /// <summary>
    /// 三不带
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public static bool IsOnlyThree(List<CardData> cards)
    {
        if (cards.Count % 3 != 0)
            return false;
        if (cards[0].m_num != cards[1].m_num)
            return false;
        if (cards[1].m_num != cards[2].m_num)
            return false;
        if (cards[0].m_num != cards[2].m_num)
            return false;

        return true;
    }


    /// <summary>
    /// 三带一
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public static bool IsThreeAndOne(List<CardData> cards)
    {
        if (cards.Count != 4)
            return false;

        if (cards[0].m_num == cards[1].m_num &&
            cards[1].m_num == cards[2].m_num)
            return true;
        else if (cards[1].m_num == cards[2].m_num &&
            cards[2].m_num == cards[3].m_num)
            return true;
        return false;
    }

    /// <summary>
    /// 三代二
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public static bool IsThreeAndTwo(List<CardData> cards)
    {
        if (cards.Count != 5)
            return false;

        if (cards[0].m_num == cards[1].m_num &&
            cards[1].m_num == cards[2].m_num)
        {
            if (cards[3].m_num == cards[4].m_num)
                return true;
        }

        else if (cards[2].m_num == cards[3].m_num &&
            cards[3].m_num == cards[4].m_num)
        {
            if (cards[0].m_num == cards[1].m_num)
                return true;
        }

        return false;
    }

    /// <summary>
    /// 炸弹
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public static bool IsBoom(List<CardData> cards)
    {
        if (cards.Count != 4)
            return false;

        if (cards[0].m_num != cards[1].m_num)
            return false;
        if (cards[1].m_num != cards[2].m_num)
            return false;
        if (cards[2].m_num != cards[3].m_num)
            return false;

        return true;
    }


    /// <summary>
    /// 王炸
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public static bool IsJokerBoom(List<CardData> cards)
    {
        if (cards.Count != 2)
            return false;
        if (cards[0].m_num == (int)Card.cardNum.XiaoWang)
        {
            if (cards[1].m_num == (int)Card.cardNum.DaWang)
                return true;
            return false;
        }
        else if (cards[0].m_num == (int)Card.cardNum.DaWang)
        {
            if (cards[1].m_num == (int)Card.cardNum.XiaoWang)
                return true;
            return false;
        }

        return false;
    }

    /// <summary>
    /// 判断是否符合出牌规则
    /// </summary>
    /// <param name="cards"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public static bool PopEnable(List<CardData> cards, out CardRelesType type)
    {
        type = CardRelesType.Invalid;
        bool isRule = false;
        switch (cards.Count)
        {
            case 1:
                isRule = true;
                type = CardRelesType.Single;
                break;
            case 2:
                if (IsDouble(cards))
                {
                    isRule = true;
                    type = CardRelesType.Double;
                }
                else if (IsJokerBoom(cards))
                {
                    isRule = true;
                    type = CardRelesType.JokerBoom;
                }
                break;
            case 3:
                if (IsOnlyThree(cards))
                {
                    isRule = true;
                    type = CardRelesType.OnlyThree;
                }
                break;
            case 4:
                if (IsBoom(cards))
                {
                    isRule = true;
                    type = CardRelesType.Boom;
                }
                else if (IsThreeAndOne(cards))
                {
                    isRule = true;
                    type = CardRelesType.ThreeAndOne;
                }

                break;
            case 5:
                if (IsStraight(cards))
                {
                    isRule = true;
                    type = CardRelesType.Straight;
                }
                else if (IsThreeAndTwo(cards))
                {
                    isRule = true;
                    type = CardRelesType.ThreeAndTwo;
                }
                break;
            case 6:
                if (IsStraight(cards))
                {
                    isRule = true;
                    type = CardRelesType.Straight;
                }
                else if (IsTripleStraight(cards))
                {
                    isRule = true;
                    type = CardRelesType.TripleStraight;
                }
                else if (IsDoubleStraight(cards))
                {
                    isRule = true;
                    type = CardRelesType.DoubleStraight;
                }
                break;
            case 7:
                if (IsStraight(cards))
                {
                    isRule = true;
                    type = CardRelesType.Straight;
                }
                break;
            case 8:
                if (IsStraight(cards))
                {
                    isRule = true;
                    type = CardRelesType.Straight;
                }
                else if (IsDoubleStraight(cards))
                {
                    isRule = true;
                    type = CardRelesType.DoubleStraight;
                }
                //else if (IsTripleStraightAndSingle(cards))
                //{
                //    isRule = true;
                //    type = CardsType.TripleStraightAndSingle;
                //}
                break;
            case 9:
                if (IsStraight(cards))
                {
                    isRule = true;
                    type = CardRelesType.Straight;
                }
                else if (IsOnlyThree(cards))
                {
                    isRule = true;
                    type = CardRelesType.OnlyThree;
                }
                break;
            case 10:
                if (IsStraight(cards))
                {
                    isRule = true;
                    type = CardRelesType.Straight;
                }
                else if (IsDoubleStraight(cards))
                {
                    isRule = true;
                    type = CardRelesType.DoubleStraight;
                }
                //else if (IsTripleStraightAndDouble(cards))
                //{
                //    isRule = true;
                //    type = CardsType.TripleStraightAndDouble;
                //}
                break;

            case 11:
                if (IsStraight(cards))
                {
                    isRule = true;
                    type = CardRelesType.Straight;
                }
                break;
            case 12:
                if (IsStraight(cards))
                {
                    isRule = true;
                    type = CardRelesType.Straight;
                }
                else if (IsDoubleStraight(cards))
                {
                    isRule = true;
                    type = CardRelesType.DoubleStraight;
                }
                //else if (IsTripleStraightAndSingle(cards))
                //{
                //    isRule = true;
                //    type = CardsType.TripleStraightAndSingle;
                //}
                else if (IsOnlyThree(cards))
                {
                    isRule = true;
                    type = CardRelesType.OnlyThree;
                }
                break;
            case 13:
                break;
            case 14:
                if (IsDoubleStraight(cards))
                {
                    isRule = true;
                    type = CardRelesType.DoubleStraight;
                }
                break;
            case 15:
                if (IsOnlyThree(cards))
                {
                    isRule = true;
                    type = CardRelesType.OnlyThree;
                }
                //else if (IsTripleStraightAndDouble(cards))
                //{
                //    isRule = true;
                //    type = CardsType.TripleStraightAndDouble;
                //}
                break;
            case 16:
                if (IsDoubleStraight(cards))
                {
                    isRule = true;
                    type = CardRelesType.DoubleStraight;
                }
                //else if (IsTripleStraightAndSingle(cards))
                //{
                //    isRule = true;
                //    type = CardsType.TripleStraightAndSingle;
                //}
                break;
            case 17:
                break;
            case 18:
                if (IsDoubleStraight(cards))
                {
                    isRule = true;
                    type = CardRelesType.DoubleStraight;
                }
                else if (IsOnlyThree(cards))
                {
                    isRule = true;
                    type = CardRelesType.OnlyThree;
                }
                break;
            case 19:
                break;

            case 20:
                if (IsDoubleStraight(cards))
                {
                    isRule = true;
                    type = CardRelesType.DoubleStraight;
                }
                break;
            default:
                break;
        }

        return isRule;
    }
}
