using TMPro;

public class UIUserInfo : BasicPopUp
{
    // 변수명 뒤에 Max가 붙으면 현재데이터 / 최대 데이터 형식으로 표현
    public TMP_Text userName;
    public TMP_Text hPMax;
    public TMP_Text mPMax;
    public TMP_Text ultimateMax;
    public TMP_Text attack;
    public TMP_Text defense;
    public TMP_Text speed;
    public TMP_Text expMax;
    public TMP_Text luck;
    public TMP_Text critical;

    public override void Open()
    {
        base.Open();
        userName.text = DataManager.Instance.gameData.playerData.playerStat.name.ToString();
        hPMax.text = $"{GameManager.Instance.player.Hp} / {GameManager.Instance.player.MaxHp}";
        mPMax.text =$"{GameManager.Instance.player.Mp} / {GameManager.Instance.player.MaxMp}";
        ultimateMax.text = $"{DataManager.Instance.gameData.playerData.playerStat.ultimateGauge} / {DataManager.Instance.gameData.playerData.playerStat.maxUltimateGauge}"; 
        attack.text = GameManager.Instance.player.Att.ToString();
        defense.text = GameManager.Instance.player.Def.ToString();
        speed.text = GameManager.Instance.player.Speed.ToString();
        expMax.text =$"{DataManager.Instance.gameData.playerData.playerStat.experience} / {DataManager.Instance.gameData.playerData.playerStat.maxExperience}"; 
        luck.text = GameManager.Instance.player.Luck.ToString();
        critical.text = GameManager.Instance.player.Cri.ToString();
    }
}
