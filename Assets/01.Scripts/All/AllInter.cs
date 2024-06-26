public interface Initialize
{
    public void Activate(); //Ȱ��ȭ �Ҷ�

    public void Deactivate(); //��Ȱ��ȭ �Ҷ�
}

public interface IProduct
{
    // add common properties and methods here
    public string ProductName { get; set; }

    // customize this for each concrete product
    public void Init();// ó�� �����Ұ�
    
}

public interface IEquipable
{
    void Equip(); // ���� ����
}

public interface IUsable
{
    void Use(); // ��� ����
}




