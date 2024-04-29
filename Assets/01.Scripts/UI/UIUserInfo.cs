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
        userName.text = DataManager.Instance.gameData.playerData.name.ToString();
        hPMax.text = $"{GameManager.Instance.player.Stat.hp} / {GameManager.Instance.player.Stat.maxHp}";
        mPMax.text =$"{GameManager.Instance.player.Stat.mp} / {GameManager.Instance.player.Stat.maxMp}";
        ultimateMax.text = $"{GameManager.Instance.player.Stat.ultimateGauge} / {GameManager.Instance.player.Stat.maxUltimateGauge}"; 
        attack.text = GameManager.Instance.player.Stat.attack.ToString();
        defense.text = GameManager.Instance.player.Stat.defense.ToString();
        speed.text = GameManager.Instance.player.Stat.speed.ToString();
        expMax.text =$"{GameManager.Instance.player.Stat.experience} / {GameManager.Instance.player.Stat.maxExperience}"; 
        luck.text = GameManager.Instance.player.Stat.luck.ToString();
        critical.text = GameManager.Instance.player.Stat.critical.ToString();
    }
}
