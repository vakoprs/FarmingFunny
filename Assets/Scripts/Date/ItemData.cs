using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

//保存所有物品类型，以便区分不同物品
public enum ItemType
{
    
    None,
    Hoe,//锄头
    Fish,
    Fishgrod,//鱼竿
    FishgrodBlue,
    FishgrodBrown,
    FishgrodPink,
    Axe,//斧头
    AxeBlue,//蓝斧头
    CarrotSeed,//萝卜种子
    Carrot,//种子
    Water,//水壶
    Diamand,//钻石
    PickAxeBlue,//蓝镐子
    HoeBlue,
    HoeCopper,
    HoeGold,
    HoePink,
    HoeSilver,
    HoeWood,
    PickAxeCopper,
    PickAxeGold,
    PickAxe,
    PickAxePink,
    PickAxeWood,
    WhiteWhiteFishBig,
    WhiteWhiteFishMiddle,
    WhiteWhiteFishSmall,
    AxeCopper,
    AxeGold,
    AxeWood,
    AxePink,
    AxeSilver,
    CabbageSeed,
    CornSeed,
    EggplantSeed,
    MushroomSeed,
    PotatoSeed,
    PumpkinSeed,
    StrawberrySeed,
    TomatoSeed,
    WatermelonSeed,
    WhiteCarrotSeed,
    Cabbage,
    Corn,
    Eggplant,
    Mushroom,
    Potato,
    Pumpkin,
    Strawberry,
    Tomato,
    Watermelon,
    WhiteCarrot,
    Copper,
    Gold,
    GreenStone,
    RedStone,
    Silver,
    Wood,
    ChickenMedicine, PigMedicine, CowSheepMedicine,
    ChickenFood, PigFood, CowSheepFood,
    Bell,
    ChickenBaby,CowBaby,SheepBaby,PigBaby,
    
    Omurice,SiuMeiRice,Tart,Pizza,Soup,Spaghetti,Bibimbap,VanillaIce,ChocoIce,

    Chickenbabymod,Pigbabymod,Cowbabymod,Sheepbabymod,

    Necklace,Earrings,YellowNecklace,

    Egg,Milk,
}
[CreateAssetMenu]
public class ItemData:ScriptableObject
{
    public ItemType type=ItemType.None;//种类
    public Sprite sprite;//图像
    public GameObject prefab;//预制体
    public int maxCount=1;//最大值
    [FormerlySerializedAs("moneycount")] public int SellMoney=0;//卖出钱数
    public int buyMoney = 0;//买入钱数
    public int GrowthDay;//生长周期
    public int MaturityDay;//成熟周期
    public int chopCount = 0;
    public int favor = 0;//好感度

}
