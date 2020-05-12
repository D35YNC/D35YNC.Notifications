# D35YNC.Notifications<Version. 1.2.3>

# README.MD is Outdated, the update is not coming soon//smile)0

Простая (почти) библиотека pop-up уведомлений.

Simple pop-up notification library. [EN](#DescriptionEN)
_____

## `RU`

[История версий](#Changes_RU)

**Содержание**

![Диаграмма](https://github.com/D35YNC/D35YNC.Notifications/blob/master/D35YNC.Notifications/Diagram.png)

Файл                              | Содержание
----------------------------------|----------------------------
./**NotifyController.cs**         | Контроллер. По факту ядро
./Config/**NotifyConfig.cs**      | Конфиги уведомлений
./Config/**NotifyStyle.cs**       | Стили уведомлений
./Config/**NotifyBehavior.cs**    | Поведение уведомлений
./Enums/**NotifyPosition.cs**     | Положения уведомлений
./Enums/**NotifyAnimaionType.cs** | Анимации уведомлений
./Window/**INotifyWindow.cs**     | Интерфейс окна уведомления
./Window/**NotifyWindow.cs**      | Логика уведомления

**Установка**

  * ~~NuGet: Install-Package D35YNC.Notifications -Version 1.2.2~~
  * Или добавь в свой проект папку "D35YNC.Notifications".

**Использование**

Из диаграммы видно, что NotifyController имеет 2 перегрузки Метода ShowNotify(..)
```C#
public void ShowNotify(Window window) {...}
public void ShowNotify(string header, string text, int timeout) {...}
```
В первом случае, необходимо проинициализировать окно уведомления NotifyWindow. Обязательно наследование от INotifyWindow.
Во втором случае, Контроллер сделает это сам.

Инициализация NotifyWindow происходит так:
```C#
new NotifyWindow(NotifyStyle, NotifyBehavior) { Header.., Text.., Timeout.., AnimationTimeout...};
```

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

_____
## `EN`
<a name="DescriptionEN"/>

[Version history](#Changes_EN)

**Contents**

File                           |Content
-------------------------------|--------------------------------------------------
./**NotifyController.cs**      |Controller. In fact it's the core.
./Config/**NotifyConfig.cs**   |Configs of notifications
./Config/**NotifyStyle.cs**    |Notifications style
./Config/**NotifyBehavior.cs** |Notifications behavior
./Enums/**NotifyPosition.cs**  |Notifications positions
./Enums/**NotifyAnimaion.cs**  |Notifications animations
./Window/**INotifyWindow.cs**  |Interface of popup window
./Window/**NotifyWindow.cs**   |Popup window logic

**Installation**

  * ~~NuGet: Install-Package D35YNC.Notifications -Version 1.2.2~~
  * Or add folder "D35YNC.Notifications" to your project
  
**Usage**

NotifyController has 2 overloads of the Method ShowNotify(..)
```C#
public void ShowNotify(Window window);
public void ShowNotify(int type, string text);
```
In the first case, you need to initialize the Notify Window. Inheritance from INotifWindow is required.
In the second case, the Controller will do it itself.

Initialization of NotifyWindow occurs as follows:
```C#
new NotifyWindow(NotifyStyle, NotifyBehavior) { Header.., Text.., Timeout.., AnimationTimeout...};
```

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
