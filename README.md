🌳 Дерево локаций ProfStaff
Простое и удобное Android-приложение для отображения иерархии локаций компании

📱 О приложении
Приложение отображает древовидную структуру локаций компании с возможностью:

Раскрытия/свертывания узлов

Наглядного отображения иерархии

<img src="https://github.com/user-attachments/assets/42a8b341-513c-4bae-85b8-3d4d8ce20770" width="300" alt="Пример интерфейса">

📥 Установка
Скачайте файл profstaff.apk на свой Android-устройство

Разрешите установку из неизвестных источников (при запросе)

Запустите установку файла

Требования: Android 5.0 (API 21) и выше

🛠 Технологии
Xamarin.Android - кроссплатформенная разработка

System.Text.Json - работа с API

RecyclerView - производительное отображение списков

⚙️ Настройка API
Приложение работает с API сервера p.inventsoft.ru. Для изменения параметров:

Откройте ApiService.cs

Измените константы:
```csharp
private const string BaseUrl = "http://p.inventsoft.ru/Locations/GetLocations";
private const string AuthToken = "ваш_токен";
private const int ClientId = ваш_id;
```
📁 Структура проекта
```text
ProfStaff/
├── Adapters/           # Адаптеры для RecyclerView
├── Models/            # Модели данных
├── Resources/         # Ресурсы приложения
│   ├── layout/        # Разметка экранов
├── Services/          # Сервисы работы с API
└── profstaff.apk      # Готовый билд приложения
```

💡 Совет: Для разработчиков - проект можно открыть в Visual Studio 2022 с установленным Xamarin
