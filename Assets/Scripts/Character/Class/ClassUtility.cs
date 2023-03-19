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
                classDetail = "โจมตีด้วยพลังโจมตี 100% เป็นความเสียหายกายภาพ\n" +
                    "มีโอกาส 20% ที่จะได้ไอเท็มจากการต่อสู้เพิ่มขึ้น 1 ชิ้น";
                break;
            case Character.CharacterClass.Magician:
                className = "Mage";
                classDetail = "การโจมตีจะใช้มานา 5 + 5% ของมานาสูงสุด" +
                    "การโจมตีปกติจะสร้างความเสียหายเวทย์ 150% ของพลังเวทย์ + 15% ของมานาสูงสุด\n" +
                    "แต่ถ้าหากมานาไม่เพียงพอจะโจมตีด้วยพลังโจมตี" +
                    "50% เป็นความเสียหายกายภาพแทน";
                break;
            case Character.CharacterClass.Rogue:
                className = "Rogue";
                classDetail = "โจมตีด้วยพลังโจมตี 75% + ความเร็ว 35% เป็นความเสียหายกายภาพ\n" +
                    "เพิ่มโอกาสในการหลบการโจมตี 10%\n" +
                    "แต่จะได้รับความเสียหายเพิ่มขึ้น 20%";
                break;
            case Character.CharacterClass.Defender:
                className = "Defender";
                classDetail = "การโจมตีจะสร้างความเสียหาย 80% ของพลังโจมตี + 10% ของพลังชีวิตสูงสุด" +
                    "เป็นความเสียหายกายภาพ\n" +
                    "และจะได้รับพลังป้องกันและต้านทานเวทย์เพิ่มขึ้น 2% เป็นเวลา 20 step ทับซ้อนสูงสุด 5 ครั้ง";
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
