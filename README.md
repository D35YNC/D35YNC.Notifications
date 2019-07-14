# D35YNC.Notifications<Ver. 1.2>

Простая (почти) библиотека pop-up уведомлений.

Simple pop-up notification library. [EN](#DescriptionEN)
_____

## `RU`

[История версий](#Changes_RU)

**Содержание**

![Диаграмма](https://github.com/D35YNC/D35YNC.Notifications/blob/master/D35YNC.Notifications/Diagram.png)

Файл                          | Содержание
------------------------------|---------------------------------------------------
./**NotifyController.cs**     |Контроллер. По факту ядро.
./Config/**NotifyConfig.cs**  |Конфиги уведомлений
./Config/**NotifyStyle.cs**   |Стили уведомлений
./Config/**NotifyBehavior.cs**|Поведение уведомлений
./Enums/**NotifyPosition.cs** |Положения уведомлений
./Enums/**NotifyAnimaion.cs** |Анимации уведомлений
./Window/**IPopupWindow.cs**  |Интерфейс окна уведомления
./Window/**NotifyWindow.cs**  |Логика уведомления

**Установка**

  * Добавьте в свой проект папку "D35YNC.Notifications".
  * Или
  * NuGet: Install-Package D35YNC.Notifications -Version 1.2.0

**Использование**

Из диаграммы видно, что NotifyController имеет 2 перегрузки Метода ShowNotify(..)
```C#
public void ShowNotify(Window window);
public void ShowNotify(int type, string text);
```
В первом случае, необходимо проинициализировать окно уведомления NotifyWindow.
Во втором случае, Контроллер сделает это сам, если тип (int type) был добавлен в Конфиг методом AddNotifyType(...).

Инициализация NotifyWindow происходит так:
```C#
new NotifyWindow(NotifyStyle, NotifyBehavior) { Header.., Text.., Timeout.., AnimationTimeout...};
```

Перед тем, как показать окно, Контроллер проверяет его. (Обязательно необходимо проинициализировать Timeout и AnimationTimeout)

Стандартный конфиг:     NotifyConfig    >  NotifyController.Config

Стандартный стиль:      NotifyStyle     >  NotifyController.Config.Style

Стандартное поведение:  NotifyBehavior  >  NotifyController.Config.Behavior

Изменение этих свойств возможно напрямую из NotifyController.

Простой пример:
```C#
  using D35YNC.Notifications;
  ...
  enum NotifyType
  {
    Default,
    Error
  }
  ...
  class SomeClass()
  {
    public NotifyController { get; set; }
    
    public SomeClass()
    {
      NotifyController = new NotifyController();

      NotifyController.Config.Style.Foreground = new SolidColorBrush(Colors.Lime);
      NotifyController.Config.Style.Background = new SolidColorBrush(Colors.Black);
      
      NotifyController.Config.Behavior.HideAnimation = NotifyAnimation.Transparent;

      NotifyController.AddNotifyType((int)NotifyType.Default, "Обычный заголовок", 2500);
      NotifyController.AddNotifyType((int)NotifyType.Error, "Заголовок ошибки", 4000);
    }
    ...
    public SomeCallingMethod()
    {
      NotifyController.ShowNotify(new NotifyWindow(NotifyController.Config.Style, NotifyController.Config.Behavior)
      {
        Header           = "Привет!",
        Text             = "Могу создать уведомление вот так",
        Timeout          = 5000,
        AnimationTimeout = 400
      });
      
      NotifyController.ShowNotify((int)NotifyType.Default, "И с использованием пользовательского типа уведомления");
    }
    ...
  }    
```

_____
## `EN`
<a name="DescriptionEN"/>

[Version history](#Changes_EN)

**Contents**

File                          |Content
------------------------------|--------------------------------------------------
./**NotifyController.cs**     |Controller. In fact it's the core.
./Config/**NotifyConfig.cs**  |Configs of notifications
./Config/**NotifyStyle.cs**   |Notifications style
./Config/**NotifyBehavior.cs**|Notifications behavior
./Enums/**NotifyPosition.cs** |Notifications positions
./Enums/**NotifyAnimaion.cs** |Notifications animations
./Window/**IPopupWindow.cs**  |Interface of popup window
./Window/**NotifyWindow.cs**  |Popup window logic

**Installation**

  * Add folder "D35YNC.Notifications" to your project
  * Or
  * NuGet: Install-Package D35YNC.Notifications -Version 1.2.0
**Usage**

NotifyController has 2 overloads of the Method ShowNotify(..)
```C#
public void ShowNotify(Window window);
public void ShowNotify(int type, string text);
```
In the first case, you need to initialize the NotifyWindow notification window.
In the second case, the Controller will do it itself if the type (int type) was added to the Config by the AddNotifyType(...).

Initialization NotifyWindow happens:
```C#
new NotifyWindow(NotifyStyle, NotifyBehavior) { Header.., Text.., Timeout.., AnimationTimeout...};
```

Before showing the window, the Controller checks it. (Be sure to initialize the Timeout and AnimationTimeout)

Standard config: NotifyConfig > NotifyController.Config

Standard style: NotifyStyle > NotifyController.Config.Style

Standard behavior: NotifyBehavior > NotifyController.Config.Behavior

Change these properties directly in NotifyController.

Simple example:
```C#
  using D35YNC.Notifications;
  ...
  enum NotifyType
  {
    Default,
    Error
  }
  ...
  class SomeClass()
  {
    public NotifyController { get; set; }
    
    public SomeClass()
    {
      NotifyController = new NotifyController();

      NotifyController.Config.Style.Foreground = new SolidColorBrush(Colors.Lime);
      NotifyController.Config.Style.Background = new SolidColorBrush(Colors.Black);

      NotifyController.AddNotifyType((int)NotifyType.Default, "Default header", 2500);
      NotifyController.AddNotifyType((int)NotifyType.Error, "Error header", 4000);
    }
    ...
    public SomeCallingMethod()
    {
      NotifyController.ShowNotify(new NotifyWindow(NotifyController.Config.Style, NotifyController.Config.Behavior)
      {
        Header           = "Hi!",
        Text             = "I can create a notification like this",
        Timeout          = 5000,
        AnimationTimeout = 400
      });
      
      NotifyController.ShowNotify((int)NotifyType.Default, "And using a custom notification type");
    }
    ...
  }    
```

_____
## **Изменения | Changes**

### `RU`
<a name="Changes_RU"/>

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
