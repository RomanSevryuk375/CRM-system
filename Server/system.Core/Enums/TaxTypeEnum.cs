namespace CRMSystem.Core.Enums
{
    public enum TaxTypeEnum
    {
        CorporateIncomeTax = 1,         // Налог на прибыль организации
        ValueAddedTax = 2,              // НДС при оказании услуг и продаже запчастей
        PropertyTax = 3,                // Налог на недвижимость и имущество предприятия
        LandTax = 4,                    // Земельный налог (при наличии земельного участка)
        SocialSecurityContributions = 5,// Взносы в фонды за сотрудников (пенсия, страхование)
        EnvironmentalTax = 6,           // Экологический налог / утилизационные сборы (если применимо)
        LocalFees = 7                   // Местные сборы и обязательные платежи (по ситуации)
    }
}

