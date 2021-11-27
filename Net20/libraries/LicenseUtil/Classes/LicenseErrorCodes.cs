using System;
using System.Collections.Generic;
using System.Text;

namespace LicenseUtil.Classes
{
    public enum LicenseErrorCodes
    {
        Unknown = 0x01, // Неопознанная ошибка
        IssuerCertificateNotFound = 0x02, // Сертификат издателя не найден (физически)
        SignatureOrCertificateCorrupted = 0x03, // Подпись лицензии нарушена или подменен сертификат издателя
        LicenseFileNotFound = 0x04, // Файл с лицензией не найден
        DetectNoSuitableLicense = 0x05, // Не найдена подходящая лицензия (общая ошибка)
        InvalidVersion = 0x06, // Неверная версия продукта (частная ошибка)
        InvalidApplication = 0x07, // Неверное приложение (частная ошибка)
        InvalidMachine = 0x08,  // Неверный отпечаток машины (частная ошибка)
        ApplicationDisabled = 0x09, // Доступ к приложению ограничен (частная ошибка)
        LicenseExpired = 0x0A, // Лицензия истекла (частная ошибка)
        ProductNotAllowed = 0x0B, // Продукт не разрешен лицензией (частная ошибка)
        LicenseFileCorrupted = 0x0C, // Лицензионный файл поврежден (частная ошибка)
        InvalidEdition = 0x0D, // Неверная редакция продукта (частная ошибка)
        Expired = 0x0E // Не найдена ни одна действующая лицензия
    };
}
