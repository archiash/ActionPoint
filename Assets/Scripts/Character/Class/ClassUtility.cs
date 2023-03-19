public static class ClassUtility
{
    public static (string, string) GetClassNameAndDetail(Character.CharacterClass characterClass)
    {
        string className = "";
        string classDetail = "";
        switch (characterClass)
        {
            case Character.CharacterClass.Adventurer:
                className = "Adventurer";
                classDetail = "���մ��¾�ѧ���� 100% �繤���������¡���Ҿ\n" +
                    "���͡�� 20% ������������ҡ��õ������������ 1 ���";
                break;
            case Character.CharacterClass.Magician:
                className = "Mage";
                classDetail = "������ը����ҹ� 5 + 5% �ͧ�ҹ��٧�ش" +
                    "������ջ��Ԩ����ҧ������������Ƿ�� 150% �ͧ��ѧ�Ƿ�� + 15% �ͧ�ҹ��٧�ش\n" +
                    "�����ҡ�ҹ������§�ͨ����մ��¾�ѧ����" +
                    "50% �繤���������¡���Ҿ᷹";
                break;
            case Character.CharacterClass.Rogue:
                className = "Rogue";
                classDetail = "���մ��¾�ѧ���� 75% + �������� 35% �繤���������¡���Ҿ\n" +
                    "�����͡��㹡���ź������� 10%\n" +
                    "������Ѻ������������������ 20%";
                break;
            case Character.CharacterClass.Defender:
                className = "Defender";
                classDetail = "������ը����ҧ����������� 80% �ͧ��ѧ���� + 10% �ͧ��ѧ���Ե�٧�ش" +
                    "�繤���������¡���Ҿ\n" +
                    "��Ш����Ѻ��ѧ��ͧ�ѹ��е�ҹ�ҹ�Ƿ��������� 2% ������ 20 step �Ѻ��͹�٧�ش 5 ����";
                break;

        }
        return (className, classDetail);
    }

    public static string GetClassName(Character.CharacterClass characterClass)
    {
        switch (characterClass)
        {
            case Character.CharacterClass.Adventurer:
                return "Adventurer";
            case Character.CharacterClass.Magician:
                return "Mage";
            case Character.CharacterClass.Rogue:
                return "Rogue";
            case Character.CharacterClass.Defender:
                return "Defender";
        }
        return "";
    }

}
