# D35YNC.Notifications<Version. 1.2.3>

Простая (почти) библиотека pop-up уведомлений.

Simple pop-up notification library. [EN](#DescriptionEN)
_____

## `RU`

[История версий](#Changes_RU)

**Содержание**

![Диаграмма](https://github.com/D35YNC/D35YNC.Notifications/blob/master/D35YNC.Notifications/Diagram.png)

Файл                             | Содержание
---------------------------------|----------------------------
./**Notification.cs**            | Абстрактный класс уведомления. Нужен для создания пользовательских окон.  
./**NotificationsController.cs** | Контроллер. Создает, показывает и закрывает уведомления.  
./**SimpleNotification.cs**      | Обычное уведомление. Заголовок, текст.  
./Enums/**AnimationType.cs**     | Типы анимаций.  
./Enums/**Position.cs**          | Позиции окон на экране.  
./Settings/**Behavior.cs**       | Поведение уведомлений на экране.  
./Settings/**WindowConfig.cs**   | Конфигурация для SimpleNotification.  

**Установка**

  * NuGet: Install-Package D35YNC.Notifications -Version 1.2.3
  * Или добавь в свой проект папку "[D35YNC.Notifications](https://github.com/D35YNC/D35YNC.Notifications/releases/download/1.2.3/D35YNC.Notifications.zip)".

Простой пример:
```C#
  using D35YNC.Notifications;
  using D35YNC.Notifications.Enums;
  using D35YNC.Notifications.NWindow;
  ...
  NotifyController NotifyController;
  ...
  void SomeConstructor()
  {
    NotifyController = new NotifyController();

    NotifyController.Config.ReserveList = true;

    NotifyController.Style.Foreground = new SolidColorBrush(Colors.Lime);
    NotifyController.Style.Background = new SolidColorBrush(Colors.Black);

    NotifyController.Behavior.ShowAnimation = NotifyAnimationType.Slide;
  }
  ...
  void SomeCallMethod()
  {
    NotifyController.ShowNotify("First called notify", "Has Timeout = 2500 and default AnimationTimeout", 2500);
	
	NotifyController.ShowNotify(new NotifyWindow(NotifyController.Style, NotifyController.Behavior)
	{
	  Header = "Second called notify",
      Text = "Has Timeout = 5000 and AnimationTimeout = 400",
      Timeout = 5000,
      AnimationTimeout = 400
	});
  }
  void SomeClosingMethod()
  {
    NotifyController.CloseAll();
  }
```

Подробнее обо всех функциях в ![./docs/func_ru.md](https://github.com/D35YNC/D35YNC.Notifications/blob/master/docs/func_ru.md)

_____
## `EN`
<a name="DescriptionEN"/>

[Version history](#Changes_EN)

**Contents**

File                             | Content
---------------------------------|----------------------------
./**Notification.cs**            | Abstract notification class. Needed for creating custom Windows.  
./**NotificationsController.cs** | Controller. Creates, displays, and closes notifications.  
./**SimpleNotification.cs**      | A normal notification. Title, text.  
./Enums/**AnimationType.cs**     | Animations types.  
./Enums/**Position.cs**          | Window positions on the screen.  
./Settings/**Behavior.cs**       | Behavior of notifications on the screen.  
./Settings/**WindowConfig.cs**   | Configuration for SimpleNotification.  

**Installation**

  * NuGet: Install-Package D35YNC.Notifications -Version 1.2.3
  * Or add folder "[D35YNC.Notifications](https://github.com/D35YNC/D35YNC.Notifications/releases/download/1.2.3/D35YNC.Notifications.zip)" to your project.


Simple example:
```C#
  using D35YNC.Notifications;
  using D35YNC.Notifications.Enums;
  using D35YNC.Notifications.NWindow;
  ...
  NotifyController NotifyController;
  ...
  void SomeConstructor()
  {
    NotifyController = new NotifyController();

    NotifyController.Config.ReserveList = true;

    NotifyController.Style.Foreground = new SolidColorBrush(Colors.Lime);
    NotifyController.Style.Background = new SolidColorBrush(Colors.Black);

    NotifyController.Behavior.ShowAnimation = NotifyAnimationType.Slide;
  }
  ...
  void SomeCallMethod()
  {
    NotifyController.ShowNotify("First called notify", "Has Timeout = 2500 and default AnimationTimeout", 2500);
	
	NotifyController.ShowNotify(new NotifyWindow(NotifyController.Style, NotifyController.Behavior)
	{
	  Header = "Second called notify",
      Text = "Has Timeout = 5000 and AnimationTimeout = 400",
      Timeout = 5000,
      AnimationTimeout = 400
	});
  }
  void SomeClosingMethod()
  {
    NotifyController.CloseAll();
  }
```

Learn more about all functions in ![./docs/func_en.md](https://github.com/D35YNC/D35YNC.Notifications/blob/master/docs/func_en.md)

_____
## **Изменения | Changes**

### `RU`
<a name="Changes_RU"/>

Version 1.2.3:
  * Много улучшений кода.
  * Багфиксы.

Version 1.2.2:
  * Полный баг-фикс в Config.Style.AutoHeight.
  * Оптимизации кода.

Version 1.2.1:
  * Частичный баг-фикс в Config.Style.AutoHeight. Раньше окна вели себя странно при свойстве = true.

Version 1.2:
  * Добавлено:
    * Расширенные конфигурации уведомлений.
    * 2 типа анимаций: Слайд, Прозрачность.
    * Возможность установить различный тип анимации для появления и скрытия уведомления.
  * Изменено:
    * Частичные правки логики.
  * Улучшения и оптимизации кода

Version 1.1.ap:
  * База для расширенной конфигурации.
  * Улучшения кода.

Version 1.1:
  * Добавлены "Кастомные" уведомления
  * Изменение иерархии кода
  
Version 1.0:
  * Базовый функционал.

### `EN`
<a name="Changes_EN"/>

Version 1.2.3:
  * Some bug fixes.
  * BiG code improvements.

Version 1.2.2:
  * Completely fixed bug in Config.Style.AutoHeight.
  * Code optimization.
  
Version 1.2.1:
  * Partially bug fixed in Config.Style.AutoHeight. Previously, Windows behaved strangely with the property = true.

Version 1.2:
  * Added:
    * Advanced configuration of notifications.
    * 2 types of animations: Slide, Transparency.
    * Ability to set different type of animation to appear and hide the notification.
  * Changed:
    * Partial logic edits.
  * Code improvements and optimizations

Version 1.1.ap:
  * Base for advanced configuration.
  * Code improvements.

Version 1.1:
  * Added "Custom" notifications
  * The change in hierarchy code
  
Version 1.0:
  * Basic functionality.
