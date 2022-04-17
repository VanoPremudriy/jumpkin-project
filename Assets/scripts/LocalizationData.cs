[System.Serializable]

/*!
	\brief Json-представление в виде класса
*/
public class LocalizationData
{
    //Json-представление локализованного текста
    public LocalizationItem[] items;
}

[System.Serializable]

/*!
	\brief Json-представление локализованного текста
*/
public class LocalizationItem
{
    ///Ключ
    public string key;

    ///Значение
    public string value;
}