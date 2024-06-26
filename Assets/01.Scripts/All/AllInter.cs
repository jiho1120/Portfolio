public interface Initialize
{
    public void Activate(); //활성화 할때

    public void Deactivate(); //비활성화 할때
}

public interface IProduct
{
    // add common properties and methods here
    public string ProductName { get; set; }

    // customize this for each concrete product
    public void Init();// 처음 설정할것
    
}

public interface IEquipable
{
    void Equip(); // 장착 로직
}

public interface IUsable
{
    void Use(); // 사용 로직
}




