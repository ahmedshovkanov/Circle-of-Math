

## Настроить lib/core/app_config.dart


```dart
//Dev Key appsflyer 
String appsFlyerDevKey = 'etcnoiHPcQsZLcXsuVJmKJ';

//id приложения в app store
String appsFlyerAppId = '6754553353'; // Для iOS'

//bandle id приложения
String bundleId = 'com.toddhale.rabcoins'; // Для iOS'

//локаль приложения
String locale = 'en'; // Для iOS'

//ОС приложения
String os = 'iOS'; // Для iOS'

//домен приложения !без пробелов и '/' 
String endpoint = 'https://henppower.com'; // Для iOS'

// ID проекта в Firebase, можно вытащить из lib/firebase_options.dart !но только после настройки Firebase
String firebaseProjectId = 'henpower-49da1'; // Для iOS'
```


### Настройка визуала 

##### Градиент 
```dart
Gradient splashGradient = LinearGradient(

	 //настройка начала и конца градиента
	begin: Alignment.topCenter,
	end: Alignment.bottomCenter,
	
	colors: [
		//можно как и hex в формате ARGB (первые 2 символа отвечают за прозрачность, оставить FF)
		Color(0xFF9320AC), 
		
		//можно и байтами указать 0-255, первое значение тоже отвечает за прозрачность
		Color.fromARGB(255, 53, 0, 51), 
	],
);



```

##### Изображение
```dart
Decoration test = BoxDecoration(
	
	image: DecorationImage(
	
		// Указываем путь к изображению
		// Для изображений из сети: NetworkImage('URL')
		// Для локальных изображений: AssetImage('assets/image.jpg')
		image: AssetImage('assets/background.jpg'),
		
		// Режим заполнения - растягивает изображение на весь контейнер
		fit: BoxFit.cover,
		// Альтернативные варианты fit:
		// BoxFit.fill - растягивает с искажением пропорций
		// BoxFit.contain - сохраняет пропорции, может быть с полями
		// BoxFit.fitWidth - по ширине контейнера
		// BoxFit.fitHeight - по высоте контейнера
		// BoxFit.scaleDown - уменьшает если нужно, но не увеличивает
		
		// Выравнивание изображения (если есть свободное пространство)
		alignment: Alignment.center,
		// Повторение изображения (если не заполняет полностью)
		// repeat: ImageRepeat.repeat, // повторять по обоим осям
		// repeat: ImageRepeat.repeatX, // повторять по горизонтали
		// repeat: ImageRepeat.repeatY, // повторять по вертикали
		
		// Цветовая фильтрация (можно наложить цвет поверх изображения)
		// colorFilter: ColorFilter.mode(
			// Colors.blue.withOpacity(0.3),
			// BlendMode.color,
		// ),
	),
	
	// Дополнительные декорации (можно комбинировать с изображением)
	borderRadius: BorderRadius.circular(12), // Скругление углов
	
	border: Border.all( // Рамка
	
		color: Colors.white,
		
		width: 2,
	
	),
	
	boxShadow: [ // Тень
	
		BoxShadow(
		
			color: Colors.black.withOpacity(0.3),
			
			blurRadius: 8,
			
			offset: Offset(0, 4),
		
		),
	
	],
	  
	
	// Градиент поверх изображения (если нужно)
	// gradient: LinearGradient(
	
		// begin: Alignment.topCenter,
		
		// end: Alignment.bottomCenter,
		
		// colors: [
		
			// Colors.transparent,
			
			// Colors.black.withOpacity(0.5),
			// ],
	// ),

),
```

#### 1. Экран загрузки
Установить градиент или изображение для загрузки
```dart
Decoration splashDecoration = const BoxDecoration(

	gradient: AppConfig.splashGradient,

);
```
настроить цвета
```dart
//цвет текста загрузки
Color loadingTextColor = Color(0xFFFFFFFF); 

//цвет спинера загрузки
Color spinerColor = Color(0xFCFFFFFF);
```


#### 2. Экран запроса на пуши
##### Установить градиент или изображение для экрана запроса пушей
```dart
Decoration pushRequestDecoration = const BoxDecoration(
	gradient: AppConfig.pushRequestFadeGradient,
);

```

##### настройка цветов
```dart
Color titleTextColor = Color(0xFFFFFFFF); //цвет верхней строчки 

Color subtitleTextColor = Color(0x80FDFDFD); //цвет нижней строчки 

Color yesButtonColor = Color(0xFFFFB301); //цвет кнопки согласия 

Color yesButtonShadowColor = Color(0xFF8B3619); //цвет тени кнопки согласия 

Color yesButtonTextColor = Color(0xFFFFFFFF); //цвет текста кнопки согласия 

Color skipTextColor = Color(0x7DF9F9F9);  //цвет текста кнопки скип 
```

##### настройка логотипа
```dart 
// Путь к логотипу, если не находит добавить в pubspec.yaml

String logoPath = 'assets/images/Logo.png';
```

#### 3. Экран ошибки подключения к интернету

##### Установить градиент или изображение для экрана 
```dart
Decoration errorScreenDecoration = const BoxDecoration(
	gradient: AppConfig.errorScreenGradient,
);
```

##### настройка цветов
```dart
Color errorScreenTextColor = Color(0xFFFFFFFF); //цвет текста
Color errorScreenIconColor = Color(0xFCFFFFFF); //цвет иконки
```


